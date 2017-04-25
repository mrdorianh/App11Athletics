using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App11Athletics.DHCToolkit;
using App11Athletics.Models;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;


namespace App11Athletics.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ImplementPropertyChanged]
    public partial class WorkoutLogCalendar : ContentPage
    {
        public WorkoutLogCalendar()
        {
            InitializeComponent();
            var inactiveColor = Color.Gray;
            var color = (Color)Application.Current.Resources["ColorBrandGlobalBlue"];
            calendarView.MinDate = CalendarView.FirstDayOfMonth(new DateTime(2017, 1, 6));
            calendarView.MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(12));
            calendarView.SelectedDateForegroundColor = Color.White;
            calendarView.SelectedDateBackgroundColor = color;
            calendarView.HighlightedDateBackgroundColor = color;
            calendarView.HighlightedDateForegroundColor = Color.White;
            calendarView.ShouldHighlightDaysOfWeekLabels = false;
            calendarView.SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill;
            calendarView.TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline;
            calendarView.TodayDateBackgroundColor = color.MultiplyAlpha(0.5);
            //            calendarView.HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
            calendarView.ShowNavigationArrows = true;
            //            calendarView.TodayDateBackgroundColor = color.MultiplyAlpha(0.5);
            calendarView.MonthTitleForegroundColor = color;
            calendarView.MonthTitleBackgroundColor = Color.White;
            calendarView.TodayDateForegroundColor = Color.White;
            calendarView.DateBackgroundColor = Color.White;
            calendarView.DateForegroundColor = color;
            calendarView.DayOfWeekLabelForegroundColor = Color.White;
            calendarView.DayOfWeekLabelBackgroundColor = color;
            calendarView.InactiveDateBackgroundColor = inactiveColor.MultiplyAlpha(0.3);
            calendarView.InactiveDateForegroundColor = inactiveColor.MultiplyAlpha(0.6);
            var today = DateTime.Today;
            calendarView.SelectedDate = today;
            selectedDateTime = today;
            calendarView.NotifyDateSelected(DateTime.Today);
            //            CalendarViewOnDateSelected(null, today);

        }

        private DateTime selectedDateTime;
        private string selectedDateString => App.LogDate(selectedDateTime);

        private async void CalendarViewOnDateSelected(object sender, DateTime dateTime)
        {
            selectedDateTime = dateTime;
            buttonGoToDate.Text = $"Go to {selectedDateString}";
            listView.BeginRefresh();
            await GetLogs();
            labelPreview.IsVisible = listView.ItemsSource.Cast<object>().Any();

            listView.EndRefresh();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Opacity = 0;

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            CalendarViewOnDateSelected(null, selectedDateTime);
            await Task.WhenAll(AnimatePages.AnimatePageIn(gridLayout), this.FadeTo(1));

        }

        private async Task GetLogs() { listView.ItemsSource = await App.Database.GetFilteredItemsAsync(selectedDateString); }

        private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //            await Navigation.PushAsync(new WorkoutLogListView(selectedDateTime));
            ((ListView)sender).SelectedItem = null;

            //            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            ////            await Navigation.PushAsync(new WorkoutLogOptionsView(se) { BindingContext = e.SelectedItem as TodoItem });
            //            listView.ItemsSource = null;

        }

        private async void ButtonGoToDate_OnClicked(object sender, EventArgs e) { await Navigation.PushAsync(new WorkoutLogListView(selectedDateTime)); }

        private async void OneRepMaxGo(object sender, EventArgs e)
        {
            await Task.Delay(50);
            await Navigation.PushAsync(new OneRepMaxList());
        }

        async void GoToTodayCalendar(object sender, EventArgs e)
        {
            await calendarView.FadeTo(0, 250U, Easing.SinIn);
            calendarView.SelectedDate = DateTime.Today;
            calendarView.DisplayedMonth = DateTime.Today;
            calendarView.NotifyDateSelected(DateTime.Today);
            calendarView.NotifyDisplayedMonthChanged(DateTime.Today);
            await Task.Delay(50);
            await calendarView.FadeTo(1, 250U, Easing.CubicIn);



        }
    }


    class WorkoutLogCalendarViewModel : INotifyPropertyChanged
    {

        public WorkoutLogCalendarViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";

        public string CountDisplay
        {
            get { return countDisplay; }
            set
            {
                countDisplay = value;
                OnPropertyChanged();
            }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() => CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }

}