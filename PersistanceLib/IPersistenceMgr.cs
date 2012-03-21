using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramLib;

namespace PersistenceLib
{
    public interface IPersistenceMgr
    {
        List<Program> GetProgramme();
        List<ArbeitsAuftrag> GetArbeitsAuftraege();
        void AddArbeitsAuftrag(ArbeitsAuftrag aa);
        void RemoveArbeitsAuftrag(ArbeitsAuftrag aa);
        List<Program> GetProgramsOfType(Typ t);
        List<Program> GetProgramsOfUebergabedaten(Uebergabedaten u);
    }
}
