
using ChalkCode.Auth;
using Core.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebAuthentication.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(User users);
    }

}