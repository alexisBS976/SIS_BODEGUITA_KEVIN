namespace SIS_BODEGUITA_KEVIN
{
    partial class FrmSistema
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
            tabSistema = new TabControl();
            tabVentas = new TabPage();
            cmbProducto = new ComboBox();
            txtMonto = new TextBox();
            lblVuelto = new Label();
            label6 = new Label();
            btnVuelto = new Button();
            txtPagoCon = new TextBox();
            label7 = new Label();
            bntAgregar = new Button();
            txtCantidad = new TextBox();
            btnCobrar = new Button();
            label11 = new Label();
            label4 = new Label();
            label10 = new Label();
            tabInventario = new TabPage();
            txtNuevaCantidad = new TextBox();
            label8 = new Label();
            btnInvModificar = new Button();
            btnConsultar = new Button();
            txtStock = new TextBox();
            label9 = new Label();
            cmbNombre = new ComboBox();
            label5 = new Label();
            tabAdmin = new TabPage();
            btnDetalle = new Button();
            btnMasVendidos = new Button();
            btnCierre = new Button();
            btnVerProductos = new Button();
            btnReporte = new Button();
            dgvReportes = new DataGridView();
            tabSistema.SuspendLayout();
            tabVentas.SuspendLayout();
            tabInventario.SuspendLayout();
            tabAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReportes).BeginInit();
            SuspendLayout();
            // 
            // tabSistema
            // 
            tabSistema.Controls.Add(tabVentas);
            tabSistema.Controls.Add(tabInventario);
            tabSistema.Controls.Add(tabAdmin);
            tabSistema.Location = new Point(12, 26);
            tabSistema.Name = "tabSistema";
            tabSistema.SelectedIndex = 0;
            tabSistema.Size = new Size(775, 431);
            tabSistema.TabIndex = 0;
            // 
            // tabVentas
            // 
            tabVentas.Controls.Add(cmbProducto);
            tabVentas.Controls.Add(txtMonto);
            tabVentas.Controls.Add(lblVuelto);
            tabVentas.Controls.Add(label6);
            tabVentas.Controls.Add(btnVuelto);
            tabVentas.Controls.Add(txtPagoCon);
            tabVentas.Controls.Add(label7);
            tabVentas.Controls.Add(bntAgregar);
            tabVentas.Controls.Add(txtCantidad);
            tabVentas.Controls.Add(btnCobrar);
            tabVentas.Controls.Add(label11);
            tabVentas.Controls.Add(label4);
            tabVentas.Controls.Add(label10);
            tabVentas.Location = new Point(4, 29);
            tabVentas.Name = "tabVentas";
            tabVentas.Padding = new Padding(3);
            tabVentas.Size = new Size(767, 398);
            tabVentas.TabIndex = 0;
            tabVentas.Text = "VENTAS";
            tabVentas.UseVisualStyleBackColor = true;
            tabVentas.Click += tabVentas_Click;
            // 
            // cmbProducto
            // 
            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(139, 39);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(265, 28);
            cmbProducto.TabIndex = 23;
            cmbProducto.MaxDropDownItems = 10;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(112, 203);
            txtMonto.Name = "txtMonto";
            txtMonto.ReadOnly = true;
            txtMonto.Size = new Size(231, 27);
            txtMonto.TabIndex = 22;
            // 
            // lblVuelto
            // 
            lblVuelto.AutoSize = true;
            lblVuelto.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVuelto.Location = new Point(518, 203);
            lblVuelto.Name = "lblVuelto";
            lblVuelto.Size = new Size(0, 28);
            lblVuelto.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(395, 199);
            label6.Name = "label6";
            label6.Size = new Size(78, 28);
            label6.TabIndex = 20;
            label6.Text = "Vuelto:";
            // 
            // btnVuelto
            // 
            btnVuelto.BackColor = Color.FromArgb(255, 255, 192);
            btnVuelto.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVuelto.Location = new Point(504, 274);
            btnVuelto.Name = "btnVuelto";
            btnVuelto.Size = new Size(200, 52);
            btnVuelto.TabIndex = 19;
            btnVuelto.Text = "CALCULAR VUELTO";
            btnVuelto.UseVisualStyleBackColor = false;
            btnVuelto.Click += btnVuelto_Click;
            // 
            // txtPagoCon
            // 
            txtPagoCon.Location = new Point(505, 140);
            txtPagoCon.Name = "txtPagoCon";
            txtPagoCon.Size = new Size(231, 27);
            txtPagoCon.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(395, 139);
            label7.Name = "label7";
            label7.Size = new Size(104, 28);
            label7.TabIndex = 17;
            label7.Text = "Pago con:";
            // 
            // bntAgregar
            // 
            bntAgregar.BackColor = Color.FromArgb(0, 192, 0);
            bntAgregar.Font = new Font("Segoe UI Emoji", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bntAgregar.Location = new Point(26, 274);
            bntAgregar.Name = "bntAgregar";
            bntAgregar.Size = new Size(201, 55);
            bntAgregar.TabIndex = 16;
            bntAgregar.Text = "AGREGAR PRODUCTOS";
            bntAgregar.UseVisualStyleBackColor = false;
            bntAgregar.Click += bntAgregar_Click;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(139, 126);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(166, 27);
            txtCantidad.TabIndex = 13;
            // 
            // btnCobrar
            // 
            btnCobrar.BackColor = Color.FromArgb(255, 128, 128);
            btnCobrar.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCobrar.Location = new Point(270, 274);
            btnCobrar.Name = "btnCobrar";
            btnCobrar.Size = new Size(157, 52);
            btnCobrar.TabIndex = 12;
            btnCobrar.Text = "COBRAR";
            btnCobrar.UseVisualStyleBackColor = false;
            btnCobrar.Click += btnCobrar_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(26, 126);
            label11.Name = "label11";
            label11.Size = new Size(101, 28);
            label11.TabIndex = 10;
            label11.Text = "Cantidad:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(26, 202);
            label4.Name = "label4";
            label4.Size = new Size(80, 28);
            label4.TabIndex = 11;
            label4.Text = "Monto:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(26, 42);
            label10.Name = "label10";
            label10.Size = new Size(95, 25);
            label10.TabIndex = 9;
            label10.Text = "Producto:";
            // 
            // tabInventario
            // 
            tabInventario.Controls.Add(txtNuevaCantidad);
            tabInventario.Controls.Add(label8);
            tabInventario.Controls.Add(btnInvModificar);
            tabInventario.Controls.Add(btnConsultar);
            tabInventario.Controls.Add(txtStock);
            tabInventario.Controls.Add(label9);
            tabInventario.Controls.Add(cmbNombre);
            tabInventario.Controls.Add(label5);
            tabInventario.Location = new Point(4, 29);
            tabInventario.Name = "tabInventario";
            tabInventario.Padding = new Padding(3);
            tabInventario.Size = new Size(767, 398);
            tabInventario.TabIndex = 1;
            tabInventario.Text = "INVENTARIO";
            tabInventario.UseVisualStyleBackColor = true;
            // 
            // txtNuevaCantidad
            // 
            txtNuevaCantidad.Location = new Point(213, 196);
            txtNuevaCantidad.Name = "txtNuevaCantidad";
            txtNuevaCantidad.Size = new Size(165, 27);
            txtNuevaCantidad.TabIndex = 19;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(39, 192);
            label8.Name = "label8";
            label8.Size = new Size(168, 28);
            label8.TabIndex = 26;
            label8.Text = "Nueva Cantidad:";
            // 
            // btnInvModificar
            // 
            btnInvModificar.BackColor = Color.Brown;
            btnInvModificar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInvModificar.ForeColor = SystemColors.ButtonHighlight;
            btnInvModificar.Location = new Point(556, 87);
            btnInvModificar.Name = "btnInvModificar";
            btnInvModificar.Size = new Size(174, 72);
            btnInvModificar.TabIndex = 25;
            btnInvModificar.Text = "MODIFICAR STOCK";
            btnInvModificar.UseVisualStyleBackColor = false;
            btnInvModificar.Click += btnInvModificar_Click_1;
            // 
            // btnConsultar
            // 
            btnConsultar.BackColor = Color.Fuchsia;
            btnConsultar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConsultar.Location = new Point(556, 214);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(174, 72);
            btnConsultar.TabIndex = 24;
            btnConsultar.Text = "CONSULTAR STOCK";
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(117, 132);
            txtStock.Name = "txtStock";
            txtStock.ReadOnly = true;
            txtStock.Size = new Size(209, 27);
            txtStock.TabIndex = 23;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(41, 131);
            label9.Name = "label9";
            label9.Size = new Size(70, 28);
            label9.TabIndex = 20;
            label9.Text = "Stock:";
            // 
            // cmbNombre
            // 
            cmbNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNombre.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbNombre.DropDownStyle = ComboBoxStyle.DropDown;
            cmbNombre.FormattingEnabled = true;
            cmbNombre.Location = new Point(160, 64);
            cmbNombre.MaxDropDownItems = 10;
            cmbNombre.Name = "cmbNombre";
            cmbNombre.Size = new Size(291, 28);
            cmbNombre.TabIndex = 21;
            cmbNombre.SelectedIndexChanged += cmbNombre_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(39, 60);
            label5.Name = "label5";
            label5.Size = new Size(103, 28);
            label5.TabIndex = 22;
            label5.Text = "Producto:";
            // 
            // tabAdmin
            // 
            tabAdmin.Controls.Add(btnDetalle);
            tabAdmin.Controls.Add(btnMasVendidos);
            tabAdmin.Controls.Add(btnCierre);
            tabAdmin.Controls.Add(btnVerProductos);
            tabAdmin.Controls.Add(btnReporte);
            tabAdmin.Controls.Add(dgvReportes);
            tabAdmin.Location = new Point(4, 29);
            tabAdmin.Name = "tabAdmin";
            tabAdmin.Padding = new Padding(3);
            tabAdmin.Size = new Size(767, 398);
            tabAdmin.TabIndex = 2;
            tabAdmin.Text = "ADMINISTRACION";
            tabAdmin.UseVisualStyleBackColor = true;
            // 
            // btnDetalle
            // 
            btnDetalle.BackColor = Color.FromArgb(255, 128, 128);
            btnDetalle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnDetalle.ForeColor = SystemColors.ButtonFace;
            btnDetalle.Location = new Point(214, 18);
            btnDetalle.Name = "btnDetalle";
            btnDetalle.Size = new Size(167, 40);
            btnDetalle.TabIndex = 23;
            btnDetalle.Text = "DETALLE VENTA";
            btnDetalle.UseVisualStyleBackColor = false;
            btnDetalle.Click += btnDetalle_Click;
            // 
            // btnMasVendidos
            // 
            btnMasVendidos.BackColor = Color.OliveDrab;
            btnMasVendidos.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMasVendidos.Location = new Point(580, 227);
            btnMasVendidos.Name = "btnMasVendidos";
            btnMasVendidos.Size = new Size(162, 127);
            btnMasVendidos.TabIndex = 22;
            btnMasVendidos.Text = "TOP PRODUCTOS MÁS VENDIDOS";
            btnMasVendidos.UseVisualStyleBackColor = false;
            btnMasVendidos.Click += btnMasVendidos_Click;
            // 
            // btnCierre
            // 
            btnCierre.BackColor = Color.Lime;
            btnCierre.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCierre.Location = new Point(580, 128);
            btnCierre.Name = "btnCierre";
            btnCierre.Size = new Size(164, 68);
            btnCierre.TabIndex = 21;
            btnCierre.Text = "CIERRE DE CAJA";
            btnCierre.UseVisualStyleBackColor = false;
            btnCierre.Click += btnCierre_Click;
            // 
            // btnVerProductos
            // 
            btnVerProductos.BackColor = Color.FromArgb(192, 192, 0);
            btnVerProductos.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnVerProductos.ForeColor = SystemColors.ButtonFace;
            btnVerProductos.Location = new Point(399, 18);
            btnVerProductos.Name = "btnVerProductos";
            btnVerProductos.Size = new Size(166, 40);
            btnVerProductos.TabIndex = 20;
            btnVerProductos.Text = "VER PRODUCTOS";
            btnVerProductos.UseVisualStyleBackColor = false;
            btnVerProductos.Click += btnVerProductos_Click;
            // 
            // btnReporte
            // 
            btnReporte.BackColor = Color.FromArgb(128, 64, 0);
            btnReporte.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btnReporte.ForeColor = SystemColors.ButtonFace;
            btnReporte.Location = new Point(23, 18);
            btnReporte.Name = "btnReporte";
            btnReporte.Size = new Size(167, 40);
            btnReporte.TabIndex = 19;
            btnReporte.Text = "VER REPORTES";
            btnReporte.UseVisualStyleBackColor = false;
            btnReporte.Click += btnReporte_Click;
            // 
            // dgvReportes
            // 
            dgvReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportes.Location = new Point(23, 64);
            dgvReportes.Name = "dgvReportes";
            dgvReportes.RowHeadersWidth = 51;
            dgvReportes.Size = new Size(542, 328);
            dgvReportes.TabIndex = 18;
            // 
            // FrmSistema
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 462);
            Controls.Add(tabSistema);
            MaximizeBox = false;
            Name = "FrmSistema";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmSistema";
            tabSistema.ResumeLayout(false);
            tabVentas.ResumeLayout(false);
            tabVentas.PerformLayout();
            tabInventario.ResumeLayout(false);
            tabInventario.PerformLayout();
            tabAdmin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReportes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabSistema;
        private TabPage tabVentas;
        private TabPage tabInventario;
        private TabPage tabAdmin;
        private Label lblVuelto;
        private Label label6;
        private Button btnVuelto;
        private TextBox txtPagoCon;
        private Label label7;
        private Button bntAgregar;
        private TextBox txtCantidad;
        private Button btnCobrar;
        private Label label11;
        private Label label4;
        private Label label10;
        private TextBox txtNuevaCantidad;
        private Label label8;
        private Button btnInvModificar;
        private Button btnConsultar;
        private TextBox txtStock;
        private Label label9;
        private ComboBox cmbNombre;
        private Label label5;
        private Button btnMasVendidos;
        private Button btnCierre;
        private Button btnVerProductos;
        private Button btnReporte;
        private DataGridView dgvReportes;
        private TextBox txtMonto;
        private Button btnDetalle;
        private ComboBox cmbProducto;
    }
}