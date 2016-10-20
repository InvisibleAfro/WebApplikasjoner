using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ObligT1.Models;
using System.Diagnostics;

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
        public List<KontoDropDown> hentKontoNr (string PersonNr){
            using ( var db = new DataConn())
            {
                    List<Konto> kontoRetur = db.Kontoer.Select(k => new Konto()
                    {
                        PersonNr = k.PersonNr,
                        Beløp = k.Beløp,
                        KontoNr = k.KontoNr
                    }).ToList();
                    List<KontoDropDown> listOfY = kontoRetur.Cast<KontoDropDown>().ToList();
                    return listOfY;                               
            }
        }
        public List<>
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