using Exam.Domain.Entities;
using Exam.Domain.Queries;
using Exam.Infrastructure.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase {

        private readonly ManagerService _managerService;
        private readonly IMediator _mediator;
        public ManagerController(ManagerService referencesManagerService, IMediator mediator) {
            _managerService = referencesManagerService;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task ExtractAllRepositories() {
            await _managerService.ExtractAll();
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories() {
            var query = new GetAllCategoryQuery();
            IEnumerable<CategoryEntity> res = await _mediator.Send(query);
            return Ok(res);
        }

    }
}
