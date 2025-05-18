namespace Servidor
{
    partial class FrmAjustarParametros
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
            this.label1 = new System.Windows.Forms.Label();
            this.numVotantes = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnGuardarNumeroVotantes = new Guna.UI2.WinForms.Guna2Button();
            this.btnExit = new System.Windows.Forms.Label();
            this.mcFecha = new System.Windows.Forms.MonthCalendar();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.numVotantes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de votantes \r\n        por mesa:";
            // 
            // numVotantes
            // 
            this.numVotantes.BackColor = System.Drawing.Color.Transparent;
            this.numVotantes.BorderRadius = 5;
            this.numVotantes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numVotantes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numVotantes.Location = new System.Drawing.Point(61, 189);
            this.numVotantes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numVotantes.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numVotantes.Name = "numVotantes";
            this.numVotantes.Size = new System.Drawing.Size(175, 47);
            this.numVotantes.TabIndex = 1;
            // 
            // btnGuardarNumeroVotantes
            // 
            this.btnGuardarNumeroVotantes.BorderRadius = 10;
            this.btnGuardarNumeroVotantes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardarNumeroVotantes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGuardarNumeroVotantes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGuardarNumeroVotantes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGuardarNumeroVotantes.FillColor = System.Drawing.Color.SlateBlue;
            this.btnGuardarNumeroVotantes.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarNumeroVotantes.ForeColor = System.Drawing.Color.White;
            this.btnGuardarNumeroVotantes.Location = new System.Drawing.Point(56, 268);
            this.btnGuardarNumeroVotantes.Name = "btnGuardarNumeroVotantes";
            this.btnGuardarNumeroVotantes.Size = new System.Drawing.Size(180, 45);
            this.btnGuardarNumeroVotantes.TabIndex = 2;
            this.btnGuardarNumeroVotantes.Text = "Guardar";
            this.btnGuardarNumeroVotantes.Click += new System.EventHandler(this.btnGuardarNumeroVotantes_Click);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(12, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(22, 22);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "X";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // mcFecha
            // 
            this.mcFecha.Location = new System.Drawing.Point(355, 118);
            this.mcFecha.MaxDate = new System.DateTime(2040, 12, 31, 0, 0, 0, 0);
            this.mcFecha.MaxSelectionCount = 1;
            this.mcFecha.Name = "mcFecha";
            this.mcFecha.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(401, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Fecha elección:";
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 10;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.SlateBlue;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(404, 358);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(180, 45);
            this.guna2Button1.TabIndex = 15;
            this.guna2Button1.Text = "Guardar";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // FrmAjustarParametros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 450);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mcFecha);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGuardarNumeroVotantes);
            this.Controls.Add(this.numVotantes);
            this.Controls.Add(this.label1);
            this.Name = "FrmAjustarParametros";
            this.Text = "Ajustar numero de votantes";
            ((System.ComponentModel.ISupportInitialize)(this.numVotantes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2NumericUpDown numVotantes;
        private Guna.UI2.WinForms.Guna2Button btnGuardarNumeroVotantes;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.MonthCalendar mcFecha;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}