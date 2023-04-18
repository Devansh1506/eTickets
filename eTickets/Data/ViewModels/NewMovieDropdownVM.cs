using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownVM
    {
        public NewMovieDropdownVM()
        {
            Producers = new List<Producer>();
            Actors = new List<Actor>();
            Cinemas = new List<Cinema>();
        }

        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
    }
}
