using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //int a = 5, b = 7;
            //IAnimal animal;
            //animal = new Perro();
            //Console.WriteLine(animal.sumar(a, b));
            //Console.WriteLine(animal.restar(a, b));
            //IAnimal gato = new Gato();
            //Console.WriteLine(gato.sumar(a, b));
            //Console.WriteLine(gato.restar(a, b));

            //IAnimal iguana = new Iguana();
            //Console.WriteLine(iguana.sumar(a, b));
            //Console.WriteLine(iguana.restar(a, b));

            //Persona persona = new Cliente();
            //persona = new Proveedor();
            //persona.FullName();

            string texto = "Hola Mundo, COMO ESTAS?";
            Ejemplo ejemplo = new Ejemplo();
            //Console.WriteLine(ejemplo.sumar(1, 2, 3, 4, 5, 6, 7, 8));
            //Console.WriteLine(ejemplo.CalcularLetras(texto, true, 'o', 'a', 'h'));

            Console.WriteLine(ejemplo.calcular(5, 7));
            Console.WriteLine(ejemplo.calcular(5, 7, true));
            Console.WriteLine(ejemplo.calcular(5, 7, 2));

            Console.ReadLine();
        }
    }

    interface IAnimal
    {
        int sumar(int a, int b);
        int restar(int a, int b);
    }
    class Perro : IAnimal
    {
        public int restar(int a, int b)
        {
            return a - b;
        }

        public int sumar(int a, int b)
        {
            return a + b;
        }
    }
    class Gato : IAnimal
    {
        int IAnimal.restar(int a, int b)
        {
            return a + b;
        }

        int IAnimal.sumar(int a, int b)
        {
            return a - b;
        }

        public int otros()
        {
            return 0;
        }
    }
    class Iguana : IAnimal
    {
        public int restar(int a, int b)
        {
            return (2 * a - b);
        }
        int IAnimal.sumar(int a, int b)
        {
            return (2 * a + b);
        }
    }
}
