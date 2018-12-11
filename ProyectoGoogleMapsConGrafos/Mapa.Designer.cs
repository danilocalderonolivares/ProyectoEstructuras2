namespace ProyectoGoogleMapsConGrafos
{
    partial class Mapa
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
            this.btnLlegar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.txtNombreVertice = new System.Windows.Forms.TextBox();
            this.lblNombreVertice = new System.Windows.Forms.Label();
            this.btnBucarVerice = new System.Windows.Forms.Button();
            this.textDatosVerticeBuscado = new System.Windows.Forms.RichTextBox();
            this.btnIrVerticer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLlegar
            // 
            this.btnLlegar.Location = new System.Drawing.Point(808, 291);
            this.btnLlegar.Name = "btnLlegar";
            this.btnLlegar.Size = new System.Drawing.Size(159, 23);
            this.btnLlegar.TabIndex = 7;
            this.btnLlegar.Text = "Camino corto";
            this.btnLlegar.UseVisualStyleBackColor = true;
            this.btnLlegar.Click += new System.EventHandler(this.btnLlegar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(887, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 15;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(-4, -1);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(751, 458);
            this.gMapControl1.TabIndex = 16;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gMapControl1_OnMarkerClick);
            // 
            // txtNombreVertice
            // 
            this.txtNombreVertice.Location = new System.Drawing.Point(778, 36);
            this.txtNombreVertice.Name = "txtNombreVertice";
            this.txtNombreVertice.Size = new System.Drawing.Size(210, 20);
            this.txtNombreVertice.TabIndex = 18;
            // 
            // lblNombreVertice
            // 
            this.lblNombreVertice.AutoSize = true;
            this.lblNombreVertice.Location = new System.Drawing.Point(815, 15);
            this.lblNombreVertice.Name = "lblNombreVertice";
            this.lblNombreVertice.Size = new System.Drawing.Size(143, 13);
            this.lblNombreVertice.TabIndex = 19;
            this.lblNombreVertice.Text = "Escriba el nombre del vertice";
            // 
            // btnBucarVerice
            // 
            this.btnBucarVerice.Location = new System.Drawing.Point(890, 64);
            this.btnBucarVerice.Name = "btnBucarVerice";
            this.btnBucarVerice.Size = new System.Drawing.Size(98, 23);
            this.btnBucarVerice.TabIndex = 20;
            this.btnBucarVerice.Text = "Buscar Vertice";
            this.btnBucarVerice.UseVisualStyleBackColor = true;
            this.btnBucarVerice.Click += new System.EventHandler(this.btnBucarVerice_Click);
            // 
            // textDatosVerticeBuscado
            // 
            this.textDatosVerticeBuscado.BackColor = System.Drawing.SystemColors.InfoText;
            this.textDatosVerticeBuscado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textDatosVerticeBuscado.ForeColor = System.Drawing.Color.Red;
            this.textDatosVerticeBuscado.Location = new System.Drawing.Point(778, 93);
            this.textDatosVerticeBuscado.Name = "textDatosVerticeBuscado";
            this.textDatosVerticeBuscado.Size = new System.Drawing.Size(210, 132);
            this.textDatosVerticeBuscado.TabIndex = 21;
            this.textDatosVerticeBuscado.Text = "";
            // 
            // btnIrVerticer
            // 
            this.btnIrVerticer.Location = new System.Drawing.Point(778, 64);
            this.btnIrVerticer.Name = "btnIrVerticer";
            this.btnIrVerticer.Size = new System.Drawing.Size(93, 23);
            this.btnIrVerticer.TabIndex = 22;
            this.btnIrVerticer.Text = "Ir al vertice";
            this.btnIrVerticer.UseVisualStyleBackColor = true;
            this.btnIrVerticer.Visible = false;
            this.btnIrVerticer.Click += new System.EventHandler(this.btnIrVerticer_Click);
            // 
            // Mapa
            // 
            this.ClientSize = new System.Drawing.Size(995, 458);
            this.Controls.Add(this.btnIrVerticer);
            this.Controls.Add(this.textDatosVerticeBuscado);
            this.Controls.Add(this.btnBucarVerice);
            this.Controls.Add(this.lblNombreVertice);
            this.Controls.Add(this.txtNombreVertice);
            this.Controls.Add(this.gMapControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLlegar);
            this.Name = "Mapa";
            this.Load += new System.EventHandler(this.Mapa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLlegar;
        private System.Windows.Forms.Label label2;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.TextBox txtNombreVertice;
        private System.Windows.Forms.Label lblNombreVertice;
        private System.Windows.Forms.Button btnBucarVerice;
        private System.Windows.Forms.RichTextBox textDatosVerticeBuscado;
        private System.Windows.Forms.Button btnIrVerticer;
    }
}

