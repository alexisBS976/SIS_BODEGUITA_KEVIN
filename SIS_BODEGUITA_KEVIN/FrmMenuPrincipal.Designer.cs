namespace SIS_BODEGUITA_KEVIN
{
    partial class FrmMenuPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnVentas = new Button();
            btnInventario = new Button();
            btnAdmin = new Button();
            btnCerrarSesion = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("SimSun-ExtG", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(100, 22);
            label1.Name = "label1";
            label1.Size = new Size(548, 44);
            label1.TabIndex = 0;
            label1.Text = "--- BODEGUITA KEVIN ---";
            // 
            // btnVentas
            // 
            btnVentas.BackColor = Color.FromArgb(192, 255, 192);
            btnVentas.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVentas.Location = new Point(232, 96);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(276, 95);
            btnVentas.TabIndex = 1;
            btnVentas.Text = "VENTAS";
            btnVentas.UseVisualStyleBackColor = false;
            btnVentas.Click += btnVentas_Click;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.FromArgb(255, 255, 192);
            btnInventario.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInventario.Location = new Point(232, 197);
            btnInventario.Name = "btnInventario";
            btnInventario.Size = new Size(276, 95);
            btnInventario.TabIndex = 2;
            btnInventario.Text = "INVENTARIO";
            btnInventario.UseVisualStyleBackColor = false;
            btnInventario.Click += btnInventario_Click;
            // 
            // btnAdmin
            // 
            btnAdmin.BackColor = Color.FromArgb(192, 192, 255);
            btnAdmin.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdmin.Location = new Point(232, 298);
            btnAdmin.Name = "btnAdmin";
            btnAdmin.Size = new Size(276, 95);
            btnAdmin.TabIndex = 3;
            btnAdmin.Text = "ADMINISTRACION";
            btnAdmin.UseVisualStyleBackColor = false;
            btnAdmin.Click += btnAdmin_Click;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.BackColor = Color.Red;
            btnCerrarSesion.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.Location = new Point(232, 399);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Size = new Size(276, 95);
            btnCerrarSesion.TabIndex = 4;
            btnCerrarSesion.Text = "CERRAR SESION";
            btnCerrarSesion.UseVisualStyleBackColor = false;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(779, 530);
            Controls.Add(btnCerrarSesion);
            Controls.Add(btnAdmin);
            Controls.Add(btnInventario);
            Controls.Add(btnVentas);
            Controls.Add(label1);
            MinimizeBox = false;
            Name = "FrmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMenuPrincipal";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnVentas;
        private Button btnInventario;
        private Button btnAdmin;
        private Button btnCerrarSesion;
    }
}