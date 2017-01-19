using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App11Athletics.Helpers;
using App11Athletics.iOS.Services;
using AVFoundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(TextToSpeech_iOS))]
namespace App11Athletics.iOS.Services
{
    public class TextToSpeech_iOS : ITextToSpeech
    {
        float volume = 0.5f;
        float pitch = 1.0f;

        /// <summary>
        /// Speak example from: 
        /// http://blog.xamarin.com/make-your-ios-7-app-speak/
        /// </summary>
        public void Speak(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                var speechSynthesizer = new AVSpeechSynthesizer();
                var speechUtterance = new AVSpeechUtterance(text)
                {
                    Rate = AVSpeechUtterance.MaximumSpeechRate / 3,
                    Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                    Volume = volume,
                    PitchMultiplier = pitch
                };

                speechSynthesizer.SpeakUtterance(speechUtterance);
            }
        }
    }
}