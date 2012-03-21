using System;
using System.Collections.Generic;
using System.Linq;
using ProgramLib;
using System.Diagnostics;
using System.Windows.Forms;
using ControlsLib;

namespace Program3
{
    public class Programm3 : Program
    {
        public override void Start()
        {
            Debug.WriteLine("Programm #3 startet...");

            this.OpenGUI();
        }

        private void OpenGUI()
        {
            GUI gui = new GUI();
            gui.Uebergabedaten = this.uebergabedaten;
            gui.Programm3 = this;
            gui.Show();
        }

        public void Save(Uebergabedaten u)
        {
            StartCtrl.Save(u);
        }
    }
}
