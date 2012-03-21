using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using PersistenceLib;
using ProgramLib;

namespace ControlsLib
{
    public class AktualisierenCtrl
    {
        Thread t;
        bool run = true;
        public IRefreshable Form { get; set; }

        private IPersistenceMgr Persistence = PersistenceCtrl.Instance;

        public void Start()
        {
            t = new Thread(this.RunThread);
            t.Start();
        }

        public void Stop()
        {
            this.run = false;
        }

        private void RunThread()
        {
            while (run)
            {
                Form.RefreshLists(Persistence.GetProgramme(), Persistence.GetArbeitsAuftraege());

                Thread.Sleep(5000);
            }
        }    
    }
}
