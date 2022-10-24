using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fungi.Validations
{
    public class Contenedores
    {

        ArrayList data = new ArrayList();
        ArrayList numLineA = new ArrayList();
        int numLine = 1;
        int num1 = 0;
        int num2 = 0;
        String lineErrors = "";



        public String analisis(String codigo)
        {
            analisisCorchetes(codigo);
            analisisParentesis(codigo);
            return lineErrors;
        }

        public void analisisCorchetes(String codigo)
        {

            data.Clear();
            numLineA.Clear();
            numLine = 1;
            num1 = 0;
            num2 = 0;
            lineErrors = "";

            for (int i = 0; i < codigo.Length; i++)
            {
                if (codigo[i] == '\n')
                {
                    numLine++;
                }

                if (codigo[i] == '{')
                {
                    data.Add(codigo[i]);
                    numLineA.Add(numLine);
                    num1++;
                }
                else if (codigo[i] == '}')
                {
                    data.Add(codigo[i]);
                    numLineA.Add(numLine);
                    num2++;
                }
            }

            if (data.Count % 2 != 0)
            {
                if (num1<num2)
                {
                    lineErrors += numLineA[numLineA.Count - 1].ToString() + " Error, se onmitió el caracter { en el código";
                }
                else
                {
                    lineErrors += numLineA[numLineA.Count - 1].ToString() + " Error, se onmitió el caracter } en el código";
                }
                
            }

        }

        public void analisisParentesis(String codigo)
        {
            data.Clear();
            numLineA.Clear();
            numLine = 1;
            num1 = 0;
            num2 = 0;
            lineErrors += "\n";

            for (int i = 0; i < codigo.Length; i++)
            {
                if (codigo[i] == '\n')
                {
                    numLine++;
                }

                if (codigo[i] == '|')
                {
                    data.Add(codigo[i]);
                    numLineA.Add(numLine);
                    num1++;
                }
            }

            if (data.Count % 2 != 0)
            {
               lineErrors += numLineA[numLineA.Count - 1].ToString() + " Error, se onmitió caracter | en el código";
            }
        }
    };
}
