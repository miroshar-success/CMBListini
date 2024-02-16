using System.Data.Entity;

namespace CMBListini.Models
{
    public class CMBContext2 : ApplicationDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PriceList> Listings { get; set; }
        public DbSet<vwUserListingIntro> vwUserListingIntro { get; set; }
        public DbSet<L6022_1Base> L6022_1Base { get; set; }
        public DbSet<L6022_1Serie> L6022_1Serie { get; set; }
        public DbSet<L6022_1Alesaggio> L6022_1Alesaggio { get; set; }
        public DbSet<L6022_1Stelo> L6022_1Stelo { get; set; }
        public DbSet<L6022_1TipoStelo> L6022_1TipoStelo { get; set; }
        public DbSet<L6022_1TipoFissaggio> L6022_1TipoFissaggio { get; set; }
        public DbSet<L6022_1EsecuzioniSpeciali> L6022_1EsecuzioniSpeciali { get; set; }
        public DbSet<L6022_1FissaggioPrice> L6022_1FissaggioPrice { get; set; }
        public DbSet<L6022_1SfiatiAria> L6022_1SfiatiAria { get; set; }
        public DbSet<L6022_1SensoriInduttivi> L6022_1SensoriInduttivi { get; set; }
        public DbSet<L6022_1FissaggioPriceCategory> L6022_1FissaggioPriceCategory { get; set; }
        public DbSet<L6022_1Guarnizione> L6022_1Guarnizione { get; set; }
        public DbSet<L6022_1GuarnizionePriceCategory> L6022_1GuarnizionePriceCategory { get; set; }
        public DbSet<L6022_1GuarnizionePrice> L6022_1GuarnizionePrice { get; set; }
        public DbSet<L6022_1MaterialeBoccola> L6022_1MaterialeBoccola { get; set; }
        public DbSet<L6022_1TrasduttorePrice> L6022_1TrasduttorePrice { get; set; }
        public DbSet<L6022_1TrasduttoreCategory> L6022_1TrasduttoreCategory { get; set; }
        public DbSet<L6022_1PiastraCetopPrice> L6022_1PiastraCetopPrice { get; set; }
        public DbSet<L6022_1PiastraCetopCategory> L6022_1PiastraCetopCategory { get; set; }
        public DbSet<L6022_1ProtezioneTrasduttore> L6022_1ProtezioneTrasduttore { get; set; }
        public DbSet<L6022_1OpzioniCilindroCategory> L6022_1OpzioniCilindroCategory { get; set; }
        public DbSet<L6022_1OpzioniCilindro> L6022_1OpzioniCilindro { get; set; }

        //aggiunte minori
        public DbSet<L6022_1SteloMonolitico> L6022_1SteloMonolitico { get; set; }
        public DbSet<L6022_1MaterialeStelo> L6022_1MaterialeStelo { get; set; }
        public DbSet<L6022_1SteloProlungato> L6022_1SteloProlungato { get; set; }
        public DbSet<L6022_1SoffiettoStelo> L6022_1SoffiettoStelo { get; set; }
        public DbSet<L6022_1Controflange> L6022_1Controflange { get; set; }
        public DbSet<L6022_1FilettaturaStelo> L6022_1FilettaturaStelo { get; set; }
        public DbSet<L6022_1Drenaggio> L6022_1Drenaggio { get; set; }
        public DbSet<L6022_1SnodoNonMantenuto> L6022_1SnodoNonMantenuto { get; set; }
        public DbSet<L6022_1Minimess> L6022_1Minimess { get; set; }
        public DbSet<L6022_1ProtezioneSensore> L6022_1ProtezioneSensore { get; set; }
        public DbSet<L6022_1VerniciaturaPrice> L6022_1VerniciaturaPrice { get; set; }
        public DbSet<L6022_1Verniciatura> L6022_1Verniciatura { get; set; }
        public DbSet<L6022_1SensoreStaffa> L6022_1SensoreStaffa { get; set; }
        public DbSet<L6022_1ConnessioniOlio> L6022_1ConnessioniOlio { get; set; }
        //
        //Accessori
        public DbSet<L6022_1AccessoriCategory> L6022_1AccessoriCategory { get; set; }
        public DbSet<L6022_1AccessoriPrice> L6022_1AccessoriPrice { get; set; }
        //

    }
}