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

namespace StoreWebSite.Controllers.Buyers
{
    public class BuyersController : Controller
    {
        public IActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    var postTask = client.PostAsync("api/Buyers/GetAll", stringContent);
                    //var postTask = client.PostAsJsonAsync<UsersModel>("api/Users/GetAll", null); ممكن استخدم هاي الجملة بدل السطرين الي قبلها
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        List<BuyersModel> t = (List<BuyersModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<BuyersModel>)));
                        var ss = JsonConvert.SerializeObject(t);
                        return View(t);

                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }

            }
            return View();

        }


        public IActionResult AddBuyers()
        {
            if (CommonProp.Token == "" || CommonProp.Token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddBuyers(BuyersModel _BuyersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {
                    var postTask = client.PostAsJsonAsync<BuyersModel>("api/Buyers/AddBuyers", _BuyersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Buyers");
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }
        }

        public IActionResult UpdateBuyers(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    var postTask = client.PostAsJsonAsync<int>("api/Buyers/FindBuyers", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        BuyersModel t = (BuyersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(BuyersModel)));
                        
                        return View(t);
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }
        }
        [HttpPost]
        public ActionResult UpdateBuyers(BuyersModel _BuyersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    var postTask = client.PostAsJsonAsync<BuyersModel>("api/Buyers/UpdateBuyers", _BuyersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Buyers");
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }

        }


        public IActionResult DeleteBuyers(int id )
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {
                    var postTask = client.PostAsJsonAsync<int>("api/Buyers/FindBuyers", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        BuyersModel t = (BuyersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(BuyersModel)));
                        
                        return View(t);
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }
        }


        [HttpPost]
        public ActionResult DeleteBuyers(BuyersModel _BuyersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {
                    var postTask = client.PostAsJsonAsync<BuyersModel>("api/Buyers/DeleteBuyers", _BuyersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Buyers");

                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }

        }



        public IActionResult DetailsBuyers(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {
                    var postTask = client.PostAsJsonAsync<int>("api/Buyers/FindBuyers", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        BuyersModel t = (BuyersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(BuyersModel)));

                        return View(t);
                    }
                    else if ((int)result.StatusCode == 401)
                    {
                        if (CommonProp.RequestRefreshToken() == false)
                        {
                            return RedirectToAction("Login", "Account");
                        };
                        Index();
                    }


                }
                catch (System.Exception)
                {
                    throw;
                }
                return View();
            }
        }




    }
}
