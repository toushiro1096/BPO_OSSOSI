using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Ejemplo
    {
        public int sumar(params int[] parametros)
        {
            int resultado = 0;
            foreach (int valor in parametros)
            {
                resultado += valor;
            }
            return resultado;
        }
        public int CalcularLetras(string texto, bool CnvrtMns = false,
            params char[] parametros)
        {
            int resultado = 0;
            if (CnvrtMns)
            {
                texto = texto.ToLower();
            }
            foreach (char valor in parametros)
            {
                resultado += texto.Split(valor).Length - 1;
            }
            return resultado;
        }
        public int calcular(int a, int b, bool restar = false)
        {
            return restar ? a - b : a + b;
        }
        public int calcular(int a, int b, int c)
        {
            return a + b + c;
        }
    }
}
