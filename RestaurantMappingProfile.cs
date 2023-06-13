using AutoMapper;

namespace API.entityFramework
{
    public class RestaurantMappingProfile: Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<TestApiClassEntityFramework, RestaurantDTO>()
                .ForMember(x => x.City, z => z.MapFrom(s => s.Address.City))
                .ForMember(x => x.Street, z => z.MapFrom(s => s.Address.Street))
                .ForMember(x => x.PostalCode, z => z.MapFrom(s => s.Address.PostalCode));
       

            CreateMap<Dish, DishDTO>();            
        }
    }
}
