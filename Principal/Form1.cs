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
using System.Globalization;

namespace Principal
{
    public partial class FrmArticulos : Form
    {
        public FrmArticulos()
        {
            InitializeComponent();
        }
        Panel Panel = new Panel();
        private void btnMouseEnter (object sender, EventArgs e)
        {
            Button btn = sender as Button;
            pBarraMenu.Controls.Add(Panel);
            Panel.BackColor = Color.FromArgb(197, 220, 160);
            Panel.Size = new Size(130, 5);
            Panel.Location = new Point(btn.Location.X, btn.Location.Y + 40);
        }
        private void btnMouseLeave(object sender, EventArgs e)
        {
            pBarraMenu.Controls.Remove(Panel);
        }
        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            Helper.Cargar(dgvArticulos, pbxArticulos);
            Helper.OcultarColumnas(dgvArticulos);
            Helper.CargarCampo(cboCampo);
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            Helper.MostraryOcultar(pMenuProductos, pMenuContactos);
        }
        private void btnContactos_Click(object sender, EventArgs e)
        {
            Helper.MostraryOcultar(pMenuContactos, pMenuProductos);
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                Articulo Seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Helper.CargarImagen(pbxArticulos, Seleccionado.ImagenUrl);
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo ArticuloNuevo = new frmAltaArticulo();
            ArticuloNuevo.ModoOperacion = frmAltaArticulo.ModoOperacionEnum.Agregar;
            ArticuloNuevo.ShowDialog();
            Helper.Cargar(dgvArticulos, pbxArticulos);
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                Articulo Seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmAltaArticulo Modificar = new frmAltaArticulo(Seleccionado);
                Modificar.ModoOperacion = frmAltaArticulo.ModoOperacionEnum.Modificar;
                Modificar.Text = "Modificar Articulo";
                Modificar.ShowDialog();
                Helper.Cargar(dgvArticulos, pbxArticulos);
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila antes de modificar un articulo", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Helper.Eliminar(dgvArticulos, pbxArticulos);
        }
        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {
            Helper.FiltroRapido(txtFiltroRapido, dgvArticulos);
            Helper.OcultarColumnas(dgvArticulos);
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Helper.CargarCriterio(cboCampo, cboCriterio);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloDatos Datos = new ArticuloDatos();
            try
            {
                if (Helper.ValidarFiltro(cboCampo, cboCriterio, txtFiltroAvanzado))
                    return;
                string Campo = cboCampo.SelectedItem.ToString();
                string Criterio = cboCriterio.SelectedItem.ToString();
                string Filtro = txtFiltroAvanzado.Text.ToString();
                dgvArticulos.DataSource = Datos.Filtrar(Campo, Criterio, Filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            Helper.Reiniciar(dgvArticulos, pbxArticulos, cboCampo, cboCriterio, txtFiltroAvanzado);
        }
        private void btnVer_Click(object sender, EventArgs e)
        {
            Helper.VerArticulo(dgvArticulos);
        }
    }
}
