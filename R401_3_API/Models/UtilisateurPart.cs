using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R401_3_API.Models
{
    public partial class Utilisateur
    {
        public override string? ToString()
        {
            return "Id: "+Id+" Login:"+Login+" Email:"+Email+" Pwd:"+Pwd+" NbAvis:"+Avis.Count();
        }
    }
}
