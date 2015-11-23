using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TribalWars2TribeDotation
{
    [XmlRoot("PlayerStatistic", Namespace = "", IsNullable = false)]
    public class PlayerStatistic
    {
        public string PlayerName { get; set; }
        public int Points { get; set; }
        public int VillagesNumber { get; set; }
        public int HonorPoints { get; set; }
    }
}
