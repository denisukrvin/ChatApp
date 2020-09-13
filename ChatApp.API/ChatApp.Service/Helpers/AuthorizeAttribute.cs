﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Models.User;
using ChatApp.Service.Models.Auth;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatApp.Service.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserModel) context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new AuthResponse { Success = false, Message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}