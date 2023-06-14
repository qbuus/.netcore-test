
using System.ComponentModel.DataAnnotations;
using API.services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace API.entityFramework
{
    [Route("/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _services;
        public RestaurantController(IRestaurantServices services)
        {
            _services = services;
        }


        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurantsDTOS = _services.getAll();

            return Ok(restaurantsDTOS);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete ([FromRoute] int id)
        {
            var isDeleted = _services.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();          
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromBody] updateRestaurantDto dto,[FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var isDone = _services.Patch(id, dto);

            if (!isDone) return NotFound();
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDTO> GetById([FromRoute] int id)
        {
            var idRestaurant = _services.getById(id);

            if (idRestaurant is null)
            {
                return NotFound();
            }
            
            return Ok(idRestaurant);
        }

        [HttpPost]
        public ActionResult NewRestaurant([FromBody] NewRestaurantDTO DTO)
        {
            if(!ModelState.IsValid)                              
            {
                return BadRequest(ModelState);
            }

            var id = _services.Create(DTO);

            return Created($"/api/restaurants/{id}", null);
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

    public class updateRestaurantDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isDelivered { get; set; }
    }
}
