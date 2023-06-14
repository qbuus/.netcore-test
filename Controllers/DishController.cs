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
    }
}
