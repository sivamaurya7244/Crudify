using Dapper;
using Microsoft.Extensions.Configuration;
using Models.Request;
using Models.Responce;
using Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Implements
{
    public class EmpolyeeRepository : IEmpolyeeRepository
    {
        public readonly IConfiguration _IConfiguration;
        public EmpolyeeRepository(IConfiguration IConfiguration)
        {
            _IConfiguration = IConfiguration;
        }

        public SqlConnection connection => new SqlConnection(_IConfiguration["ConnectionStrings:DefaultConnection"]);

        public async Task<IEnumerable<EmpolyeeList>> GetEmpolyeeList()
        {
            IEnumerable<EmpolyeeList> objEmployeeList = new List<EmpolyeeList>();

            try
            {
                using (SqlConnection con = connection)
                {
                    objEmployeeList = await con.QueryAsync<EmpolyeeList>("Emp_List", null, commandType: CommandType.StoredProcedure);
                    return objEmployeeList;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<(int status, string message)> InsertEmpolyeeData(EmpolyeeAddUpdateParam obj)
        {

            try
            {
                int status = 0;
                string message = "";

                using (SqlConnection con = connection)
                {


                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("Id", obj.Id);
                    parameters.Add("UserId", obj.UserId);
                    parameters.Add("Name", obj.Name);
                    parameters.Add("Password", obj.Password);
                    parameters.Add("Description", obj.Description);
                    parameters.Add("isActive", obj.isActive);

                    parameters.Add("Status", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("Message", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);

                    var data = await con.ExecuteAsync("Empolyee_Insert_Update", parameters, commandType: CommandType.StoredProcedure);

                    status = parameters.Get<int>("@Status");
                    message = parameters.Get<string>("@Message");

                    return (status, message);

                }
            }
            catch (Exception ex)
            {

                return (-1, ex.Message);
            }

        }

        public async Task<(int, string)> DeleteEmpolyeeData(EmpolyeeDeleteParam obj)
        {
            string message = "";
            using (SqlConnection con = connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);
                parameters.Add("Message", dbType: DbType.String, size: 250, direction: ParameterDirection.Output);

                var data = await con.ExecuteAsync("Empolyee_Delete", parameters, commandType: CommandType.StoredProcedure);
                message = parameters.Get<string>("@Message");
                return (data, message);
            }

        }

        public async Task<EmpolyeeList> DetailEmpolyeeData(EmpolyeeDetailParam obj)
        {
            EmpolyeeList getEmpolyeeList = new EmpolyeeList();
            using (SqlConnection con = connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Id", obj.Id);

                getEmpolyeeList = await con.QueryFirstOrDefaultAsync<EmpolyeeList>("Empolyee_Detail", parameters, commandType: CommandType.StoredProcedure);
                return getEmpolyeeList;
            }

        }

        public async Task<EmpolyeeList> CheckUserLogin(UserLoginParam obj)
        {
            using (SqlConnection con = connection)
            {
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("UserId", obj.UserId);
                parameters.Add("Password", obj.Password);
                var data = await con.QueryFirstAsync<EmpolyeeList>("UserLogin", parameters, commandType: CommandType.StoredProcedure);
                return data;
            }


        }




    }
}