using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    public class Attribution
    {
        public int FKIdPersonnel { get; set; }
        public int FKIdMateriel { get; set; }
        public DateTime FKDateAttribution { get; set; }

        public string Commentaire { get; set; }

        public Personnel Personnel { get; set; }
        public Materiel Materiel { get; set; }
    }
}
