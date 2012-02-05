using System;
using System.Globalization;
using System.Runtime.Serialization;
using HoogmaatheideApp.ViewModels;

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

        public DateTime GDatum { get
        {
            var dates = Geboortedatum.Split('/');
            return new DateTime(int.Parse(dates[2].Split(' ')[0]), int.Parse(dates[1]), int.Parse(dates[0]));
        } }

        public int Weken { get { return (DateTime.Now - GDatum).Days/7; } }

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
