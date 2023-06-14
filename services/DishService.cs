using API.entityFramework;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using API.exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.services
{

    public interface IDishService
    {
        int Create(int id, CreateDishDTO dto);
        DishDTO Get(int restaurantId, int dishId);
        List<DishDTO> GetAll(int restaurantId)
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

        public DishDTO Get(int restaurantId, int dishId)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null)
            {
                return new NotFoundException("Hi");
            }

            var dish = _dbContext.Dishes.FirstOrDefault(y => y.Equals(dishId));

            if (dish is null || dish.RestaurantId != restaurantId)
            {
                return new NotFoundException("Hi");
            }

            var dishDto = _mapper.Map<DishDTO>(dish);

            return dishDto;
        }

        public List<DishDTO> GetAll(int restaurantId)
        {
            var idRestaurant = _dbContext.Type.Include(x => x.Dishes).FirstOrDefault(x => x.Id == restaurantId);

            if (idRestaurant is null)
            {
                return new NotFoundException("Hi");
            }

            var dishDtos = _mapper.Map<List<DishDTO>>(idRestaurant.Dishes);
        }

        public void RemoveAll(int restaurantId)
        {
            var idRestaurant = _dbContext.Type.Include(x => x.Dishes).FirstOrDefault(x => x.Id == restaurantId);

            if (idRestaurant is null)
            {
                return new NotFoundException("Hi");
            }

            _dbContext.RemoveRange(idRestaurant.Dishes);
            _dbContext.SaveChanges();
        }
    }
}
