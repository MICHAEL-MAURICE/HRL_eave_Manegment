using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
        public Task<(JwtSecurityToken, string)> RefreshToken(string userRefreshToken);
        public void AddToCash(List<string> Users);
        public void ClearCach();
        public  void LogoutAllUsers();

    }
}
