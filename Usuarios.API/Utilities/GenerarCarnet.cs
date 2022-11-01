namespace Usuarios.API.Utilities
{
    public class GenerarCarnet
    {
        public string Generar(string nombre, string apellido, int edad)
        {
            //variable de devolucion
            string carnet = string.Empty;

            //Formamos una sola cadena
            string nombreCompleto = nombre + " " + apellido;

            //Arreglo con cada nombre y/o apellido separado
            string[] iniciales = nombreCompleto.Split(' ');

            //Variable para determinar cuantos caracteres tomar por palabra
            int cantidadCaracteresPorCarnet = 0;

            if (iniciales.Count()>2)
            {
                cantidadCaracteresPorCarnet = 1;
            }
            else
            {
                cantidadCaracteresPorCarnet = 2;
            }

            //Formamos nuestro carnet con la inicial o con las dos iniciales de cada nombre/apellido
            for (int i = 0; i < iniciales.Count(); i++)
            {
                carnet = carnet + iniciales[i].Substring(0, cantidadCaracteresPorCarnet);
            }

            //Agregamos el año de nacimiento aprox (Esto puede variar de la realidad)
            int añoNacimiento = int.Parse(DateTime.Now.Year.ToString());
            int numero = añoNacimiento - edad;

            //Agremos un numero aleatorio entre 0 y 100
            Random r = new Random();

            //Formamos nuestro carnet con las iniciales, el año de nacimiento y un número aleatorio
            carnet = carnet + numero.ToString()+r.Next(0, 100);

            return carnet;
        }
    }
}
