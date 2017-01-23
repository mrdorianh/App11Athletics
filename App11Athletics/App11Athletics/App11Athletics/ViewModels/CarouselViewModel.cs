using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Models;

namespace App11Athletics.ViewModels
{
    public class CarouselViewModel
    {
        public CarouselViewModel()
        {
            ImageCollection.Add(new CarouselModel("icon.png"));
            ImageCollection.Add(new CarouselModel("icon.png"));
            ImageCollection.Add(new CarouselModel("icon.png"));
            ImageCollection.Add(new CarouselModel("icon.png"));
            ImageCollection.Add(new CarouselModel("icon.png"));
        }
        private List<CarouselModel> imageCollection = new List<CarouselModel>();
        public List<CarouselModel> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; }
        }
    }
}
