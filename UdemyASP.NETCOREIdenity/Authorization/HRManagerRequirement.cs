using Microsoft.AspNetCore.Authorization;

namespace UdemyASP.NETCOREIdenity.Authorization
{
    public class HRManagerRequirement:IAuthorizationRequirement
    {
        public HRManagerRequirement(int month)
        {
            Month = month;
        }

        public int Month { get; }
    }
    public class HRManagerRequirementHandler : AuthorizationHandler<HRManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerRequirement requirement)
        {
            if(!context.User.HasClaim(x=>x.Type== "EmploymentDate"))
                return Task.CompletedTask;

            DateTime.TryParse(context.User.FindFirst(x => x.Type == "Employment")?.Value, out DateTime employmentDate);
            var period = DateTime.Now - employmentDate;
            if (period.Days > 30 * requirement.Month)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
