using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HoogmaatheideApp.Models
{
    [DataContract]
    public class Ras
    {
        [DataMember]
        public string Naam { get; set; }
        [DataMember]
        public string Foto { get; set; }
        [DataMember]
        public List<Nest> Nesten { get; set; }

        public Ras()
        {
            Nesten = new List<Nest>();
        }
    }
}
