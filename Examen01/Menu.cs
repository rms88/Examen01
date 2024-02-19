using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Examen01
{
    internal class Menu
    {
        Paciente paciente = new Paciente();
        Medicamento medicamento = new Medicamento(); 
        public void menuPrincipal()
        {
            string mensaje = "------------Menú Principal------------\n1- Agregar paciente.\n2- Agregar Medicamento.\n3- Asignar tratamiento.\n4- Consultas." +
            "\n5- Salir.";
            bool ciclo = true;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(mensaje);
                int opc = isNumeric(Console.ReadLine());
                int Id = -1;
                switch (opc)
                {
                    case -1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("------------El programa solo acepta números!------------\n");
                        break;
                    case 1:
                        agregarPasiente();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("------------Paciente agregado!------------\n");
                        break;
                    case 2:
                        agregarMedicamento();
                        break;
                    case 3:
                        if (Datos.pacientes.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("No hay Pacientes registrados!\n");
                        }
                        else
                        {
                            if (Datos.medicamentos.Count == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("No hay Tratamientos registrados!\n");
                            }
                            else
                            {
                                asignarTratamiento();
                            }
                        }
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        consultaReporte();
                        break;
                    case 5:
                        Console.WriteLine("Gracias por utilizar nuestro sistema!");
                        ciclo = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Numero incorrecto!\nPoner un numero valido.\n");
                        break;
                }
            } while (ciclo);
        }
        public void agregarPasiente()
        {
            Console.WriteLine("Digite su nombre:");
            string nombre = Console.ReadLine();
            int Id = -1;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Digite el # Cédula: ");
                Id = isNumeric(Console.ReadLine());
                if (Id == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------El programa solo acepta numeros!------------\n");
                }
            } while (Id == -1);
            Console.WriteLine("Digite su teléfono:");
            string telefono = Console.ReadLine();
            Console.WriteLine("Digite su tipo de sangre:");
            string tipoSangre = Console.ReadLine();
            Console.WriteLine("Digite su dirección:");
            string direccion = Console.ReadLine();
            Console.WriteLine("Digite la fecha de nacimiento:");
            DateTime nacimiento = DateTime.Parse(Console.ReadLine());
            Paciente p = new Paciente(Id, nombre, telefono, tipoSangre, direccion, nacimiento);
            p.agregarPaciente();
        }
        public void agregarMedicamento()
        {
            Console.WriteLine("Digite el nombre del medicamento:");
            string nombre = Console.ReadLine();
            int cantidad = -1;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Digite la cantidad:");
                cantidad = isNumeric(Console.ReadLine());
                if (cantidad == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------El programa solo acepta numeros!------------\n");
                }
            } while (cantidad == -1);
            Medicamento m = new Medicamento(nombre, cantidad);
            m.agregarMedicamento();
        }
        public void asignarTratamiento()
        {
            int Id = -1;
            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Digite el # Cédula del Paciente: ");
                Id = isNumeric(Console.ReadLine());
                if (Id == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("------------El programa solo acepta numeros!------------\n");
                }
            } while (Id == -1);
            int index = paciente.consultarPaciente(Id);
            if (index != -1)
            {
                if (Datos.pacientes[index].contadorMedicamentos < 3)
                {
                    Id = -1;
                    do
                    {
                        medicamento.mostrarMedicamentos();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Digite el # del Codigo: ");
                        Id = isNumeric(Console.ReadLine());
                        if (Id == -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("------------El programa solo acepta numeros!------------\n");
                        }
                        else if (Id > Datos.medicamentos.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("------------Codigo No existe!------------\n");
                            Id = -1;
                        }
                    } while (Id == -1);
                    int agrego = medicamento.revisarInventario(Id-1);
                    if (agrego != -1)
                    { 
                        paciente.agregarMedicamento(index, Id-1, agrego);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Tratamiento asignado");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("------------No se pueden agregar más de 3 medicamentos!------------\n");
                }
            }
        }
        public void consultaReporte()
        {
            Console.WriteLine($"Total de pacientes registrados: {Datos.pacientes.Count}");
            medicamento.consultarMedicamentos();
            paciente.mostrarPaciente();
        }
        public static int isNumeric(string str)
        {
            try
            {
                int a = int.Parse(str);
                return a;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
