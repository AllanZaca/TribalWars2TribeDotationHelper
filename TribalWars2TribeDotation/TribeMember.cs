using System.Xml.Serialization;

namespace TribalWars2TribeDotation
{
    [XmlRoot("TribeMember", Namespace = "", IsNullable = false)]
    public class TribeMember
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int VillagesNumber { get; set; }
        public int HonorPoints { get; set; }
    }
}
