using API.entityFramework;

namespace API.services
{
    public interface IRestaurantServices
    {
        int Create(NewRestaurantDTO dto);
        IEnumerable<RestaurantDTO> getAll();
        RestaurantDTO getById(int id);
    }
}