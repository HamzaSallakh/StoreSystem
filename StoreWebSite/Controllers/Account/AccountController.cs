using Domain.BaseEntity;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoreWebSite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebSite.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            if (CommonProp.Token == "" || CommonProp.Token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel _RegisterModel)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<RegisterModel>("api/Account/Register", _RegisterModel); 
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index", "Home");
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Login();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }

            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel _LoginModel)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(CommonProp.UrlApi);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<LoginModel>("api/Account/Login", _LoginModel);
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        
                        
                        LoginResultModel t = (LoginResultModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(LoginResultModel)));//هاي عشان اجيب البياتات الي بالداتا واحولها من جيسون لهذا الكلاس الي هو لوجين ريزولت موديل عشان اقدر اخزنهم بالستاتيك بروبرتيز ويضلوا عندي طول ما المستخدم فاتح الجهاز يعني بدل السيشن والكوكي
                        if (t.Token=="" || t.Token==null)
                        {
                            return Content("Email Or Password is wrong");
                        }
                        CommonProp.Token = t.Token;
                        CommonProp.RefreshToken = t.RefreshToken;
                        CommonProp.UserName = t.UserName;

                        return RedirectToAction("Index", "Buyers");
                        

                    }

                }
                catch (System.Exception)
                {
                    throw;
                }

            }
            return View(_LoginModel);
        }



        public IActionResult Logout()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    var postTask = client.PostAsync("api/Account/Logout", stringContent);
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        CommonProp.Token = "";
                        CommonProp.RefreshToken = "";
                        return RedirectToAction("Login", "Account");
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Login();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }

            }
            return View();
        }

    }
}
