using System;

namespace EasyChallenge.Application.Settings
{
    public static class CacheKeys
    {
        public static string Portfolio => $"portfolio_{DateTime.Now.ToShortDateString()}";
    }
}
