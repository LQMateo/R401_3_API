using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401_3_API.Models
{
    public partial class Avi
    {
        public override string? ToString()
        {
            return "Film: " + this.Film + "\nUtilisateur: " + this.Utilisateur + "\nAvis:" + this.Avis+" Note:"+this.Note;
        }
    }
}
