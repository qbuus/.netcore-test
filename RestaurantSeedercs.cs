
namespace API.entityFramework
{
    public class RestaurantSeedercs
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeedercs(RestaurantDbContext dbContext)
        { 
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.Type.Any())
                {
                    var type = GetTypes();
                    _dbContext.Type.AddRange(type);
                    _dbContext.SaveChanges();
                }
            }
        }
        
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                }, new Role()
                {
                    Name = "Admin"
                },new Role()
                {
                    Name = "Manager"
                }
            };
            return roles;
        }

        private IEnumerable<TestApiClassEntityFramework> GetTypes()
        {
            var restaurants = new List<TestApiClassEntityFramework>()
            {
                new TestApiClassEntityFramework()
                {
                    Name = "Rest",
                    Type = "Fancy",
                    Description = "Fancy food is a rest that satisfies all clients",
                    Contact = "Rest@gmail.com",
                    ContactNumber = 666444555,
                    IsDelivered = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Pasta Med",
                            Price = 16.20M,
                        }
                    },
                    Address = new Address() 
                        {
                            City = "NY",
                            Street = "UpStrr",
                            PostalCode = "31-313"
                        }
                }
            };
            return restaurants;
        }
    }
}
