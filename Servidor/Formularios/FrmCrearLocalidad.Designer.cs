namespace Servidor.Formularios
{
    partial class FrmCrearLocalidad
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
            this.numMesasLocalidad = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombreLocalidad = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnExit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMesasLocalidad)).BeginInit();
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
            this.btnGuardarNumeroVotantes.Location = new System.Drawing.Point(251, 324);
            this.btnGuardarNumeroVotantes.Name = "btnGuardarNumeroVotantes";
            this.btnGuardarNumeroVotantes.Size = new System.Drawing.Size(180, 45);
            this.btnGuardarNumeroVotantes.TabIndex = 6;
            this.btnGuardarNumeroVotantes.Text = "Guardar";
            // 
            // numMesasLocalidad
            // 
            this.numMesasLocalidad.BackColor = System.Drawing.Color.Transparent;
            this.numMesasLocalidad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numMesasLocalidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMesasLocalidad.Location = new System.Drawing.Point(363, 242);
            this.numMesasLocalidad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numMesasLocalidad.Name = "numMesasLocalidad";
            this.numMesasLocalidad.Size = new System.Drawing.Size(243, 48);
            this.numMesasLocalidad.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(157, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Numero de votantes por mesa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 32);
            this.label2.TabIndex = 8;
            this.label2.Text = "Numero de mesas:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 64);
            this.label3.TabIndex = 9;
            this.label3.Text = "Nombre de \r\nla localidad:";
            // 
            // txtNombreLocalidad
            // 
            this.txtNombreLocalidad.Animated = true;
            this.txtNombreLocalidad.BorderRadius = 5;
            this.txtNombreLocalidad.BorderThickness = 2;
            this.txtNombreLocalidad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreLocalidad.DefaultText = "";
            this.txtNombreLocalidad.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNombreLocalidad.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNombreLocalidad.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreLocalidad.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNombreLocalidad.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreLocalidad.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.txtNombreLocalidad.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNombreLocalidad.Location = new System.Drawing.Point(251, 159);
            this.txtNombreLocalidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNombreLocalidad.Name = "txtNombreLocalidad";
            this.txtNombreLocalidad.PlaceholderText = "";
            this.txtNombreLocalidad.SelectedText = "";
            this.txtNombreLocalidad.Size = new System.Drawing.Size(402, 60);
            this.txtNombreLocalidad.TabIndex = 10;
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
            // FrmCrearLocalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtNombreLocalidad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGuardarNumeroVotantes);
            this.Controls.Add(this.numMesasLocalidad);
            this.Controls.Add(this.label1);
            this.Name = "FrmCrearLocalidad";
            this.Text = "Crear localidad";
            ((System.ComponentModel.ISupportInitialize)(this.numMesasLocalidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnGuardarNumeroVotantes;
        private Guna.UI2.WinForms.Guna2NumericUpDown numMesasLocalidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtNombreLocalidad;
        private System.Windows.Forms.Label btnExit;
    }
}