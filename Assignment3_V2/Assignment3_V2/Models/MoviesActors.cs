using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3_V2.Models
{
    public class MoviesActors
    {
        public int MoviesActorsId { get; set; }
        [ForeignKey("Movies")]
        public int? MovieID { get; set; }
        public Movies? Movie { get; set; }
        [ForeignKey("Actors")]
        public int? ActorID { get; set; }
        public Actors? Actor { get; set; }


    }
}
