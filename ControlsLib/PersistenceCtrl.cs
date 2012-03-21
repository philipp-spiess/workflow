using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistenceLib;
using System.Runtime.Remoting;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace ControlsLib
{
    public static class PersistenceCtrl
    {
        private static IPersistenceMgr instance;

        public static IPersistenceMgr Instance {
            get
            {
                if (instance == null)
                {
                    /**
                     * Reflection
                     **/

                    AppSettingsReader config = new AppSettingsReader();

                    String persistence_mgr = (String)config.GetValue("PersistenceMgr", typeof(String));

                    ObjectHandle h = Activator.CreateInstanceFrom(
                        @"..\..\..\" + persistence_mgr + @"\bin\Debug\" + persistence_mgr + @".dll",
                        persistence_mgr + @".GetInstance"
                    );
                    IGetInstence instence_getter = (IGetInstence) h.Unwrap();

                    instance = instence_getter.Instence();

                } 
                return instance;
            }
        }
 
    }
}
