using API.entityFramework;
using API.Models;
using API.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace API.Controllers
{
    [Route("/api/{id}/dish")]
    [ApiController]
    public class DishController:ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        { 
            _dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int id, CreateDishDTO dto)
        {
           var newDish = _dishService.Create(id, dto);
           return Created($"api/{id}/dish/{newDish}", null);
        }

        [HttpGet("{dishId}");
        public ActionResult<DishDTO> Get(int id, [FromRoute] int dishId)
        {
            DishDTO dish = _dishService.Get(id, dishId);
            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDTO>> GetAll(int id)
        {
            var all = _dishService.GetAll(id);
            return Ok(all);
        }
    }
}
