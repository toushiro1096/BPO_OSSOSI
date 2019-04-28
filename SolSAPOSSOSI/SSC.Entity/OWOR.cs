using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class OWOR
    {
        [DataMember(Name = "PostingDate", Order = 1, EmitDefaultValue = true)]
        public string PostingDate { get; set; }
        [DataMember(Name = "DueDate ", Order = 2, EmitDefaultValue = true)]
        public string DueDate { get; set; }
        [DataMember(Name = "ItemNo", Order = 3, EmitDefaultValue = true)]
        public string ItemNo { get; set; }
        [DataMember(Name = "PlannedQty", Order = 4, EmitDefaultValue = true)]
        public double PlannedQty { get; set; }
        [DataMember(Name = "WarehouseCode", Order = 5, EmitDefaultValue = true)]
        public string WarehouseCode { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 6, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "Comments", Order = 7, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListWOR1", Order = 8, EmitDefaultValue = true)]
        public List<WOR1> ListWOR1 { get; set; }
        [DataMember(Name = "DocDate", Order = 9, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 10, EmitDefaultValue = true)]
        public string DocDueDate { get; set; }

        public enum ISeries { S1200, S1500 };

        [DataMember(Name = "Series", Order = 12, EmitDefaultValue = true)]
        public ISeries Series { get; set; }

        public enum ISeries2 { S1200, S1500 };

        [DataMember(Name = "Series2", Order = 13, EmitDefaultValue = true)]
        public ISeries2 Series2 { get; set; }

        [DataMember(Name = "CardCode", Order = 14, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "CardName", Order = 15, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "Price", Order = 16, EmitDefaultValue = true)]
        public double Price { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 17, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 18, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        [DataMember(Name = "CodigoExterno2", Order = 19, EmitDefaultValue = true)]
        public string CodigoExterno2 { get; set; }
        public OWOR()
        {
            this.PostingDate = string.Empty;
            this.DueDate = string.Empty;
            this.ItemNo = string.Empty;
            this.PlannedQty = 0;
            this.WarehouseCode = string.Empty;
            this.JrnlMemo = string.Empty;
            this.Comments = string.Empty;
            this.ListWOR1 = new List<WOR1>();
            this.DocDate = string.Empty;
            this.DocDueDate = string.Empty;
            this.Series = new ISeries();
            this.Series2 = new ISeries2();
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.Price = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.CodigoExterno = string.Empty;
            this.CodigoExterno2 = string.Empty;
        }
    }
}
