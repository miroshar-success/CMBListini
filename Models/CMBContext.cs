using System.Data.Entity;

namespace CMBListini.Models
{
    public class CMBContext : ApplicationDbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<PriceList> Listings { get; set; }
        public DbSet<vwUserListingIntro> vwUserListingIntro { get; set; }
        public DbSet<L6020_2Base> L6020_2Base { get; set; }
        public DbSet<L6020_2Serie> L6020_2Serie { get; set; }
        public DbSet<L6020_2Alesaggio> L6020_2Alesaggio { get; set; }
        public DbSet<L6020_2Stelo> L6020_2Stelo { get; set; }
        public DbSet<L6020_2TipoStelo> L6020_2TipoStelo { get; set; }
        public DbSet<L6020_2TipoFissaggio> L6020_2TipoFissaggio { get; set; }
        public DbSet<L6020_2EsecuzioniSpeciali> L6020_2EsecuzioniSpeciali { get; set; }
        public DbSet<L6020_2FissaggioPrice> L6020_2FissaggioPrice { get; set; }
        public DbSet<L6020_2SfiatiAria> L6020_2SfiatiAria { get; set; }
        public DbSet<L6020_2SensoriInduttivi> L6020_2SensoriInduttivi { get; set; }
        public DbSet<L6020_2FissaggioPriceCategory> L6020_2FissaggioPriceCategory { get; set; }
        public DbSet<L6020_2Guarnizione> L6020_2Guarnizione { get; set; }
        public DbSet<L6020_2GuarnizionePriceCategory> L6020_2GuarnizionePriceCategory { get; set; }
        public DbSet<L6020_2GuarnizionePrice> L6020_2GuarnizionePrice { get; set; }
        public DbSet<L6020_2MaterialeBoccola> L6020_2MaterialeBoccola { get; set; }
        public DbSet<L6020_2TrasduttorePrice> L6020_2TrasduttorePrice { get; set; }
        public DbSet<L6020_2TrasduttoreCategory> L6020_2TrasduttoreCategory { get; set; }
        public DbSet<L6020_2PiastraCetopPrice> L6020_2PiastraCetopPrice { get; set; }
        public DbSet<L6020_2PiastraCetopCategory> L6020_2PiastraCetopCategory { get; set; }
        public DbSet<L6020_2ProtezioneTrasduttore> L6020_2ProtezioneTrasduttore { get; set; }
        public DbSet<L6020_2OpzioniCilindroCategory> L6020_2OpzioniCilindroCategory { get; set; }
        public DbSet<L6020_2OpzioniCilindro> L6020_2OpzioniCilindro { get; set; }

        //aggiunte minori
        public DbSet<L6020_2SteloMonolitico> L6020_2SteloMonolitico { get; set; }
        public DbSet<L6020_2MaterialeStelo> L6020_2MaterialeStelo { get; set; }
        public DbSet<L6020_2SteloProlungato> L6020_2SteloProlungato { get; set; }
        public DbSet<L6020_2SoffiettoStelo> L6020_2SoffiettoStelo { get; set; }
        public DbSet<L6020_2Controflange> L6020_2Controflange { get; set; }
        public DbSet<L6020_2FilettaturaStelo> L6020_2FilettaturaStelo { get; set; }
        public DbSet<L6020_2Drenaggio> L6020_2Drenaggio { get; set; }
        public DbSet<L6020_2SnodoNonMantenuto> L6020_2SnodoNonMantenuto { get; set; }
        public DbSet<L6020_2Minimess> L6020_2Minimess { get; set; }
        public DbSet<L6020_2ProtezioneSensore> L6020_2ProtezioneSensore { get; set; }
        public DbSet<L6020_2VerniciaturaPrice> L6020_2VerniciaturaPrice { get; set; }
        public DbSet<L6020_2Verniciatura> L6020_2Verniciatura { get; set; }
        public DbSet<L6020_2SensoreStaffa> L6020_2SensoreStaffa { get; set; }
        public DbSet<L6020_2ConnessioniOlio> L6020_2ConnessioniOlio { get; set; }
        //
        //Accessori
        public DbSet<L6020_2AccessoriCategory> L6020_2AccessoriCategory { get; set; }
        public DbSet<L6020_2AccessoriPrice> L6020_2AccessoriPrice { get; set; }
        //













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
    }
}