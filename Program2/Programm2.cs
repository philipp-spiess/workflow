using System;
using System.Collections.Generic;
using System.Linq;
using ProgramLib;
using System.Diagnostics;
using System.Windows.Forms;
using ControlsLib;

namespace Program2
{
    public class Programm2 : Program
    {
        public override void Start()
        {
            Debug.WriteLine("Programm #2 startet...");

            this.OpenGUI(this.uebergabedaten);
        }

        private void OpenGUI(Uebergabedaten u)
        {
            GUI gui = new GUI();
            gui.Uebergabedaten = u;
            gui.Programm2 = this;
            gui.Show();
        }

        public void Save(Uebergabedaten u)
        {
            StartCtrl.Save(u);
        }



    }
}
