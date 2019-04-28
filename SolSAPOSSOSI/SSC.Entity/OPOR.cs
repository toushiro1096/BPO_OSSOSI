using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class OPOR
    {
        [DataMember(Name = "vendorCode", Order = 1, EmitDefaultValue = true)]
        public string LicTradNum { get; set; }
        [DataMember(Name = "VendorName", Order = 2, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "CurrencyId", Order = 3, EmitDefaultValue = true)]
        public int CurrencyId { get; set; }
        [DataMember(Name = "TakeBase", Order = 4, EmitDefaultValue = true)]
        public decimal DocRate { get; set; }
        [DataMember(Name = "PoNumber", Order = 5, EmitDefaultValue = true)]
        public int DocNum { get; set; }
        [DataMember(Name = "PoDate", Order = 6, EmitDefaultValue = true)]
        public DateTime DocDate { get; set; }
        [DataMember(Name = "StatusCode", Order = 7, EmitDefaultValue = true)]
        public string DocStatus { get; set; }
        [DataMember(Name = "Buyer", Order = 8, EmitDefaultValue = true)]
        public string U_RDC_CoEC { get; set; }
        [DataMember(Name = "BuyerName", Order = 8, EmitDefaultValue = true)]
        public string U_RDC_CoECName { get; set; }
        [DataMember(Name = "CostTotal", Order = 9, EmitDefaultValue = true)]
        public decimal CostTotal { get; set; }
        [DataMember(Name = "DiscTotal", Order = 10, EmitDefaultValue = true)]
        public decimal DiscTotal { get; set; }
        [DataMember(Name = "Impuesto", Order = 11, EmitDefaultValue = true)]
        public decimal VatSum { get; set; }
        [DataMember(Name = "Total", Order = 12, EmitDefaultValue = true)]
        public decimal DocTotal { get; set; }
        [DataMember(Name = "DocEntry", Order = 13, EmitDefaultValue = true)]
        public int DocEntry { get; set; }
        [DataMember(Name = "Comment1", Order = 14, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListaDetalle", Order = 15, EmitDefaultValue = true)]
        public List<POR1> ListPOR1 { get; set; }
        [DataMember(Name = "DocDueDate", Order = 16, EmitDefaultValue = true)]
        public DateTime DocDueDate { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 17, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public OPOR()
        {
            this.LicTradNum = string.Empty;
            this.CardName = string.Empty;
            this.CurrencyId = 0;
            this.DocRate = 0;
            this.DocNum = 0;
            this.DocDate = DateTime.Now;
            this.DocStatus = string.Empty;
            this.U_RDC_CoEC = string.Empty;
            this.U_RDC_CoECName = string.Empty;
            this.CostTotal = 0;
            this.DiscTotal = 0;
            this.VatSum = 0;
            this.DocTotal = 0;
            this.DocEntry = 0;
            this.Comments = string.Empty;
            this.ListPOR1 = new List<POR1>();
            this.DocDueDate = DateTime.Now;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }

}
