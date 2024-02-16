using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMBListini.Models
{
    [Table("PriceLists")]
    public class PriceList
    {
        [Key]
        public int PriceListID { get; set; }
        public string PriceListDesc { get; set; }
        public Boolean isActive { get; set; }
        public string LandingPage { get; set; }

        //public ViewModelQuotationAttachment ToViewModel()
        //{
        //    using (QuotationContext dbCtx = new QuotationContext())
        //    {
        //        ViewModelQuotationAttachment vmAttachment = new ViewModelQuotationAttachment();

        //        return vmAttachment;
        //    }
        //}

    }
}