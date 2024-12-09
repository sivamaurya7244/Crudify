using Models.Request;
using Models.Responce;
using System.Collections;

namespace Services.Interfaces
{
    public interface IEmpolyeeService
    {
        Task<IEnumerable> GetEmpolyeeList();
        Task<(int status, string message)> InsertEmpolyeeData(EmpolyeeAddUpdateParam obj);
        Task<EmpolyeeList> ValidateUser(UserLoginParam obj);
        Task<(int, string message)> DeleteEmpolyeeData(EmpolyeeDeleteParam obj);
        Task<EmpolyeeList> DetailEmpolyeeData(EmpolyeeDetailParam obj);
    }
}