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

namespace StoreWebSite.Controllers.Orders
{
    public class OrdersController : Controller
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

        public IActionResult AddOrders()
        {
            if (CommonProp.Token == "" || CommonProp.Token == null)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.BuyersData = BuyersData();
            return View();
        }

        [HttpPost]
        public ActionResult AddOrders(OrdersModel _OrdersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<OrdersModel>("api/Orders/AddOrders", _OrdersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<OrdersModel> t = (List<OrdersModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<OrdersModel>)));
                        return RedirectToAction("Index", "Orders");

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


        public IActionResult UpdateOrders(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Orders/FindOrders", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        OrdersModel t = (OrdersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(OrdersModel)));
                        //return RedirectToAction("Index", "Orders");
                        ViewBag.BuyersData = BuyersData();
                        ViewBag.OrdersData = OrdersData();
                        ViewBag.OrdersDataSelected = t.OrdersId;
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
        public ActionResult UpdateOrders(OrdersModel _OrdersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<OrdersModel>("api/Orders/UpdateOrders", _OrdersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<OrdersModel> t = (List<OrdersModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<OrdersModel>)));
                        return RedirectToAction("Index", "Orders");

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


        public IActionResult DeleteOrders(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Orders/FindOrders", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        OrdersModel t = (OrdersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(OrdersModel)));

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
        public ActionResult DeleteOrders(OrdersModel _OrdersModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<OrdersModel>("api/Orders/DeleteOrders", _OrdersModel);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        //var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        //JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        //JToken JData = Obj["data"];
                        //List<OrdersModel> t = (List<OrdersModel>)JsonConvert.DeserializeObject(JData.ToString(), (typeof(List<OrdersModel>)));
                        return RedirectToAction("Index", "Orders");

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



        public IActionResult DetailsOrders(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);
                try
                {

                    //HTTP POST
                    //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                    //var postTask = client.PostAsync("api/Orders/AddOrders", stringContent);
                    var postTask = client.PostAsJsonAsync<int>("api/Orders/FindOrders", id);/* ممكن استخدم هاي الجملة بدل السطرين الي قبلها*/
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        JToken JData = Obj["data"];
                        OrdersModel t = (OrdersModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(OrdersModel)));

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

        object BuyersData() {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonProp.UrlApi);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CommonProp.Token);

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
                        return t;

                    }
                return (null);
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
