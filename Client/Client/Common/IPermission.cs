using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface IPermission
    {
        bool CheckPermission(string permission);

        void RequestPermissions(string[] permissions);
    }
}
