using Microsoft.Identity.Client;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroPanda.Blazor.PowerBI.Services
{
    public interface IPowerBIAuthenticationService
    {
        public Task<TokenCredentials> GetAuthentication();        
    }
}
