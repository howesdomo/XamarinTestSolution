using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Common
{
    public interface I_IR
    {
        void Send(int carrierFrequency, int[] args);

    }
}
