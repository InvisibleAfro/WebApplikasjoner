using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ObligT1.Models
{
    public class KontoDropDown
    {
        public  string KontoNr;
    }
    public class KontoModell
    {
        public string KontoNr { get; set; }
        public string PersonNr { get; set; }
        public decimal Beløp { get; set; }

    }
    public class KundeModell
    {
        [DisplayName("Person nummer")]
        [Required(ErrorMessage = "Personnr må oppgis")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Ugyldig person nummer.")]
        public string PersonNr { get; set; }
        //public string Fornavn { get; set; }
        //public string Etternavn { get; set; }
        [DisplayName("Passord")]
        [Required(ErrorMessage = "Passord må oppgis.")]
        public string Passord { get; set; }
    }
    public class BrukerIndex
    {
        public string personNr;
    }
    public class KontoListe
    {
        public string KontoNr;
        public decimal Saldo;
    }
}