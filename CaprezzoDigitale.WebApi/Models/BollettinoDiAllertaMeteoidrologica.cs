using System.Xml.Serialization;

namespace CaprezzoDigitale.WebApi.Models
{
    [XmlRoot("alert", Namespace = "urn:oasis:names:tc:emergency:cap:1.2")]
    public class Alert
    {
        public string identifier { get; set; }
        public string sender { get; set; }
        public DateTime sent { get; set; }
        public string status { get; set; }
        public string msgType { get; set; }
        public string source { get; set; }
        public string scope { get; set; }
        public string note { get; set; }

        [XmlElement(ElementName = "info")]
        public List<Info> info { get; set; }
    }

    public class Info
    {
        public string language { get; set; }
        public string category { get; set; }
        [XmlElement(ElementName = "event")]
        public string eventElement { get; set; }
        public string responseType { get; set; }
        public string urgency { get; set; }
        public string severity { get; set; }
        public string certainty { get; set; }
        public DateTime onset { get; set; }
        public DateTime expires { get; set; }
        public string instruction { get; set; }

        [XmlElement(ElementName = "parameter")]
        public List<Parameter> parameter { get; set; }

        [XmlElement(ElementName = "area")]
        public List<Area> area { get; set; }
    }

    public class Parameter
    {
        public string valueName { get; set; }
        public string value { get; set; }
    }

    public class Area
    {
        public string areaDesc { get; set; }
    }
}
