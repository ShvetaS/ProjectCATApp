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
using System.Xml.Serialization;
using System.Xml.Linq;

namespace FinalPro2
{
    public class que
    {
        [XmlElement("quedate")]
        public string Quedate
        {
            get;
            set;
        }
        [XmlElement("quename")]
        public string Quename
        {
            get;
            set;
        }

        [XmlElement("instruction")]
        public string Instruction
        {
            get;
            set;
        }
        [XmlElement("quetext")]
        public string Quetext
        {
            get;
            set;
        }
        [XmlElement("hint")]
        public string Hint
        {
            get;
            set;
        }
        [XmlElement("solution")]
        public string Solution
        {
            get;
            set;
        }
    
    }
}
