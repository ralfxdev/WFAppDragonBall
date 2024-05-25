using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFAppDragonBall.Data.DataAccess;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WFAppDragonBall
{
    public partial class Form1 : Form
    {
        private PersonajeDB personajeDB;

        private string[] razasDragonBall = {
            "Android",
            "Bio-Android",
            "Humana",
            "Humano",
            "Majin",
            "Namekuseijin",
            "Saiyajin",
            "Saiyajin/Humano",
            "Saiyajin/Saiyajin"
        };

        public Form1()
        {
            InitializeComponent();

            personajeDB = new PersonajeDB();

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (personajeDB.TestConnection())
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            DataTable dt = personajeDB.LeerPersonajes();
            dataGridViewPersonajes.DataSource = dt;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = textBox2.Text;
            string raza = comboBox1.SelectedItem.ToString();
            int nivelPoder = (int)numericUpDown1.Value;
            string historia = historiaText.Text;
            int respuesta = personajeDB.CrearPersonaje(nombre, raza, nivelPoder, historia);
            if (respuesta >  0)
            {
                MessageBox.Show("Creado con exito");
                dataGridViewPersonajes.DataSource = personajeDB.LeerPersonajes();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Value = 0;
                comboBox1.Text = "";
                historiaText.Text = "";
            }
            else
            {
                MessageBox.Show("Error al crear");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(razasDragonBall);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void buscarPorId()
        {
            int idPersonajeABuscar = int.Parse(textBox1.Text);

            DataTable personajeEncontrado = personajeDB.BuscarPersonajePorId(idPersonajeABuscar);

            if (personajeEncontrado.Rows.Count > 0)
            {

                string nombre = personajeEncontrado.Rows[0]["nombre"].ToString();
                string raza = personajeEncontrado.Rows[0]["raza"].ToString();
                int nivelPoder = int.Parse(personajeEncontrado.Rows[0]["nivel_poder"].ToString());
                string historia = personajeEncontrado.Rows[0]["historia"].ToString();
                string fechaCreacion = personajeEncontrado.Rows[0]["fecha_creacion"].ToString();
                textBox2.Text = nombre;
                textBox3.Text = raza;
                comboBox1.Text = raza;
                numericUpDown1.Value = nivelPoder;
                historiaText.Text = historia;
                lblFCreate.Text = fechaCreacion;
            }
            else
            {
                Console.WriteLine("No se encontró el personaje con ID: " + idPersonajeABuscar);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buscarPorId();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            buscarPorId();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            try
            {
                personajeDB.EliminarPersonaje(id);
                MessageBox.Show("Borrado con exito");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Value = 0;
                comboBox1.Text = "";
                historiaText.Text = "";
                dataGridViewPersonajes.DataSource = personajeDB.LeerPersonajes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);
            string nombre = textBox2.Text;
            string raza = comboBox1.SelectedItem.ToString();
            int nivelPoder = (int)numericUpDown1.Value;
            string historia = historiaText.Text;
            int respuesta = personajeDB.ActualizarPersonaje(id, nombre, raza, nivelPoder, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("Actualizado con exito");
                dataGridViewPersonajes.DataSource = personajeDB.LeerPersonajes();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Value = 0;
                comboBox1.Text = "";
                historiaText.Text = "";
            }
            else
            {
                MessageBox.Show("Error al actualizar");
            }
        }

        private void historiaText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
