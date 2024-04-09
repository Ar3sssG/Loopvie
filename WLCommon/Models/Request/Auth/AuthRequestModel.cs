﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WLCommon.Models.Request
{
    public class AuthRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }

    public class RegisterRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
