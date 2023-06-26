using Microsoft.AspNetCore.Authorization;

internal class MinimRestaurantCount: IAuthorizationRequirement
{
    public int Count { get; }

    public MinimRestaurantCount(int count) 
    {
        Count = count;
    }
}