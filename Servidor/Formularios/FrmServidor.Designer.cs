namespace Servidor
{
    partial class FrmServidor
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
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse();
            this.pnlForm = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnStats = new Guna.UI2.WinForms.Guna2Button();
            this.btnCrearLocalidades = new Guna.UI2.WinForms.Guna2Button();
            this.btnAjustarVotantes = new Guna.UI2.WinForms.Guna2Button();
            this.panelChildFrm = new System.Windows.Forms.Panel();
            this.pnlForm.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlForm.Controls.Add(this.label1);
            this.pnlForm.Location = new System.Drawing.Point(-4, -7);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(907, 31);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseDown);
            this.pnlForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseMove);
            this.pnlForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(885, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.guna2Panel1.Controls.Add(this.btnStats);
            this.guna2Panel1.Controls.Add(this.btnCrearLocalidades);
            this.guna2Panel1.Controls.Add(this.btnAjustarVotantes);
            this.guna2Panel1.Location = new System.Drawing.Point(-4, 12);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(205, 478);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnStats
            // 
            this.btnStats.BorderRadius = 10;
            this.btnStats.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStats.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStats.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStats.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStats.FillColor = System.Drawing.Color.SlateBlue;
            this.btnStats.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnStats.ForeColor = System.Drawing.Color.White;
            this.btnStats.Location = new System.Drawing.Point(12, 283);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(180, 45);
            this.btnStats.TabIndex = 2;
            this.btnStats.Text = "Estadisticas";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnCrearLocalidades
            // 
            this.btnCrearLocalidades.BorderRadius = 10;
            this.btnCrearLocalidades.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCrearLocalidades.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCrearLocalidades.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCrearLocalidades.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCrearLocalidades.FillColor = System.Drawing.Color.SlateBlue;
            this.btnCrearLocalidades.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCrearLocalidades.ForeColor = System.Drawing.Color.White;
            this.btnCrearLocalidades.Location = new System.Drawing.Point(12, 181);
            this.btnCrearLocalidades.Name = "btnCrearLocalidades";
            this.btnCrearLocalidades.Size = new System.Drawing.Size(180, 45);
            this.btnCrearLocalidades.TabIndex = 1;
            this.btnCrearLocalidades.Text = "Crear localidades";
            this.btnCrearLocalidades.Click += new System.EventHandler(this.btnCrearLocalidades_Click);
            // 
            // btnAjustarVotantes
            // 
            this.btnAjustarVotantes.BorderRadius = 10;
            this.btnAjustarVotantes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAjustarVotantes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAjustarVotantes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAjustarVotantes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAjustarVotantes.FillColor = System.Drawing.Color.SlateBlue;
            this.btnAjustarVotantes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAjustarVotantes.ForeColor = System.Drawing.Color.White;
            this.btnAjustarVotantes.Location = new System.Drawing.Point(12, 88);
            this.btnAjustarVotantes.Name = "btnAjustarVotantes";
            this.btnAjustarVotantes.Size = new System.Drawing.Size(180, 45);
            this.btnAjustarVotantes.TabIndex = 0;
            this.btnAjustarVotantes.Text = "Ajustar parametros de elecciones";
            this.btnAjustarVotantes.Click += new System.EventHandler(this.btnAjustarVotantes_Click);
            // 
            // panelChildFrm
            // 
            this.panelChildFrm.Location = new System.Drawing.Point(207, 31);
            this.panelChildFrm.Name = "panelChildFrm";
            this.panelChildFrm.Size = new System.Drawing.Size(685, 445);
            this.panelChildFrm.TabIndex = 3;
            // 
            // FrmServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 488);
            this.Controls.Add(this.panelChildFrm);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmServidor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servidor";
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel pnlForm;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnAjustarVotantes;
        private Guna.UI2.WinForms.Guna2Button btnStats;
        private Guna.UI2.WinForms.Guna2Button btnCrearLocalidades;
        private System.Windows.Forms.Panel panelChildFrm;
        private System.Windows.Forms.Label label1;
    }
}

