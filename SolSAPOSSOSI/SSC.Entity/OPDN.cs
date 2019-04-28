using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class OPDN
    {
        [DataMember(Name = "CardCode", Order = 1, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "CardName ", Order = 2, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "TaxDate", Order = 3, EmitDefaultValue = true)]
        public string TaxDate { get; set; }
        [DataMember(Name = "DocDate", Order = 4, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 5, EmitDefaultValue = true)]
        public string DocDueDate { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 6, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "Comments", Order = 7, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListaDetalle", Order = 8, EmitDefaultValue = true)]
        public List<PDN1> ListPDN1 { get; set; }
        [DataMember(Name = "TipoDoc", Order = 9, EmitDefaultValue = true)]
        public string U_LGS_TDOC { get; set; }
        [DataMember(Name = "SerieDoc", Order = 10, EmitDefaultValue = true)]
        public string U_LGS_SDOC { get; set; }
        [DataMember(Name = "NumDoc", Order = 11, EmitDefaultValue = true)]
        public string U_LGS_NUCE { get; set; }

        public enum ISeries { S1100, S1200, S1300, S1500, S1600, S1700 };

        [DataMember(Name = "Series", Order = 12, EmitDefaultValue = true)]
        public ISeries Series { get; set; }

        public enum TIPOMOV { COM, DEG };
        [DataMember(Name = "TipoMovimiento", Order = 13, EmitDefaultValue = true)]
        public TIPOMOV U_LGS_TIPO { get; set; }
        [DataMember(Name = "DocCurrency", Order = 14, EmitDefaultValue = true)]
        public string DocCurrency { get; set; }
        [DataMember(Name = "SlpCode", Order = 15, EmitDefaultValue = true)]
        public int SlpCode { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 16, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 18, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        public OPDN()
        {
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.TaxDate = string.Empty;
            this.DocDate = string.Empty;
            this.DocDueDate = string.Empty;
            this.JrnlMemo = string.Empty;
            this.ListPDN1 = new List<PDN1>();
            this.U_LGS_TDOC = string.Empty;
            this.U_LGS_SDOC = string.Empty;
            this.U_LGS_NUCE = string.Empty;
            this.Series = new ISeries();
            this.U_LGS_TIPO = new TIPOMOV();
            this.DocCurrency = string.Empty;
            this.SlpCode = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.CodigoExterno = string.Empty;
        }
    }
}
