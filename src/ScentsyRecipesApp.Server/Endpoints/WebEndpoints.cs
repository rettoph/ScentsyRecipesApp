using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Endpoints
{
    public static class WebEndpoints
    {
        public static void Configure(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=UnitOfMeasurement}/{action=All}");
        }
    }
}
