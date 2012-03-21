using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramLib;

namespace Program2
{
    public partial class GUI : Form
    {
        public Uebergabedaten Uebergabedaten { get; set; }
        public Programm2 Programm2 { get; set; }

        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            label1.Text = Uebergabedaten.GetDaten<String>();
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Programm2.Save(Uebergabedaten);
        }
    }
}
