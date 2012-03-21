using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramLib;

namespace Program3
{
    public partial class GUI : Form
    {
        public Uebergabedaten Uebergabedaten { get; set; }
        public Programm3 Programm3 { get; set; }

        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            label1.Text = Uebergabedaten.GetDaten<String>();
        }

        private void GUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Uebergabedaten.SetDaten( GUI.ReverseString(Uebergabedaten.GetDaten<String>()) );

            Programm3.Save(Uebergabedaten);
        }

        public static string ReverseString(string text)
        {
            if (text.Length == 1 || String.IsNullOrEmpty(text))
                return text;
            else
                return ReverseString(text.Substring(1)) + text.Substring(0, 1);
        }
    }
}
