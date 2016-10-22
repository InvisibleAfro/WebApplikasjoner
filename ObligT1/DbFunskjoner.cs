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
        public bool ValiderBruker(KundeModell inn) // kalles etter innloggingsforsøk av Valider i kontroller.
        {
            using (var db = new DataConn())
            {
                try
                {
                    byte[] innHash = returnerHash(inn.Passord);
                    Kunde funnetKunde = db.Kunder.FirstOrDefault(k => k.PassordHash == innHash && k.PersonNr == inn.PersonNr);
                    if(funnetKunde == null)
                    {
                        return false;
                    }
                    else
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
        public string hentKontoNr (string PersonNr){
            using ( var db = new DataConn())
            {
                try
                {
                    IEnumerable<KontoDropDown> kontoRetur = from k in db.Kontoer
                                                            where k.PersonNr.PersonNr == PersonNr
                                                            select new KontoDropDown {
                                                                KontoNr = k.KontoNr
                                                            };
                    var serializer = new JavaScriptSerializer();
                    string returData = serializer.Serialize(kontoRetur);
                    Debug.WriteLine(returData);
                    return returData;
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error.InnerException);
                    return "Feil.";
                }                               
            }
        }
        public string ReturnerTransaskjoner (string kontoNr)
        {
            using (var db = new DataConn())
            {
                try
                {
                    IEnumerable<NyTransaksjon> data = from t in db.Transaksjoner // Trenger ikke lange ny modell.
                                                      where t.KontoFra == kontoNr
                                                      select new NyTransaksjon
                                                      {
                                                          TilKonto = t.KontoTil,
                                                          Belop = t.beløp,
                                                          Forfallsdato = t.Dato
                                                      };
                    var serializer = new JavaScriptSerializer();
                    string returData = serializer.Serialize(data);
                    Debug.WriteLine(returData);
                    return returData;
                }
                catch
                {
                    return "Feil.";
                }
            }
        }
        public string HentKontoOversikt(string PersonNr)
        {
            var serializer = new JavaScriptSerializer();
            using (var db = new DataConn())
            {
                try
                {
                    IEnumerable<KontoOversikt> kontoOversikt = from k in db.Kontoer
                                                               join ku in db.Kunder
                                                               on k.PersonNr.PersonNr equals ku.PersonNr
                                                               select new KontoOversikt
                                                               {
                                                                   Saldo = k.Beløp,
                                                                   KontoNr = k.KontoNr
                                                               };
                    string returKontoOversikt = serializer.Serialize(kontoOversikt);
                    Debug.WriteLine(returKontoOversikt);
                    return returKontoOversikt;
                }
                catch
                {
                    return "Feil";
                }
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