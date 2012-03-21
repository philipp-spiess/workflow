using System;
using System.Collections.Generic;
using System.Linq;
using ProgramLib;
using System.Diagnostics;
using System.Windows.Forms;
using ProgramLib;
using ControlsLib;

namespace Program1
{
    public class Programm1 : Program
    {
        public override void Start()
        {

            this.OpenGUI();

            Debug.WriteLine("Programm #1 endet?...");
        }

        private void OpenGUI()
        {
            GUI gui = new GUI();
            gui.p = this;
            gui.u = new Uebergabedaten(this.OutputTyp);
            gui.Show();
        }

        public void Save(Uebergabedaten u)
        {
            StartCtrl.Save(u);
        }

    }
}
