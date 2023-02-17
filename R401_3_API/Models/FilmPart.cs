using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401_3_API.Models
{
    public partial class Film
    {
        public override string? ToString()
        {
            return "Id:"+Id+" nom:"+Nom+" Description:"+Description+" Categorie"+Categorie+"\nNbAvis:"+Avis.Count();
        }
    }
}
