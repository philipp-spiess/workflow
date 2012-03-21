using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistenceLib;

using ProgramLib;
using System.Data.Odbc;
using System.Runtime.Remoting;

namespace PersistenceMgrWithoutDataSet
{
    public class PersistenceMgrWithoutDataSet : IPersistenceMgr
    {
        private OdbcConnection con;
        private String stdPath = @"..\..\..\";

        public PersistenceMgrWithoutDataSet()
        {
            String connector = "Driver={Oracle in XE};dbq=127.0.0.1:1521/XE;Uid=system;Pwd=oracle;";
            Console.WriteLine("[Database] " + connector);
            con = new OdbcConnection(connector);
            con.Open();
        }

        public List<Program> GetProgramme()
        {
            List<Program> programme = new List<Program>();

            String query = @"select pname, path, type, i_typ_name, o_typ_name from programm where i_typ_name is null";

            OdbcCommand command = new OdbcCommand(query, con);
            OdbcDataReader reader = command.ExecuteReader();

            while(reader.Read()) {
                
                ObjectHandle h = Activator.CreateInstanceFrom(
                    stdPath + reader.GetString(1),
                    reader.GetString(2)
                );

                Typ out_typ = null;
                if (!reader.IsDBNull(4))
                    out_typ = new Typ(reader.GetString(4));

                Program p = (Program)h.Unwrap();
                p.Name = reader.GetString(0);
                p.OutputTyp = out_typ;
                p.InputTyp = null;
 
                 programme.Add(p);
          
            }

            return programme;
        }

        public List<ArbeitsAuftrag> GetArbeitsAuftraege()
        {
            List<ArbeitsAuftrag> arbeitsauftraege = new List<ArbeitsAuftrag>();

            String query = "select programm.pname, programm.path, programm.type, programm.i_typ_name, "
                         + "programm.i_typ_name, daten.did, daten.typ_tname, daten.data from AA "
                         + "join programm on (AA.programm_pname = programm.pname) "
                         + "join daten on (daten.did = AA.daten_did);";

            OdbcCommand command = new OdbcCommand(query, con);
            OdbcDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                ObjectHandle h = Activator.CreateInstanceFrom(
                    stdPath + reader.GetString(1),
                    reader.GetString(2)
                );

                Typ in_typ = null;
                if (!reader.IsDBNull(3))
                    in_typ = new Typ(reader.GetString(4));

                Typ out_typ = null;
                if (!reader.IsDBNull(4))
                    out_typ = new Typ(reader.GetString(4));

                Program p = (Program)h.Unwrap();
                p.Name = reader.GetString(0);
                p.OutputTyp = out_typ;
                p.InputTyp = in_typ;

                arbeitsauftraege.Add(
                    new ArbeitsAuftrag(
                        p,
                        new Uebergabedaten(
                            reader.GetInt32(5), 
                            new Typ(reader.GetString(6)),
                            reader.GetString(7)
                        )
                    )
                );
            }

            return arbeitsauftraege;
        }

        public void AddArbeitsAuftrag(ArbeitsAuftrag aa)
        {
            if (aa.Uebergabedaten.ID <= 0)
                aa.Uebergabedaten.ID = this.getDatenID();


            OdbcCommand c;

            c = new OdbcCommand("insert into daten (did, typ_tname, data) values (?, ?, ?)", con);
            
            c.Parameters.Add("did", OdbcType.Int);
            c.Parameters.Add("typ_tname", OdbcType.VarChar);
            c.Parameters.Add("data", OdbcType.VarChar);

            c.Parameters["did"].Value = aa.Uebergabedaten.ID;
            c.Parameters["typ_tname"].Value = aa.Uebergabedaten.Typ.Name;
            c.Parameters["data"].Value = aa.Uebergabedaten.Daten;
            
            c.ExecuteNonQuery();

            c = new OdbcCommand("insert into AA (programm_pname, daten_did) values (?, ?)", con);

            c.Parameters.Add("programm_pname", OdbcType.VarChar);
            c.Parameters.Add("daten_did", OdbcType.Int);

            c.Parameters["programm_pname"].Value = aa.program.Name;
            c.Parameters["daten_did"].Value = aa.Uebergabedaten.ID;

            c.ExecuteNonQuery();
            
        }

        public void RemoveArbeitsAuftrag(ArbeitsAuftrag aa)
        {
            OdbcCommand c;

            c = new OdbcCommand("delete from AA where daten_did = ?", con);

            c.Parameters.Add("daten_did", OdbcType.Int, 4, "daten_did");
            c.Parameters["daten_did"].Value = aa.Uebergabedaten.ID;

            c.ExecuteNonQuery();

            c = new OdbcCommand("delete from daten where did = ?", con);

            c.Parameters.Add("did", OdbcType.Int, 4, "did");
            c.Parameters["did"].Value = aa.Uebergabedaten.ID;

            c.ExecuteNonQuery();

        }

        public List<Program> GetProgramsOfType(Typ t)
        {
            List<Program> programme = new List<Program>();

            String query = @"select pname, path, type, i_typ_name, o_typ_name from programm where i_typ_name = ?";

            OdbcCommand command = new OdbcCommand(query, con);
            command.Parameters.Add("i_typ_name", OdbcType.VarChar, 50, "i_typ_name");
            command.Parameters["i_typ_name"].Value = t.Name;

            OdbcDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                ObjectHandle h = Activator.CreateInstanceFrom(
                    stdPath + reader.GetString(1),
                    reader.GetString(2)
                );

                Typ out_typ = null;
                if (!reader.IsDBNull(4))
                    out_typ = new Typ(reader.GetString(4));

                Typ in_typ = null;
                if (!reader.IsDBNull(3))
                    in_typ = new Typ(reader.GetString(3));

                Program p = (Program)h.Unwrap();
                p.Name = reader.GetString(0);
                p.OutputTyp = out_typ;
                p.InputTyp = in_typ;

                programme.Add(p);

            }

            return programme;
        
        }

        public List<Program> GetProgramsOfUebergabedaten(Uebergabedaten u)
        {
            return this.GetProgramsOfType(u.Typ);
        }

        public int getDatenID()
        {
            String query = @"select daten_seq.nextval from dual";

            OdbcCommand command = new OdbcCommand(query, con);
            OdbcDataReader reader = command.ExecuteReader();

            reader.Read();

            return reader.GetInt32(0);
        }
    }
}
