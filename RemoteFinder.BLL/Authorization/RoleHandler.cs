using Microsoft.AspNetCore.Authorization;

namespace RemoteFinder.BLL.Authorization
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var roles = context.User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToArray();

            if (roles.Contains(requirement.Role))
            {
                context.Succeed(requirement);
            }
            
            return Task.CompletedTask;
        }
    }

    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}