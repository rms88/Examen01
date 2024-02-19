using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Examen01
{
    internal class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string TipoSangre { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public Medicamento[] medicamentos { get; set; }
        public int contadorMedicamentos { get; set; }
        public Paciente()
        { 
        }
        public Paciente(int id, string nombre, string telefono, string tipoSangre, string direccion, DateTime fecha)
        { 
            this.Id = id;
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.TipoSangre = tipoSangre;
            this.Direccion = direccion;
            this.FechaNacimiento = fecha;
            calcularEdad(FechaNacimiento);
            this.medicamentos = new Medicamento[3];
            this.contadorMedicamentos = 0;
        }

        public void calcularEdad(DateTime nacimiento)
        {
            try
            {
                // Obtiene la fecha actual:
                DateTime fechaActual = DateTime.Today;

                // Comprueba que la se haya introducido una fecha válida; si 
                // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje 
                // de advertencia:
                if (nacimiento >= fechaActual)
                {
                    Console.WriteLine("La fecha de nacimiento es mayor que la actual.");

                }
                else
                {
                    int edad = fechaActual.Year - nacimiento.Year;

                    // Comprueba que el mes de la fecha de nacimiento es mayor 
                    // que el mes de la fecha actual:
                    if (nacimiento.Month > fechaActual.Month)
                    {
                        --edad;
                    }
                    this.Edad = edad;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error al calcular la edad");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }
        public void agregarPaciente()
        {
            Datos.pacientes.Add(this);
        }
        public int consultarPaciente(int id)
        {
            int encontrado = -1;
            for (int i = 0; i < Datos.pacientes.Count; i++)
            {
                if (Datos.pacientes[i].Id == id)
                {
                    encontrado = i; break;
                }
            }
            if (encontrado == -1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Paciente no encontrado.\n");
                Console.ForegroundColor = ConsoleColor.White;
                return encontrado;
            }
            else
            {
                return encontrado;
            }
        }
        public void agregarMedicamento(int index, int codigo, int cantidad)
        {
            //index > indice donde esta el paciente
            //codigo > indice donde esta el medicamento
            int a = Datos.medicamentos[codigo].Cantidad;
            int b = a - cantidad;
            Datos.medicamentos[codigo].Cantidad = b;

            // creo el paciente para modificar cantidad
            Paciente paciente = Datos.pacientes[index];
            //modifico cantidad
            Medicamento medicamento = new Medicamento();
            medicamento.Cantidad = cantidad;
            medicamento.Codigo = codigo + 1;
            medicamento.Nombre = Datos.medicamentos[codigo].Nombre;
            paciente.medicamentos[paciente.contadorMedicamentos] = medicamento;
            //remplazo con los datos correctos
            Datos.pacientes[index] = paciente;
            Datos.pacientes[index].contadorMedicamentos++;
        }
        public void mostrarPaciente()
        {
            edades();
            Console.WriteLine($"Pacientes por edad:\n0-10: {Datos.edad1}\n11-30: {Datos.edad2}\n31-50: {Datos.edad3}\nMayores de 51: {Datos.edad4}");
            var personasOrdenadasPorNombre = Datos.pacientes.OrderBy(p => p.Nombre).ToList();
            Console.WriteLine("Reporte de Pacientes:");
            foreach (var persona in personasOrdenadasPorNombre)
            {
                Console.WriteLine($"Nombre: {persona.Nombre}, Edad: {persona.Edad}");
            }
        }
        public void edades()
        {
            for (int i = 0; i < Datos.pacientes.Count; i++)
            {
                if (Datos.pacientes[i].Edad <= 10)
                {
                    Datos.edad1++;
                }
                else if (Datos.pacientes[i].Edad >= 11 && Datos.pacientes[i].Edad < 31)
                {
                    Datos.edad2++;
                }
                else if (Datos.pacientes[i].Edad >= 31 && Datos.pacientes[i].Edad < 51)
                {
                    Datos.edad3++;
                }
                else
                {
                    Datos.edad4++;
                }
            }
        }

    }
}
