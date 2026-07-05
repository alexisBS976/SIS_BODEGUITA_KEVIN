using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIS_BODEGUITA_KEVIN
{
    public partial class FrmSistema : Form
    {
        // Variables globales para rastrear el subtotal acumulado
        // y la cantidad de artículos en la transacción actual
        private decimal acumuladorTotal = 0;
        private int acumuladorCantidad = 0;
        string productoEnMemoria = "";

        public FrmSistema()
        {
            InitializeComponent();
            CargarProductos();

            // Asegurar que el ComboBox permita edición y autocompletado
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        /// Constructor que permite abrir el formulario con una pestaña específica seleccionada.
        /// </summary>
        public FrmSistema(int pestanaSeleccionada)
        {
            InitializeComponent();
            CargarProductos();

            // Asegurar que el ComboBox permita edición y autocompletado
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDown;
            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;

            // Dirige automáticamente a la pestaña elegida
            tabSistema.SelectedIndex = pestanaSeleccionada;

            // Bloquea las tres pestañas por igual
            ((Control)tabSistema.TabPages[0]).Enabled = false;
            ((Control)tabSistema.TabPages[1]).Enabled = false;
            ((Control)tabSistema.TabPages[2]).Enabled = false;

            // Desbloquea solo la pestaña que vas a usar
            ((Control)tabSistema.TabPages[pestanaSeleccionada]).Enabled = true;
        }

        /// <summary>
        /// Carga los nombres de los productos desde la base de datos a los ComboBoxes
        /// </summary>
        private void CargarProductos()
        {
            try
            {
                // Usamos SQLiteConnection y llamamos a la cadena de tu clase Conexion_BD
                using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
                {
                    conexion.Open();

                    string query = "SELECT nombre FROM Productos";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                    {
                        using (SQLiteDataReader lector = cmd.ExecuteReader())
                        {
                            cmbProducto.Items.Clear();
                            cmbNombre.Items.Clear();

                            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

                            while (lector.Read())
                            {
                                string nombreProd = lector["nombre"]?.ToString() ?? "";

                                if (!string.IsNullOrEmpty(nombreProd))
                                {
                                    cmbProducto.Items.Add(nombreProd);
                                    cmbNombre.Items.Add(nombreProd);
                                    coleccion.Add(nombreProd);
                                }
                            }

                            cmbNombre.AutoCompleteCustomSource = coleccion;
                            cmbNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            cmbNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

                            cmbProducto.AutoCompleteCustomSource = coleccion;
                            cmbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            cmbProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Método reutilizable que carga el precio de un producto en txtMonto.
        /// </summary>
        private void CargarPrecioProducto(string nombreProducto)
        {
            if (string.IsNullOrWhiteSpace(nombreProducto))
            {
                txtMonto.Clear();
                return;
            }

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
            {
                string query = "SELECT precio FROM Productos WHERE nombre = @prod";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@prod", nombreProducto);

                    try
                    {
                        conexion.Open();
                        object resultado = cmd.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            txtMonto.Text = Convert.ToDecimal(resultado).ToString("N2");
                        }
                        else
                        {
                            txtMonto.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al recuperar el precio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1) return;
            CargarPrecioProducto(cmbProducto.Text);
        }

        /// <summary>
        /// Gestiona la confirmación del pago, cálculo del vuelto, inserción de la venta e inventario.
        /// </summary>
        private void btnVenta_Click(object sender, EventArgs e)
        {
            if (acumuladorTotal <= 0)
            {
                MessageBox.Show("Debe añadir productos antes de cobrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                MessageBox.Show("Ingrese el monto con el que paga el cliente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPagoCon.Text, out decimal montoPagado))
            {
                MessageBox.Show("Ingrese un monto de pago válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal vueltoFinal = montoPagado - acumuladorTotal;

            if (vueltoFinal < 0)
            {
                MessageBox.Show("El dinero ingresado es insuficiente.", "Dinero Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblVuelto.Text = "Falta Dinero";
                return;
            }

            lblVuelto.Text = "S/. " + vueltoFinal.ToString("N2");
            string productoSeleccionado = string.IsNullOrEmpty(cmbNombre.Text) ? cmbProducto.Text : cmbNombre.Text;

            if (string.IsNullOrEmpty(productoSeleccionado))
            {
                MessageBox.Show("No se ha seleccionado un producto válido para procesar el stock.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
            {
                try
                {
                    conexion.Open();

                    // Transacciones seguras usando SQLite
                    using (SQLiteTransaction transaccion = conexion.BeginTransaction())
                    {
                        string querySelect = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                        int stockActual = 0;
                        bool productoExiste = false;

                        using (SQLiteCommand cmdSelect = new SQLiteCommand(querySelect, conexion, transaccion))
                        {
                            cmdSelect.Parameters.AddWithValue("@nom", productoSeleccionado);
                            object resultado = cmdSelect.ExecuteScalar();

                            if (resultado != null && resultado != DBNull.Value)
                            {
                                stockActual = Convert.ToInt32(resultado);
                                productoExiste = true;
                            }
                        }

                        if (!productoExiste)
                        {
                            MessageBox.Show("El producto no fue encontrado en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        int nuevoStock = stockActual - acumuladorCantidad;

                        if (nuevoStock < 0)
                        {
                            MessageBox.Show($"No hay suficiente stock en el inventario. Stock actual: {stockActual}", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Guarda la venta en la Base de Datos
                        string nuevoId = Conexion_Ventas.ObtenerSiguienteId();
                        Conexion_Ventas.InsertarVenta(nuevoId, acumuladorTotal, acumuladorCantidad);

                        // Actualizar el stock
                        string queryUpdate = "UPDATE Productos SET stock_actual = @stock WHERE nombre = @nom";
                        using (SQLiteCommand cmdUpdate = new SQLiteCommand(queryUpdate, conexion, transaccion))
                        {
                            cmdUpdate.Parameters.AddWithValue("@stock", nuevoStock);
                            cmdUpdate.Parameters.AddWithValue("@nom", productoSeleccionado);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al procesar la venta: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            MessageBox.Show(
                "Venta guardada con éxito.\n\n" +
                "ID Venta: " + Conexion_Ventas.ObtenerSiguienteId() + "\n" +
                "Cantidad de Productos: " + acumuladorCantidad +
                "\nTotal Cobrado: S/. " + acumuladorTotal.ToString("N2") +
                "\nVuelto: " + lblVuelto.Text,
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            acumuladorTotal = 0;
            acumuladorCantidad = 0;

            txtCantidad.Clear();
            txtMonto.Clear();
            txtPagoCon.Clear();
            txtStock.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbNombre.SelectedIndex = -1;
        }

        /// <summary>
        /// Agrega el producto seleccionado con su respectiva cantidad al carrito virtual.
        /// </summary>
        private void bntAgregar_Click(object sender, EventArgs e)
        {
            string productoActual = string.IsNullOrEmpty(cmbNombre.Text) ? cmbProducto.Text : cmbNombre.Text;
            if (string.IsNullOrWhiteSpace(productoActual))
            {
                MessageBox.Show("Por favor, seleccione un producto antes de agregar.");
                return;
            }

            int cantidadInput;
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidadInput) || cantidadInput <= 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad válida mayor a cero (solo números enteros).");
                return;
            }

            decimal montoInput;
            if (!decimal.TryParse(txtMonto.Text.Trim(), out montoInput) || montoInput <= 0)
            {
                MessageBox.Show("El precio del producto no es válido.");
                return;
            }

            int stockDisponible = Conexion_Inventario.ConsultarStockActual(productoActual);
            if (cantidadInput > stockDisponible)
            {
                MessageBox.Show($"Stock insuficiente. Disponible: {stockDisponible} unidades, solicitadas: {cantidadInput} unidades.",
                    "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            productoEnMemoria = productoActual;
            acumuladorCantidad += cantidadInput;
            acumuladorTotal += (montoInput * cantidadInput);

            txtMonto.Text = acumuladorTotal.ToString("0.00");

            txtCantidad.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbNombre.SelectedIndex = -1;

            MessageBox.Show($"Se agregaron {cantidadInput} unidades de '{productoEnMemoria}' ", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Gestiona la cobranza definitiva y asienta las tablas relacionadas
        /// </summary>
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (acumuladorTotal <= 0)
            {
                MessageBox.Show("Debe añadir productos antes de cobrar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal montoPagado;

            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                MessageBox.Show("Ingrese el monto con el que paga el cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPagoCon.Text, out montoPagado))
            {
                MessageBox.Show("Ingrese un monto de pago válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal vueltoFinal = montoPagado - acumuladorTotal;

            if (vueltoFinal < 0)
            {
                MessageBox.Show("El dinero ingresado es insuficiente para cubrir el total.", "Dinero Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                lblVuelto.Text = "Falta Dinero";
                return;
            }

            lblVuelto.Text = "S/. " + vueltoFinal.ToString("N2");

            string nuevoId = Conexion_Ventas.ObtenerSiguienteId();
            Conexion_Ventas.InsertarVenta(nuevoId, acumuladorTotal, acumuladorCantidad);

            string productoSeleccionado = productoEnMemoria;
            int cantidadVendida = acumuladorCantidad;

            using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
            {
                conexion.Open();
                string queryIdProducto = "SELECT id_producto FROM Productos WHERE nombre = @nom";
                SQLiteCommand cmdIdProd = new SQLiteCommand(queryIdProducto, conexion);
                cmdIdProd.Parameters.AddWithValue("@nom", productoSeleccionado);

                object resId = cmdIdProd.ExecuteScalar();
                string idProducto = Convert.ToString(resId) ?? "";

                if (string.IsNullOrEmpty(idProducto))
                {
                    MessageBox.Show($"Error crítico: No se encontró el identificador de '{productoSeleccionado}' en la base de datos.", "Error de Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nuevoIdDetalle = "D001";
                string queryMaxDetalle = "SELECT MAX(id_detalle) FROM DetalleVenta";
                SQLiteCommand cmdMaxDetalle = new SQLiteCommand(queryMaxDetalle, conexion);

                object maxIdObj = cmdMaxDetalle.ExecuteScalar();

                if (maxIdObj != null && maxIdObj != DBNull.Value)
                {
                    string maxIdStr = Convert.ToString(maxIdObj) ?? "";

                    if (maxIdStr.Length > 1 && int.TryParse(maxIdStr.Substring(1), out int numeroActual))
                    {
                        nuevoIdDetalle = "D" + (numeroActual + 1).ToString("D3");
                    }
                }

                string queryInsertDetalle = @"INSERT INTO DetalleVenta (id_detalle, id_venta, id_producto, cantidad, subtotal) 
                                             VALUES (@idDetalle, @idVenta, @idProducto, @cantidad, @subtotal)";

                using (SQLiteCommand cmdDetalle = new SQLiteCommand(queryInsertDetalle, conexion))
                {
                    cmdDetalle.Parameters.AddWithValue("@idDetalle", nuevoIdDetalle);
                    cmdDetalle.Parameters.AddWithValue("@idVenta", nuevoId);
                    cmdDetalle.Parameters.AddWithValue("@idProducto", idProducto);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", cantidadVendida);
                    cmdDetalle.Parameters.AddWithValue("@subtotal", acumuladorTotal);

                    cmdDetalle.ExecuteNonQuery();
                }

                string querySelect = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                SQLiteCommand cmdSelect = new SQLiteCommand(querySelect, conexion);
                cmdSelect.Parameters.AddWithValue("@nom", productoSeleccionado);

                object resultado = cmdSelect.ExecuteScalar();

                if (resultado != null)
                {
                    int stockActual = Convert.ToInt32(resultado);
                    int nuevoStock = stockActual - cantidadVendida;

                    if (nuevoStock < 0)
                    {
                        MessageBox.Show("No hay suficiente stock en el inventario para completar la transacción.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    string queryUpdate = "UPDATE Productos SET stock_actual = @stock WHERE nombre = @nom";
                    SQLiteCommand cmdUpdate = new SQLiteCommand(queryUpdate, conexion);
                    cmdUpdate.Parameters.AddWithValue("@stock", nuevoStock);
                    cmdUpdate.Parameters.AddWithValue("@nom", productoSeleccionado);

                    cmdUpdate.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Venta guardada con éxito.\n\n" +
                "ID Venta: " + nuevoId +
                "\nCantidad de Productos: " + acumuladorCantidad +
                "\nTotal Cobrado: S/. " + acumuladorTotal.ToString("N2") +
                "\nVuelto: " + lblVuelto.Text,
                "Transacción Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information
            );

            acumuladorTotal = 0;
            acumuladorCantidad = 0;
            productoEnMemoria = "";

            txtCantidad.Clear();
            txtMonto.Clear();
            txtPagoCon.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbNombre.SelectedIndex = -1;
        }

        private void btnVuelto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                lblVuelto.Text = "S/. 0.00";
                return;
            }

            if (!decimal.TryParse(txtPagoCon.Text, out decimal montoPagado))
            {
                MessageBox.Show("Ingrese un monto válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal vuelto = montoPagado - acumuladorTotal;
            lblVuelto.Text = vuelto >= 0 ? "S/. " + vuelto.ToString("N2") : "Falta Dinero";
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto antes de consultar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string producto = cmbNombre.Text;
            int stockReal = Conexion_Inventario.ConsultarStockActual(producto);
            txtStock.Text = stockReal.ToString();
        }

        private void btnInvModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text) || string.IsNullOrEmpty(txtNuevaCantidad.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese la cantidad a añadir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtNuevaCantidad.Text, out int cantidadAIngresar) || cantidadAIngresar <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a cero para reabastecer.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string producto = cmbNombre.Text;
            Conexion_Inventario.SurtirMercaderia(producto, cantidadAIngresar);

            MessageBox.Show("¡Stock modificado con éxito! Se añadieron " + cantidadAIngresar + " unidades.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int nuevoStock = Conexion_Inventario.ConsultarStockActual(producto);
            txtStock.Text = nuevoStock.ToString();
            txtNuevaCantidad.Clear();
        }

        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text) || cmbNombre.SelectedIndex == -1) return;

            string productoSeleccionado = cmbNombre.Text;
            int stockReal = Conexion_Inventario.ConsultarStockActual(productoSeleccionado);
            txtStock.Text = stockReal.ToString();

            CargarPrecioProducto(productoSeleccionado);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            dgvReportes.DataSource = Conexion_Ventas.TraerReporte();
        }

        /// <summary>
        /// Extrae la lista completa de artículos con alertas de reposición.
        /// </summary>
        private void btnVerProductos_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
            {
                string query = @"SELECT 
                            id_producto, 
                            nombre, 
                            precio, 
                            stock_actual, 
                            stock_minimo, 
                            unidad_medida, 
                            categoria,
                            CASE 
                                WHEN stock_actual <= (stock_minimo + 1) THEN 'Reponer' 
                                ELSE 'Disponible' 
                            END AS [estado] 
                         FROM Productos";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    // Cambiado con éxito a SQLiteDataAdapter
                    using (SQLiteDataAdapter adaptador = new SQLiteDataAdapter(cmd))
                    {
                        DataTable datos = new DataTable();
                        try
                        {
                            conexion.Open();
                            adaptador.Fill(datos);
                            dgvReportes.DataSource = datos;
                            dgvReportes.AutoResizeColumns();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Proceso de arqueo o cierre de caja diario utilizando funciones compatibles con SQLite.
        /// </summary>
        private void btnCierre_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conexion = new SQLiteConnection(Conexion_BD.Cadena))
            {
                string query = @"SELECT IFNULL(SUM(total), 0) 
                                 FROM Ventas 
                                 WHERE date(fecha_venta) = date('now', 'localtime')";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conexion))
                {
                    try
                    {
                        conexion.Open();
                        decimal totalCobradoHoy = Convert.ToDecimal(cmd.ExecuteScalar());

                        MessageBox.Show("=== CIERRE DE CAJA DIARIO ===\n\n" +
                                        "Fecha: " + DateTime.Today.ToShortDateString() + "\n" +
                                        "Total Efectivo de Hoy: S/. " + totalCobradoHoy.ToString("N2"),
                                        "Cierre de Caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al realizar el cierre de caja: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnMasVendidos_Click(object sender, EventArgs e)
        {
            DataTable datosTop = Conexion_Admin.ObtenerTopProductos();
            dgvReportes.DataSource = datosTop;
            dgvReportes.AutoResizeColumns();
        }

        private void btnInvModificar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text) || string.IsNullOrEmpty(txtNuevaCantidad.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese la cantidad a añadir.");
                return;
            }

            string producto = cmbNombre.Text;
            int cantidadAIngresar = Convert.ToInt32(txtNuevaCantidad.Text);

            Conexion_Inventario.SurtirMercaderia(producto, cantidadAIngresar);

            MessageBox.Show("¡Stock modificado con éxito! Se añadieron " + cantidadAIngresar + " unidades.");

            int nuevoStock = Conexion_Inventario.ConsultarStockActual(producto);
            txtStock.Text = nuevoStock.ToString();
            txtNuevaCantidad.Clear();
        }

        private void cmbProducto_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1)
                return;

            txtMonto.Text = Conexion_Productos.ObtenerPrecioProducto(cmbProducto.Text).ToString("0.00");
        }

        private void tabVentas_Click(object sender, EventArgs e)
        {
            decimal total = acumuladorTotal;

            if (decimal.TryParse(txtPagoCon.Text, out decimal pago))
            {
                decimal vuelto = pago - total;
                lblVuelto.Text = vuelto >= 0 ? vuelto.ToString("0.00") : "0.00";
            }
            else
            {
                lblVuelto.Text = "0.00";
            }
        }

        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto antes de consultar.");
                return;
            }
            string producto = cmbNombre.Text;
            int stockReal = Conexion_Inventario.ConsultarStockActual(producto);
            txtStock.Text = stockReal.ToString();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {   
            dgvReportes.DataSource = null;
            dgvReportes.DataSource = Conexion_Admin.ObtenerDetalleVenta();
            dgvReportes.AutoResizeColumns();
        }
    }
}