using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using WebApp.Services.AuthService;

namespace WebApp.Controllers
{
    public class AuthenticationController : Controller
    {

        private IConfiguration _configuration;

        public AuthenticationController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("Auth");
            return RedirectToAction("Index", "Home", "");

        }

        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO model)
        {
            var restClient = new RestClient();
            var request = new RestRequest(_configuration["MicroservicAddress:ApiGatewayForWeb:Uri"] + "Auth/Login", Method.POST);
            request.AddHeader("Content-Type", "application/json");
            string serializeModel = JsonSerializer.Serialize(model);
            request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
            var response = restClient.Execute(request);
            if (string.IsNullOrEmpty(response.Content))
            {

                ViewBag.Message = " Login Faild ";
                return View(model);
            }

            var token = JsonSerializer.Deserialize<AuthResult>(response.Content);
            if (token.status)
            {
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                cookieOptions.Path = "/";
                Response.Cookies.Append("Auth", token.token, cookieOptions);

                return RedirectToAction("Index", "Home", "");

            }
            else
            {
                ViewBag.Message = " Login Faild ";
                return View(model);
            }

        }


    }
}
