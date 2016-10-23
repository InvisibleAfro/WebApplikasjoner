using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ObligT1.Models
{    
    public class Kunde
        {
            [Key]
            public string PersonNr { get; set; }
            public string Fornavn { get; set; }
            public string Etternavn { get; set; }
            public byte [] PassordHash { get; set; }
        }
        public class Konto
        {
            [Key]
            public string KontoNr { get; set; }
            public Kunde PersonNr { get; set; }
            public decimal Beløp { get; set; }

        }
        public class KommendeTransaksjon
        {
            [Key]
            public int TransaksjonsID { get; set; }
            public string KontoFra { get; set; }
            public string KontoTil { get; set; }
            public decimal beløp { get; set; }
            public string Dato { get; set; }
        }
        public class Transaksjon
        {
            [Key]
            public int TransaksjonsID { get; set; }
            public string KontoFra { get; set; }
            public string KontoTil { get; set; }
            public decimal beløp { get; set; }
            public string Dato { get; set; }
        }
    public class DataConn : DbContext
    {
        public DataConn()
            : base("name=Database")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DataConn>());
            Database.CreateIfNotExists();
        }
        public DbSet<Konto> Kontoer { get; set; }
        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Transaksjon> Transaksjoner { get; set; }
        public DbSet<KommendeTransaksjon> KommendeTransaksjoner { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
