using Exam.Domain.Entities;
using Exam.Domain.IRepository;
using Exam.Parser;
using Exam.Parser.Strategies;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Infrastructure.Service {
    public class ManagerService {
        private readonly Context _context;
        private readonly IStrategy _categoryStrategy;
        private readonly IServiceProvider _provider;
        private readonly ICategoryRepository _categoryRepository;
        public ManagerService(Context context, IStrategy categoryStrategy, IServiceProvider provider, ICategoryRepository categoryRepository) {
            _context = context;
            _categoryStrategy = categoryStrategy;
            _provider = provider;
            _categoryRepository = categoryRepository;
        }

        public async Task ExtractAll() {
            IEnumerable<IStrategy> strategies = _provider.GetServices<IStrategy>();

            foreach (var strategy in strategies) {
                _context.ExecuteStrategy(strategy);
            }

        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategories() {
            return   await _categoryRepository.GetAsync();
        }
    }
}
