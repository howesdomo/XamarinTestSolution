using Client.Common;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Client.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSQLiteDemo_NoteList : ContentPage
    {
        public PageSQLiteDemo_NoteList()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //// Reset the 'resume' id, since we just want to re-start here
            //((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await StaticInfo.InnerSQLiteDB.GetItemsAsync();
            
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSQLiteDemo_NoteItem
            {
                BindingContext = new NoteItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new PageSQLiteDemo_NoteItem
                {
                    BindingContext = e.SelectedItem as NoteItem
                });
            }
        }
    }
}