using System.Data;
using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Dapper;
using NutriFit.Application.Output.DTOs;
using NutriFit.Application.Output.Interfaces;
using NutriFit.Infrastructure.Output.Factory;
using NutriFit.Infrastructure.Output.Queries;
using NutriFit.Infrastructure.Shared.Shared;

namespace NutriFit.Infrastructure.Output.Repositories
{
    public class ReadUserRepository : IReadUserRepository
    {
        private readonly IDbConnection _conn;

        public ReadUserRepository(SqlFactory factory)
        {
            _conn = factory.MySqlConnection();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var query = new UserQueries().GetAllUsers();
            try
            {
                using(_conn)
                {
                    return _conn.Query<UserDTO>(query.Query) as List<UserDTO>;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuários");
            }
        }

        public UserDTO GetUsersByEmail(string email)
        {
            var query = new UserQueries().GetUsersByEmail(email);
            try
            {
                using (_conn)
                {
                    return _conn.QueryFirstOrDefault<UserDTO>(query.Query, query.Parameters) as UserDTO;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuário");
            }
        }

        public UserDTO GetUsersById(int id)
        {
            var query = new UserQueries().GetUsersById(id);
            try
            {
                using (_conn)
                {
                    return _conn.QueryFirstOrDefault<UserDTO>(query.Query, query.Parameters) as UserDTO;
                }
            }
            catch
            {
                throw new Exception("Falha ao recuperar os usuário");
            }
        }

        public async Task<ReturnGetImageUserDTO> DownloadUserImage(ImageUserDTO imageUserDTO)
        {
            var myCredentials = new AwsCredentials()
            {
                AwsKey = imageUserDTO.AwsKey,
                AwsSecretKey = imageUserDTO.AwsSecretKey,
            };
            var credentials = new BasicAWSCredentials(myCredentials.AwsKey, myCredentials.AwsSecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast2
            };

            var returnGetImageUserDTO = new ReturnGetImageUserDTO();
            MemoryStream ms = null;

            try
            {
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = imageUserDTO.BucketName,
                    Key = imageUserDTO.PathFile
                };

                using (var response = await new AmazonS3Client(credentials, config).GetObjectAsync(getObjectRequest))
                {

                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                    if (ms is null || ms.ToArray().Length < 1)
                        throw new FileNotFoundException(string.Format("Imagem não encontrada"));
                    returnGetImageUserDTO.File = ms.ToArray();
                    return returnGetImageUserDTO;
                }

            }
            catch (AmazonS3Exception ex)
            {
                throw;

            }
            catch (Exception ex)
            {
                throw;

            }

        }
    }
}
