using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;

namespace PersistenceMgrWithDataSet
{
    public class DBHelper
    {
        private OdbcConnection con;

        private OdbcDataAdapter ad_AA = null;
        private OdbcDataAdapter ad_daten = null;
 

        public DataSet Set { get; set; }

        public DBHelper()
        {
            String connector = "Driver={Oracle in XE};dbq=127.0.0.1:1521/XE;Uid=system;Pwd=oracle;";
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

            ad_AA = new OdbcDataAdapter(query, con);

            ad_AA.InsertCommand = new OdbcCommand("insert into AA (programm_pname, daten_did) values (?, ?)", con);
            ad_AA.InsertCommand.Parameters.Add("programm_pname", OdbcType.VarChar, 50, "programm_pname");
            ad_AA.InsertCommand.Parameters.Add("daten_did", OdbcType.Int, 4, "daten_did");

            ad_AA.DeleteCommand = new OdbcCommand("delete from AA where daten_did = ?", con);
            ad_AA.DeleteCommand.Parameters.Add("daten_did", OdbcType.Int, 4, "daten_did");
            
            ad_AA.Fill(tbl);

            return tbl;
        }

        private DataTable GetDaten()
        {
            DataTable tbl = new DataTable("daten");
            String query = @"select * from daten";

            ad_daten = new OdbcDataAdapter(query, con);
            
            ad_daten.InsertCommand = new OdbcCommand("insert into daten (did, typ_tname, data) values (?, ?, ?)", con);
            ad_daten.InsertCommand.Parameters.Add("did", OdbcType.Int, 4, "did");
            ad_daten.InsertCommand.Parameters.Add("typ_tname", OdbcType.VarChar, 50, "typ_tname");
            ad_daten.InsertCommand.Parameters.Add("data", OdbcType.VarChar, 1024, "data");

            ad_daten.DeleteCommand = new OdbcCommand("delete from daten where did = ?", con);
            ad_daten.DeleteCommand.Parameters.Add("did", OdbcType.Int, 4, "did");
            
            ad_daten.Fill(tbl);
            
            return tbl;
        }

        public int getDatenID()
        {
            String query = @"select daten_seq.nextval from dual";

            OdbcCommand command = new OdbcCommand(query, con);
            OdbcDataReader reader = command.ExecuteReader();

            reader.Read();

            return reader.GetInt32(0);
        }

        ~DBHelper()
        {
            try
            {
                ad_AA.Update(Set.Tables["AA"]);
                ad_daten.Update(Set.Tables["daten"]);
            }
            catch (Exception ignore) { }

            Set.AcceptChanges(); 
        }

    }
}
