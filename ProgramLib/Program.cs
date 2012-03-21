using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using ProgramLib;


namespace ProgramLib
{
    public class Program : MarshalByRefObject
    {
        public Uebergabedaten uebergabedaten { get; set; }

        public String Name { get; set; }
        public Typ InputTyp { get; set; }
        public Typ OutputTyp { get; set; }

        public virtual void Start()
        {
            Debug.WriteLine("le start");
        }


        public override string ToString()
        {
            return this.Name;
        }
    }
}
