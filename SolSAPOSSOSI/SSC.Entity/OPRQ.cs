using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    public class OPRQ
    {

        [DataMember(Name = "DocDate", Order = 1, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 2, EmitDefaultValue = true)]
        public string DocDueDate { get; set; }
        [DataMember(Name = "TaxDate", Order = 3, EmitDefaultValue = true)]
        public string TaxDate { get; set; }
        [DataMember(Name = "RequriedDate", Order = 4, EmitDefaultValue = true)]
        public string RequriedDate { get; set; }
        [DataMember(Name = "JournalMemo", Order = 5, EmitDefaultValue = true)]
        public string JournalMemo { get; set; }
        [DataMember(Name = "Comments", Order = 6, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListaDetalle", Order = 7, EmitDefaultValue = true)]
        public List<PRQ1> LstPRQ1 { get; set; }

        public enum ISeries { S1100, S1200, S1300, S1500, S1600, S1700 };

        [DataMember(Name = "Series", Order = 8, EmitDefaultValue = true)]
        public ISeries Series { get; set; }
        [DataMember(Name = "SlpCode", Order = 9, EmitDefaultValue = true)]
        public int SlpCode { get; set; }
        [DataMember(Name = "DocEntry", Order = 10, EmitDefaultValue = true)]
        public int DocEntry { get; set; }
        [DataMember(Name = "CardCode", Order = 12, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "CardName", Order = 13, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "TipoDoc", Order = 14, EmitDefaultValue = true)]
        public string U_LGS_TDOC { get; set; }
        [DataMember(Name = "SerieDoc", Order = 15, EmitDefaultValue = true)]
        public string U_LGS_SDOC { get; set; }
        [DataMember(Name = "NumDoc", Order = 16, EmitDefaultValue = true)]
        public string U_LGS_NUCE { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 17, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        [DataMember(Name = "DocNum", Order = 18, EmitDefaultValue = true)]
        public string DocNum { get; set; }
        [DataMember(Name = "NumAtCard", Order = 19, EmitDefaultValue = true)]
        public string NumAtCard { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 20, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public OPRQ()
        {
            this.DocDate = string.Empty;
            this.DocDueDate = string.Empty;
            this.TaxDate = string.Empty;
            this.RequriedDate = string.Empty;
            this.JournalMemo = string.Empty;
            this.Comments = string.Empty;
            this.LstPRQ1 = new List<PRQ1>();
            this.Series = new ISeries();
            this.SlpCode = 0;
            this.DocEntry = 0;
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.U_LGS_TDOC = string.Empty;
            this.U_LGS_SDOC = string.Empty;
            this.U_LGS_NUCE = string.Empty;
            this.CodigoExterno = string.Empty;
            this.DocNum = string.Empty;
            this.NumAtCard = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
