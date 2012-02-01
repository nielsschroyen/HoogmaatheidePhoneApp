using System.Collections.Generic;
using HoogmaatheideApp.Models;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace HoogmaatheideApp.Helpers
{
    public class JsonSerialiser
    {
        public static List<Ras> DeSerialise(string json)
        {

            var o = JObject.Parse(json);

            var rassen = (JArray)o["Rassen"];
            var nesten = (JArray)o["Nesten"];

            var rassenDic = new Dictionary<string, Ras>();
            foreach (var ras in rassen)
            {
               var r =  ras.ToObject<Ras>();
               rassenDic.Add(r.Naam, r);

            }

            foreach (var nest in nesten)
            {
                var n = nest.ToObject<Nest>();
                rassenDic[n.Naam].Nesten.Add(n);

            }

            return rassenDic.Values.OrderBy(ras => ras.Naam).ToList();
        }

       

    }
}
