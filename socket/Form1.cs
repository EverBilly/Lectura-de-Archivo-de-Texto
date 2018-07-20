using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows;

namespace socket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConexion_Click(object sender, EventArgs e)
        {
            try
            {   // Abrir el Archivo Utilizando StreamReader.
                using (StreamReader sr = new StreamReader("C:\\ArchivoPrueba.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    MessageBox.Show("Archivo Leido: " + Environment.NewLine + line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El Archivo No Pudo Ser Leido");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
