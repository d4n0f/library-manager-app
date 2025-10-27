using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Shared.DTOs
{
    public class LoginResponse
    {
        public string? TokenType { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}
