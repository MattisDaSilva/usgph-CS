using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUsgph.DBLib.Models
{
    public partial class TypeEvenement
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserIdCreate { get; set; }
        public virtual User UserCreation { get; set; } = null!;
        public virtual ICollection<Evenement> Evenement { get; set; } = new List<Evenement>();
    }
}
