using Android.Views;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Client.Components.EntryAdv), typeof(Client.Droid.Component.EntryAdvRenderer))]
namespace Client.Droid.Component
{
    public class EditorAdvRenderer : Xamarin.Forms.Platform.Android.EditorRenderer
    {
        public EditorAdvRenderer(Android.Content.Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                this.Control.InputType = Android.Text.InputTypes.ClassText;
                this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.Done;
                this.Control.KeyPress += Control_KeyPress;
            }
        }

        private void Control_KeyPress(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            if (e.Event.Action == KeyEventActions.Up)
            {
                if (e.KeyCode == Keycode.Search || e.KeyCode == Keycode.Enter)
                {
                    e.Handled = true;
                }
            }
        }
    }
}