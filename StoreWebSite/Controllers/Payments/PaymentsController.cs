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

namespace StoreWebSite.Controllers.Payments
{
    public class PaymentsController : Controller
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
                    var postTask = client.PostAsync("api/Payments/GetAll", stringContent);
                    //var postTask = client.PostAsJsonAsync<UsersModel>("api/Users/GetAll", null); ممكن استخدم هاي الجملة بدل السطرين الي قبلها
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        List<PaymentsModel> t = (List<PaymentsModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<PaymentsModel>)));
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

        public IActionResult AddPayments()
        {
            if (CommonProp.Token == "" || CommonProp.Token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.OrdersData = OrdersData();
            return View();
        }

        [HttpPost]
        public ActionResult AddPayments(PaymentsModel _PaymentsModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<PaymentsModel>("api/Payments/AddPayments", _PaymentsModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<PaymentsModel> t = (List<PaymentsModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<PaymentsModel>)));
                        return RedirectToAction("Index", "Payments");

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


        public IActionResult UpdatePayments(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Payments/FindPayments", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        PaymentsModel t = (PaymentsModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(PaymentsModel)));
                        //return RedirectToAction("Index", "Payments");
                        ViewBag.OrdersData = OrdersData();
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
        public ActionResult UpdatePayments(PaymentsModel _PaymentsModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<PaymentsModel>("api/Payments/UpdatePayments", _PaymentsModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<PaymentsModel> t = (List<PaymentsModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<PaymentsModel>)));
                        return RedirectToAction("Index", "Payments");

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


        public IActionResult DeletePayments(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Payments/FindPayments", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        PaymentsModel t = (PaymentsModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(PaymentsModel)));

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
        public ActionResult DeletePayments(PaymentsModel _PaymentsModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<PaymentsModel>("api/Payments/DeletePayments", _PaymentsModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<PaymentsModel> t = (List<PaymentsModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<PaymentsModel>)));
                        return RedirectToAction("Index", "Payments");

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



        public IActionResult DetailsPayments(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Payments/AddPayments", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Payments/FindPayments", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        PaymentsModel t = (PaymentsModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(PaymentsModel)));

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

        object OrdersData()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);

                //HTTP POST
                var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                var postTask = client.PostAsync("api/Orders/GetAll", stringContent);
                //var postTask = client.PostAsJsonAsync<UsersModel>("api/Users/GetAll", null); ممكن استخدم هاي الجملة بدل السطرين الي قبلها
                postTask.Wait();

                var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                    JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                    JToken JData = Obj["data"];
                    List<OrdersModel> t = (List<OrdersModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<OrdersModel>)));
                    return t;

                }
                return (null);
            }
        }
    }
}

