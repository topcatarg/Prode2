﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Back.Helpers
{
    public static class ProjectExtensions
    {
        public static T GetClaim<T>(this ClaimsPrincipal user, string type)
        {
            var value = user.Claims.FirstOrDefault(c => c.Type == type)?.Value;
            if (value == null)
            {
                return default(T);
            }

            var converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromString(value);
        }

    }
}
