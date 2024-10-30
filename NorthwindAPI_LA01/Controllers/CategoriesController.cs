using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI_LA01.Data;
using NorthwindAPI_LA01.Domain;

namespace NorthwindAPI_LA01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NothwindContext _context;

        public CategoriesController(NothwindContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            var categories = _context.Categories;
            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);


            
        }


        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id) {
            
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            } 
            
            return Ok(category);
        }

        [HttpGet("count")]
        public int Count() => _context.Categories.Count();

        [HttpGet("{id}/products")]
        public IEnumerable<Product> GetProductByCategoryId(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id);
        }

    }
}
