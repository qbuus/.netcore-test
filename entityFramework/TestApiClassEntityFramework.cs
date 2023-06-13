namespace API.entityFramework
{
    public class TestApiClassEntityFramework
    {
      
        public string Contact { get; set; }
        public int ContactNumber { get; set; } 
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
