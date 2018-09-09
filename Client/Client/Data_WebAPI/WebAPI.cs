using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Client.Data_WebAPI
{
    public partial class WebAPI
    {

        System.Net.Http.HttpClient mClient;

        public Uri mUri { get; private set; }

        public WebAPI(string url)
        {
            var authData = string.Format("{0}:{1}", "Xamarin", "Pa$$w0rd"); // UserName:Password
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            mClient = new HttpClient();
            mClient.MaxResponseContentBufferSize = 256000;
            mClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeaderValue);

            mUri = new Uri(url);
        }

        //public async Task<List<TodoItem>> RefreshDataAsync()
        //{
        //    Items = new List<TodoItem>();

        //    // RestUrl = http://developer.xamarin.com:8081/api/todoitems
        //    var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

        //    try
        //    {
        //        var response = await mClient.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"				ERROR {0}", ex.Message);
        //    }

        //    return Items;
        //}

        //private async void Read(RequestData requestData)
        //{
            
        //    mClient.PostAsync


        //    var response = await mClient.GetAsync(mUri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        SOAPResult soapResult = CoreUtil.JsonUtils.DeserializeObject<SOAPResult>(content);
        //    }
        //    else
        //    {
        //        
        //    }
        //}
    }
}
