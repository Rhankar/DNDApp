using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDSubClassInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DNDClassFeature> SubclassFeatures { get; set; }
    }
}
