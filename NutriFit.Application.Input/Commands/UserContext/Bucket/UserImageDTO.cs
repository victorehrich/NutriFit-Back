using NutriFit.Application.Input.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Input.Commands.UserContext.Bucket
{
    public class UserImageDTO
    {
        public string Name { get; set; } = null!;
        public MemoryStream Image { get; set; } = null!;
    }
}
