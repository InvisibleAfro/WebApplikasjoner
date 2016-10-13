using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ObligT1.Models
{
    public class KundeModell
    {
        [DisplayName("Person nummer")]
        [Required(ErrorMessage = "Personnr må oppgis")]
        //[RegularExpression(@"[0-9]{11}", ErrorMessage = "Ugyldig person nummer.")]
        public string PersonNr { get; set; }
        //public string Fornavn { get; set; }
        //public string Etternavn { get; set; }
        [DisplayName("Passord")]
        [Required(ErrorMessage ="Passord må oppgis.")]
        public string Passord { get; set; }
    }
}