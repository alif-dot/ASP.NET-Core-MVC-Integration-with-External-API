using JwtToken.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtToken.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostData(PosDataModel model)
        {
            var token = TempData["Token"] as string;

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Access token not found");
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var apiUrl = "https://dev.herlan.com/wp-json/herlan/v1/pos";

                    // Serialize the model to JSON
                    var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(model);

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    request.Headers.Add("Authorization", "Bearer " + token);
                    request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                        return Content(responseData);
                    }
                    else
                    {
                        return BadRequest("Error posting data to API: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
