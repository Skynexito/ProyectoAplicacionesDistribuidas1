namespace Servidor
{
    partial class FrmAsignacionMesa
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
            this.btnAsignarMesa = new Guna.UI2.WinForms.Guna2Button();
            this.btnExit = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numMesa = new System.Windows.Forms.Label();
            this.mcVotacion = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Asignacion de mesa";
            // 
            // btnAsignarMesa
            // 
            this.btnAsignarMesa.BorderRadius = 10;
            this.btnAsignarMesa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAsignarMesa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAsignarMesa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAsignarMesa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAsignarMesa.FillColor = System.Drawing.Color.SlateBlue;
            this.btnAsignarMesa.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAsignarMesa.ForeColor = System.Drawing.Color.White;
            this.btnAsignarMesa.Location = new System.Drawing.Point(66, 348);
            this.btnAsignarMesa.Name = "btnAsignarMesa";
            this.btnAsignarMesa.Size = new System.Drawing.Size(180, 45);
            this.btnAsignarMesa.TabIndex = 2;
            this.btnAsignarMesa.Text = "Asignar";
            this.btnAsignarMesa.Click += new System.EventHandler(this.btnAsignarMesa_Click);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(350, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 32);
            this.label2.TabIndex = 13;
            this.label2.Text = "Su numero de mesa es:";
            // 
            // numMesa
            // 
            this.numMesa.AutoSize = true;
            this.numMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMesa.Location = new System.Drawing.Point(474, 205);
            this.numMesa.Name = "numMesa";
            this.numMesa.Size = new System.Drawing.Size(30, 32);
            this.numMesa.TabIndex = 14;
            this.numMesa.Text = "0";
            // 
            // mcVotacion
            // 
            this.mcVotacion.Location = new System.Drawing.Point(31, 108);
            this.mcVotacion.Name = "mcVotacion";
            this.mcVotacion.TabIndex = 15;
            // 
            // FrmAsignacionMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 450);
            this.Controls.Add(this.mcVotacion);
            this.Controls.Add(this.numMesa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAsignarMesa);
            this.Controls.Add(this.label1);
            this.Name = "FrmAsignacionMesa";
            this.Text = "Ajustar numero de votantes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnAsignarMesa;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label numMesa;
        private System.Windows.Forms.MonthCalendar mcVotacion;
    }
}