using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriFit.Application.Output.DTOs
{
    public class ImageUserDTO
    {
        public string PathFile { get; set; }
        public string AwsKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string BucketName { get; set; }

    }
}
