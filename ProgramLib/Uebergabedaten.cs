using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ProgramLib
{
    public class Uebergabedaten
    {
        public Typ Typ { get; set; }
        public int ID { get; set; }
        private String Daten;

        public Uebergabedaten(int ID, Typ t, String d)
        {
            this.ID = ID;
            Typ = t;
            Daten = d;
        }

        public Uebergabedaten(Typ t)
        {
           Typ = t;
        }

        public T GetDaten<T>() {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(Daten))
            {
                return (T)serializer.Deserialize(reader);
            }  
        }

        public void SetDaten(object d)
        {
            var serializer = new XmlSerializer(d.GetType());

            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, d);

                this.Daten = writer.ToString();
            }
        }
    }
}
