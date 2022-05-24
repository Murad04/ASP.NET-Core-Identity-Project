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

            var value = context.User.FindFirst(x => x.Type == "EmploymentDate").Value; 
            var employmentDate = DateTime.Parse(value);
            var period = DateTime.Now - employmentDate;
            if (period.Days > 30 * requirement.Month)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
