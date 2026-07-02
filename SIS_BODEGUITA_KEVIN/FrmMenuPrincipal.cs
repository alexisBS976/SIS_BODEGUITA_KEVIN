using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SIS_BODEGUITA_KEVIN
{

    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            // Abre el sistema mandando el índice 0 (Ventas)
            FrmSistema sistema = new FrmSistema(0);
            sistema.ShowDialog();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            // Abre el sistema mandando el índice 1 (Inventario)
            FrmSistema sistema = new FrmSistema(1);
            sistema.ShowDialog();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // Abre el sistema mandando el índice 2 (Administración)
            FrmSistema sistema = new FrmSistema(2);
            sistema.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Oculta el menú actual
            this.Hide();

            // Abre el formulario de Login de forma directa
            FrmLogin login = new FrmLogin();
            login.ShowDialog();

            // Cierra definitivamente el programa al salir del login
            this.Close();
        }
    }
}