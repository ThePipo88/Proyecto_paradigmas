using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Fungi.Validations
{
    class Comparation
    {


        public bool validarIf(string linea, Dictionary<string, object> variables)
        {

            if (linea.IndexOf("And") != -1)
            {

                string[] condicion = linea.Split('|');
                string[] variablesAnt = condicion[1].Split("And");

                //System.Diagnostics.Debug.WriteLine(variablesAnt[0] + "-" + variablesAnt[1]);
                if (ComputeCondition(variablesAnt[0].Trim(), variables) && ComputeCondition(variablesAnt[1].Trim(), variables))
                {
                    System.Diagnostics.Debug.WriteLine("Ingreso And");
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No ingreso And");
                    return false;
                }
            }
            else if (linea.IndexOf("Or") != -1)
            {

                string[] condicion = linea.Split('|');
                string[] variablesAnt = condicion[1].Split("Or");

                System.Diagnostics.Debug.WriteLine(variablesAnt[0] + "-" + variablesAnt[1]);
                if (ComputeCondition(variablesAnt[0].Trim(), variables) || ComputeCondition(variablesAnt[1].Trim(),variables))
                {
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No ingreso Or");
                    return false;
                }

            }
            else
            {

                string[] condiciones = linea.Split('|');

                System.Diagnostics.Debug.WriteLine(condiciones[1]);

                if (ComputeCondition(condiciones[1], variables))
                {
                    System.Diagnostics.Debug.WriteLine("Ingreso");
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No ingreso");
                    return false;

                }
                
            }
        }

        private bool ComputeCondition(string value, Dictionary<string, object> variables)
        {

            if (value.IndexOf(">=") != -1)
            {
                string[] valores = value.Split(">=");

                if (int.Parse(esVariable(valores[0].Trim(), variables)) >= int.Parse(esVariable(valores[1].Trim(), variables)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (value.IndexOf("<=") != -1)
            {
                string[] valores = value.Split("<=");

                if (int.Parse(esVariable(valores[0].Trim(), variables)) <= int.Parse(esVariable(valores[1].Trim(), variables)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (value.IndexOf("<") != -1)
            {
                string[] valores = value.Split("<");

                if (int.Parse(esVariable(valores[0].Trim(), variables)) < int.Parse(esVariable(valores[1].Trim(), variables)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (value.IndexOf(">") != -1)
            {
                string[] valores = value.Split(">");

                if (int.Parse(esVariable(valores[0].Trim(), variables)) > int.Parse(esVariable(valores[1].Trim(), variables)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (value.IndexOf("==") != -1)
            {
                string[] valores = value.Split("==");

                if (esVariable(valores[0].Trim(), variables) == esVariable(valores[1].Trim(), variables))
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

        private string esVariable(string numero, Dictionary<string, object> variables)
        {

            //bool isNumeric = int.TryParse(numero, out _);

            int posStr = numero.IndexOf('.');
            if (posStr != -1)
            {
                numero = numero.Remove(posStr);
            }


            foreach (KeyValuePair<string, object> vr in variables)
               {
                  if (vr.Key.IndexOf(numero.Trim()) != -1)
                  {
                    ArrayList atr = (ArrayList)vr.Value;
                    string abc = (string)atr[1];
                    int posString = abc.IndexOf('.');
                    if (posString != -1)
                    {
                        abc = abc.Remove(posString);
                    }
                    System.Diagnostics.Debug.WriteLine(abc);
                    return abc;
                  }
              }


            return numero;
        }


        



    }

}
