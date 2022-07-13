namespace controladorAtm
{
    partial class PantallaIncial
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelServicio = new System.Windows.Forms.GroupBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.imagenProceso = new System.Windows.Forms.PictureBox();
            this.btn_parar = new System.Windows.Forms.Button();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.dataGridMonitorDispositivos = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txbx_visor_evento = new System.Windows.Forms.RichTextBox();
            this.tabMonitorTransaccion = new System.Windows.Forms.TabControl();
            this.codigoAtm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoConexion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2.SuspendLayout();
            this.panelServicio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagenProceso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMonitorDispositivos)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabMonitorTransaccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelServicio);
            this.tabPage2.Controls.Add(this.dataGridMonitorDispositivos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(821, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Monitor Servicio Terminal";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelServicio
            // 
            this.panelServicio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelServicio.Controls.Add(this.lblEstado);
            this.panelServicio.Controls.Add(this.imagenProceso);
            this.panelServicio.Controls.Add(this.btn_parar);
            this.panelServicio.Controls.Add(this.btn_iniciar);
            this.panelServicio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelServicio.Location = new System.Drawing.Point(450, 3);
            this.panelServicio.Name = "panelServicio";
            this.panelServicio.Size = new System.Drawing.Size(368, 230);
            this.panelServicio.TabIndex = 1;
            this.panelServicio.TabStop = false;
            this.panelServicio.Text = "Servicios Controlador";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(55, 116);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(127, 20);
            this.lblEstado.TabIndex = 3;
            this.lblEstado.Text = "Estado Servicio :";
            // 
            // imagenProceso
            // 
            this.imagenProceso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.imagenProceso.Location = new System.Drawing.Point(212, 116);
            this.imagenProceso.Name = "imagenProceso";
            this.imagenProceso.Size = new System.Drawing.Size(73, 63);
            this.imagenProceso.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imagenProceso.TabIndex = 2;
            this.imagenProceso.TabStop = false;
            // 
            // btn_parar
            // 
            this.btn_parar.Location = new System.Drawing.Point(187, 48);
            this.btn_parar.Name = "btn_parar";
            this.btn_parar.Size = new System.Drawing.Size(98, 37);
            this.btn_parar.TabIndex = 1;
            this.btn_parar.Text = "Parar";
            this.btn_parar.UseVisualStyleBackColor = true;
            this.btn_parar.Click += new System.EventHandler(this.btn_parar_Click);
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.Location = new System.Drawing.Point(59, 48);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(98, 37);
            this.btn_iniciar.TabIndex = 0;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.UseVisualStyleBackColor = true;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // dataGridMonitorDispositivos
            // 
            this.dataGridMonitorDispositivos.AllowUserToAddRows = false;
            this.dataGridMonitorDispositivos.AllowUserToDeleteRows = false;
            this.dataGridMonitorDispositivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMonitorDispositivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMonitorDispositivos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoAtm,
            this.estadoConexion});
            this.dataGridMonitorDispositivos.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridMonitorDispositivos.Location = new System.Drawing.Point(3, 3);
            this.dataGridMonitorDispositivos.Name = "dataGridMonitorDispositivos";
            this.dataGridMonitorDispositivos.ReadOnly = true;
            this.dataGridMonitorDispositivos.RowHeadersVisible = false;
            this.dataGridMonitorDispositivos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridMonitorDispositivos.Size = new System.Drawing.Size(447, 230);
            this.dataGridMonitorDispositivos.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txbx_visor_evento);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(821, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Monitor Transaccion Terminal";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txbx_visor_evento
            // 
            this.txbx_visor_evento.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txbx_visor_evento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txbx_visor_evento.Location = new System.Drawing.Point(3, 3);
            this.txbx_visor_evento.Name = "txbx_visor_evento";
            this.txbx_visor_evento.Size = new System.Drawing.Size(815, 230);
            this.txbx_visor_evento.TabIndex = 1;
            this.txbx_visor_evento.Text = "";
            // 
            // tabMonitorTransaccion
            // 
            this.tabMonitorTransaccion.Controls.Add(this.tabPage1);
            this.tabMonitorTransaccion.Controls.Add(this.tabPage2);
            this.tabMonitorTransaccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMonitorTransaccion.Location = new System.Drawing.Point(0, 0);
            this.tabMonitorTransaccion.Name = "tabMonitorTransaccion";
            this.tabMonitorTransaccion.SelectedIndex = 0;
            this.tabMonitorTransaccion.Size = new System.Drawing.Size(829, 262);
            this.tabMonitorTransaccion.TabIndex = 1;
            // 
            // codigoAtm
            // 
            this.codigoAtm.HeaderText = "Código Terminal";
            this.codigoAtm.Name = "codigoAtm";
            this.codigoAtm.ReadOnly = true;
            // 
            // estadoConexion
            // 
            this.estadoConexion.HeaderText = "Estado Conexion";
            this.estadoConexion.Name = "estadoConexion";
            this.estadoConexion.ReadOnly = true;
            this.estadoConexion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.estadoConexion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PantallaIncial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 262);
            this.Controls.Add(this.tabMonitorTransaccion);
            this.Name = "PantallaIncial";
            this.Text = "Aplicacion Controlador ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tabPage2.ResumeLayout(false);
            this.panelServicio.ResumeLayout(false);
            this.panelServicio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagenProceso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMonitorDispositivos)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabMonitorTransaccion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridMonitorDispositivos;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox txbx_visor_evento;
        private System.Windows.Forms.TabControl tabMonitorTransaccion;
        private System.Windows.Forms.GroupBox panelServicio;
        private System.Windows.Forms.Button btn_parar;
        private System.Windows.Forms.Button btn_iniciar;
        private System.Windows.Forms.PictureBox imagenProceso;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoAtm;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoConexion;


    }
}

