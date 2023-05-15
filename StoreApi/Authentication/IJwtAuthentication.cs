using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApi.Authentication
{
    public interface IJwtAuthentication<TEntity>
    {
        string GetJsonWebToken(TEntity entity);
        RefreshToken GenerateRefreshToken();
    }
}
