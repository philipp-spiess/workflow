using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistenceLib;
using ProgramLib;
using System.Runtime.Remoting;
using System.Data;

namespace PersistenceMgrWithDataSet
{
    public class PersistenceMgrWithDataSet : IPersistenceMgr
    {
        private DBHelper DB = new DBHelper();
        private String stdPath = @"..\..\..\";

        public List<Typ> GetTyps()
        {
            List<Typ> typen = new List<Typ>();

            foreach (DataRow row in DB.Set.Tables["typ"].Rows)
            {
                typen.Add(new Typ( (String)row["tname"] ) );
            }

            return typen;

        }

        public List<Program> GetProgramme()
        {
            List<Program> all = this.GetAllProgramme();
            List<Program> progs = new List<Program>();
            foreach (Program p in all)
                if (p.InputTyp == null)
                    progs.Add(p);
            return progs;
        }

        public List<Program> GetProgramsOfType(Typ t)
        {
            Console.WriteLine("GetProgramsOfType(Typ t)");

            List<Program> all = this.GetAllProgramme();
            List<Program> progs = new List<Program>();
            foreach (Program p in all)
                if (p.InputTyp != null && p.InputTyp.Name.Equals(t.Name))
                    progs.Add(p);
            return progs;
        }

        public List<Program> GetProgramsOfUebergabedaten(Uebergabedaten u)
        {
            return this.GetProgramsOfType(u.Typ);
        }

        private List<Uebergabedaten> GetAllUebergabedaten()
        {
            List<Uebergabedaten> uebergabedaten = new List<Uebergabedaten>();
            List<Typ> typen = this.GetTyps();

            foreach (DataRow row in DB.Set.Tables["daten"].Rows)
            {
                try
                {

                    Typ t = null;
                    foreach (Typ typ in typen)
                        if (typ.Name.Equals(row["typ_tname"]))
                            t = typ;

                    int did = int.Parse(row["did"].ToString());
                    String data = (String)row["data"];

                    Uebergabedaten u = new Uebergabedaten(did, t, data);

                    uebergabedaten.Add(u);
                }
                catch (DeletedRowInaccessibleException ignore) { }
            }

            return uebergabedaten;
        }

        private List<Program> GetAllProgramme()
        {
            Console.WriteLine("get all the programs");

            List<Typ> typen = this.GetTyps();
            List<Program> programme = new List<Program>();
            foreach (DataRow row in DB.Set.Tables["programm"].Rows)
            {
                ObjectHandle h = Activator.CreateInstanceFrom(
                    stdPath + row["path"],
                    (String)row["type"]
                );
                    
                Typ program_typ_i = null;
                Typ program_typ_o = null;

                if(!row["i_typ_name"].GetType().Equals(typeof(System.DBNull)))
                    foreach (Typ t in typen)
                        if (t.Name.Equals((String)row["i_typ_name"]))
                            program_typ_i = t;
                
                if(!row["o_typ_name"].GetType().Equals(typeof(System.DBNull)))
                    foreach (Typ t in typen)
                        if (t.Name.Equals((String)row["o_typ_name"]))
                            program_typ_o = t;
                            

                 Program p = (Program)h.Unwrap();
                 p.Name = (String)row["pname"];
                 p.OutputTyp = program_typ_o;
                 p.InputTyp = program_typ_i;
 
                 programme.Add(p);
            }

            return programme;
        }

        public List<ArbeitsAuftrag> GetArbeitsAuftraege()
        {
            List<ArbeitsAuftrag> arbeitsauftrage = new List<ArbeitsAuftrag>();
            List<Program> programs = GetAllProgramme();
            List<Uebergabedaten> uebergabedaten = GetAllUebergabedaten();

            foreach (DataRow row in DB.Set.Tables["AA"].Rows)
            {
                try
                {

                    Program p = null;
                    Uebergabedaten u = null;

                    foreach (Program pro in programs)
                        if (pro.Name.Equals(row["programm_pname"]))
                            p = pro;

                    foreach (Uebergabedaten ueb in uebergabedaten)
                        if (ueb.ID == int.Parse(row["daten_did"].ToString()))
                            u = ueb;

                    arbeitsauftrage.Add(new ArbeitsAuftrag(p, u));
                }
                catch (DeletedRowInaccessibleException ignore) { }
            }

            return arbeitsauftrage;
        }

        public void AddArbeitsAuftrag(ArbeitsAuftrag aa) 
        {
            if( aa.Uebergabedaten.ID <= 0 )
                aa.Uebergabedaten.ID = DB.getDatenID();

            DataRow daten_row = DB.Set.Tables["daten"].NewRow();
            DataRow aa_row = DB.Set.Tables["AA"].NewRow();

            daten_row["did"] = aa.Uebergabedaten.ID;
            daten_row["typ_tname"] = aa.Uebergabedaten.Typ.Name;
            daten_row["data"] = aa.Uebergabedaten.Daten;

            aa_row["programm_pname"] = aa.program.Name;
            aa_row["daten_did"] = aa.Uebergabedaten.ID;

            DB.Set.Tables["daten"].Rows.Add(daten_row);
            DB.Set.Tables["AA"].Rows.Add(aa_row);
        }

        public void RemoveArbeitsAuftrag(ArbeitsAuftrag aa)
        {
            int id = aa.Uebergabedaten.ID;

            DataRow deleteMe = null;

            foreach (DataRow row in DB.Set.Tables["AA"].Rows)
                if (int.Parse(row["daten_did"].ToString()) == id)
                    deleteMe = row;

            deleteMe.Delete();

            foreach (DataRow row in DB.Set.Tables["daten"].Rows)
                if (int.Parse(row["did"].ToString()) == id)
                    deleteMe = row;

            deleteMe.Delete();

        }
    }
}
