using NutriFit.Application.Input.Repositories;
using NutriFit.Domain.Entities;
using NutriFit.Infrastructure.Input.Factory;
using NutriFit.Infrastructure.Input.Queries;
using System.Data;
using Dapper;
using MySqlConnector;
using NutriFit.Application.Input.Receivers;
using NutriFit.Application.Input.Commands.UserContext.Bucket;
using NutriFit.Infrastructure.Shared.Shared;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace NutriFit.Infrastructure.Input.Repositories
{
    public class WriteUserRepository : IWriteUserRepository
    {
        private readonly IDbConnection _conn;

        public WriteUserRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public async Task<State> InsertOrUpdateUserImage(UserImageCommand userImage)
        {
            var myCredentials = new AwsCredentials()
            {
                AwsKey = userImage.AwsKey,
                AwsSecretKey = userImage.AwsSecretKey,
            };
            var credentials = new BasicAWSCredentials(myCredentials.AwsKey, myCredentials.AwsSecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = userImage.Image,
                    Key = userImage.Name,
                    BucketName = userImage.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                using var client = new AmazonS3Client(credentials, config);

                var transferUtility = new TransferUtility(client);
                await transferUtility.UploadAsync(uploadRequest);

                return new State(200, $"Imagem {userImage.Name.Split(" - ")[1]} cadastrada com sucesso", null);

            }
            catch (AmazonS3Exception ex)
            {
                return new State((int)ex.StatusCode, ex.Message, null);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, null);
            }

        }

        public void InsertUser(UserEntity user)
        {
            var query = new UserQuery().InsertUserQuery(user);
            try
            {
                using (_conn)
                {
                    _conn.Execute(query.Query, query.Parameters);
                }
            }
            catch(Exception ex)
            {
                if(ex.Message.Contains("Duplicate entry"))
                {
                    throw new Exception("Email já existente");
                }
                throw new Exception("Erro ao inserir o usuário");
            }
            
        }

        public void UpdateUser(UserEntity user)
        {
            var query = new UserQuery().UpdateUserQuery(user);
            try
            {
                using (_conn)
                {
                    _conn.Execute(query.Query, query.Parameters);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    throw new Exception("Email já existente");
                }
                throw new Exception("Erro ao inserir o usuário");
            }
        }
    }
}
