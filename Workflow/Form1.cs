using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlsLib;
using System.Diagnostics;
using ProgramLib;

delegate void RefreshListsDelegate(List<Program> programs, List<ArbeitsAuftrag> work_orders);

namespace Workflow
{
    public partial class Form1 : Form, IRefreshable
    {

        private AktualisierenCtrl aktualisierenCtrl;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            aktualisierenCtrl = new AktualisierenCtrl();
            aktualisierenCtrl.Form = this;
            aktualisierenCtrl.Start();
        }

     
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Debug.WriteLine("le close");
            aktualisierenCtrl.Stop();
        }

        public void RefreshLists(List<Program> programs, List<ArbeitsAuftrag> work_orders)
        {
            RefreshListsDelegate d = RefreshListsDelegate;

            this.Invoke(d, new Object[] { programs, work_orders });
        }

        public void RefreshListsDelegate(List<Program> programs, List<ArbeitsAuftrag> work_orders)
        {

            this.todoList.Items.Clear();
            foreach( ArbeitsAuftrag aa in work_orders ) {
                Console.WriteLine(aa.Uebergabedaten.ID);
                this.todoList.Items.Add(aa);
            }

            this.programmeList.Items.Clear();
            foreach (Program p in programs)
            {
                this.programmeList.Items.Add(p);
            }
        }

        private void startProgram_Click(object sender, EventArgs e)
        {
            Program p = (Program) programmeList.SelectedItem;
            if (p != null)
            {
                StartCtrl.Start(p);
            }
        }

        private void pursueProgram_Click(object sender, EventArgs e)
        {
            ArbeitsAuftrag aa = (ArbeitsAuftrag)todoList.SelectedItem;
            if (aa != null)
            {
                StartCtrl.Weiterfuehren(aa);
            }
        }

    }
}
