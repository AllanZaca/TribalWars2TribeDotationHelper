using System.Xml.Serialization;

namespace TribalWars2TribeDotation
{
    [XmlRoot("TribeMember", Namespace = "", IsNullable = false)]
    public class TribeMember
    {
        public string PlayerName { get; set; }
        public int Dotation { get; set; }
        public PlayerStatistic PlayerStatNew { get; set; }
        public PlayerStatistic PlayerStatOld { get; set; }

        public TribeMember()
        {
            PlayerStatOld = new PlayerStatistic();
            PlayerStatNew = new PlayerStatistic();
        }
    }

}
