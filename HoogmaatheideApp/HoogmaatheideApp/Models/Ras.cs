using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text.RegularExpressions;
using HoogmaatheideApp.ViewModels;

namespace HoogmaatheideApp.Models
{
    [DataContract]
    public class Ras : NotifyPropertyChangedBase
    {
        private string _naam;

        [DataMember]
        public string Naam
        {
            get { return _naam; }
            set { _naam = value;
                NotifyPropertyChanged(() => Naam);
            }
        }

        private string _foto;

        [DataMember]
        public string Foto
        {
            get { return _foto; }
            set { _foto = value;
            NotifyPropertyChanged(() => Foto);
            }
        }

        private string _beschrijving;
        [DataMember]
        public string Beschrijving
        {
            get { return FormatBeschrijving(_beschrijving); }
            set
            {
                _beschrijving = value;
                NotifyPropertyChanged(() => Beschrijving);
            }
        }

        private string FormatBeschrijving(string beschrijving)
        {
            var i = beschrijving.ToLower().IndexOf("<h2>");
            if(i < 10)
            {
                i = beschrijving.ToLower().IndexOf("<h3>");
            }
            if(i<10)
            {
                return Regex.Replace(beschrijving, @"<[^>]+>", "");

            }

            return Regex.Replace(beschrijving.Substring(0,i), @"<[^>]+>", "");

          
        }


        private List<Nest> _nesten;

        [DataMember]
        public List<Nest> Nesten
        {
            get { return _nesten; }
            set { _nesten = value;
                AantalReuen = Nesten.Select(n => n.Reuen).Sum();
                AantalTeven = Nesten.Select(n => n.Teven).Sum();
                AantalNesten = Nesten.Count;
                NotifyPropertyChanged(() => Nesten);
            }
        }

        public int AantalNesten
        {
            get { return _aantalNesten; }
            set
            {
                _aantalNesten = value;
                NotifyPropertyChanged(() => AantalNesten);
          
            }
        }


        public Ras()
        {
            Nesten = new List<Nest>();
        }

        private int _aantalReuen;
        public int AantalReuen
        {
            get { return _aantalReuen; }
            set { _aantalReuen = value; NotifyPropertyChanged(() => AantalReuen); }
        }

        private int _aantalTeven;
        private int _aantalNesten;

        public int AantalTeven
        {
            get { return _aantalTeven; }
            set { _aantalTeven = value; NotifyPropertyChanged(() => AantalTeven); }
        }

        public Uri Link { get { return new Uri(@"http://kennel.hoogmaatheide.be/beschikbare-pups/"+ Naam.Replace(' ' ,'-')  + @"/"); } }
    }
}
