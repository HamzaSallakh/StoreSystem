using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class JwtAuthModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public bool IsAuthentication { get; set; }
        public string Massege { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
