using MovieSystem.Common;
using MovieSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MovieSystem.Service
{
    public  class MovieService
    {
        public List<Movie> movies { get; set; } = new List<Movie>();
        public MovieService()
        { 
            var doc = new XmlDocument();
            doc.Load(@"..\..\Data\Movies.xml");
            var nodemovies= doc.SelectNodes("Movies/Movie");
            
            foreach (XmlNode movien in nodemovies)
            {
                Movie movie = new Movie()
                {
                    MovieName = movien.Attributes["MovieName"].Value,
                    Director = movien.Attributes["Director"].Value,
                    Actors = movien.Attributes["Actors"].Value,
                    Introduction = movien.Attributes["Introduction"].Value,
                    MovieType = movien.Attributes["MovieType"].Value,
                    Price = decimal.Parse(movien.Attributes["Price"].Value),
                    ImaPath = movien.Attributes["ImaPath"].Value 
                };
                foreach (XmlNode ticketn in movien)
                {
                    MovieTime movieTime = new MovieTime()
                    {
                        MovieTimes = ticketn.Attributes["time"].Value,
                        Tickets = GetTickets(7,5)
                    };
                    movie.ListMovieTime.Add(movieTime);
                } 
                movies.Add(movie); 
            } 
        }

        private List<Ticket> GetTickets(int col,int row)
        {
            List<Ticket> tickets = new List<Ticket>(); 
            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    tickets.Add(new Ticket()
                    {
                        TicketNo = $"{i+1}排{j+1}座"
                    }); ;
                }
            }
            return tickets;
        }

        //获取场次
        public List<Ticket> GetMoviesTime(string moviename,string time)
        {
            var movie = movies.First(t => t.MovieName == moviename);
            return movie.ListMovieTime.First(f => f.MovieTimes == time).Tickets;

        }
        public decimal BuyTickets(string moviename,string movietime,List<Ticket> tickets,TicketType ticketType=TicketType.全票,double discount=1d) 
        {
            var movie = movies.First(t => t.MovieName == moviename);
            var price = movie.Price;
            decimal sumparice = 0;
            foreach (var item in tickets)
            {
                if (item.IsBuy)
                {
                    throw new Exception("该票已售出");
                }
                item.IsBuy = true;
                switch (ticketType)
                {

                    case TicketType.全票:
                        sumparice += price;
                        break;
                    case TicketType.学生票:
                    case TicketType.儿童票:
                        sumparice += price/2;  
                        break;
                    case TicketType.打折票:
                        sumparice += price * (decimal)discount;
                        break;
                    case TicketType.赠票:
                        break;
                    default:
                        break;
                }

            }

            return sumparice;

        }


    }
}
