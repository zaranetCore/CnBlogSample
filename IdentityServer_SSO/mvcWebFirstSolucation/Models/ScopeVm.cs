using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvcWebFirstSolucation.Models
{
    public class ScopeVm
    {
        public string name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Emphasize { get; set; }
        public bool Required { get; set; }
        public bool Checked { get; set; }
    }
}
