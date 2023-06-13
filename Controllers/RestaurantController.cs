
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace API.entityFramework
{
    [Route("/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurants = _dbContext.Type.Include(r => r.Address).Include(r => r.Dish).ToList();

            var restaurantsDTOS = _mapper.Map<List<RestaurantDTO>>(restaurants);

            return Ok(restaurantsDTOS);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDTO> GetById([FromRoute] int id)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null)
            {
                return NotFound();
            }

            var idRestaurantDTOS = _mapper.Map<RestaurantDTO>(idRestaurant);

            return Ok(idRestaurantDTOS);
        }
    }
}
