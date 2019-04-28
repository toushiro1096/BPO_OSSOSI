using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class CampoUsuario
    {
        public enum m_tipos { alfanumerico, numerico, fecha, unidades, general };
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public m_tipos tipo { get; set; }
        public CampoUsuario()
        {
            Nombre = string.Empty;
            Valor = string.Empty;
            tipo = m_tipos.alfanumerico;
        }
        public static dynamic getValueOnType(CampoUsuario obj)
        {
            switch (obj.tipo)
            {
                case CampoUsuario.m_tipos.alfanumerico: return obj.Valor;
                case CampoUsuario.m_tipos.numerico: return double.Parse(obj.Valor);
                case CampoUsuario.m_tipos.unidades: return int.Parse(obj.Valor);
                case CampoUsuario.m_tipos.fecha: return Convert.ToDateTime(obj.Valor, new CultureInfo("ru-RU"));
                default: return null;
            }
        }

    }
}
