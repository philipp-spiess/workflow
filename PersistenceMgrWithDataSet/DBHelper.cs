using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;

namespace PersistenceMgrWithDataSet
{
    public class DBHelper
    {
        private OdbcConnection con;

        public DataSet Set { get; set; }

        public DBHelper()
        {
            String connector = "Driver={Oracle in XE};dbq=127.0.0.1:1521/XE;Uid=ORACLE;Pwd=oracle;";
            Console.WriteLine("[Database] " + connector);
            con = new OdbcConnection(connector);
            con.Open();

            Set = new DataSet();

            Set.Tables.Add(GetProgramme());
            Set.Tables.Add(GetTypen());
            Set.Tables.Add(GetAAs());
            Set.Tables.Add(GetDaten());

        }

        private DataTable GetProgramme()
        {
            DataTable tbl = new DataTable("programm");
            String query = @"select * from programm";

            using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, con))
            {
                adapter.Fill(tbl);
            }

            return tbl;
        }

        private DataTable GetTypen()
        {
            DataTable tbl = new DataTable("typ");
            String query = @"select * from typ";

            using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, con))
            {
                adapter.Fill(tbl);
            }

            return tbl;
        }

        private DataTable GetAAs()
        {
            DataTable tbl = new DataTable("AA");
            String query = @"select * from AA";

            using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, con))
            {
                adapter.Fill(tbl);
            }

            return tbl;
        }

        private DataTable GetDaten()
        {
            DataTable tbl = new DataTable("daten");
            String query = @"select * from daten";

            using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, con))
            {
                adapter.Fill(tbl);
            }

            return tbl;
        }
    }
}
