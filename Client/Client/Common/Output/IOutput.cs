using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface IOutput
    {
        void Info(string tag, string msg);

        void Error(string tag, string msg);

        void Warn(string tag, string msg);
    }
}
