using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Client.Effects
{
    /// <summary>
    /// Long pressed effect. Used for invoking commands on long press detection cross platform
    /// </summary>
    public class LongPressedEffect : RoutingEffect
    {
        public LongPressedEffect() : base("Client.Effects.LongPressedEffect")
        {
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(LongPressedEffect), (object)null);

        public static ICommand GetCommand(BindableObject view)
        {
            // return (ICommand)view.GetValue(CommandProperty);
            object o = view.GetValue(CommandProperty);
            if (o is ICommand)
            {
                return o as ICommand;
            }
            else
            {
                return null;
            }
        }

        public static void SetCommand(BindableObject view, ICommand value)
        {
            view.SetValue(CommandProperty, value);
        }


        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(LongPressedEffect), (object)null);

        public static object GetCommandParameter(BindableObject view)
        {
            return view.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(BindableObject view, object value)
        {
            view.SetValue(CommandParameterProperty, value);
        }
    }
}
