namespace Servidor
{
    partial class FrmCliente
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
            this.components = new System.ComponentModel.Container();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnlForm = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSalir = new Guna.UI2.WinForms.Guna2Button();
            this.btnCerrarMesa = new Guna.UI2.WinForms.Guna2Button();
            this.btnRegistrarVotos = new Guna.UI2.WinForms.Guna2Button();
            this.btnAsignacionMesa = new Guna.UI2.WinForms.Guna2Button();
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
            this.pnlForm.Controls.Add(this.lblNombre);
            this.pnlForm.Controls.Add(this.label1);
            this.pnlForm.Location = new System.Drawing.Point(-4, -7);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(907, 31);
            this.pnlForm.TabIndex = 1;
            this.pnlForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseDown);
            this.pnlForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseMove);
            this.pnlForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlForm_MouseUp);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblNombre.Location = new System.Drawing.Point(12, 10);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(86, 20);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "CLIENTE";
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
            this.guna2Panel1.Controls.Add(this.btnSalir);
            this.guna2Panel1.Controls.Add(this.btnCerrarMesa);
            this.guna2Panel1.Controls.Add(this.btnRegistrarVotos);
            this.guna2Panel1.Controls.Add(this.btnAsignacionMesa);
            this.guna2Panel1.Location = new System.Drawing.Point(-4, 12);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(205, 478);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnSalir
            // 
            this.btnSalir.BorderRadius = 10;
            this.btnSalir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSalir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSalir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSalir.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSalir.FillColor = System.Drawing.Color.SlateBlue;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(12, 347);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(180, 45);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnCerrarMesa
            // 
            this.btnCerrarMesa.BorderRadius = 10;
            this.btnCerrarMesa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCerrarMesa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCerrarMesa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCerrarMesa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCerrarMesa.FillColor = System.Drawing.Color.SlateBlue;
            this.btnCerrarMesa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCerrarMesa.ForeColor = System.Drawing.Color.White;
            this.btnCerrarMesa.Location = new System.Drawing.Point(12, 254);
            this.btnCerrarMesa.Name = "btnCerrarMesa";
            this.btnCerrarMesa.Size = new System.Drawing.Size(180, 45);
            this.btnCerrarMesa.TabIndex = 2;
            this.btnCerrarMesa.Text = "Cerrar mesa";
            this.btnCerrarMesa.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnRegistrarVotos
            // 
            this.btnRegistrarVotos.BorderRadius = 10;
            this.btnRegistrarVotos.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistrarVotos.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRegistrarVotos.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRegistrarVotos.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRegistrarVotos.FillColor = System.Drawing.Color.SlateBlue;
            this.btnRegistrarVotos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegistrarVotos.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarVotos.Location = new System.Drawing.Point(12, 167);
            this.btnRegistrarVotos.Name = "btnRegistrarVotos";
            this.btnRegistrarVotos.Size = new System.Drawing.Size(180, 45);
            this.btnRegistrarVotos.TabIndex = 1;
            this.btnRegistrarVotos.Text = "Registrar votos";
            this.btnRegistrarVotos.Click += new System.EventHandler(this.btnCrearLocalidades_Click);
            // 
            // btnAsignacionMesa
            // 
            this.btnAsignacionMesa.BorderRadius = 10;
            this.btnAsignacionMesa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAsignacionMesa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAsignacionMesa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAsignacionMesa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAsignacionMesa.FillColor = System.Drawing.Color.SlateBlue;
            this.btnAsignacionMesa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAsignacionMesa.ForeColor = System.Drawing.Color.White;
            this.btnAsignacionMesa.Location = new System.Drawing.Point(12, 74);
            this.btnAsignacionMesa.Name = "btnAsignacionMesa";
            this.btnAsignacionMesa.Size = new System.Drawing.Size(180, 45);
            this.btnAsignacionMesa.TabIndex = 0;
            this.btnAsignacionMesa.Text = "Asignacion de mesa";
            this.btnAsignacionMesa.Click += new System.EventHandler(this.btnAsignarMesa_Click);
            // 
            // panelChildFrm
            // 
            this.panelChildFrm.Location = new System.Drawing.Point(207, 31);
            this.panelChildFrm.Name = "panelChildFrm";
            this.panelChildFrm.Size = new System.Drawing.Size(685, 445);
            this.panelChildFrm.TabIndex = 3;
            // 
            // FrmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 488);
            this.Controls.Add(this.panelChildFrm);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENTE";
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel pnlForm;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button btnAsignacionMesa;
        private Guna.UI2.WinForms.Guna2Button btnCerrarMesa;
        private Guna.UI2.WinForms.Guna2Button btnRegistrarVotos;
        private System.Windows.Forms.Panel panelChildFrm;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnSalir;
        private System.Windows.Forms.Label lblNombre;
    }
}

