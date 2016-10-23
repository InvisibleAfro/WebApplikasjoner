using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ObligT1.Models;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace ObligT1.Controllers
{
    public class KundesController : Controller
    {
        public ActionResult LagBruker()
        {
            return View();
        }
        public ActionResult RegistrerKommendeUtbetaling(KommendeUtbetaling innData)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("IndexBruker");
            }
            return RedirectToAction("IndexBruker");
        }
        public string KontoOversikt()
        {
                DbFunskjoner df = new DbFunskjoner();
                return df.HentKontoOversikt((string)Session["PersonNr"]);
        }
        [HttpPost]
        public ActionResult LagBruker(KundeModell innKunde)
        {
            if (DbFunskjoner.LagBruker(innKunde))
            {
                return RedirectToAction("LoggInn");
            }
            else
            {
                return View();
            }
        }
        public string HentKontoNr()
        {
                DbFunskjoner df = new DbFunskjoner();
                string  kontoNr = df.hentKontoNr((string)Session["PersonNr"]);
                Debug.WriteLine(kontoNr);
                return kontoNr; // funksjonsfilen lager json streng, bare å sende videre.
        }
        public ActionResult KommendeUtbetalinger (string PersonNr)
        {
                DbFunskjoner df = new DbFunskjoner();
                return View(df.HentKommendeUtbetalinger(PersonNr));
        }
        public string HentTransaksjoner (string KontoNr)
        {
            DbFunskjoner df = new DbFunskjoner();
            return df.ReturnerTransaskjoner(KontoNr);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ValiderNyTransaksjon(KommendeUtbetaling innTransaksjon)
        {
            if (TryValidateModel(innTransaksjon)) // Hvorfor fungerer ingen av disse?? :(
            {
                return RedirectToAction("IndexBruker");

            }
            if (ModelState.IsValid) // Hvorfor fungerer ingen av disse?? :(
            {
                return RedirectToAction("IndexBruker");
            }
            return RedirectToAction("IndexBruker");
        }
        public ActionResult DetteFunker()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Valider(KundeModell innKunde)
        {
            if (ModelState.IsValid)
            {
                DbFunskjoner df = new DbFunskjoner();
                if (!df.ValiderBruker(innKunde))
                {
                    Session["Forsøk"] = true; // brukes i LoggInn for å vise "Feil brukernavn eller passord.
                    return RedirectToAction("LoggInn");
                }
                else
                {
                    Session["RiktigPassord"] = true;
                    Session["PersonNr"] = innKunde.PersonNr;
                    return RedirectToAction("EnGangsKode");
                }              
            }
            else
            {
                return RedirectToAction("LoggInn");
            }
        }
        public ActionResult EnGangsKode()
        {   if (Session["RiktigPassord"] != null && (bool)Session["RiktigPassord"] == true)
            {
                return View();
            }
            return RedirectToAction("LoggInn");
        }
        [HttpPost]
        public ActionResult EnGangsKode(EnGangsKode inn)
        {
            if (ModelState.IsValid && Session["RiktigPassord"] !=null && (bool)Session["RiktigPassord"]==true)
            {
                Session["Innlogget"] = true;
                return RedirectToAction("IndexBruker");
            }
            /*** else if ( sjekke om koden som ble oppgitt ikke stemmer){
             *  Session["FeilKode"] == true; // bruker denne for å vise at koden var feil.
                return View();
                ***/
            return View();
        }
        public ActionResult IndexBruker()
        {
            if (Session["Innlogget"] != null) //Sjekker om session esksisterer for å unngå instance error i neste linje 
            {
                var innlogget = (bool)Session["Innlogget"] == true;
                if (innlogget)
                {
                    KommendeUtbetalinger((string)Session["PersonNr"]);
                    return View();
                }
                return RedirectToAction("LoggInn");
            }
            return RedirectToAction("LoggInn");
        }
        public ActionResult LoggInn()
        {
            return View();
        }
    }
}
