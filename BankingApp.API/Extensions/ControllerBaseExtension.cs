using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Extensions
{
    public static class ControllerBaseExtension
    {
        private static readonly string DefaultClaimsType = "Id";
        public static Guid GetCurrentUserId(this ControllerBase controller)
        {
            return Guid.Parse(controller.User.Claims.Single(c => c.Type == DefaultClaimsType).Value);
        }
    }
}
