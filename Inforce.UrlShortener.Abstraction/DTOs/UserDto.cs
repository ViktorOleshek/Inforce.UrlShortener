﻿namespace Inforce.UrlShortener.Abstraction.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}