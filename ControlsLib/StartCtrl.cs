using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramLib;
using System.Windows.Forms;
using PersistenceLib;


namespace ControlsLib
{
    public class StartCtrl
    {
        private static IPersistenceMgr Persistence = PersistenceCtrl.Instance;

        public static void Start(Program p)
        {
            p.Start();
        }

        public static void Weiterfuehren(ArbeitsAuftrag aa)
        {
            Persistence.RemoveArbeitsAuftrag(aa);
            aa.program.uebergabedaten = aa.Uebergabedaten;
            aa.program.Start();
        }

        public static void Save(Uebergabedaten u)
        {
            if (u.Typ == null)
            {
                MessageBox.Show("Das Programm endet hier, das es keinem Typ angehört und somit keine weiteren Programme hat. Die letzten bekannten Daten: " + u.GetDaten<String>());
            }
            else
            {
                SelectProgram gui = new SelectProgram();
                gui.u = u;
                gui.Show();
            }
        }

        public static void CreateAA(Uebergabedaten u, Program p)
        {
            Persistence.AddArbeitsAuftrag(new ArbeitsAuftrag(p, u));
        }

    }
}
