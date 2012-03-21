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


                    ObjectHandle h = Activator.CreateInstanceFrom(
                        @"..\..\..\PersistenceMgrWithDataSet\bin\Debug\PersistenceMgrWithDataSet.dll",
                        @"PersistenceMgrWithDataSet.GetInstance"
                    );
                    IGetInstence instence_getter = (IGetInstence) h.Unwrap();

                    instance = instence_getter.Instence();

                } 
                return instance;
            }
        }
 
    }
}
