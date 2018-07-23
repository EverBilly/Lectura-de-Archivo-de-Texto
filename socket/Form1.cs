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
                    textBox4.Text = ("Archivo Leido: " + Environment.NewLine + linea);
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
            XElement root = new XElement(Convert.ToString(textBox2.Text).Trim());
            foreach (string line in File.ReadAllLines(textBox1.Text)) {
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
            textBox5.Text = (root.ToString());  // display the result on console
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
            documento.LoadXml(textBox5.Text);
            documento.PreserveWhitespace = true;
            documento.Save("data.xml");
            MessageBox.Show("ARCHIVO GUARDADO EXITOSAMENTE");
        }
    }
}
