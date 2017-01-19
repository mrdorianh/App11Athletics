using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App11Athletics.Models
{
    public class Discover11AthleticsModel
    {
        public static ObservableCollection<Trainer> _Trainers;

        static Discover11AthleticsModel()
        {
            _Trainers = new ObservableCollection<Trainer>();

            _Trainers.Add(new Trainer
            {
                Name = "Todd Johnson",
                ShortName = "Todd",
                Detail = "Co-Owner/Trainer",
                Description =
                    "Todd Johnson BS, CPT, USAW has been in the health and fitness industry for over 8 years with a background in exercise physiology and sports management. Todd has experience doing corrective exercise and physical therapy working in a hospital setting, along with being a strength and conditioning coach for multiple sports at the high school level and has worked with athletes at the collegiate level.",
                Email = "mailto:tjohnson@11athletics.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/08/trainers-89-of-119-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/todd-johnson/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Brian Saunders",
                ShortName = "Brian",
                Detail = "Co-Owner/Trainer",
                Description =
                    "Brian Saunders BS, MBA, AT, CPT, USAW has been in the health and wellness industry for over 15 years. He has been employed as a head athletic trainer, strength and conditioning coach, athletic director and fitness director at various facilities. Brian has trained professional and colligate athletes as well as starting and developing 11athletics the magazine. Brian uses his background in athletic training to help clients stay injury free first and develop athleticism second.",
                Email = "mailto:bsaunders@11athletics.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/08/briansface-16-of-27-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/brian-saunders/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Matt Wainscott",
                ShortName = "Matt",
                Detail = "Trainer",
                Description =
                    "Matt Wainscott CPT, ISSA has 10 years of experience training a various range of clients and athletes. Has trained collegiate, high school, and beginner level athletes in multiple sports. In 2006 he set a goal to compete in a national natural bodybuilding contest and won his first title that same year. He ran with that winning attitude and continues to help prepare both male and female athletes for regional and national level fitness shows. He believes with the right mental focus and hard work anybody can become the athlete they’ve envisioned themselves to be.",
                Email = "mailto:mgwainscott@aol.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/08/trainers-22-of-119-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/matt-wainscott/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Stony Burks",
                ShortName = "Stony",
                Detail = "Trainer",
                Description =
                    "Stony Burks BA, ISSA has been in the health and wellness industry for over 8 years.  He has also been employed as a coach in multiple sports such as basketball and football.  Stony believes that with hard work, determination and discipline every goal is achievable.",
                Email = "mailto:westayfit11@gmail.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2016/05/stone-cold-53-of-72-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/stony-burks/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Jason Katz",
                ShortName = "Jason",
                Detail = "Trainer",
                Description =
                    "Jason Katz, USAW has been working in the fitness industry for 5 years.  Katz has experience and expertise working with kids through multiple years at summer camps and enjoys teaching them the fundamentals of fitness.  He trains people at all levels with a focus on weight-loss and Olympic lifts for athletes.",
                Email = "mailto:jaskatz12@yahoo.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/08/trainers-106-of-119-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/jason-katz/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Ben Van Treese",
                ShortName = "Ben",
                Detail = "Trainer",
                Description =
                    "Ben Van Treese BS, CPT, USAW has been coaching and training for over 8 years. He got his start by training water skiers and marine corps officer candidates at Ohio State University. For the past 5 years Ben has worked with varying client populations including athletes, elderly, weight loss and post chemo/radiology individuals to improve mobility and increase fitness levels.",
                Email = "mailto:bvtmobility@gmail.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/08/trainers-46-of-119-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/ben-vantreese/",
                Color = "#005EBF"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Stephanie Gudorf",
                ShortName = "Stephanie",
                Detail = "Co-Owner/Chiropractor",
                Description =
                    "Dr. Gudorf received her doctorate of chiropractic from Life University. She has been practicing chiropractic care for over 17 years. She practices various techniques of spinal manipulation including manual, drop / Thompson technique, activator instrument adjusting and myofascial release. Her scope of practice has included working with various professional and collegiate athletes as well as the recreational athlete.Stephanie also works with children and overall family wellness.",
                Email = "mailto:sgudorf@11athletics.com",
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2014/06/stephanie2-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/stephanie-gudorf/",
                Color = "#029902"
            });
            _Trainers.Add(new Trainer
            {
                Name = "Jen Pierce",
                ShortName = "Jen",
                Detail = "Massage Therapist",
                Description =
                    "Jen Pierce, LMT has been doing Massage Therapy since 2002 and was licensed by the State Medical Board of Ohio in 2007. With a background in Athletic Training and certifications in specialities such as Myofascial Release, she specializes in Therapeutic, Deep Tissue and Sports Massage. Jen will do her best to understand what is causing your pain and to fix the problem. She is compassionate about her trade and her clients. She will always keep the clients best health interests as the main priority.",
                Email = string.Empty,
                ImageUrl =
                    "http://www.11athletics.com/wordpress/wp-content/uploads/2015/07/Jen-38-of-69-1024x683.jpg",
                BioUrl = "http://www.11athletics.com/personnel/jen-pierce/",
                Color = "#029902"
            });
        }

        public static Trainer First()
        {
            var result = _Trainers.ToList<Trainer>();
            return result.First();

        }

        public static List<Trainer> GetAllTrainers()
        {
            var result = _Trainers.ToList<Trainer>();
            return result;
        }

        public static List<Trainer> GetTrainers(int count)
        {
            var result = _Trainers.Take(count).ToList<Trainer>();
            return result;
        }

        public static ObservableCollection<Trainer> Trainers => _Trainers;
    }
}