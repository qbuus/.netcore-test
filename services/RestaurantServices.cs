using API.entityFramework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public RestaurantServices(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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
            _logger.LogWarning($"{id} delete action");
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);

            if (idRestaurant is null) return false;

            _dbContext.Type.Remove(idRestaurant);
            _dbContext.SaveChanges();
            return true;
        }

        public bool Patch(int id, updateRestaurantDto dto)
        {
            var idRestaurant = _dbContext.Type.FirstOrDefault(x => x.Id == id);
            if (idRestaurant is null) return false;

            idRestaurant.Name = dto.Name;
            idRestaurant.Description = dto.Description;
            idRestaurant.IsDelivered = dto.isDelivered;

            _dbContext.SaveChanges(); return true;
        }
    }
}

