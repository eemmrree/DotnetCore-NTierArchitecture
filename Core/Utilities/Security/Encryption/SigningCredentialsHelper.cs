using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredential(SecurityKey security)
        {
            return new SigningCredentials(security, SecurityAlgorithms.Aes256CbcHmacSha512);
        }
    }
}
