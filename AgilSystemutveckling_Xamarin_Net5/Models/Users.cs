﻿namespace AgilSystemutveckling_Xamarin_Net5.Models
{
    public class Users
    {
        public int Id { get; set; }
        public FullNames FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Accesses Access { get; set; }
        public string Address { get; set; }
        public bool Blocked { get; set; } = false;

    }
}