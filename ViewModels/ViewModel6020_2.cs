using CMBListini.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMBListini.ViewModels
{
    public class ViewModelL6020_2
    {
        // gestione campo per inserimento manuale del codice
        public string CodiceDigitato { get; set; }

        //Organization
        public Organization OrganizationData { get; set; }
        public int OrganizationDiscount { get; set; }
        public int OrganizationDiscountPlus { get; set; }
        //
        //Calcolo
        public string InputCode { get; set; }
        //
        //Discount
        [DisplayName("Sconto")]
        [Range(0, 100)]
        public string InputDiscount { get; set; }
        [DisplayName("Extra Sconto")]
        [Range(0, 100)]
        public string InputDiscountPlus { get; set; }
        //

        //MaggiorazioneTotale
        [DisplayName("Extra Prezzo")]
        [Required(ErrorMessage = "Il campo deve almeno essere 0")]
        [DataType(DataType.Currency, ErrorMessage = "Il campo deve essere un numero")]
        public decimal inputExtraPrezzo { get; set; }
        //

        //base
        [DisplayName("Serie")]
        [Required(ErrorMessage = "Campo richiesto")]
        public string InputSerieID { get; set; }

        [DisplayName("Alesaggio")]
        [Required(ErrorMessage = "Campo richiesto")]
        public string InputAlesaggioID { get; set; }

        [DisplayName("Stelo")]
        [Required(ErrorMessage = "Campo richiesto")]
        public string InputSteloID { get; set; }

        [DisplayName("Corsa")]
        [Required(ErrorMessage = "Campo richiesto")]
        [Range(1, 5000)]
        public string InputCorsa { get; set; }

        [DisplayName("Tipo Stelo")]
        [Required(ErrorMessage = "Campo richiesto")]
        public string InputTipoSteloID { get; set; }

        [DisplayName("Tipo Fissaggio")]
        [Required(ErrorMessage = "Campo richiesto")]
        public string InputTipoFissaggioID { get; set; }
        //fine base

        //opzionali

        [DisplayName("Trasduttore")]
        public string InputTrasduttoreID { get; set; }

        [DisplayName("Connettori Trasduttore")]
        public Boolean InputConnettoriTrasduttore { get; set; }

        [DisplayName("Protezione Trasduttore")]
        public Boolean InputProtezioneTrasduttore { get; set; }

        //Magneti

        [DisplayName("Sensore e Staffa")]
        [Range(0, 5)]
        public int InputMA3GA3 { get; set; }

        //[DisplayName("MA3GA4")]
        //[Range(0, 5)]
        //public int InputMA3GA4 { get; set; }

        //

        [DisplayName("Distanziali")]
        [Range(0, 9)]
        public string InputDistanziali { get; set; }

        [DisplayName("Sfiati Aria")]
        public string InputSfiatiAriaID { get; set; }

        [DisplayName("Sensori Induttivi")]
        public string InputSensoriInduttiviID { get; set; }

        [DisplayName("Guarnizioni")]
        public string InputGuarnizioniID { get; set; }

        [DisplayName("Materiale Boccola")]
        public string InputMaterialeBoccolaID { get; set; }

        [DisplayName("Dadi Incassati")]
        public Boolean InputDadiIncassati { get; set; }

        [DisplayName("Minimess")]
        public string InputMinimessID { get; set; }

        [DisplayName("Verniciatura")]
        public string InputVerniciaturaID { get; set; }

        [DisplayName("Piastra Cetop")]
        public string InputPiastraCetopID { get; set; }


        //fine opzionali

        //Aggiunte
        [DisplayName("Controflange")]
        public Boolean InputControflangia { get; set; }

        [DisplayName("Stelo Monolitico")]
        public Boolean InputSteloMonolitico { get; set; }

        [DisplayName("Soffietto Stelo NBR")]
        public Boolean InputSoffiettoStelo { get; set; }

        [DisplayName("Drenaggio")]
        public Boolean InputDrenaggio { get; set; }

        [DisplayName("Snodo senza manutenzione")]
        public Boolean InputSnodoNonMantenuto { get; set; }

        [DisplayName("Protezione Sensore")]
        public Boolean InputProtezioneSensore { get; set; }

        [DisplayName("Materiale Stelo")]
        public string InputMaterialeSteloID { get; set; }

        [DisplayName("Filettatura Stelo")]
        public string InputFilettaturaStelo { get; set; }

        [DisplayName("Opzioni Cilindro")]
        public string InputOpzioniCilindroID { get; set; }

        [DisplayName("Stelo Prolungato")]
        [Range(0, 2000)]
        public int InputSteloProlungato { get; set; }

        [DisplayName("Connessioni Olio non Std")]
        public string InputConnessioniOlio { get; set; }

        [DisplayName("N° Connessioni Olio non Std")]
        [Range(0, 4)]
        public int InputConnessioniOlioN { get; set; }
        //
        //Accessori
        [DisplayName("Accessori Stelo")]
        public string InputAccessoriSteloID { get; set; }

        [DisplayName("Accessori Cilindro")]
        public string InputAccessoriCilindroID { get; set; }

 
        [Display(Name = "Codiceimmesso")]
        public string Codiceimmesso { get; set; }

        public SelectList ListSerie { get; set; }
        public SelectList ListAlesaggio { get; set; }
        public SelectList ListStelo { get; set; }
        public SelectList ListTipoStelo { get; set; }
        public SelectList ListTipoFissaggio { get; set; }

        //opzionali
        public SelectList ListTrasduttore { get; set; }
        public SelectList ListSfiatiAria { get; set; }
        public SelectList ListSensoriInduttivi { get; set; }
        public SelectList ListGuarnizioni { get; set; }
        public SelectList ListFilettaturaStelo { get; set; }
        public SelectList ListMaterialeBoccola { get; set; }
        public SelectList ListMinimess { get; set; }
        public SelectList ListVerniciatura { get; set; }
        public SelectList ListPiastraCetop { get; set; }
        public SelectList ListOpzioniCilindro { get; set; }
        public SelectList ListConnessioniOlio { get; set; }

        //


        //Aggiunte "z"
        public SelectList ListMaterialeStelo { get; set; }

        //
        //Accessori
        public SelectList ListAccessoriStelo { get; set; }
        public SelectList ListAccessoriCilindro { get; set; }
        //

        //Totali

        //public string InputCorsa { get; set; }
        public decimal CilindroTotal { get; set; }
        public decimal OpzioniTotal { get; set; }
        public decimal TrasduttoreTotal { get; set; }
        public decimal GuarnizioniKitTotal { get; set; }
        public decimal FinalTotal { get; set; }

        public L6202_2Calc UpdateModelFromViewModel(L6202_2Calc Model, CMBContext DbCtx)
        {

            Model.SerieID = this.InputSerieID;
            return Model;

        }
    }
}