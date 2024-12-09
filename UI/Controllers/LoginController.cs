
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Responce;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        

       
        public async Task<IActionResult> UserLogin([FromBody] UserLoginParam obj)
        {
            try
            {
                // Call the API to authenticate the user
                string responseString = await AuthenticateUserAsync(obj.UserId, obj.Password);
                var vData = JsonConvert.DeserializeObject<clsResponse>(responseString);

                if (vData.isSuccess)
                {
                    // Set session value if authentication is successful
                    HttpContext.Session.SetString("UserLoggedIn", "true");
                    TempData["JWTToken"] = vData.data;
                    return Json(vData);
                }
                else
                {
                    return Json(new { isSuccess = false, message = "An error occurred during login." });
                }
            }
            catch (Exception ex)
            {
                // Log and handle error
                Console.WriteLine(ex.Message);
                return Json(new { isSuccess = false, message = "An error occurred during login." });
            }
        }

        /// <summary>
        /// Invokes the API to authenticate the user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="password">Password</param>
        /// <returns>True if authenticated, otherwise false</returns>
        private async Task <string> AuthenticateUserAsync(string userId, string password)
        {
            string strData = "";
            try
            {
                // Create HttpClient
                var client = _httpClientFactory.CreateClient();

                // API URL
                string apiUrl = "https://localhost:7201/api/Empolyee/LoginUser";

                // Prepare request payload
                var loginData = new
                {
                    UserId = userId,
                    Password = password
                };
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Call API
                var response = client.PostAsJsonAsync(apiUrl, loginData).Result;

                // Check response
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response content
                    var responseString =  response.Content.ReadAsStringAsync();
                    if (responseString.IsCompleted)
                    {
                        strData= responseString.Result;
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                strData= "";
            }
            return strData;
        }

       
    }
}
