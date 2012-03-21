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
        // private List<ArbeitsAuftrag> work_orders = new List<ArbeitsAuftrag>();
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
                Typ t = null;

                foreach (Typ typ in typen)
                    if (typ.Name.Equals(row["typ_name"]))
                        t = typ;

                Uebergabedaten u = new Uebergabedaten((int) row["did"], t, (String) row["data"]);
            }

            return uebergabedaten;
        }

        private List<Program> GetAllProgramme()
        {
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
                Program p = null;
                Uebergabedaten u = null;

                foreach(Program pro in programs)
                    if(pro.Name.Equals(row["programm_pname"]))
                        p = pro;

                foreach(Uebergabedaten ueb in uebergabedaten)
                    if(u.ID == (int) row["daten_did"])
                        u = ueb;

                arbeitsauftrage.Add( new ArbeitsAuftrag(p, u) );
            }

            return arbeitsauftrage;
        }

        public void AddArbeitsAuftrag(ArbeitsAuftrag aa) 
        {
            this.work_orders.Add(aa);
        }

        public void RemoveArbeitsAuftrag(ArbeitsAuftrag aa)
        {
            this.work_orders.Remove(aa);
        }
    }
}
