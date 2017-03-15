using System.Collections.ObjectModel;
using App11Athletics.Models;

namespace App11Athletics.ViewModels
{

    class Discover11AthleticsViewModel
    {
        public Discover11AthleticsViewModel()
        {

        }
        public ObservableCollection<Trainer> ListCollectionTrainers => Discover11AthleticsModel._Trainers;
    }
}
