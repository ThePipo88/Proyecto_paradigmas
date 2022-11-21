using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fungi.Validations
{
    class Atributes_Methods
    {
        ArrayList funciones = new ArrayList();
        Validations.Aritmetics aritmetics = new Validations.Aritmetics();
        String lineErrors = "";

        public String analisis(String codigo)
        {
            agregarFunciones(codigo);
            return lineErrors;
        }

        public void agregarFunciones(String codigo)
        {
            
            bool fnc = false;
            string[] words = codigo.Split('\n');
            ArrayList alm = null;

            for (int i = 0; i < words.Length; i++)
            {
                if (fnc == false)
                {
                    alm = new ArrayList();
                    string[] functions = words[i].Split(' ');
                    for (int j = 0; j < functions.Length; j++)
                    {
                        if(functions[j] == "function")
                        {
                            string[] nomb = functions[j + 1].Split('|');
                            alm.Add(nomb[0]);
                            if (j == 0)
                            {
                                alm.Add(false);
                                alm.Add("Null");
                                string[] gp = words[i].Split('|');
                                if (gp.Length > 1)
                                {
                                    alm.Add(gp[1]);
                                }
                                else
                                {
                                    alm.Add("NUll");
                                }
                            
                            }
                            else{
                                alm.Add(true);
                                alm.Add(functions[j - 1]);
                                string[] gp = words[i].Split('|');
                                if(gp.Length > 1)
                                {
                                    alm.Add(gp[1]);
                                }
                                else
                                {
                                    alm.Add("NUll");
                                }
                            }
                            alm.Add(i);
                            fnc = true;
                        }
                    }
                }
                else
                {
                    string[] functions = words[i].Split(' ');
                    if (functions[functions.Length-1] == "}.")
                    {
                        fnc = false;
                        alm.Add(i);
                        funciones.Add(alm);
                    }
                }
                
            }

            /*
            System.Diagnostics.Debug.WriteLine(funciones.Count);

            for (int i = 0; i < funciones.Count; i++)
            {
                ArrayList ar = (ArrayList)funciones[i];
                for (int j = 0; j < ar.Count; j++)
                {
                    System.Diagnostics.Debug.WriteLine(ar[j]);
                }
            }
            */
        }


        public Dictionary<string, object> buscarVariables(string code) {

            Dictionary<string, object> variables = new Dictionary<string, object>();

            string[] words = code.Split('\n');

            for (int i = 0; i < words.Length; i++)
            {
                string[] word = words[i].Split(' ');
                for (int j = 0; j < word.Length; j++)
                {

                        if (word[j] == "String")
                        {
                            ArrayList vrString = new ArrayList();
                            vrString.Add("String");
                            vrString.Add(extraerTexto(words[i]));
                            vrString.Add(i);
                            variables.Add(word[j + 1], vrString);
                        }
                        else if (word[j] == "Float")
                        {
                            ArrayList vrFloat = new ArrayList();
                            vrFloat.Add("Float");
                            vrFloat.Add(word[j + 3]);
                            vrFloat.Add(i);
                            variables.Add(word[j + 1], vrFloat);
                        }
                        else if (word[j] == "Flag")
                        {
                            ArrayList vrFlag = new ArrayList();
                            vrFlag.Add("Flag");
                            vrFlag.Add(word[j + 3]);
                            vrFlag.Add(i);
                            variables.Add(word[j + 1], vrFlag);
                        }
                        else if (word[j] == "Number")
                        {

                        ArrayList vrNumber = new ArrayList();

                        if (words[i].IndexOf("+") != -1 || words[i].IndexOf("-") != -1 || words[i].IndexOf("*") != -1 || words[i].IndexOf("/") != -1)
                         {

                            vrNumber.Add("Number");
                            if (words[i].IndexOf("+") != -1)
                            {
                                vrNumber.Add(aritmetics.operacionesAritmeticas(words[i], "+",variables));

                            }
                            else if (words[i].IndexOf("-") != -1)
                            {

                                vrNumber.Add(aritmetics.operacionesAritmeticas(words[i], "-", variables));

                            }
                            else if (words[i].IndexOf("*") != -1)
                            {

                                vrNumber.Add(aritmetics.operacionesAritmeticas(words[i], "*", variables));

                            }
                            else if (words[i].IndexOf("/") != -1)
                            {
                                vrNumber.Add(aritmetics.operacionesAritmeticas(words[i], "/", variables));
                            }
                            
                            vrNumber.Add(i);

                            //System.Diagnostics.Debug.WriteLine(vrNumber[1]);

                            variables.Add(word[j + 1].Trim(), vrNumber);
                        }
                        else
                          {
                            vrNumber.Add("Number");
                            int posString = word[j + 3].IndexOf('.');
                            vrNumber.Add(word[j + 3].Remove(posString));
                            vrNumber.Add(i);
                            variables.Add(word[j + 1].Trim(), vrNumber);
                          }
                        }

                        variables.Remove("function");
                }
            }

            return variables;
        }


        private string extraerTexto(string linea)
        {
            bool enc = false;
            string texto = "";
            for (int i = 0; i < linea.Length;i++)
            {
                if (enc)
                {
                    if (linea[i] != '"')
                    {
                        texto = texto + linea[i];
                    }
                    else
                    {
                        enc = false;
                    }
                    
                }
                else
                {
                    if (linea[i] == '"')
                    {
                        enc = true;
                    }
                }
                
            }
            return texto;
        }
    }
}
