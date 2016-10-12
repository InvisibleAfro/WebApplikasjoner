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
        [Required(ErrorMessage = "Personnr må oppgis")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Personnr må være bestå av nøyaktig 11 tall.")]
        [DisplayName("Person Nummer")]
        public string PersonNr { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Passord { get; set; }
    }
}