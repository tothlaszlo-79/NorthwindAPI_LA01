using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindAPI_LA01.Data;
using NorthwindAPI_LA01.Domain;

namespace NorthwindAPI_LA01.Controllers
{
    // Define the route for this controller and specify that it is an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Dependency injection for the database context
        private readonly NothwindContext _context;

        // Constructor to initialize the database context
        public CategoriesController(NothwindContext context)
        {
            _context = context;
        }

        // GET: api/categories
        // Retrieves all categories from the database
        [HttpGet]
        public IActionResult getAll()
        {
            var categories = _context.Categories;
            if (categories == null)
            {
                return NotFound(); // Return 404 if no categories are found
            }

            return Ok(categories); // Return 200 with the list of categories
        }

        // GET: api/categories/{id}
        // Retrieves a specific category by its ID
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(); // Return 404 if the category is not found
            }

            return Ok(category); // Return 200 with the category data
        }

        // GET: api/categories/count
        // Returns the count of categories in the database
        [HttpGet("count")]
        public int Count() => _context.Categories.Count();

        // GET: api/categories/{id}/products
        // Retrieves all products for a specific category by its ID
        [HttpGet("{id}/products")]
        public IEnumerable<Product> GetProductByCategoryId(int id)
        {
            return _context.Products.Where(p => p.CategoryId == id);
        }
    }
}
