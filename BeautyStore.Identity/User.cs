using Microsoft.AspNetCore.Identity;
using System;

namespace BeautyStore.Identity
{
    public class User : IdentityUser
    {
        public Guid OuterGuid { get; set; }
    }
}
