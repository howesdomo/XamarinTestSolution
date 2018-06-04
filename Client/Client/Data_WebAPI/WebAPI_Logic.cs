using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Data_WebAPI
{
    public partial class WebAPI
    {

        //public async Task<List<TodoItem>> RefreshDataAsync()
        //{
        //    Items = new List<TodoItem>();

        //    // RestUrl = http://developer.xamarin.com:8081/api/todoitems
        //    var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

        //    try
        //    {
        //        var response = await client.GetAsync(uri);
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
    }
}
