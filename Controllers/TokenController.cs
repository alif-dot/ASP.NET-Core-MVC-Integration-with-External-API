using JwtToken.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JwtToken.Controllers
{
    public class TokenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> GenerateToken(TokenRequest model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://dev.herlan.com/wp-json/herlan/v1/token/generate");
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(model.UserName), "username");
        //        content.Add(new StringContent(model.Password), "password");
        //        request.Content = content;

        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();

        //        var token = await response.Content.ReadAsStringAsync();

        //        // Store the token in local storage
        //        ViewBag.Token = token;
        //        return View("Index");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GenerateToken(TokenRequest model)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://dev.herlan.com/wp-json/herlan/v1/token/generate");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(model.UserName), "username");
                content.Add(new StringContent(model.Password), "password");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                // Extract only the token from the response data
                var token = responseData.token;

                // Store the token in TempData
                //TempData["Token"] = token;

                // Store the token in session
                HttpContext.Session.SetString("Token", token);

                return RedirectToAction("ViewPoint", "Users");
            }
        }
    }
}
