using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fungi.Validations
{
    class Aritmetics
    {

        public string operacionesAritmeticas(string linea, string operacion, Dictionary<string, object> variables)
        {

            string resultado = "";
            string[] sum = linea.Split('=');
            string[] numeros = sum[1].Split(operacion);
            int posString0 = numeros[0].IndexOf('.');
            int posString = numeros[1].IndexOf('.');

            

            if (operacion == "+")
            {
                int suma = esNumero(numeros[0], variables) + esNumero(numeros[1].Remove(posString), variables);
                resultado = suma.ToString();
            }
            else if (operacion == "-")
            {

                int resta = esNumero(numeros[0], variables) - esNumero(numeros[1].Remove(posString), variables);
                resultado = resta.ToString();
            }
            else if (operacion == "*")
            {

                int multiplicacion = esNumero(numeros[0], variables) * esNumero(numeros[1].Remove(posString), variables);
                resultado = multiplicacion.ToString();

            }
            else if (operacion == "/")
            {

                int division = esNumero(numeros[0], variables) / esNumero(numeros[1].Remove(posString), variables);
                resultado = division.ToString();

            }

            return resultado;
        }

        private int esNumero(string numero, Dictionary<string, object> variables)
        {

            bool isNumeric = int.TryParse(numero, out _);

            //System.Diagnostics.Debug.WriteLine("Hola"+numero.Trim()+ "Hola");

            if (isNumeric)
            {
                
                return int.Parse(numero);
            }
            else
            {

                int num = 0;
                foreach (KeyValuePair<string, object> vr in variables)
                {
                    if (vr.Key.IndexOf(numero.Trim()) != -1)
                    {
                        ArrayList atr = (ArrayList)vr.Value;
                        //System.Diagnostics.Debug.WriteLine((string)atr[1]);
                        num = int.Parse((string)atr[1]);
                    }
                }


                return num;
            }
        }


    }
}
