using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligT1.Models;

namespace ObligT1
{
    public class DbFunskjoner
    {
        public bool ValiderBruker()
        {
            return false;
        }
        public bool lagreKunde(KundeModell innKunde)
        {
            using (var db = new DataConn()) 
            {
                try
                {
                    var nyKunde = new Kunde();
                    nyKunde.Fornavn = innKunde.Fornavn;
                    nyKunde.Etternavn = innKunde.Etternavn;
                    nyKunde.PersonNr = innKunde.PersonNr;
                    nyKunde.Passord = innKunde.Passord;
                    db.Kunder.Add(nyKunde);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}