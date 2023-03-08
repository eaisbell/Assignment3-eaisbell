using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Assignment3_V2.Models
{
    public class Actors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        [Url]
        public string? IMBDLink { get; set; }
        public byte[]? ActorPhoto { get; set; }
        [ForeignKey("Movies")]
        public int? MovieId { get; set; }
        public Movies? Movie { get; set; }


    }
}
