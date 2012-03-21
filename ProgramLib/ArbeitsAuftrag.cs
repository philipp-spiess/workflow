using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramLib
{
    public class ArbeitsAuftrag
    {
        public Program program { get; set; }
        public Uebergabedaten Uebergabedaten { get; set; }
        public int ID = -1;

        public ArbeitsAuftrag(int id, Program p, Uebergabedaten u)
        {
            this.ID = id;
            this.program = p;
            this.Uebergabedaten = u;
        }

        public ArbeitsAuftrag(Program p, Uebergabedaten u)
        {
            this.program = p;
            this.Uebergabedaten = u;
        }

        public override string ToString()
        {
            return "AA #" + ID + " mit " + program.ToString();
        }
    }
}
