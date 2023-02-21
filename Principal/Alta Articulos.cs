using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Datos;
using System.Configuration;
using System.IO;

namespace Principal
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo Articulo = null;
        private OpenFileDialog Archivo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public enum ModoOperacionEnum
        {
            Agregar,
            Modificar
        }

        public ModoOperacionEnum ModoOperacion { get; set; }

        public frmAltaArticulo(Articulo Articulo)
        {
            InitializeComponent();
            this.Articulo = Articulo;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            CategoriaDatos categoriaDatos = new CategoriaDatos();
            MarcaDatos marcaDatos = new MarcaDatos();
            try
            {
                cboCategoria.DataSource = categoriaDatos.Listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboCategoria.SelectedIndex = -1;
                cboMarca.DataSource = marcaDatos.Listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.SelectedIndex = -1;
                if (ModoOperacion == ModoOperacionEnum.Agregar)
                {
                    lblAltaArticulo.Text = "Agregar Articulo";
                }
                else if (ModoOperacion == ModoOperacionEnum.Modificar)
                {
                    lblAltaArticulo.Text = "Modificar Articulo";
                }
                if(Articulo != null)
                {
                    txtCodigo.Text = Articulo.Codigo;
                    txtNombre.Text = Articulo.Nombre;
                    txtDescripcion.Text = Articulo.Descripcion;
                    txtImagenUrl.Text = Articulo.ImagenUrl;
                    Helper.CargarImagen(pbxArticuloNuevo, txtImagenUrl.Text);
                    cboCategoria.SelectedValue = Articulo.Categoria.Id;
                    cboMarca.SelectedValue = Articulo.Marca.Id;
                    nudPrecio.Value = Articulo.Precio;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ArticuloDatos Datos = new ArticuloDatos();
            if (Articulo == null)
                Articulo = new Articulo();
            string Codigo = txtCodigo.Text;
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            string ImagenUrl = txtImagenUrl.Text;
            Categoria Categoria = (Categoria)cboCategoria.SelectedItem;
            Marca Marca = (Marca)cboMarca.SelectedItem;
            Decimal Precio = nudPrecio.Value;
            Articulo.Codigo = Codigo;
            Articulo.Nombre = Nombre;
            Articulo.Descripcion = Descripcion;
            Articulo.ImagenUrl = ImagenUrl;
            Articulo.Marca = Marca;
            Articulo.Categoria = Categoria;
            Articulo.Precio = Precio;
            if (Archivo != null && !(txtImagenUrl.Text.ToUpper().Contains("HTTP")))
            {
                try
                {
                    if (ImagenUrl != null && ImagenUrl != "")
                    {
                        string filePath = ConfigurationManager.AppSettings["images-folder"] + Archivo.SafeFileName;
                        if (!(File.Exists(filePath)))
                            File.Copy(Archivo.FileName, filePath);
                        else
                            MessageBox.Show("La imagen que intenta guardar ya existe en la carpeta local");
                    }
                    else
                    {
                        lblValidarImagen.BackColor = Color.Red;
                        lblValidarImagen.Text = "X";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (ModoOperacion == ModoOperacionEnum.Agregar)
            {
                if (ValidarArticuloNuevo(Codigo, Nombre, Descripcion, ImagenUrl, Categoria, Marca, Precio))
                {
                    Datos.AgregarArticulo(Articulo);
                    MessageBox.Show("Agregado exitosamente");
                    txtCodigo.Text = "";
                    txtNombre.Text = "";
                    txtDescripcion.Text = "";
                    txtImagenUrl.Text = "";
                    cboCategoria.SelectedIndex = -1;
                    cboMarca.SelectedIndex = -1;
                    nudPrecio.Value = 0;
                    pbxArticuloNuevo.Image = null;
                }
                else
                    MessageBox.Show("Debes completar todos los campos");
            }
            else if (ModoOperacion == ModoOperacionEnum.Modificar)
            {
                if (ValidarArticuloNuevo(Codigo, Nombre, Descripcion, ImagenUrl, Categoria, Marca, Precio))
                {
                    Datos.ModificarArticulo(Articulo);
                    MessageBox.Show("Modificado exitosamente");
                    Close();
                }
            }
        }
        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            Helper.CargarImagen(pbxArticuloNuevo, txtImagenUrl.Text);
        }
        private bool ValidarArticuloNuevo(string Codigo, string Nombre, string Descripcion, string ImagenUrl, Categoria Categoria, Marca Marca, Decimal Precio)
        {
            int con = 0;
            if (Codigo == null || Codigo == "")
            {
                lblValidarCodigo.BackColor = Color.Red;
                lblValidarCodigo.Text = "X";
            }
            else
                con++;
            if (Nombre == null || Nombre == "")
            {
                lblValidarNombre.BackColor = Color.Red;
                lblValidarNombre.Text = "X";
            }
            else
                con++;
            if (Descripcion == null || Descripcion == "")
            {
                lblValidarDescripcion.BackColor = Color.Red;
                lblValidarDescripcion.Text = "X";
            }
            else
                con++;
            if (ImagenUrl == null || ImagenUrl == "")
            {
                lblValidarImagen.BackColor = Color.Red;
                lblValidarImagen.Text = "X";
            }
            else
                con++;
            if (Categoria == null)
            {
                lblValidarCategoria.BackColor = Color.Red;
                lblValidarCategoria.Text = "X";
            }
            else
                con++;
            if (Marca == null)
            {
                lblValidarMarca.BackColor = Color.Red;
                lblValidarMarca.Text = "X";
            }
            else
                con++;
            if (string.IsNullOrEmpty(nudPrecio.Text) || Precio == 0)
            {
                lblValidarPrecio.BackColor = Color.Red;
                lblValidarPrecio.Text = "X";
            }
            else
                con++;
            if (con == 7)
                return true;

           return false;
        }
        private void ValidarLabel(Label label)
        {
            label.BackColor = Color.FromArgb(56, 77, 72);
            label.Text = "";
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLabel(lblValidarCodigo);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLabel(lblValidarNombre);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLabel(lblValidarDescripcion);
        }

        private void txtImagenUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLabel(lblValidarImagen);
        }
        private void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            ValidarLabel(lblValidarImagen);
        }

        private void cboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarLabel(lblValidarCategoria);
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarLabel(lblValidarMarca);
        }

        private void nudPrecio_ValueChanged(object sender, EventArgs e)
        {
            ValidarLabel(lblValidarPrecio);
        }

        private void nudPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLabel(lblValidarPrecio);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Archivo = new OpenFileDialog();
            Archivo.Filter = "jpg|*.jpg|png|*.png";
            if (Archivo.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = Archivo.FileName;
                Helper.CargarImagen(pbxArticuloNuevo, Archivo.FileName);
            }
        }
    }
}
