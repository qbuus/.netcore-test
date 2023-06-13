namespace API.entityFramework
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsDelivered { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        public List<DishDTO> Dishes { get; set; }
    }
}
