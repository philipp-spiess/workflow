using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistenceLib;

namespace PersistenceMgrWithoutDataSet
{
    public class GetInstance : IGetInstence
    {
        private static IPersistenceMgr instance;

        public IPersistenceMgr Instence()
        {
            if (instance == null) instance = new PersistenceMgrWithoutDataSet();
            return (IPersistenceMgr)instance;
        }
    }
}
