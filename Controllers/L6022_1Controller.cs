using CMBListini.Models;
using CMBListini.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CMBListini.Controllers
{
	[SessionValidation]
    public class L6022_1Controller : Controller
    {
        // GET: L6022_1
        public ActionResult Index()
        {
            ViewBag.Username = Session["username"];
            ViewBag.Organization = Session["Organization"];
            try
            {
                return View("Index", GetComponentsViewModelData());
            }
            catch
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
        private ViewModelL6022_1 GetComponentsViewModelData()
        {
            ViewModelL6022_1 VM = new ViewModelL6022_1();


            L6022_1Calc quotation = new L6022_1Calc();

            using (CMBContext dbCtx = new CMBContext())
            {
                //Discount
                string Organization = (string)Session["Organization"];
                var OrganizationData = dbCtx.Organizations.Where(x => x.OrganizationName == Organization).First();
                string StringDiscount = OrganizationData.OrganizationDiscount ?? "0+0";

                Regex rg = new Regex(@"^\d+\+\d+$");
                if (!rg.IsMatch(StringDiscount)) {
                    StringDiscount = "0+0";
                }

                string[] orgDiscountsubs = StringDiscount.Split('+');
                bool customDiscountEnable = false;
                int customDiscount = 0;
                int customExtraDiscount = 0;
                bool customDiscountMod = false;
                if (StringDiscount == "0+0") {
                    var CustomDis = dbCtx.Users.SqlQuery("SELECT * " +
                        "FROM dbo.Users " +
                        "WHERE DISCOUNTMODIFY_START <= GETDATE() AND DISCOUNTMODIFY_STOP >= GETDATE() " +
                        "AND DISCOUNTMOD IS NOT NULL AND DISCOUNT IS NOT NULL AND DISCOUNT > 0 AND DISCOUNT <= 100 " +
                        "AND DISCOUNTEXTRA IS NOT NULL ANd DISCOUNTEXTRA > 0 and DISCOUNTEXTRA <= 100 " +
                        "AND UserId = '" + (string)Session["UserName"] + "'")
                        .FirstOrDefault();
                    if (CustomDis != null)
                    {
                        customDiscountEnable = true;
                        customDiscount = (int)CustomDis.DISCOUNT;
                        customExtraDiscount = (int)CustomDis.DISCOUNTEXTRA;
                        customDiscountMod = CustomDis.DISCOUNTMOD == true;
                    }
                }
                
                VM = new L6022_1Calc().ToViewModel(
                    Int32.Parse(orgDiscountsubs[0]), 
                    Int32.Parse(orgDiscountsubs[1]),
                    customDiscountEnable,
                    customDiscountMod,
                    customDiscount,
                    customExtraDiscount
                );
                VM.OrganizationData = dbCtx.Organizations.Where(x => x.OrganizationName == Organization).First();
            }
            return VM;
        }

        [HttpPost]
        public Boolean SelectComboDaCodiceArticolo (string ArticleCode, ViewModelL6022_1 FormData)
        {
            if (ArticleCode.Length>=13)
            {
                // il codice è correttamente lungo almeno 13 caratteri
                // popolo
                try
                {
                    string codinserito = FormData.CodiceDigitato;

                }
                catch (Exception ex)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public ActionResult UpdateListAlesaggio(string InputSerieID)
        {
            Dictionary<string, string> articlesByCategory = new Dictionary<string, string>();
            try
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    var articleCategories = dbCtx.L6022_1Alesaggio.Where(c => c.AlesaggioSerieID == InputSerieID).OrderBy(c => c.AlesaggioLength).ToArray();
                    //Extract all articles matching a specific category
                    foreach (L6022_1Alesaggio category in articleCategories)
                    {
                        //L6022_1Alesaggio[] articlesBySelectedCategory = dbCtx.L6022_1Alesaggio.Where(a =>  == category.ArticleCategoryID && a.InUse).ToArray();
                        articlesByCategory.Add(category.AlesaggioID.ToString(), Convert.ToInt32(category.AlesaggioLength).ToString());
                    }
                }
                return Content(JsonConvert.SerializeObject(new { status = true, values = articlesByCategory }));
            }

            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
            }
        }

        //funzione che aggiorna il contenuto di stelo su schermata listino
        [HttpPost]
        public ActionResult UpdateListStelo(string InputAlesaggioID, string InputSerieID)
        {
            Dictionary<string, string> articlesByCategory = new Dictionary<string, string>();
            int IntAlesaggioID = 1;
            try
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    var articleCategories = dbCtx.L6022_1Stelo.Where(c => c.SteloAlesaggioID == 1).OrderBy(c => c.SteloValue).ToArray();
                    if (InputAlesaggioID != "")
                    {
                        IntAlesaggioID = Int32.Parse(InputAlesaggioID);
                        articleCategories = dbCtx.L6022_1Stelo.Where(c => c.SteloAlesaggioID == IntAlesaggioID).OrderBy(c => c.SteloValue).ToArray();

                    }
                    else
                    {
                        //prendo alesaggi ID della serie
                        var AlesaggioCategory = dbCtx.L6022_1Alesaggio.Where(c => c.AlesaggioSerieID == InputSerieID).FirstOrDefault();
                        articleCategories = dbCtx.L6022_1Stelo.Where(c => c.SteloAlesaggioID == AlesaggioCategory.AlesaggioID).OrderBy(c => c.SteloValue).ToArray();
                    }

                    //Extract all articles matching a specific category
                    foreach (L6022_1Stelo category in articleCategories)
                    {
                        //L6022_1Alesaggio[] articlesBySelectedCategory = dbCtx.L6022_1Alesaggio.Where(a =>  == category.ArticleCategoryID && a.InUse).ToArray();
                        articlesByCategory.Add(category.SteloID.ToString(), category.SteloAcronym + " - " + category.SteloDesc.ToString());
                    }
                }
                return Content(JsonConvert.SerializeObject(new { status = true, values = articlesByCategory }));
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
            }
        }

        [HttpPost]
        public ActionResult ParseCode(string serie, string alesaggio, string stelo, string corsa, string tipoStelo, string tipoFissaggio)
        {
            using (CMBContext dbCtx = new CMBContext()) { 
                var Serie = dbCtx.L6022_1Serie.Where(x => x.SerieisActive).Where(x => x.SerieID == serie).FirstOrDefault();
                if (Serie == null) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "ID serie non valido > " + serie }));
                }
                var al = Decimal.Parse(alesaggio);
                var Alesaggio = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioisActive).Where(x => x.AlesaggioSerieID == Serie.SerieID).Where(x => x.AlesaggioLength == al).FirstOrDefault();
                if (Alesaggio == null) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Valore alesaggio non valido > " + alesaggio }));
                }
                var Stelo = dbCtx.L6022_1Stelo.Where(x => x.SteloisActive).Where(x => x.SteloAlesaggioID == Alesaggio.AlesaggioID).Where(x => x.SteloAcronym == stelo).FirstOrDefault();
                if (Stelo == null) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Acronym stelo non valido > " + stelo }));
                }
                var Corsa = Int32.Parse(corsa);
                if (Corsa <= 0 || Corsa > 5000) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Corsa puo' avere un valore minimo di 0 e massimo di 5000 > " + corsa }));
                }
                var TipoStelo = dbCtx.L6022_1TipoStelo.Where(x => x.isActive).Where(x => x.TipoSteloAcronym == tipoStelo).FirstOrDefault();
                if (TipoStelo == null) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Acronym tipo stelo non valido > " + tipoStelo }));
                }
                var TipoFissaggio = dbCtx.L6022_1TipoFissaggio.Where(x => x.isActive).Where(x => x.TipoFissaggioAcronym == tipoFissaggio).FirstOrDefault();
                if (TipoFissaggio == null) {
                    return Content(JsonConvert.SerializeObject(new { status = false, message = "Acronym tipo fissaggio non valido > " + tipoFissaggio }));
                }
                var Series = dbCtx.L6022_1Serie.Where(x => x.SerieisActive).ToList().Select(x => new { Value = x.SerieID, Text = x.SerieID + " - " + x.SerieDesc }).ToList();
                var Alesaggios = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioisActive).Where(x => x.AlesaggioSerieID == Serie.SerieID).ToList().Select(x => new { Value = x.AlesaggioID, Text = x.AlesaggioLength }).ToList();
                var Stelos = dbCtx.L6022_1Stelo.Where(x => x.SteloisActive).Where(x => x.SteloAlesaggioID == Alesaggio.AlesaggioID).ToList().Select(x => new { Value = x.SteloID, Text = x.SteloAcronym + " - " + x.SteloDesc }).ToList();
                var TipoStelos = dbCtx.L6022_1TipoStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoSteloID, Text = x.TipoSteloAcronym + " - " + x.TipoSteloDesc }).ToList();
                var TipoFissaggios = dbCtx.L6022_1TipoFissaggio.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoFissaggioID, Text = x.TipoFissaggioAcronym + " - " + x.TipoFissaggioDesc }).ToList();

                return Content(JsonConvert.SerializeObject(new { 
                    status = true,
                    Series = Series,
                    Alesaggios = Alesaggios,
                    Stelos = Stelos,
                    TipoStelos = TipoStelos,
                    TipoFissaggios = TipoFissaggios,

                    SerieID = Serie.SerieID,
                    AlesaggioID = Alesaggio.AlesaggioID,
                    SteloID = Stelo.SteloID,
                    Corsa = Corsa,
                    TipoSteloID = TipoStelo.TipoSteloID,
                    TipoFissaggioID = TipoFissaggio.TipoFissaggioID,
                }));
            }
        }
        
        //funzione aggiorna codice in schermata listino
        [HttpPost]
        public string UpdateCode(string ConnessioniOlio, string OpzioniCilindroID, string ProtezioneSensore, string SerieID, string AlesaggioID, string SteloID, string Corsa, string TipoSteloID, string TipoFissaggioID, string Distanziali, string SfiatiAriaID, string SensoriInduttiviID, string GuarnizioneID, string Controflangia, string SteloMonolitico, string MaterialeSteloID, string SteloProlungato, string SoffiettoStelo, string FilettaturaSteloID, string MaterialeBoccolaID, string Drenaggio, string SnodoNonMantenuto, string DadiIncassati, string MinimessID, string ProtezioneTrasduttore)
        {   
            //rimpiazzo simboli strani in Corsa
            Corsa = Corsa.Replace(".", "");
            string ActualCode = "";
            int IntAlesaggio = 0;
            //SERIE
            if (SerieID != "" && SerieID != null)
            {
                ActualCode = SerieID;
            }
            else
            {
                return "-";
            }
            //ALESAGGIO
            if (AlesaggioID != null)
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    var AlesaggioLength = new L6022_1Alesaggio();
                    if (AlesaggioID == "")
                    {
                        AlesaggioLength = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioSerieID == SerieID).FirstOrDefault();
                    }
                    else
                    {
                        IntAlesaggio = Int32.Parse(AlesaggioID);
                        AlesaggioLength = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioID == IntAlesaggio).FirstOrDefault();
                    }
                    int LAlesaggio = Convert.ToInt32(AlesaggioLength.AlesaggioLength);
                    string LAddition = string.Format("{0:000}", LAlesaggio);
                    ActualCode += LAddition;
                }
            }

            //STELO
            if (SteloID != null)
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    var SteloCode = new L6022_1Stelo();
                    if (SteloID == "")
                    {
                        if (AlesaggioID == "")
                        {
                            var AlesaggioCategory = dbCtx.L6022_1Alesaggio.Where(c => c.AlesaggioSerieID == SerieID).FirstOrDefault();
                            SteloCode = dbCtx.L6022_1Stelo.Where(c => c.SteloAlesaggioID == AlesaggioCategory.AlesaggioID).FirstOrDefault();

                        }
                        else
                        {
                            IntAlesaggio = Int32.Parse(AlesaggioID);
                            SteloCode = dbCtx.L6022_1Stelo.Where(x => x.SteloAlesaggioID == IntAlesaggio).FirstOrDefault();
                        }
                    }
                    else
                    {
                        var IntStelo = Int32.Parse(SteloID);
                        SteloCode = dbCtx.L6022_1Stelo.Where(x => x.SteloID == IntStelo).FirstOrDefault();
                    }

                    ActualCode += SteloCode.SteloAcronym;
                }
            }

            //CORSA
            if (Corsa != null && Corsa != "")
            {
                string CAddition = Corsa.PadLeft(4, '0');
                if (CAddition.Length > 4)
                {
                    CAddition = CAddition.Substring(0, 4);
                }
                ActualCode += CAddition;
            }

            //TIPOSTELO
            if (TipoSteloID != null && TipoSteloID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntTipoStelo = Int32.Parse(TipoSteloID);
                    var TipoSteloCategory = dbCtx.L6022_1TipoStelo.Where(c => c.TipoSteloID == IntTipoStelo).FirstOrDefault();
                    ActualCode += TipoSteloCategory.TipoSteloAcronym;
                }
            }

            //x,y,w CHECK
            if (FilettaturaSteloID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntFilettaturaSteloID = Int32.Parse(FilettaturaSteloID);
                    var TipoFilettaturaStelo = dbCtx.L6022_1FilettaturaStelo.Where(c => c.FilettaturaSteloID == IntFilettaturaSteloID).FirstOrDefault();
                    ActualCode += TipoFilettaturaStelo.FilettaturaSteloAcronym;
                }
            }
            //z CHECK
            if (ConnessioniOlio != "" || OpzioniCilindroID != "" || ProtezioneTrasduttore == "true" || ProtezioneSensore == "true" || MinimessID != "" || DadiIncassati == "true" || SnodoNonMantenuto == "true" || Drenaggio == "true" || Controflangia == "true" || SteloMonolitico == "true" || MaterialeSteloID != "" || SteloProlungato != "0" || SoffiettoStelo == "true" || MaterialeBoccolaID != "")
            {
                ActualCode += "z";
            }

            //TIPOFISSAGGIO
            if (TipoFissaggioID != null && TipoFissaggioID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntTipoFissaggio = Int32.Parse(TipoFissaggioID);
                    var TipoFissaggioCategory = dbCtx.L6022_1TipoFissaggio.Where(c => c.TipoFissaggioID == IntTipoFissaggio).FirstOrDefault();
                    ActualCode += TipoFissaggioCategory.TipoFissaggioAcronym;
                }

            }
            //PARTE DI CODICE OPZIONALE
            //Posizione sensori induttivi
            //
            //Posizione sfiati ad aria
            //
            //Posizioni regolazioni frenature
            //
            //Posizione connessioni
            //
            //Distanziali (1 cifra)
            if (Distanziali != "0")
            {
                if (!ActualCode.Contains("/"))
                {
                    ActualCode += " / ";
                }
                ActualCode += Distanziali;
            }
            //Sfiati aria
            if (SfiatiAriaID != null && SfiatiAriaID != "")
            {
                if (!ActualCode.Contains("/"))
                {
                    ActualCode += " / ";
                }
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntSfiatiAriaID = Int32.Parse(SfiatiAriaID);
                    var SfiatiAriaCategory = dbCtx.L6022_1SfiatiAria.Where(c => c.SfiatiAriaID == IntSfiatiAriaID).FirstOrDefault();
                    ActualCode += SfiatiAriaCategory.SfiatiAriaAcronym;
                }
            }
            //
            //Guarnizioni
            if (GuarnizioneID != null && GuarnizioneID != "")
            {
                if (!ActualCode.Contains("/"))
                {
                    ActualCode += " / ";
                }
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntGuarnizioneID = Int32.Parse(GuarnizioneID);
                    var GuarnizioneCategory = dbCtx.L6022_1Guarnizione.Where(c => c.GuarnizioneID == IntGuarnizioneID).FirstOrDefault();
                    ActualCode += GuarnizioneCategory.GuarnizioneAcronym;
                }
            }
            //Sensori induttivi
            if (SensoriInduttiviID != null && SensoriInduttiviID != "")
            {
                if (!ActualCode.Contains("/"))
                {
                    ActualCode += " / ";
                }
                using (CMBContext dbCtx = new CMBContext())
                {
                    int IntSensoriInduttiviID = Int32.Parse(SensoriInduttiviID);
                    var SensoriInduttiviCategory = dbCtx.L6022_1SensoriInduttivi.Where(c => c.SensoriInduttiviID == IntSensoriInduttiviID).FirstOrDefault();
                    ActualCode += SensoriInduttiviCategory.SensoriInduttiviAcronym;
                }
            }
            return ActualCode;
        }

        [HttpPost]
        //calcolo totali
        public string PriceCalc(ViewModelL6022_1 FormData)
        {
            decimal RollerGrossTotal = 0;
            decimal RollerNetTotal = 0;
            decimal AttachmentGrossTotal = 0;
            decimal AttachmentNetTotal = 0;
            decimal GrossTotal = 0;
            decimal TransducerTotal = 0;
            decimal FinalTotal = 0;
            
            //Discount
            decimal InputDiscount = Int32.Parse(FormData.InputDiscount == null ? "0" : FormData.InputDiscount);
            decimal InputDiscountPlus = Int32.Parse(FormData.InputDiscountPlus == null ? "0" : FormData.InputDiscountPlus);
            
            //ExtraPrezzo
            decimal InputExtraPrezzo = FormData.inputExtraPrezzo;
            
            //Base
            decimal BasePriceTotal = 0;
            decimal BaseRun = 0;
            decimal BaseRunAfter = 0;
            decimal BaseRunStd = 0;
            decimal BaseRunAfterStd = 0;
            decimal BasePriceTotalStd = 0;

            //Alesaggio
            int InputAlesaggioID = Int32.Parse(FormData.InputAlesaggioID);

            //Stelo
            int InputSteloID = Int32.Parse(FormData.InputSteloID);

            //TipoStelo
            int InputTipoSteloID = Int32.Parse(FormData.InputTipoSteloID);

            //Corsa
            decimal FormCorsa = Decimal.Parse(FormData.InputCorsa);

            //PredisposizioneTrasduttore
            decimal PredisposizioneTrasduttorePrice = 0;

            //Trasduttore
            int InputTrasduttoreID = Int32.Parse(FormData.InputTrasduttoreID ?? "0");
            decimal TrasduttorePrice = 0;

            //ConnettoriTrasduttore
            Boolean InputConnettoriTrasduttore = FormData.InputConnettoriTrasduttore;

            //SensoreStaffa //MA3GA3
            int InputMA3GA3 = FormData.InputMA3GA3;
            decimal MA3GA3Price = 0;

            //Distanziali
            int Distanziali = Int32.Parse(FormData.InputDistanziali);
            decimal DistanzialiPrice = 0;

            //TipoFissaggio
            int InputTipoFissaggio = Int32.Parse(FormData.InputTipoFissaggioID);

            //Fissaggio
            decimal FissaggioPrice = 0;
            decimal FissaggioPriceNoDiscount = 0;

            //SfiatiArea
            int InputSfiatiAria = Int32.Parse(FormData.InputSfiatiAriaID ?? "0");
            decimal SfiatiAriaPrice = 0;

            //SensoriInduttivi
            int InputSensoriInduttivi = Int32.Parse(FormData.InputSensoriInduttiviID ?? "0");
            decimal SensoriInduttiviPrice = 0;

            //Guarnizioni
            int InputGuarnizione = Int32.Parse(FormData.InputGuarnizioniID ?? "0");
            decimal GuarnizionePrice = 0;
            decimal GuarnizioneKitRicambioPrice = 0;

            //Controflangia
            Boolean InputControflangia = FormData.InputControflangia;
            decimal ControflangliaPrice = 0;

            //SteloMonolitico
            Boolean InputSteloMonolitico = FormData.InputSteloMonolitico;
            decimal SteloMonoliticoPrice = 0;

            //SoffiettoStelo
            Boolean InputSoffiettoStelo = FormData.InputSoffiettoStelo;
            decimal SoffiettoSteloPrice = 0;

            //ConnessioniOlio
            int InputConnessioniOlioID = Int32.Parse(FormData.InputConnessioniOlio ?? "0");
            int InputConnessioniOlioN = FormData.InputConnessioniOlioN;
            decimal ConnessioniOlioPrice = 0;

            //Drenaggio
            Boolean InputDrenaggio = FormData.InputDrenaggio;
            decimal DrenaggioPrice = 0;

            //MaterialeStelo
            int InputMaterialeSteloID = Int32.Parse(FormData.InputMaterialeSteloID ?? "0");
            decimal MaterialeSteloPrice = 0;

            //SteloProlungato
            int InputSteloProlungato = FormData.InputSteloProlungato;
            decimal SteloProlungatoPrice = 0;

            //DadiIncassati
            Boolean InputDadiIncassati = FormData.InputDadiIncassati;
            decimal DadiIncassatiPrice = 0;


            //MaterialeBoccola
            int InputMaterialeBoccola = Int32.Parse(FormData.InputMaterialeBoccolaID ?? "0");
            decimal MaterialeBoccolaPrice = 0;

            //SnodoNonMantenuto
            Boolean InputSnodoNonMantenuto = FormData.InputSnodoNonMantenuto;
            decimal SnodoNonMantenutoPrice = 0;

            //Minimess
            int InputMinimessID = Int32.Parse(FormData.InputMinimessID ?? "0");
            decimal MinimessPrice = 0;

            //ProtezioneSensore
            Boolean InputProtezioneSensore = FormData.InputProtezioneSensore;
            decimal ProtezioneSensorePrice = 0;

            //Verniciatura
            int InputVerniciatura = Int32.Parse(FormData.InputVerniciaturaID ?? "0");
            decimal VerniciaturaPrice = 0;

            //Ammortizzatori (Basarsi su tipo stelo)
            decimal AmmortizzatoriPrice = 0;

            //PiastraCetop
            int InputPiastraCetop = Int32.Parse(FormData.InputPiastraCetopID ?? "0");
            decimal PiastraCetopPrice = 0;

            //FilettaturaStelo
            int InputFilettaturaSteloID = Int32.Parse(FormData.InputFilettaturaStelo ?? "0");
            decimal FilettaturaSteloPrice = 0;

            //ProtezioneTrasduttore
            Boolean InputProtezioneTrasduttore = FormData.InputProtezioneTrasduttore;
            decimal ProtezioneTrasduttorePrice = 0;

            //OpzioniCilindro
            int InputOpzioniCilindro = Int32.Parse(FormData.InputOpzioniCilindroID ?? "0");
            decimal OpzioniCilindroPrice = 0;

            //Accessori Stelo/Cilindro
            int InputAccessoriStelo = Int32.Parse(FormData.InputAccessoriSteloID ?? "0");
            int InputAccessoriCilindro = Int32.Parse(FormData.InputAccessoriCilindroID ?? "0");
            decimal AccessoriSteloPrice = 0;
            decimal AccessoriCilindroPrice = 0;

            //calcoli
            using (CMBContext dbCtx = new CMBContext())
            {
                //vars
                decimal Corsa = FormCorsa;
                decimal CorsaStd = FormCorsa;
                //datasets
                L6022_1Alesaggio FormAlesaggio = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioID == InputAlesaggioID).First();
                L6022_1Stelo FormStelo = dbCtx.L6022_1Stelo.Where(x => x.SteloID == InputSteloID).First();
                L6022_1TipoStelo FormTipoStelo = dbCtx.L6022_1TipoStelo.Where(x => x.TipoSteloID == InputTipoSteloID).First();
                L6022_1Serie FormSerie = dbCtx.L6022_1Serie.Where(x => x.SerieID == FormData.InputSerieID).First();
                L6022_1TipoFissaggio FormTipoFissaggio = dbCtx.L6022_1TipoFissaggio.Where(x => x.TipoFissaggioID == InputTipoFissaggio).First();
                L6022_1FissaggioPrice FormFissaggioPrice = dbCtx.L6022_1FissaggioPrice.Where(x => x.FissaggioPriceCategoryID == FormTipoFissaggio.FissaggioPriceCategoryID && x.FissaggioAlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                L6022_1FissaggioPriceCategory FormFissaggioPriceCategory = dbCtx.L6022_1FissaggioPriceCategory.Where(x => x.FissaggioPriceCategoryID == FormTipoFissaggio.FissaggioPriceCategoryID).First();
                //datasets opzionali sono popolati negli appositi contesti

                //Calcolo prezzo base basato su Corsa
                //Prendo costo della Base
                L6022_1Base FormBase = dbCtx.L6022_1Base.Where(x => x.BaseInox == FormSerie.SerieInox && x.BaseStP == FormTipoStelo.TipoSteloPassante && x.AlesaggioLength == FormAlesaggio.AlesaggioLength && x.SteloValue == FormStelo.SteloValue).First();
                //Controllo Corsa
                //Tolgo prezzo base
                //Multiplier e' il 40% aggiuntivo che con le serie M3 e T3 avviene per il costo della UntilRun. Reso Modulare
                BaseRun = FormBase.UntilRunPrice * FormBase.IncreaseRunMultiplier;
                Corsa -= FormBase.LimitLength;
                //aggiunto costi supplementari
                while (Corsa > 0)
                {
                    BaseRunAfter += FormBase.BeyondRunPrice;
                    Corsa -= FormBase.LimitLength;
                }
                BasePriceTotal = BaseRun + BaseRunAfter;
                //Fine prezzo Base

                //calcolo Base Std
                L6022_1Serie FormSerieStd = dbCtx.L6022_1Serie.Where(x => x.isStandard == true).First();
                L6022_1Base FormBaseStd = dbCtx.L6022_1Base.Where(x => x.BaseInox == FormSerieStd.SerieInox && x.BaseStP == FormTipoStelo.TipoSteloPassante && x.AlesaggioLength == FormAlesaggio.AlesaggioLength && x.SteloValue == FormStelo.SteloValue).First();

                //rialcolo prezzo base nuova
                BaseRunStd = FormBaseStd.UntilRunPrice * FormBaseStd.IncreaseRunMultiplier;
                CorsaStd -= FormBaseStd.LimitLength;
                //aggiunto costi supplementari
                while (CorsaStd > 0)
                {
                    BaseRunAfterStd += FormBaseStd.BeyondRunPrice;
                    CorsaStd -= FormBaseStd.LimitLength;
                }
                BasePriceTotalStd = BaseRunStd + BaseRunAfterStd;

                //Fissaggi
                if (FormFissaggioPriceCategory.isStandard != true)
                {
                    if (FormTipoFissaggio.OutDiscount == true)
                    {
                        FissaggioPriceNoDiscount = FormFissaggioPrice.FissaggioPrice * FormTipoFissaggio.TirantiMultiplier;
                    }
                    else
                    {
                        FissaggioPrice = FormFissaggioPrice.FissaggioPrice * FormTipoFissaggio.TirantiMultiplier;
                    }
                }
                //Fine Fissaggi
                //PredisposizioneTrasduttore
                if (FormSerie.SerieTransducer == true)
                {
                    PredisposizioneTrasduttorePrice = (BasePriceTotal * FormSerie.SerieTransducerMultiplier);
                    RollerGrossTotal += PredisposizioneTrasduttorePrice;
                }

                //TipoStelo Ammortizzatori
                if (FormTipoStelo.TipoSteloAmmortizzato == true)
                {
                    L6022_1FissaggioPriceCategory FormFissaggioPriceCategoryAmmortizzatori = dbCtx.L6022_1FissaggioPriceCategory.Where(x => x.isAmmortizzatori == true).First();
                    L6022_1FissaggioPrice FormFissaggioPriceAmmortizzatori = dbCtx.L6022_1FissaggioPrice.Where(x => x.FissaggioPriceCategoryID == FormFissaggioPriceCategoryAmmortizzatori.FissaggioPriceCategoryID && x.FissaggioAlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                    AmmortizzatoriPrice = FormFissaggioPriceAmmortizzatori.FissaggioPrice * FormTipoStelo.TipoSteloNAmmortizzatori;
                    RollerGrossTotal += AmmortizzatoriPrice;
                }
                RollerGrossTotal += BasePriceTotal + FissaggioPrice;
                ////OPZIONALI
                ///
                //Trasduttore
                if (InputTrasduttoreID != 0)
                {
                    decimal CorsaTrasduttore = FormCorsa;
                    L6022_1TrasduttorePrice FormTrasduttore = dbCtx.L6022_1TrasduttorePrice.Where(x => x.TrasduttorePriceID == InputTrasduttoreID).First();
                    TrasduttorePrice = FormTrasduttore.TrasduttorePriceRun;
                    CorsaTrasduttore -= FormTrasduttore.TrasduttorePriceRunSlice;

                    if (CorsaTrasduttore > 0)
                    {
                        decimal CorsaTrasduttoreRemains = CorsaTrasduttore / FormTrasduttore.TrasduttorePriceRunSlice;
                        CorsaTrasduttoreRemains = Math.Ceiling(CorsaTrasduttoreRemains);
                        TrasduttorePrice += CorsaTrasduttoreRemains * FormTrasduttore.TrasduttorePriceAfterRun;
                    }

                    if (InputConnettoriTrasduttore == true)
                    {
                        TrasduttorePrice += FormTrasduttore.TrasduttorePriceConn;
                    }

                    RollerGrossTotal += TrasduttorePrice;
                    TransducerTotal = TrasduttorePrice;
                }

                //SensoreStaffa
                if (InputMA3GA3 > 0)
                {
                    try
                    {
                        L6022_1SensoreStaffa FormMA3GA3 = dbCtx.L6022_1SensoreStaffa.Where(x => x.inUse == true).First();
                        MA3GA3Price = (FormMA3GA3.SensorePrice + FormMA3GA3.StaffaPrice) * InputMA3GA3;
                        if (FormMA3GA3.onDiscount == true)
                        {
                            AttachmentGrossTotal += MA3GA3Price;
                        }
                        else
                        {
                            RollerNetTotal += MA3GA3Price;
                        }

                    }
                    catch (Exception e)
                    {
                        return "Configurazione attiva non trovata per SensoreStaffa";
                    }
                }

                //Distanziali
                DistanzialiPrice = FormBase.SpacerPrice * Distanziali;
                //Fine Distanziali
                //Sfiati Aria
                if (InputSfiatiAria != 0)
                {
                    L6022_1SfiatiAria FormSfiatiAria = dbCtx.L6022_1SfiatiAria.Where(x => x.SfiatiAriaID == InputSfiatiAria).First();
                    SfiatiAriaPrice = (FormSfiatiAria.SfiatiAriaPrice * FormSfiatiAria.SfiatiAriaPriceMultiplier) * FormSfiatiAria.NSfiati;
                }
                //
                //Sensori Induttivi
                if (InputSensoriInduttivi != 0)
                {
                    L6022_1SensoriInduttivi FormSensoriInduttivi = dbCtx.L6022_1SensoriInduttivi.Where(x => x.SensoriInduttiviID == InputSensoriInduttivi).First();
                    L6022_1FissaggioPriceCategory FormCategoryAmm = dbCtx.L6022_1FissaggioPriceCategory.Where(x => x.isStandard == true).First(); //da verificare se si prende cosi' il prezzo
                    L6022_1FissaggioPrice FormAmmortizzatore = dbCtx.L6022_1FissaggioPrice.Where(x => x.FissaggioPriceCategoryID == FormCategoryAmm.FissaggioPriceCategoryID && x.FissaggioAlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                    //se ho già un cilindro ammortizzato allora non aggiungo ammortizzatori
                    if (FormTipoStelo.TipoSteloNAmmortizzatori != 0)
                    {
                        SensoriInduttiviPrice = (((FormSensoriInduttivi.SensoriInduttiviPrice * FormSensoriInduttivi.SensoriInduttiviPriceMultiplier) * FormSensoriInduttivi.NSensori));
                    }
                    else
                    {
                        SensoriInduttiviPrice = (((FormSensoriInduttivi.SensoriInduttiviPrice * FormSensoriInduttivi.SensoriInduttiviPriceMultiplier) * FormSensoriInduttivi.NSensori) + FormAmmortizzatore.FissaggioPrice * FormSensoriInduttivi.NAmm);
                    }
                }
                //
                //Guarnizioni
                if (InputGuarnizione != 0)
                {
                    try
                    {
                        L6022_1Guarnizione FormGuarnizione = dbCtx.L6022_1Guarnizione.Where(x => x.GuarnizioneID == InputGuarnizione).First();
                        //L6022_1GuarnizionePriceCategory FormCategoryGuarn = dbCtx.L6022_1GuarnizionePriceCategory.Where(x => x.isStandard == true).First(); //da verificare se si prende cosi' il prezzo
                        if (FormGuarnizione.GuarnizioneMultiplier != 0)
                        {
                            GuarnizionePrice += BaseRun * FormGuarnizione.GuarnizioneMultiplier;
                        }
                        if (FormGuarnizione.isMagg == true)
                        {
                            L6022_1GuarnizionePrice FormGuarnizionePrice = dbCtx.L6022_1GuarnizionePrice.Where(x => x.GuarnizionePriceCategoryID == FormGuarnizione.GuarnizionePriceCategoryID && x.GuarnizionePriceAlesaggioLength == FormAlesaggio.AlesaggioLength && x.GuarnizionePriceSteloValue == FormStelo.SteloValue).First();
                            GuarnizionePrice += FormGuarnizionePrice.GuarnizionePrice * FormGuarnizione.GuarnizioneKitMultiplier;
                        }
                        GuarnizioneKitRicambioPrice = GuarnizionePrice;
                    }
                    catch (Exception e)
                    {
                        return "Maggiorazione non trovata per la Guarnizione selezionata";
                    }
                }
                //
                //FINE OPZIONALI

                //Aggiunte "z"
                //Controflange
                if (InputControflangia != false)
                {
                    try
                    {
                        L6022_1Controflange FormControflange = dbCtx.L6022_1Controflange.Where(x => x.ControflangeAlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                        if (FormTipoFissaggio.TipoFissaggioControflangeSpecial == false)
                        {

                            ControflangliaPrice = FormControflange.ControflangePrice;
                        }
                        else
                        {
                            ControflangliaPrice = FormFissaggioPrice.FissaggioPrice * FormTipoFissaggio.TipoFissaggioControflangeMultiplier;

                        }

                        if (FormTipoFissaggio.TipoFissaggioControflangeSpecial == false)
                        {
                            AttachmentGrossTotal += ControflangliaPrice;
                        }
                        else
                        {
                            RollerNetTotal += ControflangliaPrice;
                        }

                    }
                    catch (Exception e)
                    {
                        return "Controflange prezzo non trovato per Alesaggio immesso";
                    }
                }
                //
                //SteloMonolitico
                if (InputSteloMonolitico != false)
                {
                    L6022_1SteloMonolitico FormSteloMonolitico = dbCtx.L6022_1SteloMonolitico.Where(x => x.inUse).First();
                    SteloMonoliticoPrice = BasePriceTotalStd * FormSteloMonolitico.SteloMonoliticoPriceMultiplier;
                    if (FormSteloMonolitico.onDiscount == true)
                    {
                        AttachmentGrossTotal += SteloMonoliticoPrice;
                    }
                    else
                    {
                        RollerNetTotal += SteloMonoliticoPrice;
                    }
                }

                //SoffiettoStelo
                if (InputSoffiettoStelo != false)
                {
                    try
                    {
                        L6022_1SoffiettoStelo FormSoffiettoStelo = dbCtx.L6022_1SoffiettoStelo.Where(x => x.SoffiettoSteloFrom <= FormCorsa && x.SoffiettoSteloTo >= FormCorsa).First();
                        SoffiettoSteloPrice = FormSoffiettoStelo.SoffiettoSteloPrice;
                        if (FormSoffiettoStelo.onDiscount == true)
                        {
                            AttachmentGrossTotal += SoffiettoSteloPrice;
                        }
                        else
                        {
                            RollerNetTotal += SoffiettoSteloPrice;
                        }
                    }
                    catch (Exception e)
                    {
                        return "Prezzo non trovato per la lunghezza Corsa in SoffiettoStelo";
                    }
                }

                //MaterialeStelo prendere sempre base standard A3
                if (InputMaterialeSteloID != 0)
                {
                    //prendo base standard
                    L6022_1MaterialeStelo FormMaterialeStelo = dbCtx.L6022_1MaterialeStelo.Where(x => x.MaterialeSteloID == InputMaterialeSteloID).First();
                    MaterialeSteloPrice = BasePriceTotalStd * FormMaterialeStelo.MaterialeSteloPriceMultiplier;
                    if (FormMaterialeStelo.onDiscount == true)
                    {
                        AttachmentGrossTotal += MaterialeSteloPrice;
                    }
                    else
                    {
                        RollerNetTotal += MaterialeSteloPrice;
                    }
                }

                //MaterialeBoccola
                if (InputMaterialeBoccola != 0)
                {
                    //prendo base standard
                    L6022_1MaterialeBoccola FormMaterialeBoccola = dbCtx.L6022_1MaterialeBoccola.Where(x => x.MaterialeBoccolaID == InputMaterialeBoccola).First();
                    MaterialeBoccolaPrice = FormBaseStd.UntilRunPrice * FormMaterialeBoccola.MaterialeBoccolaMultiplier;
                    if (FormTipoStelo.TipoSteloPassante == true)
                    {
                        MaterialeBoccolaPrice *= FormMaterialeBoccola.MaterialeBoccolaStPMultiplier;
                    }

                    if (FormMaterialeBoccola.onDiscount == true)
                    {
                        AttachmentGrossTotal += MaterialeBoccolaPrice;
                    }
                    else
                    {
                        RollerNetTotal += MaterialeBoccolaPrice;
                    }
                }

                //SteloProlungato
                if (InputSteloProlungato != 0)
                {
                    L6022_1SteloProlungato FormSteloProlungato = dbCtx.L6022_1SteloProlungato.Where(x => x.SteloProlungatoStartLength < InputSteloProlungato && x.SteloProlungatoEndLength > InputSteloProlungato).First();
                    decimal SteloProlungatoSlicing = InputSteloProlungato / FormSteloProlungato.SteloProlungatoSlice;
                    SteloProlungatoSlicing = Math.Ceiling(SteloProlungatoSlicing);
                    SteloProlungatoPrice = (FormBaseStd.BeyondRunPrice * FormSteloProlungato.SteloProlungatoRunPriceMultiplier) * SteloProlungatoSlicing;
                    if (FormSteloProlungato.onDiscount == true)
                    {
                        AttachmentGrossTotal += SteloProlungatoPrice;
                    }
                    else
                    {
                        RollerNetTotal += SteloProlungatoPrice;
                    }
                }
                //Connessioni Olio
                if (InputConnessioniOlioID != 0)
                {
                    L6022_1ConnessioniOlio FormConnessioniOlio = dbCtx.L6022_1ConnessioniOlio.Where(x => x.ConnessioniOlioID == InputConnessioniOlioID).First();
                    //prendere Magg Fissaggio per prezzo
                    ConnessioniOlioPrice = (FormFissaggioPrice.FissaggioMagg * FormConnessioniOlio.ConnessioniOlioMultiplier) * InputConnessioniOlioN;

                    if (FormConnessioniOlio.onDiscount == true)
                    {
                        AttachmentGrossTotal += ConnessioniOlioPrice;
                    }
                    else
                    {
                        RollerNetTotal += ConnessioniOlioPrice;
                    }
                }

                //Drenaggio
                if (InputDrenaggio != false)
                {
                    L6022_1Drenaggio FormDrenaggio = dbCtx.L6022_1Drenaggio.Where(x => x.inUse).First();
                    //aggiungo prezzo kit guarnizioni con basso attrito
                    L6022_1Guarnizione FormDrenaggioGuarnizione = dbCtx.L6022_1Guarnizione.Where(x => x.GuarnizioneID == FormDrenaggio.DrenaggioGuarnizioniID).First();
                    L6022_1GuarnizionePrice FormDrenaggioGuarnizionePrice = dbCtx.L6022_1GuarnizionePrice.Where(x => x.GuarnizionePriceCategoryID == FormDrenaggioGuarnizione.GuarnizionePriceCategoryID && x.GuarnizionePriceAlesaggioLength == FormAlesaggio.AlesaggioLength && x.GuarnizionePriceSteloValue == FormStelo.SteloValue).First();
                    DrenaggioPrice = (BaseRunStd * FormDrenaggio.DrenaggioPriceMultiplier) + FormDrenaggioGuarnizionePrice.GuarnizionePrice;
                    if (FormDrenaggio.onDiscount == true)
                    {
                        AttachmentGrossTotal += DrenaggioPrice;
                    }
                    else
                    {
                        RollerNetTotal += DrenaggioPrice;
                    }
                }

                //SnodoNonMantenuto
                if (InputSnodoNonMantenuto != false)
                {
                    L6022_1SnodoNonMantenuto FormSnodoNonMantenuto = dbCtx.L6022_1SnodoNonMantenuto.Where(x => x.SteloValue == FormStelo.SteloValue && x.AlesaggioLength == FormAlesaggio.AlesaggioLength).First();

                    SnodoNonMantenutoPrice = FormSnodoNonMantenuto.SnodoNonMantenutoPrice;

                    if (FormFissaggioPriceCategory.isSnodo == true)
                    {
                        SnodoNonMantenutoPrice += SnodoNonMantenutoPrice;
                    }

                    if (FormSnodoNonMantenuto.onDiscount == true)
                    {
                        AttachmentGrossTotal += SnodoNonMantenutoPrice;
                    }
                    else
                    {
                        RollerNetTotal += SnodoNonMantenutoPrice;
                    }
                }

                //DadiIncassati
                if (InputDadiIncassati != false)
                {
                    DadiIncassatiPrice = FormFissaggioPrice.FissaggioMagg;
                    if (FormFissaggioPrice.onDiscountMagg == true)
                    {
                        AttachmentGrossTotal += DadiIncassatiPrice;
                    }
                    else
                    {
                        RollerNetTotal += DadiIncassatiPrice;
                    }
                }

                //FilettaturaStelo
                if (InputFilettaturaSteloID != 0)
                {
                    L6022_1FilettaturaStelo FormFilettaturaStelo = dbCtx.L6022_1FilettaturaStelo.Where(x => x.FilettaturaSteloID == InputFilettaturaSteloID).First();
                    FilettaturaSteloPrice = FormFilettaturaStelo.FilettaturaSteloPrice;
                    if (FormFilettaturaStelo.onDiscount == true)
                    {
                        AttachmentGrossTotal += FilettaturaSteloPrice;
                    }
                    else
                    {
                        RollerNetTotal += FilettaturaSteloPrice;
                    }
                }

                //Minimess
                if (InputMinimessID != 0)
                {
                    L6022_1Minimess FormMinimess = dbCtx.L6022_1Minimess.Where(x => x.MinimessID == InputMinimessID).First();
                    MinimessPrice = FormMinimess.MinimessPrice * FormMinimess.MinimessPriceMultiplier;
                    if (FormMinimess.onDiscount == true)
                    {
                        AttachmentGrossTotal += MinimessPrice;
                    }
                    else
                    {
                        RollerNetTotal += MinimessPrice;
                    }
                }

                //ProtezioneSensore
                if (InputProtezioneSensore != false && InputSensoriInduttivi != 0)
                {
                    L6022_1ProtezioneSensore FormProtezioneSensore = dbCtx.L6022_1ProtezioneSensore.Where(x => x.inUse).First();
                    L6022_1SensoriInduttivi FormProtezioneSensoreInduttivi = dbCtx.L6022_1SensoriInduttivi.Where(x => x.SensoriInduttiviID == InputSensoriInduttivi).First();
                    ProtezioneSensorePrice = FormProtezioneSensore.ProtezioneSensorePrice * FormProtezioneSensoreInduttivi.NSensori;
                    if (FormProtezioneSensore.onDiscount == true)
                    {
                        AttachmentGrossTotal += ProtezioneSensorePrice;
                    }
                    else
                    {
                        RollerNetTotal += ProtezioneSensorePrice;
                    }
                }

                //PiastraCetop
                if (InputPiastraCetop != 0)
                {
                    L6022_1PiastraCetopPrice FormPiastraCetop = dbCtx.L6022_1PiastraCetopPrice.Where(x => x.PiastraCetopCategoryID == InputPiastraCetop && x.AlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                    PiastraCetopPrice = FormPiastraCetop.PiastraCetopPrice;
                    if (FormPiastraCetop.onDiscount == true)
                    {
                        AttachmentGrossTotal += PiastraCetopPrice;
                    }
                    else
                    {
                        RollerNetTotal += PiastraCetopPrice;
                    }
                }

                //ProtezioneTrasduttore
                if (InputProtezioneTrasduttore == true)
                {
                    L6022_1ProtezioneTrasduttore FormProtezioneTrasduttore = dbCtx.L6022_1ProtezioneTrasduttore.Where(x => x.inUse == true).First();
                    ProtezioneTrasduttorePrice = FormProtezioneTrasduttore.ProtezioneTrasduttorePrice * FormProtezioneTrasduttore.ProtezioneTrasduttoreMultiplier;
                    if (FormProtezioneTrasduttore.onDiscount == true)
                    {
                        AttachmentGrossTotal += ProtezioneTrasduttorePrice;
                    }
                    else
                    {
                        RollerNetTotal += ProtezioneTrasduttorePrice;
                    }
                    TransducerTotal += ProtezioneTrasduttorePrice;
                }

                //Verniciatura
                if (InputVerniciatura != 0)
                {
                    decimal CorsaVerniciatura = FormCorsa;
                    L6022_1Verniciatura FormVerniciatura = dbCtx.L6022_1Verniciatura.Where(x => x.VerniciaturaID == InputVerniciatura).First();
                    L6022_1VerniciaturaPrice FormVerniciaturaPrice = dbCtx.L6022_1VerniciaturaPrice.Where(x => x.AlesaggioLength == FormAlesaggio.AlesaggioLength && x.VerniciaturaID == InputVerniciatura).First();
                    VerniciaturaPrice = FormVerniciaturaPrice.VerniciaturaBasePrice;
                    CorsaVerniciatura -= FormVerniciatura.VerniciaturaBaseLength;

                    if (CorsaVerniciatura > 0)
                    {
                        decimal CorsaVerniciaturaRemains = CorsaVerniciatura / FormVerniciatura.VerniciaturaAfterBaseLength;
                        CorsaVerniciaturaRemains = Math.Ceiling(CorsaVerniciaturaRemains);
                        VerniciaturaPrice += CorsaVerniciaturaRemains * FormVerniciaturaPrice.VerniciaturaAfterBasePrice;
                    }

                    if (FormVerniciatura.onDiscount == true)
                    {
                        AttachmentGrossTotal += VerniciaturaPrice;
                    }
                    else
                    {
                        RollerNetTotal += VerniciaturaPrice;
                    }
                }
                //OpzioniCilindro
                if (InputOpzioniCilindro != 0)
                {
                    L6022_1OpzioniCilindro FormOpzioniCilindro = dbCtx.L6022_1OpzioniCilindro.Where(x => x.OpzioniCilindroID == InputOpzioniCilindro).First();

                    if (FormOpzioniCilindro.OpzioniCilindroCategoryID == 1)
                    {
                        OpzioniCilindroPrice = (RollerGrossTotal - TrasduttorePrice) * FormOpzioniCilindro.OpzioniCilindroMultiplier;
                    }
                    if (FormOpzioniCilindro.OpzioniCilindroCategoryID == 2)
                    {
                        OpzioniCilindroPrice = (BasePriceTotal + DistanzialiPrice) * FormOpzioniCilindro.OpzioniCilindroMultiplier;
                    }
                    if (FormOpzioniCilindro.OpzioniCilindroCategoryID == 3)
                    {
                        OpzioniCilindroPrice = (BasePriceTotal + DistanzialiPrice + FissaggioPrice) * FormOpzioniCilindro.OpzioniCilindroMultiplier;
                    }
                    if (FormOpzioniCilindro.OpzioniCilindroCategoryID == 5)
                    {
                        string AcronymFissaggio = FormOpzioniCilindro.OpzioniCilindroVar;
                        L6022_1TipoFissaggio FormOpzioniCilindroFissaggio = dbCtx.L6022_1TipoFissaggio.Where(x => x.TipoFissaggioAcronym == AcronymFissaggio).First();
                        L6022_1FissaggioPrice FormOpzioniCilindroFissaggioPrice = dbCtx.L6022_1FissaggioPrice.Where(x => x.FissaggioPriceCategoryID == FormOpzioniCilindroFissaggio.FissaggioPriceCategoryID && x.FissaggioAlesaggioLength == FormAlesaggio.AlesaggioLength).First();
                        OpzioniCilindroPrice = FormOpzioniCilindroFissaggioPrice.FissaggioPrice;
                    }

                    if (FormOpzioniCilindro.onDiscount == true)
                    {
                        AttachmentGrossTotal += OpzioniCilindroPrice;
                    }
                    else
                    {
                        RollerNetTotal += OpzioniCilindroPrice;
                    }
                }

                //Accessori Stelo/Cilindro
                if (InputAccessoriStelo != 0)
                {
                    try
                    {
                        L6022_1AccessoriCategory FormAccessoriSteloCategory = dbCtx.L6022_1AccessoriCategory.Where(x => x.AccessoriCategoryID == InputAccessoriStelo).First();
                        L6022_1AccessoriPrice FormAccessoriStelo = dbCtx.L6022_1AccessoriPrice.Where(x => x.AccessoriCategoryID == InputAccessoriStelo && x.AccessoriPriceAlesaggioLength == FormAlesaggio.AlesaggioLength && x.AccessoriPriceSteloValue == FormStelo.SteloValue).First();

                        AccessoriSteloPrice = FormAccessoriStelo.AccessoriPrice * FormAccessoriSteloCategory.AccessoriCategoryPriceMultiplier;
                        AttachmentGrossTotal += AccessoriSteloPrice;
                    }
                    catch (Exception e)
                    {
                        return "Prezzo non trovato per configurazione in Accessori Stelo";
                    }
                }

                if (InputAccessoriCilindro != 0)
                {
                    try
                    {
                        L6022_1AccessoriCategory FormAccessoriCilindroCategory = dbCtx.L6022_1AccessoriCategory.Where(x => x.AccessoriCategoryID == InputAccessoriCilindro).First();
                        L6022_1AccessoriPrice FormAccessoriCilindro = dbCtx.L6022_1AccessoriPrice.Where(x => x.AccessoriCategoryID == InputAccessoriCilindro && x.AccessoriPriceAlesaggioLength == FormAlesaggio.AlesaggioLength && x.AccessoriPriceSteloValue == FormStelo.SteloValue).First();

                        AccessoriCilindroPrice = FormAccessoriCilindro.AccessoriPrice * FormAccessoriCilindroCategory.AccessoriCategoryPriceMultiplier;
                        AttachmentGrossTotal += AccessoriCilindroPrice;
                    }
                    catch (Exception e)
                    {
                        return "Prezzo non trovato per configurazione in Accessori Cilindro";
                    }
                }
            }

            //Fine Calcoli

            //Somma Totali Lordi
            //
            //Somma Accessori Lorda
            AttachmentGrossTotal += DistanzialiPrice + SfiatiAriaPrice + SensoriInduttiviPrice + GuarnizionePrice;
            //Totale Lordo
            GrossTotal = RollerGrossTotal + AttachmentGrossTotal;
            //Sconto

            if (InputDiscount != 0)
            {
                decimal DiscountValue = GrossTotal * (InputDiscount / 100);
                GrossTotal -= DiscountValue;
            }

            if (InputDiscountPlus != 0)
            {
                decimal DiscountPlusValue = GrossTotal * (InputDiscountPlus / 100);
                GrossTotal -= DiscountPlusValue;
            }
            //Fine Sconto
            //Totali Netti
            RollerNetTotal += FissaggioPriceNoDiscount;
            AttachmentNetTotal = RollerNetTotal + AttachmentGrossTotal;
            //Fine Totali Netti
            FinalTotal = GrossTotal + RollerNetTotal;
            //ExtraPrezzo
            FinalTotal += InputExtraPrezzo;

            string FinalString = (RollerGrossTotal).ToString("0.00") + "-" + AttachmentNetTotal.ToString("0.00") + "-" + GuarnizioneKitRicambioPrice.ToString("0.00") + "-" + TransducerTotal.ToString("0.00") + "-" + FinalTotal.ToString("0.00");
            return FinalString;
        }

        [HttpPost]
        public ActionResult UpdateListAccessoriStelo(string FilettaturaSteloID)
        {
            Dictionary<string, string> articlesByCategory = new Dictionary<string, string>();
            int InputFilettaturaSteloID = Int32.Parse(FilettaturaSteloID);
            try
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    Boolean XOption = false;
                    if (FilettaturaSteloID != "")
                    {
                        var articleCFilettatura = dbCtx.L6022_1FilettaturaStelo.Where(x => x.FilettaturaSteloID == InputFilettaturaSteloID).First();
                        XOption = articleCFilettatura.XOption;
                    }
                    var articleCategories = dbCtx.L6022_1AccessoriCategory.Where(c => c.XOption == XOption && c.AccessoriGroupID == 1 && c.isActive == true).OrderBy(c => c.AccessoriCategoryCode).ToArray();

                    //Extract all articles matching a specific category
                    foreach (L6022_1AccessoriCategory category in articleCategories)
                    {
                        //L6022_1Alesaggio[] articlesBySelectedCategory = dbCtx.L6022_1Alesaggio.Where(a =>  == category.ArticleCategoryID && a.InUse).ToArray();
                        articlesByCategory.Add(category.AccessoriCategoryID.ToString(), category.AccessoriCategoryDesc + " / " + category.AccessoriCategoryDesc2);
                    }
                }
                return Content(JsonConvert.SerializeObject(new { status = true, values = articlesByCategory }));
            }

            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
            }
        }

        [HttpPost]
        public ActionResult UpdateListAccessoriCilindro(string FilettaturaSteloID)
        {
            Dictionary<string, string> articlesByCategory = new Dictionary<string, string>();
            int InputFilettaturaSteloID = Int32.Parse(FilettaturaSteloID);
            try
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    Boolean XOption = false;
                    if (FilettaturaSteloID != "")
                    {
                        var articleCFilettatura = dbCtx.L6022_1FilettaturaStelo.Where(x => x.FilettaturaSteloID == InputFilettaturaSteloID).First();
                        XOption = articleCFilettatura.XOption;
                    }
                    var articleCategories = dbCtx.L6022_1AccessoriCategory.Where(c => c.XOption == XOption && c.AccessoriGroupID == 2 && c.isActive == true).OrderBy(c => c.AccessoriCategoryCode).ToArray();
                    //Extract all articles matching a specific category
                    foreach (L6022_1AccessoriCategory category in articleCategories)
                    {
                        //L6022_1Alesaggio[] articlesBySelectedCategory = dbCtx.L6022_1Alesaggio.Where(a =>  == category.ArticleCategoryID && a.InUse).ToArray();
                        articlesByCategory.Add(category.AccessoriCategoryID.ToString(), category.AccessoriCategoryDesc + " / " + category.AccessoriCategoryDesc2);
                    }
                }
                return Content(JsonConvert.SerializeObject(new { status = true, values = articlesByCategory }));
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { status = false, message = ex.Message }));
            }
        }

        [HttpPost]
        public Boolean TransductorActivation(string SerieID)
        {
            Boolean Activation = false;
            if (SerieID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    L6022_1Serie FormSerie = dbCtx.L6022_1Serie.Where(x => x.SerieID == SerieID).First();
                    if (FormSerie.SerieTransducer == true)
                    {
                        Activation = true;
                    }
                }
            }
            return Activation;
        }

        [HttpPost]
        public int ActivateSfiati(string SerieID)
        {
            int Activation = 0;
            if (SerieID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    L6022_1Serie FormSerie = dbCtx.L6022_1Serie.Where(x => x.SerieID == SerieID).First();
                    if (FormSerie.SerieTransducer == true)
                    {
                        L6022_1SfiatiAria FormSfiati = dbCtx.L6022_1SfiatiAria.Where(x => x.NSfiati == 2).First();
                        Activation = FormSfiati.SfiatiAriaID;
                    }
                }
            }
            return Activation;
        }

        //Magneti 

        [HttpPost]
        public Boolean MagnetsActivation(string SerieID)
        {
            Boolean Activation = false;
            if (SerieID != "")
            {
                using (CMBContext dbCtx = new CMBContext())
                {
                    L6022_1Serie FormSerie = dbCtx.L6022_1Serie.Where(x => x.SerieID == SerieID).First();
                    if (FormSerie.SerieMagnets == true)
                    {
                        Activation = true;
                    }
                }
            }
            return Activation;
        }

        [HttpPost]
        public string CreateSendEmail(ViewModelL6022_1 FormData)
        {
            FileContentResult Csv = ExportCSV(FormData);
            string username = (string)Session["Username"];
            string UserEmail = "";
            //get Email
            using (CMBContext dbCtx = new CMBContext())
            {
                User UserInfo = dbCtx.Users.Where(x => x.UserID == username).First();
                UserEmail = UserInfo.Email ?? "-";
            }

            if (UserEmail == "-" || UserEmail == "")
            {
                return "<strong>Uh-Oh!</strong> Email dell'utente non trovata";
            }

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
            mail.From = new MailAddress("email@domain.it");
            mail.To.Add(UserEmail);
            mail.Subject = "Configurazione Cilindro";
            mail.Body = "In allegato il file CSV della configurazione.";

            Attachment attachment;
            Stream CsvStream = new MemoryStream(Csv.FileContents);
            attachment = new Attachment(CsvStream, Csv.FileDownloadName);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("email@domain.it", "password");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            return "<strong>Successo!</strong> Invio completato!";
        }
        public FileContentResult ExportCSV(ViewModelL6022_1 FormData)
        {
            string username = (string)Session["Username"];
            string organization = (string)Session["Organization"];
            int IntAlesaggioID = Int32.Parse(FormData.InputAlesaggioID);
            int InputSteloID = Int32.Parse(FormData.InputSteloID);
            int InputTipoSteloID = Int32.Parse(FormData.InputTipoSteloID);
            int InputTipoFissaggioID = Int32.Parse(FormData.InputTipoFissaggioID);

            //Trasduttore
            int InputTrasduttoreID = Int32.Parse(FormData.InputTrasduttoreID ?? "0");
            Boolean InputConnettoriTrasduttore = FormData.InputConnettoriTrasduttore;
            Boolean InputProtezioneTrasduttore = FormData.InputProtezioneTrasduttore;

            //SensoreStaffa
            int InputSensoreStaffa = FormData.InputMA3GA3;

            //Distanziali
            int InputDistanziali = Int32.Parse(FormData.InputDistanziali ?? "0");

            //Sfiati Aria
            int InputSfiatiAriaID = Int32.Parse(FormData.InputSfiatiAriaID ?? "0");

            //Sensori Induttivi
            int InputSensoriInduttiviID = Int32.Parse(FormData.InputSensoriInduttiviID ?? "0");

            //Guarnizioni
            int InputGuarnizioniID = Int32.Parse(FormData.InputGuarnizioniID ?? "0");

            //Materiale Stelo
            int InputMaterialeSteloID = Int32.Parse(FormData.InputMaterialeSteloID ?? "0");

            //Filettatura Stelo
            int InputFilettaturaStelo = Int32.Parse(FormData.InputFilettaturaStelo ?? "0");

            //Materiale Boccola
            int InputMaterialeBoccolaID = Int32.Parse(FormData.InputMaterialeBoccolaID ?? "0");

            //Minimess
            int InputMinimessID = Int32.Parse(FormData.InputMinimessID ?? "0");

            //Stelo Prolungato
            int InputSteloProlungato = FormData.InputSteloProlungato;

            //Priastra Cetop
            int InputPriastraCetop = Int32.Parse(FormData.InputPiastraCetopID ?? "0");

            //Opzioni Cilindro
            int InputOpzioniCilindro = Int32.Parse(FormData.InputOpzioniCilindroID ?? "0");

            //Verniciatura
            int InputVerniciatura = Int32.Parse(FormData.InputVerniciaturaID ?? "0");

            //Controflange
            Boolean InputControFlangia = FormData.InputControflangia;

            //Stelo Monolitico
            Boolean InputSteloMonolitico = FormData.InputSteloMonolitico;

            //Soffietto Stelo NBR
            Boolean InputSoffiettoStelo = FormData.InputSoffiettoStelo;

            //Snodo senza Manutenzione
            Boolean InputSnodoNonMantenuto = FormData.InputSnodoNonMantenuto;

            //Drenaggio
            Boolean InputDrenaggio = FormData.InputDrenaggio;

            //Dadi Incassati
            Boolean InputDadiIncassati = FormData.InputDadiIncassati;

            //Protezione Sensore
            Boolean InputProtezioneSensore = FormData.InputProtezioneSensore;

            //Accessori Stelo
            int InputAccessoriStelo = Int32.Parse(FormData.InputAccessoriSteloID ?? "0");

            //Accessori Cilindro
            int InputAccessoriCilindro = Int32.Parse(FormData.InputAccessoriCilindroID ?? "0");

            //Connessioni Olio
            int InputConnessioniOlio = Int32.Parse(FormData.InputConnessioniOlio ?? "0");

            using (CMBContext dbCtx = new CMBContext())
            {
                //L6020_Ser quotation = dbCtx.Quotations.Where(x => x.QuotationID == quotationID).First();
                L6022_1Alesaggio FormAlesaggio = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioID == IntAlesaggioID).First();
                L6022_1Stelo FormStelo = dbCtx.L6022_1Stelo.Where(x => x.SteloID == InputSteloID).First();
                L6022_1TipoStelo FormTipoStelo = dbCtx.L6022_1TipoStelo.Where(x => x.TipoSteloID == InputTipoSteloID).First();
                L6022_1Serie FormSerie = dbCtx.L6022_1Serie.Where(x => x.SerieID == FormData.InputSerieID).First();
                L6022_1TipoFissaggio FormTipoFissaggio = dbCtx.L6022_1TipoFissaggio.Where(x => x.TipoFissaggioID == InputTipoFissaggioID).First();
                L6022_1FissaggioPrice FormFissaggioPrice = dbCtx.L6022_1FissaggioPrice.Where(x => x.FissaggioPriceCategoryID == FormTipoFissaggio.FissaggioPriceCategoryID && x.FissaggioAlesaggioLength == FormAlesaggio.AlesaggioLength).First();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("sep=;"); //specifico ad Excel che il separatore è ";" perchè in CSV inglesi di default è "," mentre in italia il default è ";"
                sb.AppendLine("Generali;");
                sb.AppendLine("Utente;" + username);
                sb.AppendLine("Organizzazione;" + organization);
                sb.AppendLine($"");
                sb.AppendLine("Codice; " + FormData.InputCode);
                sb.AppendLine($"");
                sb.AppendLine("Base;");

                sb.AppendLine("Serie;" + FormData.InputSerieID);
                sb.AppendLine("Alesaggio;=\"" + Convert.ToInt32(FormAlesaggio.AlesaggioLength) + "\"");
                sb.AppendLine("Stelo;" + FormStelo.SteloAcronym);
                sb.AppendLine("Corsa;=\"" + Int32.Parse(FormData.InputCorsa) + "\"");
                sb.AppendLine("TipoStelo;" + FormTipoStelo.TipoSteloAcronym);
                sb.AppendLine("TipoFissaggio;=\"" + FormTipoFissaggio.TipoFissaggioAcronym + "\"");
                sb.AppendLine($"");

                //Opzionali

                //Sfiati Aria
                if (InputSfiatiAriaID != 0)
                {
                    L6022_1SfiatiAria FormSfiatiAria = dbCtx.L6022_1SfiatiAria.Where(x => x.SfiatiAriaID == InputSfiatiAriaID).First();
                    sb.AppendLine("SfiatiAria;=\"" + FormSfiatiAria.SfiatiAriaAcronym + "\"");
                }
                //Guarnizioni
                if (InputGuarnizioniID != 0)
                {
                    L6022_1Guarnizione FormGuarnizione = dbCtx.L6022_1Guarnizione.Where(x => x.GuarnizioneID == InputGuarnizioniID).First();
                    sb.AppendLine("Guarnizioni;" + FormGuarnizione.GuarnizioneAcronym);
                }
                //Distanziali
                if (InputDistanziali != 0)
                {
                    sb.AppendLine("Distanziali;=\"" + InputDistanziali + "\"");
                }
                //Minimess
                if (InputMinimessID != 0)
                {
                    L6022_1Minimess FormMinimess = dbCtx.L6022_1Minimess.Where(x => x.MinimessID == InputMinimessID).First();
                    sb.AppendLine("Minimess;" + FormMinimess.MinimessDesc);
                }
                //Filettatura Stelo
                if (InputFilettaturaStelo != 0)
                {
                    L6022_1FilettaturaStelo FormFilettaturaStelo = dbCtx.L6022_1FilettaturaStelo.Where(x => x.FilettaturaSteloID == InputFilettaturaStelo).First();
                    sb.AppendLine("FilettaturaStelo;" + FormFilettaturaStelo.FilettaturaSteloAcronym);
                }
                //Materiale Stelo
                if (InputMaterialeSteloID != 0)
                {
                    L6022_1MaterialeStelo FormMaterialeStelo = dbCtx.L6022_1MaterialeStelo.Where(x => x.MaterialeSteloID == InputMaterialeSteloID).First();
                    sb.AppendLine("MaterialeStelo;" + FormMaterialeStelo.MaterialeSteloDesc);
                }
                //Stelo Prolungato
                if (InputSteloProlungato != 0)
                {
                    sb.AppendLine("SteloProlungato;=\"" + InputSteloProlungato + "\"");
                }
                //SteloMonolitico
                if (InputSteloMonolitico)
                {
                    sb.AppendLine("SteloMonolitico;Vero");
                }
                //SoffiettoStelo
                if (InputSoffiettoStelo)
                {
                    sb.AppendLine("SoffiettoSteloNBR;Vero");
                }
                //Materiale Boccola
                if (InputMaterialeBoccolaID != 0)
                {
                    L6022_1MaterialeBoccola FormMaterialeBoccola = dbCtx.L6022_1MaterialeBoccola.Where(x => x.MaterialeBoccolaID == InputMaterialeBoccolaID).First();
                    sb.AppendLine("MaterialeBoccola;" + FormMaterialeBoccola.MaterialeBoccolaDesc);
                }

                //Drenaggio
                if (InputDrenaggio)
                {
                    sb.AppendLine("Drenaggio;Vero");
                }
                //DadiIncassati
                if (InputDadiIncassati)
                {
                    sb.AppendLine("DadiIncassati;Vero");
                }

                //Connessioni Olio
                if (InputConnessioniOlio != 0)
                {
                    L6022_1ConnessioniOlio FormConnessioniOlio = dbCtx.L6022_1ConnessioniOlio.Where(x => x.ConnessioniOlioID == InputConnessioniOlio).First();
                    sb.AppendLine("Connessioni Olio;" + FormConnessioniOlio.ConnessioniOlioDesc + "; Valore: " + FormData.InputConnessioniOlioN);
                }

                //Opzioni Cilindro
                if (InputOpzioniCilindro != 0)
                {
                    L6022_1OpzioniCilindro FormOpzioniCilindro = dbCtx.L6022_1OpzioniCilindro.Where(x => x.OpzioniCilindroID == InputOpzioniCilindro).First();
                    sb.AppendLine("OpzioniCilindro;" + FormOpzioniCilindro.OpzioniCilindroDesc);
                }
                //Piastra Cetop
                if (InputPriastraCetop != 0)
                {
                    L6022_1PiastraCetopCategory FormPiastraCetopCategory = dbCtx.L6022_1PiastraCetopCategory.Where(x => x.PiastraCetopCategoryID == InputPriastraCetop).First();
                    sb.AppendLine("PiastraCetop;" + FormPiastraCetopCategory.PiastraCetopCategoryDesc);
                }
                //Snodo senza Manutenzione
                if (InputSnodoNonMantenuto)
                {
                    sb.AppendLine("SnodoSenzaManutenzione;Vero");
                }
                //Controflange
                if (InputControFlangia)
                {
                    sb.AppendLine("Controflange;Vero");
                }
                //Verniciatura
                if (InputVerniciatura != 0)
                {
                    L6022_1Verniciatura FormVerniciatura = dbCtx.L6022_1Verniciatura.Where(x => x.VerniciaturaID == InputVerniciatura).First();
                    sb.AppendLine("Verniciatura;" + FormVerniciatura.VerniciaturaDesc);
                }
                sb.AppendLine($"");


                //Sensori Induttivi
                if (InputSensoriInduttiviID != 0)
                {
                    L6022_1SensoriInduttivi FormSensoriInduttivi = dbCtx.L6022_1SensoriInduttivi.Where(x => x.SensoriInduttiviID == InputSensoriInduttiviID).First();
                    sb.AppendLine("SensoriInduttivi;" + FormSensoriInduttivi.SensoriInduttiviAcronym);
                }
                //Protezione Sensore
                if (InputProtezioneSensore)
                {
                    sb.AppendLine("ProtezioneSensore;Vero");
                }
                sb.AppendLine($"");

                //Trasduttore
                if (InputTrasduttoreID != 0)
                {
                    sb.AppendLine($"");
                    L6022_1TrasduttorePrice FormTrasduttore = dbCtx.L6022_1TrasduttorePrice.Where(x => x.TrasduttorePriceID == InputTrasduttoreID).First();
                    sb.AppendLine("Trasduttore;" + FormTrasduttore.TrasduttorePriceDesc.Replace("\"", "_"));
                    if (InputConnettoriTrasduttore)
                    {
                        sb.AppendLine("Connettori;Vero");
                    }
                    if (InputProtezioneTrasduttore)
                    {
                        sb.AppendLine("Protezione;Vero");
                    }

                }
                //SteloStaffa
                if (InputSensoreStaffa != 0)
                {
                    sb.AppendLine("Sensore Staffa;=\"" + InputSensoreStaffa + "\"");
                }
                sb.AppendLine($"");

                //Accessori Stelo
                if (InputAccessoriStelo != 0)
                {
                    L6022_1AccessoriCategory FormAccessoriStelo = dbCtx.L6022_1AccessoriCategory.Where(x => x.AccessoriCategoryID == InputAccessoriStelo).First();
                    sb.Append("AccessoriStelo;" + FormAccessoriStelo.AccessoriCategoryCode + ";" + FormAccessoriStelo.AccessoriCategoryDesc);
                }

                //Accessori Cilindro
                if (InputAccessoriCilindro != 0)
                {
                    L6022_1AccessoriCategory FormAccessoriCilindro = dbCtx.L6022_1AccessoriCategory.Where(x => x.AccessoriCategoryID == InputAccessoriCilindro).First();
                    sb.Append("AccessoriCilindro;" + FormAccessoriCilindro.AccessoriCategoryCode + ";" + FormAccessoriCilindro.AccessoriCategoryDesc);
                }

                sb.AppendLine($"");
                sb.AppendLine($"");
                sb.AppendLine("Totali;");
                sb.AppendLine("Cilindro Netto;" + FormData.CilindroTotal);
                sb.AppendLine("Opzioni Netto;" + FormData.OpzioniTotal);
                if (InputTrasduttoreID != 0)
                {
                    sb.AppendLine("Trasduttore;" + FormData.TrasduttoreTotal);
                }
                sb.AppendLine("Totale Finale;" + FormData.FinalTotal);
                sb.AppendLine("Kit Guarnizioni ricambio;" + FormData.GuarnizioniKitTotal);
                string filename = FormData.InputCode.Replace("/", "_") + ".csv";
                return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", filename);
            }
        }
    }
}