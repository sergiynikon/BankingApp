using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.API.Extensions
{
    public static class ControllerBaseExtension
    {
        public static Guid GetCurrentUserId(this ControllerBase controller)
        {
            return Guid.Parse(controller.User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
        }
    }
}
