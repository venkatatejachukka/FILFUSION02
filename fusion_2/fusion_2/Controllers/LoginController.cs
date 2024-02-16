using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace fusion_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("InsertIndustry")]
        public
void
InsertIndustry
(
string
companyName,
string
industryType,

string
state,
string
country,
string
contactNo,
string
email,
string
password)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UniCoreDpCNS")))
            {
                connection.Open();
                using
                (SqlCommand command =
                new
                SqlCommand(
                "sp_insertCompanyData"
                , connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Add parameters
                    command.Parameters.AddWithValue("@CompanyName"
    , companyName); command.Parameters.AddWithValue(
    "@IndustryType"
    , industryType); command.Parameters.AddWithValue(
    "@State"
    , state); command.Parameters.AddWithValue(
    "@Country"
    , country); command.Parameters.AddWithValue(
    "@ContactNo"
    , contactNo); command.Parameters.AddWithValue(
    "@Email"
    , email); command.Parameters.AddWithValue(
    "@Password"
    , password);
                    // Execute the stored procedure
                    command.ExecuteNonQuery();
                }
            }

        }

    }
}







