using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieSystem.Model
{
    public  class Movie
    {
        public string MovieName { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Introduction { get; set; }
        public string MovieType { get; set; }
        public decimal Price { get; set; }

        public string ImaPath { get; set; }
        public List<MovieTime> ListMovieTime { get; set; } = new List<MovieTime>();

        public Image image 
        {
            get 
            {
                if (File.Exists(ImaPath))
                {
                    return Image.FromFile(ImaPath);
                }
                return null;
            
            }
        }
    }
}
