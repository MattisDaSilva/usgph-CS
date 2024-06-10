using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUsgph.DBLib.Models
{
    public partial class Evenement
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public int TypeEvenementId { get; set; }
        public virtual TypeEvenement Type { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserIdCreate { get; set; }
        public virtual User UserCreation { get; set; } = null!;
    }
}
