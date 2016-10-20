using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligT1.Models;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace ObligT1
{
    public class DbFunskjoner
    {
        public bool ValiderBruker(KundeModell inn) // kalles etter innloggingsforsøk.
        {
            using (var db = new DataConn())
            {
                try
                {
                    byte[] innHash = returnerHash(inn.Passord);
                    Kunde funnetKunde = db.Kunder.FirstOrDefault(k => k.PassordHash == innHash && k.PersonNr == inn.PersonNr);
                    if(funnetKunde == null)
                    {
                        Debug.WriteLine("ValiderBruker = Null");
                        return false;
                    }
                    else
                    {
                        Debug.WriteLine("ValiderBruker = true");
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        public string hentKontoNr (string PersonNr){
            using ( var db = new DataConn())
            {
                IEnumerable<KontoModell> kontoData = from k in db.Kontoer
                                                      join ku in db.Kunder
                                                      on k.PersonNr.PersonNr equals ku.PersonNr
                                                      select new KontoModell
                                                      {
                                                            PersonNr = ku.PersonNr.ToString(),
                                                            Beløp = k.Beløp,
                                                            KontoNr = k.KontoNr
                                                       };
                IEnumerable<KontoDropDown> kontoRetur = from k in db.Kontoer
                                                        where k.PersonNr.PersonNr == PersonNr
                                                        select new KontoDropDown { };
                var serializer = new JavaScriptSerializer();
                string returData = serializer.Serialize(kontoRetur);
                return returData;                               
            }
        }
        
        public static bool LagBruker (KundeModell innKunde)
        {
            using (var db = new DataConn())
            {
                try
                {
                    Kunde nyKunde = new Kunde();
                    nyKunde.PersonNr = innKunde.PersonNr;
                    nyKunde.PassordHash = returnerHash(innKunde.Passord);
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
        public static byte [] returnerHash (string innPassord)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            byte[] utData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            return algoritme.ComputeHash(utData);
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