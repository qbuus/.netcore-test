
using Microsoft.AspNetCore.Mvc;

namespace API.entityFramework
{
    [Route("/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult<IEnumerable<TestApiClassEntityFramework>> GetAll()
        {
            var restaurants = _dbContext.Type.ToList();

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<TestApiClassEntityFramework>> GetById([FromRoute] int id)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null)
            {
                return NotFound();
            }

            return Ok(idRestaurant);
        }
    }
}
