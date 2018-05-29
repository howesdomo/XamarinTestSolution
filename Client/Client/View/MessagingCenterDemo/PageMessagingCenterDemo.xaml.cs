using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMessagingCenterDemo : ContentPage
    {
        public PageMessagingCenterDemo()
        {
            InitializeComponent();


            BindingContext = new PageMessagingCenterDemoViewModel();

            // Send messages when buttons are pressed
            var button1 = new Button { Text = "Say Hi" };
            button1.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<PageMessagingCenterDemo>(this, "Hi");
            };

            var button2 = new Button { Text = "Say Hi to John" };
            button2.Clicked += (sender, e) =>
            {
                MessagingCenter.Send<PageMessagingCenterDemo, string>(this, "Hi", "John");
            };

            var button3 = new Button { Text = "Unsubscribe from alert" };
            button3.Clicked += (sender, e) =>
            {
                MessagingCenter.Unsubscribe<PageMessagingCenterDemo, string>(subscriber: this, message: "Hi");
                DisplayAlert("Unsubscribed",
                            "This page has stopped listening, so no more alerts; however the ViewModel is still receiving messages.",
                            "OK");
            };

            // Subscribe to a message (which the ViewModel has also subscribed to) to pop up an Alert
            MessagingCenter.Subscribe<PageMessagingCenterDemo, string>
            (
                subscriber: this,
                message: "Hi",
                callback: (sender, arg) =>
                {
                    DisplayAlert("Message Received", "arg=" + arg, "OK");
                }
             );

            var listView = new ListView();
            listView.SetBinding(ListView.ItemsSourceProperty, "Greetings");

            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children = { button1, button2, button3, listView }
            };
        }
    }

    public class PageMessagingCenterDemoViewModel
    {
        public System.Collections.ObjectModel.ObservableCollection<string> Greetings { get; set; }

        public PageMessagingCenterDemoViewModel()
        {
            Greetings = new ObservableCollection<string>();

            MessagingCenter.Subscribe<PageMessagingCenterDemo>
            (
                subscriber: this,
                message: "Hi",
                callback: (sender) =>
                {
                    Greetings.Add("Hi");
                }
            );

            MessagingCenter.Subscribe<PageMessagingCenterDemo, string>
            (
                this,
                "Hi",
                (sender, arg) =>
                {
                    Greetings.Add("Hi " + arg);
                }
            );
        }
    }
}