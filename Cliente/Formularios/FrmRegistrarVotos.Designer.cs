namespace Servidor.Formularios
{
    partial class FrmRegistrarVotos
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
            this.btnGuardarNumeroVotantes = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Label();
            this.numMesa = new Guna.UI2.WinForms.Guna2NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numMesa)).BeginInit();
            this.SuspendLayout();
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
            this.btnGuardarNumeroVotantes.Location = new System.Drawing.Point(219, 375);
            this.btnGuardarNumeroVotantes.Name = "btnGuardarNumeroVotantes";
            this.btnGuardarNumeroVotantes.Size = new System.Drawing.Size(180, 45);
            this.btnGuardarNumeroVotantes.TabIndex = 6;
            this.btnGuardarNumeroVotantes.Text = "Guardar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(197, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Registro de votos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 32);
            this.label2.TabIndex = 8;
            this.label2.Text = "Numero de mesa:";
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(12, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(22, 22);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "X";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // numMesa
            // 
            this.numMesa.BackColor = System.Drawing.Color.Transparent;
            this.numMesa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numMesa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMesa.Location = new System.Drawing.Point(318, 71);
            this.numMesa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMesa.Name = "numMesa";
            this.numMesa.Size = new System.Drawing.Size(243, 48);
            this.numMesa.TabIndex = 12;
            // 
            // FrmRegistrarVotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 450);
            this.Controls.Add(this.numMesa);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGuardarNumeroVotantes);
            this.Controls.Add(this.label1);
            this.Name = "FrmRegistrarVotos";
            this.Text = "Votos";
            ((System.ComponentModel.ISupportInitialize)(this.numMesa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnGuardarNumeroVotantes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label btnExit;
        private Guna.UI2.WinForms.Guna2NumericUpDown numMesa;
    }
}