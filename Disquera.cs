public class Disquera
{
    private String sRuta = "archivo/archivo.txt";
    private Int32 nContador;
    private Disco[] discos;
    private Archivo archivo;

    public Disquera()
    {
        this.nContador = 0;
        this.archivo = new Archivo();
        if (File.Exists(this.sRuta))
        {
            this.ActualizarArreglo();
        }
        else
        {
            this.discos = new Disco[0];
        }
    }
    public void AgregarDisco(Disco disco)
    {
        if (this.discos.Length > 0)
        {
            archivo.EditarArchivo(this.sRuta, disco);
        }
        else
        {
            archivo.EditarArchivo(this.sRuta, disco, true);
        }
        this.ActualizarArreglo();
    }

    public void EliminarDisco(String sNombre)
    {
        Int32[] nIndices = this.BuscarDisco(sNombre);
        if (nIndices.Length == 0)
        {
            Console.WriteLine("Aún no se guarda ningún disco");
            return;
        }
        else if (nIndices.Length > 1)
        {
            Console.Clear();
            Console.WriteLine("MÁS DE UN DISCO COINCIDE CON EL NOMBRE");
            for (int i = 0; i < nIndices.Length; i++)
            {
                Console.WriteLine($"{i + 1}.-{this.discos[nIndices[i]].sNombreDisco}, {this.discos[nIndices[i]].nCantidadCanciones}");
            }
            Console.WriteLine("Escoja el disco a eliminar: ");
            try
            {
                Int32 op = Int32.Parse(Console.ReadLine());
                this.discos[nIndices[op - 1]].sNombreDisco = "<Null>";
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("=====PRESIONE CUALQUIER TECLA PARA VOLVER AL MENÚ=====");
                Console.ReadKey();
            }
        }
        else
        {
            this.discos[nIndices[0]].sNombreDisco = "<Null>";
        }

        Disco[] discos = new Disco[this.discos.Length - 1];
        Int32 j = 0;
        for (int i = 0; i < discos.Length; i++)
        {
            if (this.discos[i].sNombreDisco == "<Null>")
            {
                j++;
                discos[i] = this.discos[j];
            }
            else
            {
                discos[i] = this.discos[j];
            }
            j++;
        }
        this.archivo.RecrearArchivo(this.sRuta, discos);
        this.ActualizarArreglo();
    }

    public void MostrarDiscos()
    {
        Console.Clear();
        Console.WriteLine("Nombre del disco a buscar (Escriba 'todos' para mostrar todos los discos):");
        String sDisco = Console.ReadLine();
        if (sDisco == "todos")
        {
            for (int i = 0; i < this.discos.Length; i++)
            {
                Console.WriteLine($"Nombre: {this.discos[i].sNombreDisco} | Precio: ${this.discos[i].dPrecio} | Cantidad de canciones: {this.discos[i].nCantidadCanciones} | Fecha: {this.discos[i].doFechaCompra.ToString("dd/MM/yyyy")}");
            }
        }
        else
        {
            Int32[] nIndices = this.BuscarDisco(sDisco);
            if (nIndices.Length == 0)
            {
                Console.WriteLine("Aún no se guardan discos");
                return;
            }
            for (int i = 0; i < nIndices.Length; i++)
            {
                Console.WriteLine($"Nombre: {this.discos[nIndices[i]].sNombreDisco} | Precio: ${this.discos[nIndices[i]].dPrecio} | Cantidad de canciones: {this.discos[nIndices[i]].nCantidadCanciones} | Fecha: {this.discos[nIndices[i]].doFechaCompra.ToString("dd/MM/yyyy")}");
            }
        }
    }

    private Int32[] BuscarDisco(String sNombreDisco)
    {
        Int32 nCantidad = 1;
        String sIndices = "-1";

        if (this.discos.Length > 0)
        {
            do
            {
                for (int i = 0; i < this.discos.Length; i++)
                {
                    if (this.discos[i].sNombreDisco.Equals(sNombreDisco))
                    {
                        sIndices += $"|{i}";
                        nCantidad++;
                    }
                }
                if (sIndices == "-1")
                {
                    Console.WriteLine("NO EXISTE EL DISCO CON ESE NOMBRE");
                    Console.WriteLine("Introduzca el nombre nuevamente: ");
                    sNombreDisco = Console.ReadLine();
                }
            } while (sIndices == "-1");
        }
        else
        {
            return new int[0];
        }


        String[] sIndices2 = sIndices.Split("|");
        Int32[] nIndices = new int[nCantidad - 1];
        
        for (int i = 1; i < nCantidad; i++)
        {
            nIndices[i - 1] = Int32.Parse(sIndices2[i]);
        }
        return nIndices;
    }

    private void ActualizarArreglo()
    {
        try
        {
            String[] sDiscos = this.archivo.LeerArchivo(this.sRuta);
            String[] sDatos;
            Disco[] discos = new Disco[sDiscos.Length];
            for (int i = 0; i < discos.Length; i++)
            {
                sDatos = sDiscos[i].Split("|");
                discos[i].sNombreDisco = sDatos[0];
                discos[i].nCantidadCanciones = Int32.Parse(sDatos[1]);
                discos[i].dPrecio = Double.Parse(sDatos[2]);
                discos[i].doFechaCompra = DateTime.Parse(sDatos[3]);
            }
            this.discos = discos;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}