using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SkinetEndPoint.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        //we use api controller because then we don't need to specify controller where to look for the attribute e.g [fromBody], [FromQuey] etc

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type,sort);
            var products = await repo.ListAsync(spec);
            return Ok(products);

        }
        [HttpGet("{id:int}")] // api/products/2
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id); 
            if (product == null) return NotFound();
            return product;
        }



        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if(await repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct",new {id = product.Id});
            }
           
            return BadRequest("Problem creating product");
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product");
            repo.Update(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem Updating product");
        }


        //changes left

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            repo.Remove(product);
            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem Deleting the product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }


        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
