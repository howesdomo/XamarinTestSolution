using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Client.iOS
{
    public class ScreenDirection : Util.XamariN.IScreenDirection
    {

        public void ForceLandscape()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.LandscapeLeft), new NSString("orientation"));
        }

        public void ForceNosensor()
        {
            // throw new NotImplementedException();
        }

        public void ForcePortrait()
        {
            UIDevice.CurrentDevice.SetValueForKey(new NSNumber((int)UIInterfaceOrientation.Portrait), new NSString("orientation"));
        }

        public void Unspecified()
        {
            // throw new NotImplementedException();
        }
    }
}