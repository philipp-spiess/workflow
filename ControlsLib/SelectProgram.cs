using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramLib;

namespace ControlsLib
{
    public partial class SelectProgram : Form
    {
        public Uebergabedaten u { get; set; }

        public SelectProgram()
        {
            InitializeComponent();
        }

        private void SelectProgram_Load(object sender, EventArgs e)
        {
            foreach (Program p in PersistenceCtrl.Instance.GetProgramsOfUebergabedaten(u))
            {
                listBox1.Items.Add(p);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                StartCtrl.CreateAA(u, (Program) listBox1.SelectedItem);
                this.Close();
            }
        }
    }
}
