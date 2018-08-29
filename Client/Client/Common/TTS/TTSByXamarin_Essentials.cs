using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace Client.Common
{
    // TOOD 全部方法测试过, 都不能正常播放TTS, 可能仍需要等 Xamarin.Essentials 正式版 ????
    public class TTSByXamarin_Essentials
    {

        // ***************************************************** Step 1

        public async Task SpeakNowDefaultSettings()
        {
            await TextToSpeech.SpeakAsync("Hello World");
        }

        public void SpeakNowDefaultSettings2()
        {
            TextToSpeech.SpeakAsync("Hello World").ContinueWith((t) =>
            {
                // Logic that will run after utterance finishes.

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }


        // ***************************************************** Step 2

        CancellationTokenSource cts;
        public async Task SpeakNowDefaultSettings3()
        {
            cts = new CancellationTokenSource();
            await TextToSpeech.SpeakAsync("Hello World", cancelToken: cts.Token);

            // This method will block until utterance finishes.
        }

        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? false)
                return;

            cts.Cancel();
        }

        // ***************************************************** Step 3


        bool isBusy = false;
        public void SpeakMultiple()
        {
            isBusy = true;
            Task.Run(async () =>
            {
                await TextToSpeech.SpeakAsync("Hello World 1");
                await TextToSpeech.SpeakAsync("Hello World 2");
                await TextToSpeech.SpeakAsync("Hello World 3");
                isBusy = false;
            });

            if (isBusy == false)
            {

            }

            // or you can query multiple without a Task:
            Task.WhenAll(
                TextToSpeech.SpeakAsync("Hello World 1"),
                TextToSpeech.SpeakAsync("Hello World 2"),
                TextToSpeech.SpeakAsync("Hello World 3"))
                .ContinueWith((t) => { isBusy = false; }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // ***************************************************** Step 4
        public async Task SpeakNow()
        {
            var settings = new SpeakSettings()
            {
                Volume = 0.75f,
                Pitch = 1.0f
            };

            await TextToSpeech.SpeakAsync("Hello World", settings);
        }

        // ***************************************************** Step 5

        public async Task SpeakNow2()
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            Locale locale = null;
            if (locales != null && locales.GetEnumerator().MoveNext())
            {
                locale = locales.GetEnumerator().Current;
            }

            var settings = new SpeakSettings()
            {
                Volume = 0.75f,
                Pitch = 1.0f,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync("Hello World", settings);

        }
    }
}
