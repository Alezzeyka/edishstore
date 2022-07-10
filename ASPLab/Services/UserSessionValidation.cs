using ASPLab.Data.Interfaces;
using ASPLab.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPLab.Services
{
    public static class UserSessionValidation
    {

        public static User GetCurrentUser(HttpContext httpContext, IUser userRep)
        {
            if (httpContext.Session.GetString("UserID") == null)
            {
                return null;
            }
            return userRep.GetUserById(new Guid(httpContext.Session.GetString("UserID")));
        }
        public static bool IsUserSessionValid(HttpContext httpContext, IUser userRep)
        {
            return httpContext.Session.GetString("UserID") != null;
        }
        public static bool IsUserSessionValid(HttpContext httpContext, IUser userRep, Guid userId)
        {
            return IsUserSessionValid(httpContext,userRep) && new Guid(httpContext.Session.GetString("UserID")) == userId;
        }
    }
}
