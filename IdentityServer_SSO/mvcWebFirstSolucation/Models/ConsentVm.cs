using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcWebFirstSolucation.Models
{
    public class ConsentVm :InputConsentViewModel 
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string Logo { get; set; }
        public IEnumerable<ScopeVm> IdentityScopes { get; set; }
        public IEnumerable<ScopeVm> ResourceScopes { get; set; }

    }
}
