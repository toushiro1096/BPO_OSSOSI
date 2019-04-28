using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class RCT3
    {
        [DataMember(Name = "CreditCardNumber", Order = 1, EmitDefaultValue = true)]
        public string CreditCardNumber { get; set; }
        [DataMember(Name = "CardValidUntil", Order = 2, EmitDefaultValue = true)]
        public string CardValidUntil { get; set; }
        [DataMember(Name = "VoucherNum", Order = 3, EmitDefaultValue = true)]
        public string VoucherNum { get; set; }
        [DataMember(Name = "CreditCard", Order = 4, EmitDefaultValue = true)]
        public string CreditCard { get; set; }
        [DataMember(Name = "Amount", Order = 5, EmitDefaultValue = true)]
        public double Amount { get; set; }
        [DataMember(Name = "AmountFC", Order = 6, EmitDefaultValue = true)]
        public double AmountFC { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 7, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public RCT3()
        {
            this.CreditCardNumber = string.Empty;
            this.CardValidUntil = string.Empty;
            this.VoucherNum = string.Empty;
            this.CreditCard = string.Empty;
            this.Amount = 0;
            this.AmountFC = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
