using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fungi.Validations
{
    class EndLine
    {
        ArrayList data = new ArrayList();
        ArrayList numLineA = new ArrayList();
        int numLine = 1;
        int num1 = 0;
        int num2 = 0;
        String lineErrors = "";



        public String analisis(String codigo)
        {
            analisisFinal(codigo);
            return lineErrors;
        }

        public void analisisFinal(String codigo)
        {

            numLine = 1;
            lineErrors = "";
            string[] words = codigo.Split('\n');



            for (int i = 0; i < words.Length; i++)
            {

                    if (words[i].Length > 1)
                    {
                    System.Diagnostics.Debug.WriteLine(words[i][(words[i].Length) - 1]);
                    if (words[i][(words[i].Length)-1] != '{')
                        {
                        
                        if (words[i][(words[i].Length) - 1] != '.')
                            {
                                lineErrors += (i+1) + " Error, se onmitió el caracter . en el código";
                            }
                        }
                    }
            }

        }

   }
}
