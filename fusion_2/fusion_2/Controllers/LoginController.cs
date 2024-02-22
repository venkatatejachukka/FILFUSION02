using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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




       


        [HttpPost("login")]
        public int Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("UniCoreDpCNS")))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("Fetchinglogin", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Pass", password);

                    // Use ExecuteScalar to get the result from the stored procedure
                    object result = command.ExecuteScalar();

                    // Check if the result is not null and convert it to int
                    if (result != null && int.TryParse(result.ToString(), out int res))
                    {
                        return res;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }




    }
}







