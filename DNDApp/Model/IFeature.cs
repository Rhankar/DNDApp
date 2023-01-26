using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public interface IFeature
    {
        void ApplyFeature(DNDCharacter owner);
    }
}
