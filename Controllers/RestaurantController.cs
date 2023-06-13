
using System.ComponentModel.DataAnnotations;
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

        [HttpPost]
        public ActionResult NewRestaurant([FromBody] NewRestaurantDTO DTO)
        {
            if(!ModelState.IsValid)                              
            {
                return BadRequest(ModelState);
            }

            var createdRestaurant = _mapper.Map<TestApiClassEntityFramework>(DTO);

            _dbContext.Type.Add(createdRestaurant);
            
            _dbContext.SaveChanges();

            return Created($"/api/restaurants/{createdRestaurant.Id}", null);
        }
    }

    public class NewRestaurantDTO
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsDelivered { get; set; }
        public string Contact { get; set; }
        public int ContactNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street{ get; set; }
        public string postalCode { get; set; }  
    }
}
