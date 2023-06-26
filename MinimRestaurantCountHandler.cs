using System.Security.Claims;
using API.entityFramework;
using Microsoft.AspNetCore.Authorization;

namespace API
{
   
    public class MinimRestaurantCountHandler : AuthorizationHandler<MinimRestaurantCount>
    {

        private readonly RestaurantDbContext _context;

        public MinimRestaurantCountHandler(RestaurantDbContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimRestaurantCount requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var RequiredCount = _context.Restaurant.Count(c => c.CreatedById == userId);

            if (RequiredCount >= requirement.Count)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
