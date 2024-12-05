using Models.Request;
using Models.Responce;
using System.Collections;

namespace Repository.Interfaces
{
    public interface IEmpolyeeRepository
    {
        Task<IEnumerable<EmpolyeeList>> GetEmpolyeeList();
        Task<(int status,string message)> InsertEmpolyeeData(EmpolyeeAddUpdateParam obj);
        Task<EmpolyeeList> CheckUserLogin(UserLoginParam obj);
        Task<(int, string)> DeleteEmpolyeeData(EmpolyeeDeleteParam obj);
        Task<EmpolyeeList> DetailEmpolyeeData(EmpolyeeDetailParam obj);
    }
}