using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

using Serilog;

namespace Exam.Infrastructure.Repository;

public abstract class BaseRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : Entity, IAggregateRoot
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IMapper _mapper;

    protected BaseRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Guid id, string[]? includeProperties = null)
    {
        Log.Information("Был сделан запрос на получение с id: {}");
        return await _dbSet.ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string[]? includeProperties = null)
    {
        IQueryable<TEntity> query = _dbSet;
        try {
            if (filter != null) {
                query = query.Where(filter);
            }

            if (includeProperties is { Length: > 0 }) {
                query = query.ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties);
            }
            return await query.FirstOrDefaultAsync();
        } catch (Exception) {

            Log.Error("Ошибка в методе FirstOrDefault");
            return null;
        }
        

    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? includeProperties = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null) {
            query = query.Where(filter);
        }

        if (includeProperties is { Length: > 0 }) {
            query = query.ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties);
        }

        if (orderBy != null) {
            Log.Information("Выполнение запроса с сортировкой"); 
            try {
                return orderBy(query).ToList();
            } catch (Exception ex) {
                Log.Error(ex, "Ошибка при выполнении запроса с сортировкой"); 
                throw;
            }
        } else {
            Log.Information("Выполнение запроса без сортировки"); 
            try {
                return await query.ToListAsync();
            } catch (Exception ex) {
                Log.Error(ex, "Ошибка при выполнении запроса без сортировки"); 
                throw;
            }
        }
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;
        return await query.AnyAsync(filter);
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            return await query.CountAsync(filter);
        }

        return await query.CountAsync();
    }

    public async Task DeleteRange(Guid[] ids) {
        try {
            var entityToDelete = await GetAsync(x => ids.Contains(x.Id));
            DeleteRange(entityToDelete.ToList());
            Log.Information("Успешно удален диапазон объектов типа {EntityType}.", typeof(TEntity).Name);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при выполнении метода DeleteRange в репозитории для {EntityType}.", typeof(TEntity).Name);
            throw;
        }
    }

    public async Task AddAsync(TEntity entity) {
        try {
            await _dbSet.AddAsync(entity);
            Log.Information("Успешно добавлен объект типа {EntityType}.", typeof(TEntity).Name);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при выполнении метода AddAsync в репозитории для {EntityType}.", typeof(TEntity).Name);
            throw;
        }
    }

    public async Task AddRangeAsync(TEntity[] entity) {
        try {
            await _dbSet.AddRangeAsync(entity);
            Log.Information("Успешно добавлен диапазон объектов типа {EntityType}.", typeof(TEntity).Name);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при выполнении метода AddRangeAsync в репозитории для {EntityType}.", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual async Task Delete(Guid id) {
        try {
            var entityToDelete = await GetAsync(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
            Log.Information("Успешно удален объект типа {EntityType} с ID {EntityId}.", typeof(TEntity).Name, id);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при выполнении метода Delete в репозитории для {EntityType}.", typeof(TEntity).Name);
            throw;
        }
    }

    protected virtual void Delete(TEntity entityToDelete) {
        try {
            if (_context.Entry(entityToDelete).State == EntityState.Detached) {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            Log.Information("Успешно удален объект типа {EntityType} с ID {EntityId}.", typeof(TEntity).Name, entityToDelete.Id);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при удалении объекта типа {EntityType} с ID {EntityId}.", typeof(TEntity).Name, entityToDelete.Id);
            throw;
        }
    }

    protected virtual void DeleteRange(List<TEntity> entityToDelete) {
        try {
            if (_context.Entry(entityToDelete).State == EntityState.Detached) {
                _dbSet.AttachRange(entityToDelete);
            }
            _dbSet.RemoveRange(entityToDelete);
            Log.Information("Успешно удален диапазон объектов типа {EntityType}.", typeof(TEntity).Name);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при удалении диапазона объектов типа {EntityType}.", typeof(TEntity).Name);
            throw;
        }
    }

    public virtual void Update(TEntity entityToUpdate) {
        try {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            Log.Information("Успешно обновлен объект типа {EntityType} с ID {EntityId}.", typeof(TEntity).Name, entityToUpdate.Id);
        } catch (Exception ex) {
            Log.Error(ex, "Ошибка при обновлении объекта типа {EntityType} с ID {EntityId}.", typeof(TEntity).Name, entityToUpdate.Id);
            throw;
        }
    }

    public async Task SaveChangesAsync() {
       await  _context.SaveChangesAsync();
    }
}
