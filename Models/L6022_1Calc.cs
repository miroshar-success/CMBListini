using CMBListini.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace CMBListini.Models
{
    [Table("L6022_1Calc")]
    public class L6022_1Calc
    {
        [Key]
        public int CalcID { get; set; }
        public String SerieID { get; set; }

        public ViewModelL6022_1 ToViewModel(int Discount, int DiscountPlus, bool cusomDiscountEnable, bool cusomDiscountMod, int customDiscount, int customDiscountExtra)
        {
            using (CMBContext2 dbCtx = new CMBContext2())
            {

                ViewModelL6022_1 vm6022_1 = new ViewModelL6022_1();

                var SerieTipi = dbCtx.L6022_1Serie.Where(x => x.SerieisActive).ToList().Select(x => new { Value = x.SerieID, Text = x.SerieID + " - " + x.SerieDesc }).ToList();
                var AlesaggioTipi = dbCtx.L6022_1Alesaggio.Where(x => x.AlesaggioisActive).ToList().Select(x => new { Value = x.AlesaggioID, Text = x.AlesaggioLength }).ToList();
                var SteloTipi = dbCtx.L6022_1TipoStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoSteloID, Text = x.TipoSteloAcronym + " - " + x.TipoSteloDesc }).ToList();
                var FissaggioTipi = dbCtx.L6022_1TipoFissaggio.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoFissaggioID, Text = x.TipoFissaggioAcronym + " - " + x.TipoFissaggioDesc }).ToList();

                //opzionali
                var TrasduttoreTipi = dbCtx.L6022_1TrasduttorePrice.Where(x => x.isActive).ToList().Select(x => new { Value = x.TrasduttorePriceID, Text = x.TrasduttorePriceDesc }).ToList();
                var SfiatiTipi = dbCtx.L6022_1SfiatiAria.Where(x => x.isActive).ToList().Select(x => new { Value = x.SfiatiAriaID, Text = x.SfiatiAriaAcronym + " - " + x.SfiatiAriaDesc }).ToList();
                var SensoriTipi = dbCtx.L6022_1SensoriInduttivi.Where(x => x.isActive).ToList().Select(x => new { Value = x.SensoriInduttiviID, Text = x.SensoriInduttiviAcronym + " - " + x.SensoriInduttiviDesc }).ToList();
                var GuarnizioniTipi = dbCtx.L6022_1Guarnizione.Where(x => x.isActive).ToList().Select(x => new { Value = x.GuarnizioneID, Text = x.GuarnizioneAcronym + " - " + x.GuarnizioneDesc }).ToList();
                var FilettaturaSteloTipi = dbCtx.L6022_1FilettaturaStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.FilettaturaSteloID, Text = x.FilettaturaSteloAcronym + " - " + x.FilettaturaSteloDesc }).ToList();
                var MaterialeBoccolaTipi = dbCtx.L6022_1MaterialeBoccola.Where(x => x.isActive).ToList().Select(x => new { Value = x.MaterialeBoccolaID, Text = x.MaterialeBoccolaDesc }).ToList();
                var MinimessTipi = dbCtx.L6022_1Minimess.Where(x => x.isActive).ToList().Select(x => new { Value = x.MinimessID, Text = x.MinimessDesc }).ToList();
                var VerniciaturaTipi = dbCtx.L6022_1Verniciatura.Where(x => x.isActive).ToList().Select(x => new { Value = x.VerniciaturaID, Text = x.VerniciaturaDesc }).ToList();
                var PiastraCetopTipi = dbCtx.L6022_1PiastraCetopCategory.Where(x => x.isActive).ToList().Select(x => new { Value = x.PiastraCetopCategoryID, Text = x.PiastraCetopCategoryDesc }).ToList();
                var OpzioniCilindroTipi = dbCtx.L6022_1OpzioniCilindro.Where(x => x.isActive).ToList().Select(x => new { Value = x.OpzioniCilindroID, Text = x.OpzioniCilindroDesc }).ToList();
                var ConnessioniOlioTipi = dbCtx.L6022_1ConnessioniOlio.Where(x => x.isActive).ToList().Select(x => new { Value = x.ConnessioniOlioID, Text = x.ConnessioniOlioDesc }).ToList();
                //

                //Aggiunte "z"
                var MaterialeStelo = dbCtx.L6022_1MaterialeStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.MaterialeSteloID, Text = x.MaterialeSteloDesc }).ToList();
                //

                //Accessori
                var AccessoriStelo = dbCtx.L6022_1AccessoriCategory.Where(x => x.isActive && x.AccessoriGroupID == 1 && x.XOption == false).OrderBy(x => x.AccessoriCategoryCode).ToList().Select(x => new { Value = x.AccessoriCategoryID, Text = x.AccessoriCategoryDesc + " " + x.AccessoriCategoryDesc2 }).ToList();
                var AccessoriCilindro = dbCtx.L6022_1AccessoriCategory.Where(x => x.isActive && x.AccessoriGroupID == 2 && x.XOption == false).OrderBy(x => x.AccessoriCategoryCode).ToList().Select(x => new { Value = x.AccessoriCategoryID, Text = x.AccessoriCategoryCode + " - " + x.AccessoriCategoryDesc + " " + x.AccessoriCategoryDesc2 }).ToList();
                //

                vm6022_1.ListSerie = new SelectList(SerieTipi, "Value", "Text", vm6022_1.InputSerieID);
                vm6022_1.ListAlesaggio = new SelectList("");
                vm6022_1.ListStelo = new SelectList("");
                vm6022_1.ListTipoStelo = new SelectList(SteloTipi, "Value", "Text", vm6022_1.InputTipoSteloID);
                vm6022_1.ListTipoFissaggio = new SelectList(FissaggioTipi, "Value", "Text", vm6022_1.InputTipoFissaggioID);

                //opzionali
                vm6022_1.ListTrasduttore = new SelectList(TrasduttoreTipi, "Value", "Text", vm6022_1.InputTrasduttoreID);
                vm6022_1.ListSfiatiAria = new SelectList(SfiatiTipi, "Value", "Text", vm6022_1.InputSfiatiAriaID);
                vm6022_1.ListSensoriInduttivi = new SelectList(SensoriTipi, "Value", "Text", vm6022_1.InputSensoriInduttiviID);
                vm6022_1.ListGuarnizioni = new SelectList(GuarnizioniTipi, "Value", "Text", vm6022_1.InputGuarnizioniID);
                vm6022_1.ListFilettaturaStelo = new SelectList(FilettaturaSteloTipi, "Value", "Text", vm6022_1.InputFilettaturaStelo);
                vm6022_1.ListMaterialeBoccola = new SelectList(MaterialeBoccolaTipi, "Value", "Text", vm6022_1.InputMaterialeBoccolaID);
                vm6022_1.ListMinimess = new SelectList(MinimessTipi, "Value", "Text", vm6022_1.InputMinimessID);
                vm6022_1.ListVerniciatura = new SelectList(VerniciaturaTipi, "Value", "Text", vm6022_1.InputVerniciaturaID);
                vm6022_1.ListPiastraCetop = new SelectList(PiastraCetopTipi, "Value", "Text", vm6022_1.InputPiastraCetopID);
                vm6022_1.ListOpzioniCilindro = new SelectList(OpzioniCilindroTipi, "Value", "Text", vm6022_1.InputOpzioniCilindroID);

                //
                //Aggiunte "z"
                vm6022_1.ListMaterialeStelo = new SelectList(MaterialeStelo, "Value", "Text", vm6022_1.InputMaterialeSteloID);
                vm6022_1.ListConnessioniOlio = new SelectList(ConnessioniOlioTipi, "Value", "Text", vm6022_1.InputConnessioniOlio);

                //

                //Accessori
                vm6022_1.ListAccessoriStelo = new SelectList(AccessoriStelo, "Value", "Text", vm6022_1.InputAccessoriSteloID);
                vm6022_1.ListAccessoriCilindro = new SelectList(AccessoriCilindro, "Value", "Text", vm6022_1.InputAccessoriCilindroID);

                //

                //Discount
                vm6022_1.InputDiscount = Discount.ToString();
                vm6022_1.InputDiscountPlus = DiscountPlus.ToString();
                //

                vm6022_1.CustomDiscountEnable = cusomDiscountEnable;
                vm6022_1.CustomDiscountMod = cusomDiscountMod;
                vm6022_1.CustomDiscount = customDiscount;
                vm6022_1.CustomExtraDiscount = customDiscountExtra;

                return vm6022_1;
            }

        }


    }
}