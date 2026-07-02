namespace SIS_BODEGUITA_KEVIN
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lb1 = new Label();
            groupLogin = new GroupBox();
            btnIngresar = new Button();
            txtContrasena = new TextBox();
            txtUsuario = new TextBox();
            lb3 = new Label();
            lb2 = new Label();
            groupLogin.SuspendLayout();
            SuspendLayout();
            // 
            // lb1
            // 
            lb1.AutoSize = true;
            lb1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lb1.Location = new Point(136, 35);
            lb1.Name = "lb1";
            lb1.Size = new Size(317, 38);
            lb1.TabIndex = 0;
            lb1.Text = "Por favor, inicie sesión";
            // 
            // groupLogin
            // 
            groupLogin.BackColor = SystemColors.ActiveCaption;
            groupLogin.Controls.Add(btnIngresar);
            groupLogin.Controls.Add(txtContrasena);
            groupLogin.Controls.Add(txtUsuario);
            groupLogin.Controls.Add(lb3);
            groupLogin.Controls.Add(lb2);
            groupLogin.Location = new Point(30, 91);
            groupLogin.Name = "groupLogin";
            groupLogin.Size = new Size(521, 254);
            groupLogin.TabIndex = 1;
            groupLogin.TabStop = false;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(255, 128, 128);
            btnIngresar.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnIngresar.Location = new Point(160, 185);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(182, 49);
            btnIngresar.TabIndex = 5;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(200, 129);
            txtContrasena.MaxLength = 4;
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(198, 27);
            txtContrasena.TabIndex = 4;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(160, 58);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(204, 27);
            txtUsuario.TabIndex = 2;
            txtUsuario.TextChanged += txtUsuario_TextChanged;
            // 
            // lb3
            // 
            lb3.AutoSize = true;
            lb3.Font = new Font("Segoe UI Emoji", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lb3.Location = new Point(30, 125);
            lb3.Name = "lb3";
            lb3.Size = new Size(164, 31);
            lb3.TabIndex = 1;
            lb3.Text = "CONTRASEÑA:";
            // 
            // lb2
            // 
            lb2.AutoSize = true;
            lb2.Font = new Font("Segoe UI Emoji", 13.8F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lb2.Location = new Point(30, 54);
            lb2.Name = "lb2";
            lb2.Size = new Size(115, 31);
            lb2.TabIndex = 0;
            lb2.Text = "USUARIO:";
            // 
            // FrmLogin
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 373);
            Controls.Add(groupLogin);
            Controls.Add(lb1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión";
            groupLogin.ResumeLayout(false);
            groupLogin.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lb1;
        private GroupBox groupLogin;
        private TextBox txtContrasena;
        private TextBox txtUsuario;
        private Label lb3;
        private Label lb2;
        private Button btnIngresar;
    }
}
