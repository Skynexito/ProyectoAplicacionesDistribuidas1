namespace Servidor.Formularios
{
    partial class FrmEstadisticas
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnExit = new System.Windows.Forms.Label();
            this.chartVotos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbbLocalidades = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cbbMesa = new Guna.UI2.WinForms.Guna2ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartVotos)).BeginInit();
            this.SuspendLayout();
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
            // chartVotos
            // 
            chartArea6.Name = "ChartArea1";
            this.chartVotos.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartVotos.Legends.Add(legend6);
            this.chartVotos.Location = new System.Drawing.Point(104, 119);
            this.chartVotos.Name = "chartVotos";
            this.chartVotos.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartVotos.Series.Add(series6);
            this.chartVotos.Size = new System.Drawing.Size(475, 300);
            this.chartVotos.TabIndex = 13;
            this.chartVotos.Text = "chart1";
            // 
            // cbbLocalidades
            // 
            this.cbbLocalidades.AutoRoundedCorners = true;
            this.cbbLocalidades.BackColor = System.Drawing.Color.Transparent;
            this.cbbLocalidades.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbLocalidades.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbLocalidades.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbLocalidades.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbLocalidades.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbLocalidades.ItemHeight = 30;
            this.cbbLocalidades.Items.AddRange(new object[] {
            "Mostrar todos"});
            this.cbbLocalidades.Location = new System.Drawing.Point(104, 37);
            this.cbbLocalidades.Name = "cbbLocalidades";
            this.cbbLocalidades.Size = new System.Drawing.Size(304, 36);
            this.cbbLocalidades.TabIndex = 14;
            this.cbbLocalidades.DropDown += new System.EventHandler(this.cbbLocalidades_DropDown);
            this.cbbLocalidades.SelectedIndexChanged += new System.EventHandler(this.cbbLocalidades_SelectedIndexChanged);
            // 
            // cbbMesa
            // 
            this.cbbMesa.AutoRoundedCorners = true;
            this.cbbMesa.BackColor = System.Drawing.Color.Transparent;
            this.cbbMesa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbMesa.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbMesa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbbMesa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbbMesa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbbMesa.ItemHeight = 30;
            this.cbbMesa.Location = new System.Drawing.Point(443, 37);
            this.cbbMesa.MaxDropDownItems = 5;
            this.cbbMesa.MaxLength = 5;
            this.cbbMesa.Name = "cbbMesa";
            this.cbbMesa.Size = new System.Drawing.Size(136, 36);
            this.cbbMesa.TabIndex = 15;
            this.cbbMesa.DropDown += new System.EventHandler(this.cbbMesa_DropDown);
            this.cbbMesa.SelectedIndexChanged += new System.EventHandler(this.cbbMesa_SelectedIndexChanged);
            // 
            // FrmEstadisticas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 450);
            this.Controls.Add(this.cbbMesa);
            this.Controls.Add(this.cbbLocalidades);
            this.Controls.Add(this.chartVotos);
            this.Controls.Add(this.btnExit);
            this.Name = "FrmEstadisticas";
            this.Text = "FrmEstadisticas";
            this.Load += new System.EventHandler(this.FrmEstadisticas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartVotos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVotos;
        private Guna.UI2.WinForms.Guna2ComboBox cbbLocalidades;
        private Guna.UI2.WinForms.Guna2ComboBox cbbMesa;
    }
}