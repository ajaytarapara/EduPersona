using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Shared.Models.Response
{
    public class AccessTokenByRefreshTokenResponse
    {
        public string NewAccessToken { get; set; } = null!;
        public DateTime RefreshTokenExpiredAt { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTime SessionExpiredAt { get; set; }
        public int SessionId { get; set; }
    }
}