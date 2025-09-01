public class Main
{
    private Disquera disquera;
    private Disco disco;

    public Main()
    {
        disco = new Disco();
        disquera = new Disquera();
    }

    public void Menu()
    {
        Int32 op = 0;
        do
        {
            Console.Clear();
            Console.WriteLine("MENU\n(1)Agregar Disco\n(2)Eliminar Disco\n(3)Mostrar Discos\n(0)Salir");
            op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    while (true)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("Para salir teclee 'enter'");
                            Console.WriteLine("Nombre del disco: ");
                            this.disco.sNombreDisco = Console.ReadLine();

                            if (this.disco.sNombreDisco == "enter") break;

                            Console.WriteLine("Cantidad de canciones: ");
                            this.disco.nCantidadCanciones = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Precio: ");
                            this.disco.dPrecio = Double.Parse(Console.ReadLine());
                            Console.WriteLine("Fecha de compra(dd/mm/yyyy):");
                            while (true)
                            {
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime fecha))
                                {
                                    this.disco.doFechaCompra = fecha;
                                    this.disquera.AgregarDisco(this.disco);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Fecha invalida");
                                    Console.ReadKey();
                                }
                            }
                        }
                        catch (System.Exception)
                        {
                            throw;
                        }
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Disco a Eliminar: ");
                    String sDisco = Console.ReadLine();
                    this.disquera.EliminarDisco(sDisco);
                    Console.Write("Presione cualquier tecla...");
                    Console.ReadKey();
                    break;
                case 3:
                    this.disquera.MostrarDiscos();
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        } while (op != 0);
    }
}