using System;

namespace AuthLayer.Repository
{
    public interface IAuth
    {
        string GenerateToken(string username,string password, int? userid);
    }
}