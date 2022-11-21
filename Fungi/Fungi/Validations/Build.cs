using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace Fungi.Validations
{
    class Build
    {

        Validations.Atributes_Methods atr_mth = new Validations.Atributes_Methods();
        Validations.Comparation comparation = new Validations.Comparation();
        Dictionary<string, object> variables = new Dictionary<string, object>();
        Stack miPila = new Stack();
        String resultado = "";
        int i = 0;


        public String separarCodigo(String codigo)
        {

            resultado = "";
            variables = atr_mth.buscarVariables(codigo);

            //Encontrar el main
            int lineMain = 0;
            string[] words = codigo.Split('\n');

            for (int r = 0; r < words.Length; r++)
            {
                if (words[r].IndexOf("main") != -1)
                {
                    lineMain = r;
                }
            }

            //Vamos a buscar las funciones que contengan el main
            for (i = lineMain; i < words.Length; i++)
            {
                if (words[i].IndexOf("Print") != -1)
                {
                    if (extraerTexto(words[i]) != "")
                    {
                        //System.Diagnostics.Debug.WriteLine(extraerTexto(words[i]));
                        resultado = resultado + extraerTexto(words[i]);
                    }
                    else
                    {
                        string[] variable = words[i].Split('|');
                        ArrayList atr = extraerVariable(variable[1]);
                        resultado += atr[1] +"\n";
                    }
                    
                }
                else if (words[i].IndexOf("If") != -1)
                {
                    System.Diagnostics.Debug.WriteLine("If encontrado");
                    if(comparation.validarIf(words[i], variables))
                    {
                        //miPila.Push();
                        int cA = 1;
                        int cC = 0;
                        int j = i;
                        while (cA != cC)
                        {
                            j++;

                            if (words[j].IndexOf('}') != -1)
                            {
                                cC++;
                            }
                            else if (words[j].IndexOf('{') != -1)
                            {
                                cA++;
                            }
                        }
                        if (words[j].IndexOf("else") != -1 || words[j + 1].IndexOf("else") != -1)
                        {
                            miPila.Push("No");
                        }
                    }
                    else
                    {
                        int cA = 1;
                        int cC = 0;
                        while (cA != cC)
                        {
                            i++;
                            if (words[i].IndexOf('}') != -1)
                            {
                                cC++;
                            }
                            else if (words[i].IndexOf('{') != -1)
                            {
                                cA++;
                            }
                        }

                        if (words[i+1].IndexOf("else") != -1 || words[i].IndexOf("else") != -1)
                        {
                            miPila.Push("Si");
                        }
                    }
                }
                else if (words[i].IndexOf("else") != -1)
                {
                    string palabra = (string)miPila.Pop();

                    if (palabra == "No")
                    {
                        int cA = 1;
                        int cC = 0;
                        while (cA != cC)
                        {
                            i++;
                            if (words[i].IndexOf('{') != -1)
                            {
                                cA++;
                            }
                            else if (words[i].IndexOf('}') != -1)
                            {
                                cC++;
                            }
                        }
                    }
                }
                else if (words[i].IndexOf("For") != -1)
                {

                    string[] separate = words[i].Split('|');

                    string[] var = separate[1].Split(',');

                    int inicio = int.Parse(var[1]);

                    int fin = int.Parse(var[2]);

                    int cantidad = int.Parse(var[3]);

                    i++;

                    int LineaInicial = i;

                    int Lineafinal = getLastLine(i, words);

                    for (int j = inicio; j < fin; j = j + cantidad)
                    {

                        i = LineaInicial;

                        while (i <= Lineafinal)
                        {
                            analiceBucle(words);
                            i++;
                        }

                    }

                    i = Lineafinal;

                }
                else if (words[i].IndexOf("While") != -1)
                {
                    
                }
                else
                {
                    //Metodo en continuacion
                    
                    if (words[i].IndexOf("=") != -1 && words[i].IndexOf("Number") == -1)
                    {

                        string[] separate = words[i].Split('=');

                        

                        if (separate[1].IndexOf('+') != -1)
                        {                                           
                            string[] vrbls = separate[1].Split('+');

                            //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                            int res = int.Parse(buscarVariable(vrbls[0].Trim())) + int.Parse(buscarVariable(vrbls[1].Trim()));

                            sustituirValor(separate[0],res.ToString());

                        }
                        else if (separate[1].IndexOf('-') != -1)
                        {
                            string[] vrbls = separate[1].Split('-');

                            //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                            int res = int.Parse(buscarVariable(vrbls[0].Trim())) - int.Parse(buscarVariable(vrbls[1].Trim()));

                            sustituirValor(separate[0], res.ToString());

                        }
                        else if (separate[1].IndexOf('*') != -1)
                        {

                            string[] vrbls = separate[1].Split('*');

                            //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                            int res = int.Parse(buscarVariable(vrbls[0].Trim())) * int.Parse(buscarVariable(vrbls[1].Trim()));

                            sustituirValor(separate[0], res.ToString());

                        }
                        else if (separate[1].IndexOf('/') != -1)
                        {

                            string[] vrbls = separate[1].Split('/');

                            //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                            int res = int.Parse(buscarVariable(vrbls[0].Trim())) / int.Parse(buscarVariable(vrbls[1].Trim()));

                            sustituirValor(separate[0], res.ToString());

                        }
                        else
                        {
                            sustituirValor(separate[0], buscarVariable(separate[1].Trim()));
                        }
                    
                    }
  
                }
            }

            return resultado;
        }


        private int getLastLine(int i, string[] words)
        {
            int cA = 1;
            int cC = 0;
            while (cA != cC)
            {
                i++;
                if (words[i].IndexOf('{') != -1)
                {
                    cA++;
                }
                else if (words[i].IndexOf('}') != -1)
                {
                    cC++;
                }
            }

            return i;

        }

        private string buscarVariable(string numero)
        {

            int posString = numero.IndexOf('.');
            string v1 = numero;
            if (posString != -1)
            {
                v1 = numero.Remove(posString);
            }

            

            bool isNumeric = int.TryParse(v1, out _);

            //System.Diagnostics.Debug.WriteLine("Hola"+numero.Trim()+ "Hola");

            if (isNumeric)
            {

                return v1;
            }
            else
            {

                int num = 0;
                foreach (KeyValuePair<string, object> vr in variables)
                {
                    if (vr.Key.IndexOf(v1.Trim()) != -1)
                    {
                        ArrayList atr = (ArrayList)vr.Value;
                        //System.Diagnostics.Debug.WriteLine((string)atr[1]);
                        return (string)atr[1];
                    }
                }


                return numero;
            }
        }


        private bool sustituirValor(string codigo, string valor)
        {

            foreach (KeyValuePair<string, object> vr in variables)
            {
                if (codigo.IndexOf((string)vr.Key) != -1)
                {
                    ArrayList atr = (ArrayList)vr.Value;
                    string nombre = (string)vr.Key;
                    atr[1] = valor;
                    //System.Diagnostics.Debug.WriteLine((string)atr[1]);
                    variables.Remove(nombre);
                    variables.Add(nombre, atr);

                    return true;
                }
            }

            return false;
        }

        private ArrayList extraerVariable(string linea)
        {

            ArrayList atr = new ArrayList();

            foreach (KeyValuePair<string, object> author in variables)
            {
                if (author.Key == linea)
                {
                    atr = (ArrayList)author.Value;
                    return atr;
                }
            }

            return atr;
        }


        private string extraerTexto(string linea)
        {
            bool enc = false;
            string texto = "";
            for (int i = 0; i < linea.Length; i++)
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


        private void analiceBucle(string[] words)
        {
            if (words[i].IndexOf("Print") != -1)
            {
                if (extraerTexto(words[i]) != "")
                {
                    //System.Diagnostics.Debug.WriteLine(extraerTexto(words[i]));
                    resultado = resultado + extraerTexto(words[i]);
                }
                else
                {
                    string[] variable = words[i].Split('|');
                    ArrayList atr = extraerVariable(variable[1]);
                    resultado += atr[1] + "\n";
                }

            }
            else if (words[i].IndexOf("If") != -1)
            {
                if (comparation.validarIf(words[i], variables))
                {
                    //miPila.Push();
                    int cA = 1;
                    int cC = 0;
                    int j = i;
                    while (cA != cC)
                    {
                        j++;

                        if (words[j].IndexOf('}') != -1)
                        {
                            cC++;
                        }
                        else if (words[j].IndexOf('{') != -1)
                        {
                            cA++;
                        }
                    }
                    if (words[j].IndexOf("else") != -1 || words[j + 1].IndexOf("else") != -1)
                    {
                        miPila.Push("No");
                    }
                }
                else
                {
                    int cA = 1;
                    int cC = 0;
                    while (cA != cC)
                    {
                        i++;
                        if (words[i].IndexOf('}') != -1)
                        {
                            cC++;
                        }
                        else if (words[i].IndexOf('{') != -1)
                        {
                            cA++;
                        }
                    }

                    if (words[i + 1].IndexOf("else") != -1 || words[i].IndexOf("else") != -1)
                    {
                        miPila.Push("Si");
                    }
                }
            }
            else if (words[i].IndexOf("else") != -1)
            {
                string palabra = (string)miPila.Pop();

                if (palabra == "No")
                {
                    int cA = 1;
                    int cC = 0;
                    while (cA != cC)
                    {
                        i++;
                        if (words[i].IndexOf('{') != -1)
                        {
                            cA++;
                        }
                        else if (words[i].IndexOf('}') != -1)
                        {
                            cC++;
                        }
                    }
                }
            }
            else if (words[i].IndexOf("For") != -1)
            {

                string[] separate = words[i].Split('|');

                string[] var = separate[1].Split(',');

                int inicio = int.Parse(var[1]);

                int fin = int.Parse(var[2]);

                int cantidad = int.Parse(var[3]);

                i++;

                int Lineafinal = getLastLine(i, words);

                for (int j = inicio; j < fin; j = j + cantidad)
                {

                    while (i <= Lineafinal)
                    {
                        analiceBucle(words);
                        i++;
                    }

                }

                i = Lineafinal;

            }
            else if (words[i].IndexOf("While") != -1)
            {

            }
            else
            {
                //Metodo en continuacion

                if (words[i].IndexOf("=") != -1 && words[i].IndexOf("Number") == -1)
                {

                    string[] separate = words[i].Split('=');



                    if (separate[1].IndexOf('+') != -1)
                    {
                        string[] vrbls = separate[1].Split('+');

                        //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                        int res = int.Parse(buscarVariable(vrbls[0].Trim())) + int.Parse(buscarVariable(vrbls[1].Trim()));

                        sustituirValor(separate[0], res.ToString());

                    }
                    else if (separate[1].IndexOf('-') != -1)
                    {
                        string[] vrbls = separate[1].Split('-');

                        //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                        int res = int.Parse(buscarVariable(vrbls[0].Trim())) - int.Parse(buscarVariable(vrbls[1].Trim()));

                        sustituirValor(separate[0], res.ToString());

                    }
                    else if (separate[1].IndexOf('*') != -1)
                    {

                        string[] vrbls = separate[1].Split('*');

                        //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                        int res = int.Parse(buscarVariable(vrbls[0].Trim())) * int.Parse(buscarVariable(vrbls[1].Trim()));

                        sustituirValor(separate[0], res.ToString());

                    }
                    else if (separate[1].IndexOf('/') != -1)
                    {

                        string[] vrbls = separate[1].Split('/');

                        //System.Diagnostics.Debug.WriteLine(buscarVariable(vrbls[0].Trim())+ " "+ buscarVariable(vrbls[1].Trim()));
                        int res = int.Parse(buscarVariable(vrbls[0].Trim())) / int.Parse(buscarVariable(vrbls[1].Trim()));

                        sustituirValor(separate[0], res.ToString());

                    }
                    else
                    {
                        sustituirValor(separate[0], buscarVariable(separate[1].Trim()));
                    }

                }

            }
        }
    }
}
