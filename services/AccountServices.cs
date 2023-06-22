using API.Controllers;
using API.entityFramework;

namespace API.entityFramework
{
    public class AccountServices
    {
        private readonly RestaurantDbContext _dbContext;
        public AccountServices(RestaurantDbContext context)
        {
            _dbContext = context;
        }
        public void RegisterUser(RegisterUserDTO dto)
        {
            var newUser = new RegisterUserDTO()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };

            _dbContext.Users.Add(newUser);
        }
    }
}
