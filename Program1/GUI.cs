using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramLib;

namespace Program1
{
    public partial class GUI : Form
    {

        public Programm1 p { get; set; }
        public Uebergabedaten u { get; set; }

        public GUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            u.SetDaten(textBox1.Text);
        }

        private void GUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            p.Save(u);
        }

    }
}
