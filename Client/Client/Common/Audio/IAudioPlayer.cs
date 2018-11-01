using System;

namespace Client.Common
{
    public interface IAudioPlayer
    {
        void PlayAssetsFile(string fileName);

        void PlayBeep();

        void PlayError();
    }
}
