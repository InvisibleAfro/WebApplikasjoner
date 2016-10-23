using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ObligT1.Models
{
    public class EnGangsKode
    {
        [DisplayName("Skriv inn engangskode:")]
        [Required(ErrorMessage = "Oppgi engangskode.")]
        [RegularExpression(@"[0-9]{6}", ErrorMessage = "Ugyldig nummer. Nøyaktig 6 siffer.")]
        public string Kode { get; set; }
    }
    public class KontoDropDown
    {
        public  string KontoNr;
    }
    
    public class Transaksjoner
    {
        public string TilKonto;
        public decimal Belop;
        public string Forfallsdato;
    }
    public class RegistrerKommendeUtbetaling
    {
        public string FraKonto;
        [Required(ErrorMessage = "Kontonummer må oppgis")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Ugyldig kontonummer.")]
        public string TilKonto;
        [Required(ErrorMessage = "Beløp må oppgis")]
        [RegularExpression(@"[0-9]+}", ErrorMessage = "Ugyldig beløp.")]
        public decimal Belop;
        [Required(ErrorMessage = "Forfallsdato må oppgis")]
        [RegularExpression(@"[0-9.-]+", ErrorMessage = "Ugylidg dato.")]
        public string Forfallsdato;
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
        [RegularExpression(@"[0-9a-zæåA-ZÆÅ.-_]+", ErrorMessage = "Ugylid passord.")]
        public string Passord { get; set; }
    }
    public class KontoOversikt
    {
        public string KontoNr;
        public decimal Saldo;
    }
}