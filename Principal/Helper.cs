using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Dominio;

namespace Principal
{
    public static class Helper
    {
        public static List<Articulo> ListaArticulos;
        public static void CargarImagen(PictureBox CajaImagen, string Imagen)
        {
            try
            {
                CajaImagen.Load(Imagen);
            }
            catch (Exception)
            {
                CajaImagen.Load("https://tse4.mm.bing.net/th?id=OIP.dxt2_gkvMt-3ZUVo8RF9SQHaHa&pid=Api&P=0");
            }
        }
        public static void Cargar(DataGridView Grilla, PictureBox CajaImagen)
        {
            try
            {
                ArticuloDatos Datos = new ArticuloDatos();
                CultureInfo Culture = new CultureInfo("es-AR");
                Culture.NumberFormat.CurrencySymbol = "ARS";
                ListaArticulos = Datos.Listar();
                Grilla.DataSource = ListaArticulos;
                Grilla.Columns["Precio"].DefaultCellStyle.Format = "C2";
                Grilla.Columns["Precio"].DefaultCellStyle.FormatProvider = Culture;
                CargarImagen(CajaImagen, ListaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void MostraryOcultar(Panel panel1, Panel panel2)
        {
            if (!panel1.Visible)
                panel1.Visible = true;
            else
                panel1.Visible = false;
            if (panel2.Visible)
                panel2.Visible = false;
        }
        public static void OcultarColumnas(DataGridView Grilla)
        {
            Grilla.Columns["Id"].Visible = false;
            Grilla.Columns["ImagenUrl"].Visible = false;
        }
        public static void Eliminar(DataGridView dataGridView, PictureBox pictureBox)
        {
            ArticuloDatos Datos = new ArticuloDatos();
            if (dataGridView.CurrentRow != null && dataGridView.CurrentRow.DataBoundItem != null)
            {
                Articulo Seleccionado;
                try
                {
                    DialogResult Resultado = MessageBox.Show("¿Esta seguro que quiere eliminar?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Resultado == DialogResult.Yes)
                    {
                        Seleccionado = (Articulo)dataGridView.CurrentRow.DataBoundItem;
                        Datos.EliminarArticulo(Seleccionado.Id);
                        Cargar(dataGridView, pictureBox);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("Debe seleccionar una fila antes de eliminar un articulo", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void FiltroRapido(TextBox textBox, DataGridView dataGridView)
        {
            List<Articulo> ListaFiltrada;
            string Filtro = textBox.Text;
            if (Filtro.Length >= 3)
                ListaFiltrada = ListaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(Filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(Filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(Filtro.ToUpper()));
            else
                ListaFiltrada = ListaArticulos;
            dataGridView.DataSource = null;
            dataGridView.DataSource = ListaFiltrada;
            CultureInfo Culture = new CultureInfo("es-AR");
            Culture.NumberFormat.CurrencySymbol = "ARS";
            dataGridView.Columns["Precio"].DefaultCellStyle.Format = "C2";
            dataGridView.Columns["Precio"].DefaultCellStyle.FormatProvider = Culture;
        }
        public static void CargarCampo(ComboBox Campo)
        {
            Campo.Items.Add("Precio");
            Campo.Items.Add("Nombre");
            Campo.Items.Add("Marca");
            Campo.Items.Add("Categoria");
        }
        public static void CargarCriterio(ComboBox Campo, ComboBox Criterio)
        {
            string Opcion = Campo.SelectedItem.ToString();
            if(Opcion == "Precio")
            {
                Criterio.Items.Clear();
                Criterio.Items.Add("Mayor a");
                Criterio.Items.Add("Menor a");
                Criterio.Items.Add("Igual a");
            }
            else
            {
                Criterio.Items.Clear();
                Criterio.Items.Add("Comienza con");
                Criterio.Items.Add("Termina con");
                Criterio.Items.Add("Contiene");
            }
        }
        public static bool SoloNumeros(string Cadena)
        {
            foreach (char Caracter in Cadena)
            {
                if (!(char.IsNumber(Caracter)))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ValidarFiltro(ComboBox Campo, ComboBox Criterio, TextBox Filtro)
        {
            if(Campo.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar un campo");
                return true;
            }
            if(Criterio.SelectedIndex < 0)
            {
                MessageBox.Show("Debes seleccionar un criterio");
                return true;
            }
            if(string.IsNullOrEmpty(Filtro.Text))
            {
                MessageBox.Show("Debes cargar el filtro");
                return true;
            }
            if(Campo.SelectedItem.ToString() == "Precio")
            {
                if(!(SoloNumeros(Filtro.Text)))
                {
                    MessageBox.Show("Solo numeros si se usa el campo de precio");
                    return true;
                }
            }
            return false;
        }
        public static void Reiniciar(DataGridView dataGridView, PictureBox pictureBox, ComboBox Campo, ComboBox Criterio, TextBox textBox)
        {
            Cargar(dataGridView, pictureBox);
            OcultarColumnas(dataGridView);
            Campo.Items.Clear();
            Criterio.Items.Clear();
            textBox.Text = "";
            CargarCampo(Campo);
        }
        public static void VerArticulo(DataGridView dataGridView)
        {
            if(dataGridView.CurrentRow != null && dataGridView.CurrentRow.DataBoundItem != null)
            {
                try
                {
                    Articulo Seleccionado = (Articulo)dataGridView.CurrentRow.DataBoundItem;
                    MessageBox.Show("Codigo: " + Seleccionado.Codigo + "\n" +
                    "Nombre: " + Seleccionado.Nombre + "\n" +
                    "Descripcion: " + Seleccionado.Descripcion + "\n" +
                    "Categoria: " + Seleccionado.Categoria.Descripcion + "\n" +
                    "Marca: " + Seleccionado.Marca.Descripcion + "\n" +
                    "Precio: ARS " + Seleccionado.Precio.ToString("#,##0.00"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
                MessageBox.Show("Seleccione un articulo para ver", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
