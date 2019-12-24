using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Domain.Contracts
{
    public interface IApiUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
