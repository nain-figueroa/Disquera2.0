public class Archivo
{
    private StreamWriter escritor;

    public void EditarArchivo(String sRuta, Disco disco,Boolean bPrimerVez = false)
    {
        if (!bPrimerVez)
        {
            try
            {
                if (File.Exists(sRuta))
                {
                    this.escritor = new StreamWriter(sRuta, true);
                    this.escritor.WriteLine($"{disco.sNombreDisco}|{disco.nCantidadCanciones}|{disco.dPrecio}|{disco.doFechaCompra}");
                    this.escritor.Close();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        else
        {
            this.escritor = new StreamWriter(sRuta);
            this.escritor.WriteLine($"{disco.sNombreDisco}|{disco.nCantidadCanciones}|{disco.dPrecio}|{disco.doFechaCompra}");
            this.escritor.Close();
        }
    }
    public void RecrearArchivo(String sRuta, Disco[] discos)
    {
        String[] sDiscos = new string[discos.Length];
        for (int i = 0; i < discos.Length; i++)
        {
            if (discos[i].sNombreDisco != "<Null>")
            {
                sDiscos[i] = $"{discos[i].sNombreDisco}|{discos[i].nCantidadCanciones}|{discos[i].dPrecio}|{discos[i].doFechaCompra}";
            }
        }
        File.WriteAllLines(sRuta, sDiscos);
    }
    public String[] LeerArchivo(String sRuta)
    {
        String[] sDiscos = new string[1];
        try
        {
            if (File.Exists(sRuta))
            {
                sDiscos = File.ReadAllLines(sRuta);
            }
            else
            {
                Console.WriteLine("No se ha guardado nigún disco aún...\nPresione cualquier tecla para volver...");
                Console.ReadKey();
            }
        }
        catch (System.Exception)
        {

            throw;
        }
        return sDiscos;
    }
}