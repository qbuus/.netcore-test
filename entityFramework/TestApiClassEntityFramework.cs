using System.Net;

namespace API.entityFramework
{
    public class TestApiClassEntityFramework
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsDelivered { get; set; }
        public string Contact { get; set; }
        public int ContactNumber { get; set; } 
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
