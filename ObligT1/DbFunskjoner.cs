using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligT1.Models;

namespace ObligT1
{
    public class DbFunskjoner
    {
        public bool ValiderBruker(KundeModell inn)
        {
            using (var db = new DataConn())
            {
                try
                {
                    string passord = from k in db.Kunder
                                  where k.PersonNr == inn.PersonNr
                                  select k;

                    if(passord == null)
                    {
                        return false;
                    }
                    else if(System.Text.Encoding.ASCII.GetBytes(passord) == inn.PassordHash)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool lagreKunde(KundeModell innKunde)
        {
            using (var db = new DataConn()) 
            {
                try
                {
                    var nyKunde = new Kunde();
                    //nyKunde.Fornavn = innKunde.Fornavn;
                    //nyKunde.Etternavn = innKunde.Etternavn;
                    nyKunde.PersonNr = innKunde.PersonNr;
                    //nyKunde.Passord = innKunde.Passord;
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