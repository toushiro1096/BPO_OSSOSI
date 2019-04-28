using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
 [DataContract]
    public class ORPD
    {
        [DataMember(Name = "CardCode", Order = 1, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "CardName", Order = 2, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "NumAtCard", Order = 3, EmitDefaultValue = true)]
        public string NumAtCard { get; set; }
        [DataMember(Name = "DocDate", Order = 4, EmitDefaultValue = true)]
        public DateTime DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 5, EmitDefaultValue = true)]
        public DateTime DocDueDate { get; set; }
        [DataMember(Name = "SalesPersonCode", Order = 6, EmitDefaultValue = true)]
        public int SalesPersonCode { get; set; }
        [DataMember(Name = "Comments", Order = 7, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListaDetalle", Order = 8, EmitDefaultValue = true)]
        public List<PDN1> ListRPD1 { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 9, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "Jrnlmemo", Order = 10, EmitDefaultValue = true)]
        public string Jrnlmemo { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 11, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }

        public ORPD()
        {
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.NumAtCard = string.Empty;
            this.DocDate = DateTime.Now;
            this.DocDueDate = DateTime.Now;
            this.SalesPersonCode = 0;
            this.Comments = string.Empty;
            this.ListRPD1 = new List<PDN1>();
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.Jrnlmemo = string.Empty;
            this.CodigoExterno = string.Empty;
        }
    }
}
