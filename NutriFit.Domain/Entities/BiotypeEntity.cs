using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class BiotypeEntity :BaseEntity
    {
        public BiotypeEntity(string biotypeName)
        {
            BiotypeName = biotypeName;
        }

        public string BiotypeName { get; private set; }
    }
}
