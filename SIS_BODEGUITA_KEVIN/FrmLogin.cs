using System;
using System.Windows.Forms;

namespace SIS_BODEGUITA_KEVIN
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {}
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validación de credenciales estáticas en código
            if (txtUsuario.Text == "admin" && txtContrasena.Text == "1234")
            {
                // Notifica al usuario que el acceso fue concedido de manera exitosa
                MessageBox.Show("¡Bienvenido! Logueado con éxito.", "Sistemas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Instancia que nos redirigue al formulario principal del sistema
                FrmMenuPrincipal sistema = new FrmMenuPrincipal();
                sistema.Show();
                this.Hide();
            }
            else
            {
                // Alerta en caso de que el usuario o la contraseña no coincidan con los valores válidos
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtUsuario.Clear();
            txtContrasena.Clear();
            txtUsuario.Focus();
        }
    }
}
