using JwtToken.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JwtToken.Controllers
{
    public class UsersController : Controller
    {

        [HttpGet]
        public IActionResult ViewPoint()
        {
            return View();
        }

       // [HttpPost]
       //public IActionResult ViewPoint(IFormCollection form)
       // {
       //     // Get the token from TempData
       //     var token = TempData["Token"] as string;
       //     var phoneNumber = form["phoneNumber"].ToString();
       //     //if (string.IsNullOrEmpty(phoneNumber))
       //     //{
       //     //    phoneNumber = "01789620408";
       //     //}

       //     // Perform the API call
       //     if (!string.IsNullOrEmpty(token))
       //     {
       //         using (var client = new HttpClient())
       //         {
       //             var encodedPhoneNumber = Uri.EscapeDataString(phoneNumber);
       //             var apiUrl = $"https://dev.herlan.com/wp-json/herlan/v1/pos?phone={encodedPhoneNumber}";
       //             var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
       //             request.Headers.Add("Authorization", "Bearer " + token);
       //             var response = client.SendAsync(request).Result;

       //             if (response.IsSuccessStatusCode)
       //             {
       //                 var data = response.Content.ReadAsStringAsync().Result;

       //                 // Parse the JSON data
       //                 var userData = JsonConvert.DeserializeObject<UserData>(data);

       //                 // Pass the parsed data to the view
       //                 ViewBag.UserData = userData;

       //                 // Return JSON data
       //                 //return Json(new { availablePoints = userData.available_points });
       //             }

       //             else
       //             {
       //                 //return Json(new { error = "Error fetching user data" });
       //                 ViewBag.Error = "Error fetching user data";
       //             }
       //         }
       //     }
       //     else
       //     {
       //         //return Json(new { error = "Token not found in TempData" });
       //         ViewBag.Error = "Invalid Number";
       //     }

       //     return View();
       // }
        public IActionResult ViewPoint(string phoneNumber)
        {
            // Get the token from TempData
            //var token = TempData["Token"] as string;

            // Get the token from session
            var token = HttpContext.Session.GetString("Token");

            // var phoneNumber = form["phoneNumber"].ToString();
            //if (string.IsNullOrEmpty(phoneNumber))
            //{
            //    phoneNumber = "01789620408";
            //}

            // Perform the API call
            //if (!string.IsNullOrEmpty(token))
            if (phoneNumber.Length == 11)
            {
                using (var client = new HttpClient())
                {
                    var encodedPhoneNumber = Uri.EscapeDataString(phoneNumber);
                    var apiUrl = $"https://dev.herlan.com/wp-json/herlan/v1/pos?phone={encodedPhoneNumber}";
                    var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                    request.Headers.Add("Authorization", "Bearer " + token);
                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;

                        // Parse the JSON data
                        var userData = JsonConvert.DeserializeObject<UserData>(data);

                        // Pass the parsed data to the view
                        //ViewBag.UserData = userData;
                        ViewData["UserData"] = userData;

                        // Return JSON data
                        //return Json(new { availablePoints = userData.available_points });
                    }

                    else
                    {
                        //return Json(new { error = "Error fetching user data" });
                        ViewBag.Error = "Error fetching user data";
                    }
                }
            }
            else
            {
                //return Json(new { error = "Token not found in TempData" });
                ViewBag.Error = "Invalid Number";
            }

            return View();
        }
    }
}
