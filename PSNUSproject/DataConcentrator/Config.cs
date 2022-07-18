using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DataConcentrator
{
    public class Config
    {
        public static void CreateConfig()
        {
            XmlSerializer xs = new XmlSerializer(typeof(DigitalInput));
            using (StreamWriter streamWriter = System.IO.File.CreateText("digital_inputs"))
            {
                xs.Serialize(streamWriter, IOContext.Instance.DigitalInputs.ToList());
            }

            xs = new XmlSerializer(typeof(DigitalOutput));
            using (StreamWriter streamWriter = System.IO.File.CreateText("digital_outputs"))
            {
                xs.Serialize(streamWriter, IOContext.Instance.DigitalOutputs.ToList());
            }

            xs = new XmlSerializer(typeof(AnalogInput));
            using (StreamWriter streamWriter = System.IO.File.CreateText("analog_inputs"))
            {
                xs.Serialize(streamWriter, IOContext.Instance.AnalogInputs.ToList());
            }

            xs = new XmlSerializer(typeof(AnalogOutput));
            using (StreamWriter streamWriter = System.IO.File.CreateText("analog_outputs"))
            {
                xs.Serialize(streamWriter, IOContext.Instance.AnalogOutputs.ToList());
            }
        }

        public static void ReadConfig(string filename)
        {
            
            XmlSerializer xs = new XmlSerializer(typeof(DigitalInput));
            xs.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            xs.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            using (FileStream fs = new FileStream("digital_inputs.xml", FileMode.Open))
            {
                List<DigitalInput> DIs = (List<DigitalInput>)xs.Deserialize(fs);
                foreach (DigitalInput DI in DIs)
                {
                    IOContext.Instance.DigitalInputs.Add(DI);
                }
            }

            xs = new XmlSerializer(typeof(DigitalOutput));
            using (FileStream fs = new FileStream("digital_outputs.xml", FileMode.Open))
            {
                List<DigitalOutput> DOs = (List<DigitalOutput>)xs.Deserialize(fs);
                foreach (DigitalOutput DO in DOs)
                {
                    IOContext.Instance.DigitalOutputs.Add(DO);
                }
            }

            xs = new XmlSerializer(typeof(AnalogInput));
            using (FileStream fs = new FileStream("analog_inputs.xml", FileMode.Open))
            {
                List<AnalogInput> AIs = (List<AnalogInput>)xs.Deserialize(fs);
                foreach (AnalogInput AI in AIs)
                {
                    IOContext.Instance.AnalogInputs.Add(AI);
                }
            }

            xs = new XmlSerializer(typeof(AnalogOutput));
            using (FileStream fs = new FileStream("analog_outputs.xml", FileMode.Open))
            {
                List<AnalogOutput> AOs = (List<AnalogOutput>)xs.Deserialize(fs);
                foreach (AnalogOutput AO in AOs)
                {
                    IOContext.Instance.AnalogOutputs.Add(AO);
                }
            }

            IOContext.Instance.SaveChanges();
        }

        private static void serializer_UnknownNode (object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
        }

        private static void serializer_UnknownAttribute (object sender, XmlAttributeEventArgs e)
        {
            XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " + attr.Name + "='" + attr.Value + "'");
        }
    }
}
