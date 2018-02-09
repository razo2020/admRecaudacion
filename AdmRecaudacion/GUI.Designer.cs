namespace AdmRecaudacion
{
    partial class GUI
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cbxListBancos = new System.Windows.Forms.ComboBox();
            this.lblNomLista = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtboxOpen = new System.Windows.Forms.TextBox();
            this.opeFileDialogo = new System.Windows.Forms.OpenFileDialog();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lblOpen = new System.Windows.Forms.Label();
            this.rBtn1 = new System.Windows.Forms.RadioButton();
            this.rBtn2 = new System.Windows.Forms.RadioButton();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(770, 409);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Abrir";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cbxListBancos
            // 
            this.cbxListBancos.FormattingEnabled = true;
            this.cbxListBancos.Items.AddRange(new object[] {
            "BANCO DE CREDITO DEL PERU",
            "BANCO INTERBANK",
            "BBVA BANCO CONTINENTAL",
            "SCOTIABANK PERU SAA"});
            this.cbxListBancos.Location = new System.Drawing.Point(12, 46);
            this.cbxListBancos.Name = "cbxListBancos";
            this.cbxListBancos.Size = new System.Drawing.Size(331, 21);
            this.cbxListBancos.TabIndex = 2;
            this.cbxListBancos.SelectedIndexChanged += new System.EventHandler(this.cbxListBancos_SelectedIndexChanged);
            // 
            // lblNomLista
            // 
            this.lblNomLista.AutoSize = true;
            this.lblNomLista.Location = new System.Drawing.Point(12, 19);
            this.lblNomLista.Name = "lblNomLista";
            this.lblNomLista.Size = new System.Drawing.Size(118, 13);
            this.lblNomLista.TabIndex = 3;
            this.lblNomLista.Text = "Seleccionar Financiera:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(911, 330);
            this.dataGridView1.TabIndex = 4;
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(491, 44);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "Buscar";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtboxOpen
            // 
            this.txtboxOpen.Enabled = false;
            this.txtboxOpen.Location = new System.Drawing.Point(572, 46);
            this.txtboxOpen.Name = "txtboxOpen";
            this.txtboxOpen.Size = new System.Drawing.Size(354, 20);
            this.txtboxOpen.TabIndex = 6;
            // 
            // opeFileDialogo
            // 
            this.opeFileDialogo.FileName = "CREP1109 27.10";
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 411;
            this.lineShape1.X2 = 411;
            this.lineShape1.Y1 = 7;
            this.lineShape1.Y2 = 68;
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.Location = new System.Drawing.Point(491, 19);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Size = new System.Drawing.Size(157, 13);
            this.lblOpen.TabIndex = 8;
            this.lblOpen.Text = "Seleccionar archivo a procesar:";
            // 
            // rBtn1
            // 
            this.rBtn1.AutoSize = true;
            this.rBtn1.Checked = true;
            this.rBtn1.Location = new System.Drawing.Point(347, 17);
            this.rBtn1.Name = "rBtn1";
            this.rBtn1.Size = new System.Drawing.Size(55, 17);
            this.rBtn1.TabIndex = 9;
            this.rBtn1.TabStop = true;
            this.rBtn1.Text = "Online";
            this.rBtn1.UseVisualStyleBackColor = true;
            this.rBtn1.CheckedChanged += new System.EventHandler(this.rBtn1_CheckedChanged);
            // 
            // rBtn2
            // 
            this.rBtn2.AutoSize = true;
            this.rBtn2.Location = new System.Drawing.Point(424, 17);
            this.rBtn2.Name = "rBtn2";
            this.rBtn2.Size = new System.Drawing.Size(51, 17);
            this.rBtn2.TabIndex = 10;
            this.rBtn2.TabStop = true;
            this.rBtn2.Text = "Local";
            this.rBtn2.UseVisualStyleBackColor = true;
            this.rBtn2.CheckedChanged += new System.EventHandler(this.rBtn2_CheckedChanged);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(938, 444);
            this.shapeContainer1.TabIndex = 7;
            this.shapeContainer1.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(851, 409);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 444);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.rBtn2);
            this.Controls.Add(this.rBtn1);
            this.Controls.Add(this.lblOpen);
            this.Controls.Add(this.txtboxOpen);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblNomLista);
            this.Controls.Add(this.cbxListBancos);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "GUI";
            this.Text = "Recaudacion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ComboBox cbxListBancos;
        private System.Windows.Forms.Label lblNomLista;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtboxOpen;
        private System.Windows.Forms.OpenFileDialog opeFileDialogo;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.RadioButton rBtn1;
        private System.Windows.Forms.RadioButton rBtn2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Button btnGuardar;
    }
}

