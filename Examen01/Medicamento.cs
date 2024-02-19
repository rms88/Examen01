using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen01
{
    internal class Medicamento
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public static int contador = 0;
        List<int> total = new List<int>();
        public Medicamento() { }
        public Medicamento(string nombre, int cantidad)
        {
            contador++;
            this.Codigo = contador;
            this.Nombre = nombre;
            this.Cantidad = cantidad;
        }
        public void agregarMedicamento()
        {
            Datos.medicamentos.Add(this);
        }
        public void mostrarMedicamentos()
        {
            Console.WriteLine("Los medicamentos que tenemos son los siguientes:\nCodigo        Nombre      Cantidad");
            for (int i = 0; i < Datos.medicamentos.Count; i++)
            {
                Console.WriteLine($"{Datos.medicamentos[i].Codigo}          {Datos.medicamentos[i].Nombre}          {Datos.medicamentos[i].Cantidad}");
            }
        }
        public int revisarInventario(int codigo)
        {
            int num = -1;
            if (Datos.medicamentos[codigo].Cantidad == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Insuficiente medicamento para asignar");
                num = -1;
            }
            else
            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Digite la cantidad a recetar: ");
                    num = Menu.isNumeric(Console.ReadLine());
                    if (num == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("------------El programa solo acepta numeros!------------\n");
                    }
                    else if (num > Datos.medicamentos[codigo].Cantidad)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cantidad de medicamentos insuficientes para recetar\n");
                        num = -1;
                    }
                    else if (num == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cantidad de medicamentos a asignar no puede ser '0'\n");
                        num = -1;
                    }
                } while (num == -1);
            }
            return num;
        }
        public void consultarMedicamentos()
        {
            for (int i = 0; i < Datos.pacientes.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Datos.pacientes[i].medicamentos[j] == null)
                    {
                        break;
                    }
                    else
                    {
                        int a = Datos.pacientes[i].medicamentos[j].Codigo;
                        medicamentosRep(a);
                    }
                }
            }
            imprimirMedicamentos();
        }
        public void medicamentosRep(int a)
        {
            bool noEncontrado = true;
            if (total.Count == 0)
            {
                total.Add(a);
            }
            else
            {
                for (int i = 0; i < this.total.Count; i++)
                {
                    if (this.total[i] == a)
                    {
                        noEncontrado = false;
                    }
                }
                if (noEncontrado)
                {
                    total.Add(a);
                }
            }
        }
        public void imprimirMedicamentos()
        {
            string todoMedicamentos = "Medicamentos recetados: ";
            for (int i = 0; i < total.Count; i++)
            {
                todoMedicamentos += Datos.medicamentos[total[i]-1].Nombre + ", ";
            }
            Console.WriteLine(todoMedicamentos);
        }
    }
}
