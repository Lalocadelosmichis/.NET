using System.Text.RegularExpressions;

namespace Rutificador
{
    public class Program
    {

        public static void Main(String[] args)
        {
            bool valido = false;
            String rut;

            while (!valido)
            {
                Console.WriteLine("Ingrese su rut INCLUYENDO EL GUION:");
                rut = Console.ReadLine();
                valido = ValidarRut(rut);
                if (!valido)
                {
                    Console.WriteLine("Intente denuevo\nRecuerde añadir el guion");
                }
                else
                {
                    Console.WriteLine("Rut ingresado correctamente!");
                    Console.WriteLine(rut);
                }
            }
        }

        public static bool ValidarRut(String _rut)
        {
            _rut = _rut.ToUpper();
            _rut = _rut.Replace(" ", "");
            _rut = _rut.Replace(".", "");
            Regex exp = new Regex("^([0-9]+-[0-9K])$");
            if (!exp.IsMatch(_rut) || _rut == "")
            {
                Console.WriteLine("Ese no es un formato de rut valido!");
                return false;
            }
            char dv = Convert.ToChar(_rut.Substring(_rut.Length - 1, 1));
            Console.WriteLine("el digito verificador es: " + dv);
            char[] corte = { '-' };
            String[] rutTemp = _rut.Split(corte);
            String rutR = RutVolteado(rutTemp);
            bool d = VerificarCodigo(rutR, dv);
            if (!d)
            {
                Console.WriteLine("el digito verificador no coincide");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool VerificarCodigo(String stringInvertido, char numeroVerificador)
        {
            int rutNum = 0;
            String rut2 = stringInvertido;
            int count = 0;
            int suma = 0;
            int suma2 = 0;

            for (int i = 0; i < rut2.Length; i++)
            {
                rutNum = int.Parse("" + rut2[i]);
                switch (count)
                {
                    case 0:
                        suma += rutNum * 2;
                        break;
                    case 1:
                        suma += rutNum * 3;
                        break;
                    case 2:
                        suma += rutNum * 4;
                        break;
                    case 3:
                        suma += rutNum * 5;
                        break;
                    case 4:
                        suma += rutNum * 6;
                        break;
                    case 5:
                        suma += rutNum * 7;
                        break;

                }
                count++;
                if (count > 5)
                {
                    count = 0;
                }

            }

            suma2 = (int)suma / 11;
            suma2 = (suma - (11 * suma2));
            suma = 11 - suma2;

            if (suma == 10 && numeroVerificador.Equals('K'))
            {
                return true;
            }
            else if (suma == 11 && numeroVerificador.Equals('0'))
            {
                return true;
            }

            if (int.TryParse("" + numeroVerificador, out int result))
            {
                if (suma == result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static String RutVolteado (string[] rut)
        {
            String _rut = rut[0];
            char[] arreglo = _rut.ToCharArray();
            String revertido = String.Empty;
            for (int i = arreglo.Length - 1; i >= 0; i--) 
            {
                revertido += arreglo[i];
            }
            return revertido;
        }
    }

}