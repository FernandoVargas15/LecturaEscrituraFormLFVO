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
using System.Text.RegularExpressions;

namespace LecturaEscrituraTxtLFVO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txbEdad.TextChanged += ValidarEdad;
            txbTelefono.Leave += ValidarTelefono;
            txbNombre.TextChanged += ValidarNombre;
            txbApellidos.TextChanged += ValidarApellidos;  // Cambié TextAlignChanged a TextChanged
            txbEstatura.TextChanged += ValidarEstatura;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Datos de los TextBox
            string nombres = txbNombre.Text;
            string apellidos = txbApellidos.Text;
            string edad = txbEdad.Text;
            string estatura = txbEstatura.Text;
            string Telefono = txbTelefono.Text;

            // Género
            string genero = "";
            if (rdoHombre.Checked)
            {
                genero = "Hombre";
            }
            else if (rdoMujer.Checked)
            {
                genero = "Mujer";
            }

            // Cadena de datos
            string datos = $"Nombres: {nombres}\r\nApellidos: {apellidos}\r\nTeléfono: {Telefono} \r\nEstatura: {estatura} cm\r\nEdad: {edad} años\r\nGénero: {genero}";

            // Guardar datos en archivo de texto
            string rutaArchivo = "C:/Users/fergu/OneDrive/Escritorio/GUARDAR_DATOS/datos.txt";
            bool archivoExiste = File.Exists(rutaArchivo);

            using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
            {
                if (archivoExiste)
                {
                    writer.WriteLine(datos);
                }
                else
                {
                    writer.WriteLine(datos); // Guardar datos en archivo si no existe
                }
            }

            MessageBox.Show("Datos guardados con éxito:\n\n" + datos, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EsEnteroValido(string valor)
        {
            int Resultado;
            return int.TryParse(valor, out Resultado);
        }

        private bool EsDecimalValido(string valor)
        {
            decimal Resultado;
            return decimal.TryParse(valor, out Resultado);
        }

        private bool EsEnteroValido10Digitos(string valor)
        {
            long Resultado;
            return long.TryParse(valor, out Resultado) && valor.Length == 10;
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");  // Arreglé la expresión regular
        }

        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Verificar si el TextBox no está vacío antes de validar
            if (!string.IsNullOrEmpty(textBox.Text) && !EsEnteroValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una edad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Verificar si el TextBox no está vacío antes de validar
            if (!string.IsNullOrEmpty(textBox.Text) && !EsDecimalValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese una estatura válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }

        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length == 10 && EsEnteroValido10Digitos(textBox.Text))
            {
                textBox.BackColor = Color.AliceBlue;
            }
            else
            {
                textBox.BackColor = Color.Red;
                MessageBox.Show("Por favor, ingrese un número de teléfono de 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Verificar si el TextBox no está vacío antes de validar
            if (!string.IsNullOrEmpty(textBox.Text) && !EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese apellidos válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }


        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Solo validar si el campo no está vacío
            if (!string.IsNullOrEmpty(textBox.Text) && !EsTextoValido(textBox.Text))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Clear();
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar datos
            txbNombre.Clear();
            txbApellidos.Clear();
            txbEstatura.Clear();
            txbEdad.Clear();
            txbTelefono.Clear();
            rdoHombre.Checked = false;
            rdoMujer.Checked = false;
        }
    }
}
