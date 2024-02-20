using CMBListini.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace CMBListini.Models
{
    [Table("L6202_2Calc")]
    public class L6202_2Calc
    {
        [Key]
        public int CalcID { get; set; }
        public String SerieID { get; set; }

        public ViewModelL6020_2 ToViewModel(int Discount, int DiscountPlus, bool cusomDiscountEnable, bool cusomDiscountMod, int customDiscount, int customDiscountExtra)
        {
            using (CMBContext dbCtx = new CMBContext())
            {
                // CARICANO I COMBO
                ViewModelL6020_2 vm6020_2 = new ViewModelL6020_2();

                var SerieTipi = dbCtx.L6020_2Serie.Where(x => x.SerieisActive).ToList().Select(x => new { Value = x.SerieID, Text = x.SerieID + " - " + x.SerieDesc }).ToList();
                var AlesaggioTipi = dbCtx.L6020_2Alesaggio.Where(x => x.AlesaggioisActive).ToList().Select(x => new { Value = x.AlesaggioID, Text = x.AlesaggioLength }).ToList();
                var SteloTipi = dbCtx.L6020_2TipoStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoSteloID, Text = x.TipoSteloAcronym + " - " + x.TipoSteloDesc }).ToList();
                var FissaggioTipi = dbCtx.L6020_2TipoFissaggio.Where(x => x.isActive).ToList().Select(x => new { Value = x.TipoFissaggioID, Text = x.TipoFissaggioAcronym + " - " + x.TipoFissaggioDesc }).ToList();

                //opzionali
                var TrasduttoreTipi = dbCtx.L6020_2TrasduttorePrice.Where(x => x.isActive).ToList().Select(x => new { Value = x.TrasduttorePriceID, Text = x.TrasduttorePriceDesc }).ToList();
                var SfiatiTipi = dbCtx.L6020_2SfiatiAria.Where(x => x.isActive).ToList().Select(x => new { Value = x.SfiatiAriaID, Text = x.SfiatiAriaAcronym + " - " + x.SfiatiAriaDesc }).ToList();
                var SensoriTipi = dbCtx.L6020_2SensoriInduttivi.Where(x => x.isActive).ToList().Select(x => new { Value = x.SensoriInduttiviID, Text = x.SensoriInduttiviAcronym + " - " + x.SensoriInduttiviDesc }).ToList();
                var GuarnizioniTipi = dbCtx.L6020_2Guarnizione.Where(x => x.isActive).ToList().Select(x => new { Value = x.GuarnizioneID, Text = x.GuarnizioneAcronym + " - " + x.GuarnizioneDesc }).ToList();
                var FilettaturaSteloTipi = dbCtx.L6020_2FilettaturaStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.FilettaturaSteloID, Text = x.FilettaturaSteloAcronym + " - " + x.FilettaturaSteloDesc }).ToList();
                var MaterialeBoccolaTipi = dbCtx.L6020_2MaterialeBoccola.Where(x => x.isActive).ToList().Select(x => new { Value = x.MaterialeBoccolaID, Text = x.MaterialeBoccolaDesc }).ToList();
                var MinimessTipi = dbCtx.L6020_2Minimess.Where(x => x.isActive).ToList().Select(x => new { Value = x.MinimessID, Text = x.MinimessDesc }).ToList();
                var VerniciaturaTipi = dbCtx.L6020_2Verniciatura.Where(x => x.isActive).ToList().Select(x => new { Value = x.VerniciaturaID, Text = x.VerniciaturaDesc }).ToList();
                var PiastraCetopTipi = dbCtx.L6020_2PiastraCetopCategory.Where(x => x.isActive).ToList().Select(x => new { Value = x.PiastraCetopCategoryID, Text = x.PiastraCetopCategoryDesc }).ToList();
                var OpzioniCilindroTipi = dbCtx.L6020_2OpzioniCilindro.Where(x => x.isActive).ToList().Select(x => new { Value = x.OpzioniCilindroID, Text = x.OpzioniCilindroDesc }).ToList();
                var ConnessioniOlioTipi = dbCtx.L6020_2ConnessioniOlio.Where(x => x.isActive).ToList().Select(x => new { Value = x.ConnessioniOlioID, Text = x.ConnessioniOlioDesc }).ToList();

                //Aggiunte "z"
                var MaterialeStelo = dbCtx.L6020_2MaterialeStelo.Where(x => x.isActive).ToList().Select(x => new { Value = x.MaterialeSteloID, Text = x.MaterialeSteloDesc }).ToList();

                //Accessori
                var AccessoriStelo = dbCtx.L6020_2AccessoriCategory.Where(x => x.isActive && x.AccessoriGroupID == 1 && x.XOption == false).OrderBy(x => x.AccessoriCategoryCode).ToList().Select(x => new { Value = x.AccessoriCategoryID, Text = x.AccessoriCategoryDesc + " " + x.AccessoriCategoryDesc2 }).ToList();
                var AccessoriCilindro = dbCtx.L6020_2AccessoriCategory.Where(x => x.isActive && x.AccessoriGroupID == 2 && x.XOption == false).OrderBy(x => x.AccessoriCategoryCode).ToList().Select(x => new { Value = x.AccessoriCategoryID, Text = x.AccessoriCategoryCode + " - " + x.AccessoriCategoryDesc + " " + x.AccessoriCategoryDesc2 }).ToList();

                vm6020_2.ListSerie = new SelectList(SerieTipi, "Value", "Text", vm6020_2.InputSerieID);
                vm6020_2.ListAlesaggio = new SelectList("");
                vm6020_2.ListStelo = new SelectList("");
                vm6020_2.ListTipoStelo = new SelectList(SteloTipi, "Value", "Text", vm6020_2.InputTipoSteloID);
                vm6020_2.ListTipoFissaggio = new SelectList(FissaggioTipi, "Value", "Text", vm6020_2.InputTipoFissaggioID);

                //opzionali
                vm6020_2.ListTrasduttore = new SelectList(TrasduttoreTipi, "Value", "Text", vm6020_2.InputTrasduttoreID);
                vm6020_2.ListSfiatiAria = new SelectList(SfiatiTipi, "Value", "Text", vm6020_2.InputSfiatiAriaID);
                vm6020_2.ListSensoriInduttivi = new SelectList(SensoriTipi, "Value", "Text", vm6020_2.InputSensoriInduttiviID);
                vm6020_2.ListGuarnizioni = new SelectList(GuarnizioniTipi, "Value", "Text", vm6020_2.InputGuarnizioniID);
                vm6020_2.ListFilettaturaStelo = new SelectList(FilettaturaSteloTipi, "Value", "Text", vm6020_2.InputFilettaturaStelo);
                vm6020_2.ListMaterialeBoccola = new SelectList(MaterialeBoccolaTipi, "Value", "Text", vm6020_2.InputMaterialeBoccolaID);
                vm6020_2.ListMinimess = new SelectList(MinimessTipi, "Value", "Text", vm6020_2.InputMinimessID);
                vm6020_2.ListVerniciatura = new SelectList(VerniciaturaTipi, "Value", "Text", vm6020_2.InputVerniciaturaID);
                vm6020_2.ListPiastraCetop = new SelectList(PiastraCetopTipi, "Value", "Text", vm6020_2.InputPiastraCetopID);
                vm6020_2.ListOpzioniCilindro = new SelectList(OpzioniCilindroTipi, "Value", "Text", vm6020_2.InputOpzioniCilindroID);

                //Aggiunte "z"
                vm6020_2.ListMaterialeStelo = new SelectList(MaterialeStelo, "Value", "Text", vm6020_2.InputMaterialeSteloID);
                vm6020_2.ListConnessioniOlio = new SelectList(ConnessioniOlioTipi, "Value", "Text", vm6020_2.InputConnessioniOlio);

                //Accessori
                vm6020_2.ListAccessoriStelo = new SelectList(AccessoriStelo, "Value", "Text", vm6020_2.InputAccessoriSteloID);
                vm6020_2.ListAccessoriCilindro = new SelectList(AccessoriCilindro, "Value", "Text", vm6020_2.InputAccessoriCilindroID);

                //Discount
                vm6020_2.InputDiscount = Discount.ToString();
                vm6020_2.InputDiscountPlus = DiscountPlus.ToString();

                vm6020_2.CustomDiscountEnable = cusomDiscountEnable;
                vm6020_2.CustomDiscountMod = cusomDiscountMod;
                vm6020_2.CustomDiscount = customDiscount;
                vm6020_2.CustomExtraDiscount = customDiscountExtra;

                return vm6020_2;
            }

        }


    }
}