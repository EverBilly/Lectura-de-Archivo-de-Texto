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
using System.Configuration;
using System.Xml.Linq;
using System.Xml;

namespace socket
{
    public partial class Form1 : Form
    {

        string UbicacionArchivo = ConfigurationManager.ConnectionStrings["UbicacionArchivo"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox1.Text = Convert.ToString(UbicacionArchivo);
        }

        private void btnConexion_Click(object sender, EventArgs e)
        {

            try
            {   // Abrir el Archivo Utilizando StreamReader.
                using (StreamReader sr = new StreamReader(UbicacionArchivo))
                {
                    // Leer el Archivo y Mostrar la Informacion del Documento.
                    String linea = sr.ReadToEnd();
                    richTextBox1.Text = (linea);
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
            //XML();
        }

        public class AppConfig
        {
            public static void EstableceParametros(string clave, string palabra)
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                conf.AppSettings.Settings.Remove(clave);

                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSetings");
                conf.AppSettings.Settings.Add(clave, palabra);
                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSetings");

            }

            public static string RecuperaValor(string clave, string predeterminado)
            {
                string retornar = ConfigurationManager.AppSettings[clave];
                if (retornar == null)
                {
                    retornar = predeterminado;
                }
                return retornar;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppConfig.EstableceParametros(Convert.ToString(textBox2.Text).Trim(), Convert.ToString(textBox3.Text).Trim());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = AppConfig.RecuperaValor(textBox2.Text, "Error");
        }

       //CARGAR ARCHIVO XML
        public void XML()
        {
            try
            {

                XElement root = new XElement(Convert.ToString(textBox2.Text).Trim());
                foreach (string line in File.ReadAllLines(textBox1.Text))
                {
                    string[] fields = line.Split('|');
                    XElement record = new XElement(Convert.ToString(textBox3.Text).Trim());
                    int pos = 0;
                    foreach (String sp in fields)
                    {
                        pos += 1;
                        XElement field = new XElement(string.Format("posicion", pos.ToString()), new XAttribute("att", "content"), new XAttribute("name", "SAT")); // prepare child nodes
                        field.Add(sp);
                        record.Add(field); // add to parent node
                    }
                    root.Add(record);
                }
                richTextBox2.Text = (root.ToString());  // display the result on console
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Debe Llenar Los Campos" + e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrirarchivo = new OpenFileDialog();
            abrirarchivo.Title = "Seleccione El Archivo de Texto";
            abrirarchivo.Filter = "Documentos de Texto (*.txt)|*.txt";
            abrirarchivo.FileName = textBox1.Text;

            if (abrirarchivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = abrirarchivo.FileName;
            }
            XML();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XmlDocument documento = new XmlDocument();
            documento.LoadXml(richTextBox2.Text);
            documento.PreserveWhitespace = true;
            documento.Save("data.xml");
            MessageBox.Show("ARCHIVO GUARDADO EXITOSAMENTE");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //FUNCIONA GUARDAR ARCHIVO TXT
            StreamWriter crear = new StreamWriter("data.txt");
            string contenido = richTextBox1.Text;
            if (contenido != "")
            {
                crear.Write(contenido.ToString());
                crear.Flush();
                crear.Close();
                MessageBox.Show("ARCHIVO GUARDADO EXITOSAMENTE");
            }
            else
            {
                MessageBox.Show("ARCHIVO VACIO");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string lineas = richTextBox1.Text;

            string arreglado = lineas.Replace('[', '<').Replace(']', '>');
            MessageBox.Show(arreglado);
        }

        private bool CompararArchivos(string archivo1, string archivo2)
        {
            int archivo1_bytes, archivo2_bytes;
            FileStream archivo1s, archivo2s;

            if (archivo1 == archivo2)
            {

                MessageBox.Show("Son Iguales");
                return true;
            }
            archivo1s = new FileStream(archivo1, FileMode.Open);
            archivo2s = new FileStream(archivo2, FileMode.Open);

            if (archivo1s.Length != archivo2s.Length)
            {
                archivo1s.Close();
                archivo2s.Close();
                MessageBox.Show("No Son Iguales");
                return false;
            }

            do
            {
                archivo1_bytes = archivo1s.ReadByte();
                archivo2_bytes = archivo2s.ReadByte();
            }

            while ((archivo1_bytes == archivo2_bytes) && (archivo1_bytes != -1));

            archivo1s.Close();
            archivo2s.Close();

            return((archivo1_bytes - archivo2_bytes) == 0);
        }
    }
}
