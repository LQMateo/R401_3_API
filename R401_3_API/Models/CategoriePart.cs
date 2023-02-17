using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401_3_API.Models
{
    public partial class Categorie
    {
        public override string? ToString()
        {
            return "ID:" + this.Id + " Nom:" + this.Nom+" Description:"+Description+" NbFilms"+Films.Count() ;
        }
    }
}
