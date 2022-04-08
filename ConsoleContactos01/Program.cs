using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleContactos01
{
    class Program
    {
        static void Main(string[] args)
        {
            //AgregarUnRegistro();
            //BorrarRegistro();
            using (var context=new ContactosDbContext())
            {
                Console.Write("Ingrese el nro de Id a modificar:");
                var id = int.Parse(Console.ReadLine());
                var contactoEnDb = context.Contactos.SingleOrDefault(c => c.ContactoId == id);
                if (contactoEnDb!=null)
                {
                    contactoEnDb.Nombre = "Nuevo Nombre";
                    contactoEnDb.Apellido = "Nuevo Apellido";

                    context.SaveChanges();
                    Console.WriteLine("Registro Editado");
                }
                else
                {
                    Console.WriteLine("Id no encontrado en la tabla");
                }
            }
            ListarRegistros();

            Console.ReadLine();
        }

        private static void BorrarRegistro()
        {
            using (var context = new ContactosDbContext())
            {
                context.Database.Log = Console.WriteLine;
                var contactoEnDb = context.Contactos
                    .SingleOrDefault(c => c.ContactoId == 3);
                if (contactoEnDb != null)
                {
                    context.Contactos.Remove(contactoEnDb);
                    context.SaveChanges();
                    Console.WriteLine("Registro Borrado");
                }
                else
                {
                    Console.WriteLine("Id de registro inexistente");
                }
            }
        }

        private static void AgregarUnRegistro()
        {
            using (var context = new ContactosDbContext())
            {
                context.Database.Log = Console.WriteLine;
                var contacto = new Contacto()
                {
                    Nombre = "Ana",
                    Apellido = "Franck"
                };
                context.Contactos.Add(contacto);
                context.SaveChanges();
                Console.WriteLine("Registro Agregado");
            }
        }

        private static void ListarRegistros()
        {
            using (var context = new ContactosDbContext())
            {
                context.Database.Log = Console.WriteLine;
                var lista = context.Contactos
                                    .OrderBy(c=>c.ContactoId)
                                    .ToList();
                foreach (var contacto in lista)
                {
                    Console.WriteLine($"{contacto.ContactoId}-{contacto.Nombre} {contacto.Apellido}");
                }
            }
        }
    }
}
