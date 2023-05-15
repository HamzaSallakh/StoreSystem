using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiersOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiersOn;
        public DateTime CreateOn { get; set; }
        public DateTime? RevokeOn { get; set; }
        public bool IsActive => RevokeOn == null && !IsExpired;
    }
}
