namespace Servidor
{
    partial class FrmAjustarNVotantes
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
            ((System.ComponentModel.ISupportInitialize)(this.numVotantes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero de votantes por mesa";
            // 
            // numVotantes
            // 
            this.numVotantes.BackColor = System.Drawing.Color.Transparent;
            this.numVotantes.BorderRadius = 5;
            this.numVotantes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numVotantes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numVotantes.Location = new System.Drawing.Point(222, 178);
            this.numVotantes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numVotantes.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numVotantes.Name = "numVotantes";
            this.numVotantes.Size = new System.Drawing.Size(243, 46);
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
            this.btnGuardarNumeroVotantes.Location = new System.Drawing.Point(250, 277);
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
            // FrmAjustarNVotantes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnGuardarNumeroVotantes);
            this.Controls.Add(this.numVotantes);
            this.Controls.Add(this.label1);
            this.Name = "FrmAjustarNVotantes";
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
    }
}