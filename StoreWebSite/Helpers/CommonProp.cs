using Domain.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebSite.Helpers
{
    public class CommonProp
    {
        public static string Token { get; set; }
        public static string RefreshToken { get; set; }

        public static string UrlApi { get; set; }
        public static string UserName { get; set; }




        public static bool RequestRefreshToken()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(UrlApi);

                    //HTTP POST
                    var Req = new RefreshTokenModel
                    {
                        Token = RefreshToken
                    };
                    var postTask = client.PostAsJsonAsync<RefreshTokenModel>("api/Account/RefreshToken", Req);//هون البرامتر الثاني بهذا الفنكشن بستقبل اوبجيكت فقط فعشان هيك سوينا كلاس وسوينا منه اوبجيكت وحطيناه بدل بروبرتي الريفرش توكن الي موجود بهذا الكلاس الي هو كومون بروب 
                    postTask.Wait();

                    var result = postTask.Result;//هاي يعني بعد ما عمل ويت واستنى انه فاريبل البوست تاسك ترجع داتا رجعلي هون الداتا الي رجعت بعد الويت

                    if (result.IsSuccessStatusCode)
                    {
                        var data = result.Content.ReadAsStringAsync().Result;//يعني اقرا البيانات الي جاي بالريزولت كسترنج
                        JObject Obj = JObject.Parse(data);//سوينا هاي عشان نجيب من الستاندرد جيسون بس الداتا
                        if ((bool)Obj["success"] == true)
                        {
                            JToken JData = Obj["data"];
                            LoginResultModel t = (LoginResultModel)JsonConvert.DeserializeObject(JData.ToString(), (typeof(LoginResultModel)));


                            CommonProp.Token = t.Token;
                            CommonProp.RefreshToken = t.RefreshToken;
                            CommonProp.UserName = t.UserName;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;

                }
                catch (System.Exception)
                {
                    throw;
                }

            }

        }
    }
}
