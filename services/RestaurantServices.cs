using API.entityFramework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public RestaurantDTO getById(int id)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null)
            {
                return null;
            }

            var result = _mapper.Map<RestaurantDTO>(idRestaurant);
            return result;
        }

        public IEnumerable<RestaurantDTO> getAll()
        {
            var restaurants = _dbContext.Type.ToList();

            var result = _mapper.Map<List<RestaurantDTO>>(restaurants);

            return result;
        }

        public int Create(NewRestaurantDTO dto)
        {
            var createdRestaurant = _mapper.Map<TestApiClassEntityFramework>(dto);

            _dbContext.Type.Add(createdRestaurant);

            _dbContext.SaveChanges();

            return createdRestaurant.Id;
        }

        public bool Delete(int id)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null) return false;

            _dbContext.Type.Remove(idRestaurant);
            _dbContext.SaveChanges();
            return true;
        }
    }
}

