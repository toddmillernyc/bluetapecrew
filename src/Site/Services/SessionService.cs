﻿using System;
using Microsoft.AspNetCore.Http;
using Site.Extensions;
using Site.Services.Interfaces;

namespace Site.Services
{
    public class SessionService : ISessionService
    {
        private const string SessionIdKey = "_SessionId";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string SessionId()
        {
            var sessionId = _httpContextAccessor.HttpContext.Session.Get<string>(SessionIdKey);
            if (!string.IsNullOrEmpty(sessionId)) return sessionId;

            _httpContextAccessor.HttpContext.Session.Set(SessionIdKey, Guid.NewGuid().ToSessionIdString());
            sessionId = _httpContextAccessor.HttpContext.Session.Get<string>(SessionIdKey);
            return sessionId;
        }
    }
}