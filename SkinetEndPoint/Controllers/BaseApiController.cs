using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SkinetEndPoint.RequestHelpers;

namespace SkinetEndPoint.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>( IGenericRepository<T> repo, ISpecification<T> spec, int pageIndex, int pageSize ) where T: BaseEntity
        {
            var items = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);
            var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

            return Ok(pagination);
        }
    }
}
