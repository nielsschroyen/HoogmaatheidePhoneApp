using System.Runtime.Serialization;

namespace HoogmaatheideApp.Models
{
    [DataContract]
    public class Nest
    {
        [DataMember]
        public string Naam { get; set; }
        [DataMember]
        public double Prijs { get; set; }
        [DataMember]
        public string Youtube { get; set; }
        [DataMember]
        public string YoutubeDatum { get; set; }
        [DataMember]
        public string Geboortedatum { get; set; }
        [DataMember]
        public string Foto { get; set; }
        [DataMember]
        public string FotoDatum { get; set; }
        [DataMember]
        public int Reuen { get; set; }
        [DataMember]
        public int Teven { get; set; }

    }
}
