using System;
using System.Collections.Generic;
using System.Text;

namespace DataRooms.Entity
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
        public IEnumerable<UserRoleMapping> AssignedRoles { get; set; }


        public AuthenticateResponse(User user, string token,DateTime? expiration)
        {
            Id = user.Id;
            FirstName = user.FullName;
            Username = user.UserName;
            Token = token;
            Expiration = expiration;
            CompanyId = user.CompanyId;
            CompanyName = user.CompanyName;
        }
    }
}
