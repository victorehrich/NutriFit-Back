﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Domain.Entities
{
    public class SexEntity
    {
        public SexEntity(string sexName)
        {
            SexName = sexName;
        }

        public string SexName { get; private set; }
    }
}
