using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.Helpers;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged 
    {
        public UserProfileViewModel()
        {
            UpdateValueCommand =
                new Command<string>(execute: (string parameter) => { UpdateValue(parameter); },
                    canExecute: (string parameter) => { return false; });
            RefreshCanExecutes();
        }
        
        private void UpdateValue(string s) {}

        void RefreshCanExecutes()
        {
            ((Command) UpdateValueCommand).ChangeCanExecute();

        }

        async void CalculateBmrDce()
        {
//            Women: BMR = 655 + (4.35 x weight in pounds) + (4.7 x height in inches) - (4.7 x age in years)
////            Men: BMR = 66 + (6.23 x weight in pounds) + (12.7 x height in inches) - (6.8 x age in years)
//            await Task.Run(() =>
//            {
//                var w = Settings.UserWeight;
//                var hF = Settings.UserHeightFt;
//                var hI = Settings.UserHeightIn;
//                var a = Settings.UserAge;
//                var g = Settings.UserGender;
//                var af = Settings.UserAlf;
//                var h = (hF * 12) + hI;
////                var height = (hF * 12) + (hI * 2.54);
//                double bmr;
//                if (g.ToLower() == "female")
//                {
////                    Female BMR
//                        bmr = 655 + (4.35 * w) + (4.7 * h) - (4.7 * a);
//                   
//                }
//                else if (g.ToLower() == "male")
//                {
////                    Male BMR
//                    bmr =  bmr = 66 + (6.23 * w) + (12.7 * h) - (6.8 * a);
//                }
//                var dce = af * bmr;
//                Settings.UserDce = dce;
//                Settings.UserBmr = bmr;
//            });
        }

        public ICommand UpdateValueCommand { get; }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}