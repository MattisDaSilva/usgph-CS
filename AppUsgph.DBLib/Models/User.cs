using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUsgph.DBLib.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Evenement> Evenement { get; set; } = new List<Evenement>();
        public virtual ICollection<TypeEvenement> TypeEvenement { get; set; } = new List<TypeEvenement>();
    }
}
