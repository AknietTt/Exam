
using AutoMapper;
using Exam.Domain.Entities;
using Exam.Domain.IRepository;
using Exam.Domain.Queries;
using MediatR;

namespace Exam.Infrastructure.Handlers;
public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryEntity>> {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper) {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryEntity>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {
       return    await _categoryRepository.GetAsync();
    }
}
