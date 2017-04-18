using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App11Athletics.Annotations;
using PropertyChanged;
using SQLite;

namespace App11Athletics.Models
{
    [ImplementPropertyChanged]
    public class TodoItem : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool IsMaxLift { get; set; }
        public string OneRepMax { get; set; } = string.Empty;
        public string LoggedDate { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
        public bool NotSelected => !IsSelected;
        public string Name { get; set; } = string.Empty;
        public string WSets { get; set; } = string.Empty;
        public string WReps { get; set; } = string.Empty;
        public string LiftWeight { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool Done { get; set; }
        public bool NotDone => !Done;
        public string RefPercent { get; set; } = string.Empty;

        public bool HasRef => !string.IsNullOrEmpty(RefPercent);

        public string RefCalculatedPercentage
        {
            get
            {
                if (!string.IsNullOrEmpty(RefPercent) && !string.IsNullOrEmpty(OneRepMax))
                    return GetPercentageOfMax(RefPercent, OneRepMax);
                return string.Empty;
            }
        }

        public string GetPercentageOfMax(string percentage, string max)
        {
            var m = Convert.ToDouble(max);
            var p = Convert.ToDouble(percentage);

            var r = (p / 100) * m;
            return Convert.ToInt32(r).ToString();

        }



        #region Implementation of INotifyPropertyChanged



        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}