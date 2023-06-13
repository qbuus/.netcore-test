
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
                if(!_dbContext.Type.Any())
                {
                    var type = GetTypes();
                    _dbContext.Type.AddRange(type);
                }
            }
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
