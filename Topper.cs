using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FinalPro2
{
    public class Topper
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("percentile")]
        public string Percentile { get; set; }

        [XmlElement("year")]
        public string Year { get; set; }

        [XmlElement("quote")]
        public string Quote { get; set; }

        [XmlElement("say")]
        public string Say { get; set; }

        [XmlElement("photo")]
        public string Photo { get; set; }
    }
}
