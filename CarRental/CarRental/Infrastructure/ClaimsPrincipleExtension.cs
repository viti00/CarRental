﻿using System.Security.Claims;

namespace CarRental.Infrastructure
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            string id = null;

            if (user.Identity.IsAuthenticated == true)
            {
                id = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return id;

        }

        public static bool IsAdministrator(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                return false;
            }
            return user.IsInRole("Admin"/*AdministratorRoleName*/);
        }
    }
}
