﻿namespace MoneyApp.WebApi.Models.User
{
    public class LoginUser
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string App { get; set; } = string.Empty;
    }
}