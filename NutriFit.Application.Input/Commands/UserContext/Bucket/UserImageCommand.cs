using NutriFit.Application.Input.Commands.Interfaces;


namespace NutriFit.Application.Input.Commands.UserContext.Bucket
{
    public class UserImageCommand : ICommand
    {
        public string Name { get; set; } = null!;
        public MemoryStream Image { get; set; } = null!;
        public string BucketName { get; set; } = null!;
        public string AwsKey { get; set; } = "";
        public string AwsSecretKey { get; set; } = "";
    }
}
