using Models.Request;
using Models.Responce;
using Repository.Implements;
using Repository.Interfaces;
using Services.Interfaces;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Text;

namespace Services.Implements
{
    public class EmpolyeeService : IEmpolyeeService
    {
        public static IEmpolyeeRepository _IEmpolyeeRepository;
        public EmpolyeeService(IEmpolyeeRepository IEmpolyeeRepository)
        {
            _IEmpolyeeRepository = IEmpolyeeRepository;
        }

        public async Task<IEnumerable> GetEmpolyeeList()
        {
            try
            {
                var vData = await _IEmpolyeeRepository.GetEmpolyeeList();
                return vData;
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
                obj.Password = DecryptPassword(obj.Password);
                var vData = await _IEmpolyeeRepository.InsertEmpolyeeData(obj);
                return vData;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<(int, string message)> DeleteEmpolyeeData(EmpolyeeDeleteParam obj)
        {
            try
            {
                var res = await _IEmpolyeeRepository.DeleteEmpolyeeData(obj);
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<EmpolyeeList> DetailEmpolyeeData(EmpolyeeDetailParam obj)
        {
            try
            {
                EmpolyeeList getEmpolyeeList = new EmpolyeeList();
                getEmpolyeeList = await _IEmpolyeeRepository.DetailEmpolyeeData(obj);
                return getEmpolyeeList;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<EmpolyeeList> ValidateUser(UserLoginParam obj)
        {
            EmpolyeeList empolyeeList = new EmpolyeeList();
            try
            {
                obj.Password = EncryptPass(obj.Password);
                empolyeeList = await _IEmpolyeeRepository.CheckUserLogin(obj);
            }
            catch (Exception ex)
            {
            }
            return empolyeeList;

        }

        public static string EncryptPass(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                return null;

            }
            else
            {
                byte[] storePass = Encoding.ASCII.GetBytes(pass);
                string encryptedPassword = Convert.ToBase64String(storePass);
                return encryptedPassword;
            }
        }

        public static string DecryptPassword(string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(pass))
                {
                    return null;
                }
                else
                {
                    //byte[] encryptedPassword = Convert.FromBase64String(pass);
                    //string decryptedPassword = Encoding.ASCII.GetString(encryptedPassword);
                    string decryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(pass));
                    return decryptedPassword;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}