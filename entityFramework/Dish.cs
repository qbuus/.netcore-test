namespace API.entityFramework
{
    public class Dish
    {
        public int RestaurantId { get; set; }
        public virtual TestApiClassEntityFramework Restaurant { get; set; }
    }
}