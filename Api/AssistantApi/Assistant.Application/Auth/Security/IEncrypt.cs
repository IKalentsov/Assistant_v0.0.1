﻿namespace Assistant.Application.Auth.Security
{
    public interface IEncrypt
    {
        string HashPassword(string password, string salt);
    }
}
