﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fungi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Validations.Contenedores contenedores = new Validations.Contenedores();
        public String numLine = "1\r\n";
        public int count = 1;


        public MainWindow()
        {
            this.RemoveHandler(KeyDownEvent, new KeyEventHandler(fileCodeSpace_KeyDown));

            this.AddHandler(KeyDownEvent, new KeyEventHandler(fileCodeSpace_KeyDown), true);
            InitializeComponent();
            fileLineSpace.Text = numLine;
        }


        private void opNuevo_click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, fileCodeSpace.Text);

        }

        private void opAbrir_click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document"; // Default file name
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                string[] lines = System.IO.File.ReadAllLines(filename);
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    fileCodeSpace.Text += line+'\n';
                }
            }

        }


        private void fileCodeSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                count++;
                fileLineSpace.Text = "";
                var caretIndex = fileCodeSpace.CaretIndex;
                fileCodeSpace.Text = fileCodeSpace.Text.Insert(caretIndex, "\n");
                numLine += count.ToString() + "\r\n";
                fileLineSpace.Text = numLine;
                fileCodeSpace.CaretIndex = caretIndex + 1;
            }
            else if (e.Key == Key.Back)
            {
                if (fileCodeSpace.Text.Length != 0)
                {
                    if (fileCodeSpace.Text[(fileCodeSpace.Text.Length) - 1] == '\n')
                    {

                        fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));
                        fileLineSpace.Text = fileLineSpace.Text.Remove(fileLineSpace.Text.LastIndexOf(Environment.NewLine));

                        fileLineSpace.AppendText("\r\n");

                        numLine = numLine.Remove(numLine.LastIndexOf(Environment.NewLine));
                        numLine = numLine.Remove(numLine.LastIndexOf(Environment.NewLine));
                        numLine += "\r\n";

                        var caretIndex = fileCodeSpace.CaretIndex;
                        count--;
                        fileCodeSpace.Select((fileCodeSpace.Text.Length), 0);
                    }
                }
                
            }
        }


        private void fileLineSpace_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }


        private void txtOutput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var caretIndex = txtOutput.CaretIndex;
                txtOutput.Text = txtOutput.Text.Insert(caretIndex, "\n");
                txtOutput.CaretIndex = caretIndex + 1;
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == sv1)
            {
                sv2.ScrollToVerticalOffset(e.VerticalOffset);
                sv2.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                sv1.ScrollToVerticalOffset(e.VerticalOffset);
                sv1.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        private void fileLineSpace_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void fileCodeSpace_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Ejemplos
        private void opReservadas_click(object sender, RoutedEventArgs e)
        {
            String reservadas = "Number numero = 12.\nFloat flotante = 8,56.\nString mensaje = 'Hello World'.\nFlag bandera = True.\nString nulo = Null.";
            fileCodeSpace.Text += "\n" + reservadas;

        }

        private void opControl_click(object sender, RoutedEventArgs e)
        {
            String condicionales = "If|h>0|{\n   Print|'Mayor a 0' |.\n}else{\n   Print|'Menor o igual a 0' |.\n}.";
            String bucles = "For|n,0,10,1|{\n   Print|n|.\n}.\n\nNumber n = 0.\nWhile|n<7|{\n   n++.\n}.";
            String funciones = "String function hola||{\n  Return|hola|.\n}.\n\nfunction hola||{\n   Print|”Hola”|.\n}.\n\nfunction main{\nString hola = “Como les va”;\n}.\n";


            fileCodeSpace.Text += "\n" + condicionales+"\n\n"+ bucles;

        }

        private void opFunciones_click(object sender, RoutedEventArgs e)
        {
            String funciones = "String function hola||{\n  Return|hola|.\n}.\n\nfunction hola||{\n   Print|”Hola”|.\n}.\n\nfunction main{\n   String hola = “Como les va”;\n}.\n";


            fileCodeSpace.Text += "\n" + funciones;

            sumarLineas();
        }

        private void opOperaciones_click(object sender, RoutedEventArgs e)
        {
            String operaciones = "Number numero1 = 4.\nNumber numero2 = 2.\n\nNumber sum = numero1+numero2.\nNumber rest = numero1-numero2.\nNumber mult = numero1*numero2.\nNumber div = numero1/numero2.";


            fileCodeSpace.Text += "\n" + operaciones;

        }

        private void opCompilar_click(object sender, RoutedEventArgs e)
        {
            String resultado = contenedores.analisis(fileCodeSpace.Text);
            System.Diagnostics.Debug.WriteLine(resultado);
            txtOutput.Text = resultado;
        }

        private void sumarLineas()
        {
            numLine = "";
            for (int i =0; i < fileCodeSpace.Text.Length; i++)
            {
                if (fileCodeSpace.Text[i] == '\n')
                {
                    count++;
                    numLine += count.ToString() + "\r\n";
                }
            }
            fileLineSpace.Text += numLine;
        }
        
    }
}
