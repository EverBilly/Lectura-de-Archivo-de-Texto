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
                using (StreamReader sr = new StreamReader(textBox1.Text))
                {
                    // Leer el Archivo y Mostrar la Informacion del Documento.
                    String linea = sr.ReadToEnd();
                    MessageBox.Show("Archivo Leido: " + Environment.NewLine + linea);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El Archivo No Pudo Ser Leido");
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirarchivo = new OpenFileDialog();
            abrirarchivo.Title = "Seleccione El Archivo de Texto";
            abrirarchivo.Filter = "Documentos de Texto (*.txt)|*.txt";
            abrirarchivo.FileName = textBox1.Text;

            if (abrirarchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = abrirarchivo.FileName;
            }
        }

    }
}
