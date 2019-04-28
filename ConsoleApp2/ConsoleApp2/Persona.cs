using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public interface Persona
    {
        string FullName();
        bool FlgCompras();
    }
    public class Cliente : Persona
    {
        public string PrimerNombre { get; set; }
        public string PrimerApellido { get; set; }
        bool Persona.FlgCompras()
        {
            return true;
        }

        string Persona.FullName()
        {
            return PrimerNombre + " " + PrimerApellido;
        }
    }
    public class Proveedor : Persona
    {
        public string RazonSocial { get; set; }
        bool Persona.FlgCompras()
        {
            return false;
        }

        string Persona.FullName()
        {
            return RazonSocial;
        }
    }
}
