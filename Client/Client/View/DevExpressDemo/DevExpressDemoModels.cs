using System;
using System.Collections.Generic;
using System.Text;

namespace Client.View.DevExpressDemo
{
    public class HelloDevExpressOrder
    {

        public bool? IsSelected { get; set; }

        public Xamarin.Forms.ImageSource Photo { get; set; }

        public string OrderNo { get; set; }

        public DateTime CreateTime { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public decimal? LackPrice { get; set; }

        public Sub_HelloDevExpressOrder Description { get; set; }
    }

    public class Sub_HelloDevExpressOrder
    {
        public string Msg { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
