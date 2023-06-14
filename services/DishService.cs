using API.entityFramework;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using API.exceptions;
using AutoMapper;

namespace API.services
{

    public interface IDishService
    {
        int Create(int id, CreateDishDTO dto);
    }
    public class DishService:IDishService
    {   
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public int Create (int id, CreateDishDTO dto)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null)
            {
                return new NotFoundException("Hi");
            }

            var entity = _mapper.Map<Dish>(dto);

            _dbContext.Dishes.Add(entity);

            _dbContext.SaveChanges();

            return entity.RestaurantId;
        }
    }
}
