

using Exam.Domain.Entities;
using MediatR;

namespace Exam.Domain.Queries;
public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryEntity>> {
}
