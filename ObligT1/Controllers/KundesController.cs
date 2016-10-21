﻿using System;
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
        private DataConn db = new DataConn();
        public ActionResult LagBruker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LagBruker(KundeModell innKunde)
        {
            if (DbFunskjoner.LagBruker(innKunde))
            {
                return RedirectToAction("DetteFunker");
            }
            else
            {
                return View();
            }
        }
        public string HentKontoNr()
        {
            using (var db = new DataConn())
            {
                DbFunskjoner df = new DbFunskjoner();
                string  kontoNr = df.hentKontoNr((string)Session["PersonNr"]); // er det lurt å bruke session her?
                Debug.WriteLine(kontoNr);
                return kontoNr; // funksjonsfilen lager json streng, bare å sende videre.
            }
        }
        [HttpPost]
        public ActionResult ValiderNyTransaksjon (NyTransaksjon inn)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("IndexBruker");
            }
            return RedirectToAction("IndexBruker");
        }
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
                    Session["Innlogget"] = true;
                    Session["PersonNr"] = innKunde.PersonNr;
                    return RedirectToAction("IndexBruker");
                }              
            }
            else
            {
                return RedirectToAction("LoggInn");
            }
        }
        public ActionResult IndexBruker()
        {
            if (Session["Innlogget"] != null) //Sjekke om session esksisterer for å unngå instance error i neste linje 
            {
                var innlogget = (bool)Session["Innlogget"] == true;
                if (innlogget)
                {
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
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // GET: Kundes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kundes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonNr,Fornavn,Etternavn,Passord")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Kunder.Add(kunde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kunde);
        }

        // GET: Kundes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Kundes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonNr,Fornavn,Etternavn,Passord")] Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kunde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kunde);
        }

        // GET: Kundes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kunde kunde = db.Kunder.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Kundes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kunde kunde = db.Kunder.Find(id);
            db.Kunder.Remove(kunde);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
