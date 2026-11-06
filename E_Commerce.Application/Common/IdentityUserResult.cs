using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public class IdentityUserResult
    {
        public IdentityUserResult(string id, string? email, string? displayName, string? userName)
        {
            Id=id;
            Email=email;
            DisplayName=displayName;
            UserName=userName;
        }

        public string Id { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? DisplayName { get; set; } = default!;
        public string? UserName { get; set; } = default!;


    }
}
