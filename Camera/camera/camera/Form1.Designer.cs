namespace cameraxd
{
    partial class Form1
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
            this.refresh = new System.Windows.Forms.Button();
            this.camlist = new System.Windows.Forms.ComboBox();
            this.camoutput = new System.Windows.Forms.PictureBox();
            this.brightness = new System.Windows.Forms.TrackBar();
            this.contrast = new System.Windows.Forms.TrackBar();
            this.saturation = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.takephoto = new System.Windows.Forms.Button();
            this.startfilm = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.stopfilm = new System.Windows.Forms.Button();
            this.startcamera = new System.Windows.Forms.Button();
            this.stopcamera = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.detectstart = new System.Windows.Forms.Button();
            this.detectstop = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.TextBox();
            this.pixdiff = new System.Windows.Forms.TrackBar();
            this.pixcount = new System.Windows.Forms.TrackBar();
            this.delayframe = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.camoutput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixdiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixcount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayframe)).BeginInit();
            this.SuspendLayout();
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(66, 58);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(164, 28);
            this.refresh.TabIndex = 0;
            this.refresh.Text = "Odśwież";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // camlist
            // 
            this.camlist.FormattingEnabled = true;
            this.camlist.Location = new System.Drawing.Point(405, 58);
            this.camlist.Name = "camlist";
            this.camlist.Size = new System.Drawing.Size(394, 28);
            this.camlist.TabIndex = 1;
            this.camlist.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // camoutput
            // 
            this.camoutput.Location = new System.Drawing.Point(66, 167);
            this.camoutput.Name = "camoutput";
            this.camoutput.Size = new System.Drawing.Size(1156, 620);
            this.camoutput.TabIndex = 2;
            this.camoutput.TabStop = false;
            // 
            // brightness
            // 
            this.brightness.Location = new System.Drawing.Point(330, 826);
            this.brightness.Maximum = 100;
            this.brightness.Minimum = -100;
            this.brightness.Name = "brightness";
            this.brightness.Size = new System.Drawing.Size(365, 69);
            this.brightness.TabIndex = 3;
            this.brightness.Scroll += new System.EventHandler(this.brightness_Scroll);
            // 
            // contrast
            // 
            this.contrast.Location = new System.Drawing.Point(330, 901);
            this.contrast.Maximum = 100;
            this.contrast.Minimum = -100;
            this.contrast.Name = "contrast";
            this.contrast.Size = new System.Drawing.Size(365, 69);
            this.contrast.TabIndex = 4;
            this.contrast.Scroll += new System.EventHandler(this.contrast_Scroll);
            // 
            // saturation
            // 
            this.saturation.Location = new System.Drawing.Point(330, 976);
            this.saturation.Maximum = 100;
            this.saturation.Minimum = -100;
            this.saturation.Name = "saturation";
            this.saturation.Size = new System.Drawing.Size(365, 69);
            this.saturation.TabIndex = 5;
            this.saturation.Scroll += new System.EventHandler(this.saturation_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 834);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Jasność";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 910);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Kontrast";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(236, 986);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nasycenie";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Obraz z wybranej kamery";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(304, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Lista kamer:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // takephoto
            // 
            this.takephoto.Location = new System.Drawing.Point(1254, 243);
            this.takephoto.Name = "takephoto";
            this.takephoto.Size = new System.Drawing.Size(130, 36);
            this.takephoto.TabIndex = 11;
            this.takephoto.Text = "Zrób zdjęcie";
            this.takephoto.UseVisualStyleBackColor = true;
            this.takephoto.Click += new System.EventHandler(this.button2_Click);
            // 
            // startfilm
            // 
            this.startfilm.Location = new System.Drawing.Point(1254, 323);
            this.startfilm.Name = "startfilm";
            this.startfilm.Size = new System.Drawing.Size(130, 38);
            this.startfilm.TabIndex = 12;
            this.startfilm.Text = "Start film";
            this.startfilm.UseVisualStyleBackColor = true;
            this.startfilm.Click += new System.EventHandler(this.startfilm_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(1418, 1005);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(130, 40);
            this.exit.TabIndex = 13;
            this.exit.Text = "Wyjdź";
            this.exit.UseVisualStyleBackColor = true;
            // 
            // stopfilm
            // 
            this.stopfilm.Location = new System.Drawing.Point(1418, 323);
            this.stopfilm.Name = "stopfilm";
            this.stopfilm.Size = new System.Drawing.Size(130, 38);
            this.stopfilm.TabIndex = 14;
            this.stopfilm.Text = "Stop film";
            this.stopfilm.UseVisualStyleBackColor = true;
            this.stopfilm.Click += new System.EventHandler(this.stopfilm_Click);
            // 
            // startcamera
            // 
            this.startcamera.Location = new System.Drawing.Point(1254, 167);
            this.startcamera.Name = "startcamera";
            this.startcamera.Size = new System.Drawing.Size(130, 36);
            this.startcamera.TabIndex = 15;
            this.startcamera.Text = "start camera";
            this.startcamera.UseVisualStyleBackColor = true;
            this.startcamera.Click += new System.EventHandler(this.startcamera_Click);
            // 
            // stopcamera
            // 
            this.stopcamera.Location = new System.Drawing.Point(1418, 167);
            this.stopcamera.Name = "stopcamera";
            this.stopcamera.Size = new System.Drawing.Size(130, 36);
            this.stopcamera.TabIndex = 16;
            this.stopcamera.Text = "stop camera";
            this.stopcamera.UseVisualStyleBackColor = true;
            this.stopcamera.Click += new System.EventHandler(this.stopcamera_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // detectstart
            // 
            this.detectstart.Location = new System.Drawing.Point(1254, 399);
            this.detectstart.Name = "detectstart";
            this.detectstart.Size = new System.Drawing.Size(130, 40);
            this.detectstart.TabIndex = 17;
            this.detectstart.Text = "start detection";
            this.detectstart.UseVisualStyleBackColor = true;
            this.detectstart.Click += new System.EventHandler(this.detectstart_Click);
            // 
            // detectstop
            // 
            this.detectstop.Location = new System.Drawing.Point(1418, 401);
            this.detectstop.Name = "detectstop";
            this.detectstop.Size = new System.Drawing.Size(130, 38);
            this.detectstop.TabIndex = 18;
            this.detectstop.Text = "stop detection";
            this.detectstop.UseVisualStyleBackColor = true;
            this.detectstop.Click += new System.EventHandler(this.detectstop_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1250, 471);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "pixel diff";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1232, 542);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "pixel count";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1229, 608);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "framedelay";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1250, 680);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 25;
            this.label9.Text = "Wykryto ruchu";
            this.label9.Visible = false;
            // 
            // pixdiff
            // 
            this.pixdiff.Location = new System.Drawing.Point(1322, 458);
            this.pixdiff.Maximum = 100;
            this.pixdiff.Minimum = 1;
            this.pixdiff.Name = "pixdiff";
            this.pixdiff.Size = new System.Drawing.Size(226, 69);
            this.pixdiff.TabIndex = 22;
            this.pixdiff.Value = 1;
            this.pixdiff.Scroll += new System.EventHandler(this.pixdiff_Scroll);
            // 
            // pixcount
            // 
            this.pixcount.Location = new System.Drawing.Point(1322, 533);
            this.pixcount.Maximum = 1000;
            this.pixcount.Minimum = 10;
            this.pixcount.Name = "pixcount";
            this.pixcount.Size = new System.Drawing.Size(226, 69);
            this.pixcount.TabIndex = 23;
            this.pixcount.Value = 10;
            this.pixcount.Scroll += new System.EventHandler(this.pixcount_Scroll);
            // 
            // delayframe
            // 
            this.delayframe.Location = new System.Drawing.Point(1322, 608);
            this.delayframe.Maximum = 60;
            this.delayframe.Name = "delayframe";
            this.delayframe.Size = new System.Drawing.Size(226, 69);
            this.delayframe.TabIndex = 24;
            this.delayframe.Scroll += new System.EventHandler(this.delayframe_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1649, 1132);
            this.Controls.Add(this.delayframe);
            this.Controls.Add(this.pixcount);
            this.Controls.Add(this.pixdiff);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.detectstop);
            this.Controls.Add(this.detectstart);
            this.Controls.Add(this.stopcamera);
            this.Controls.Add(this.startcamera);
            this.Controls.Add(this.stopfilm);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.startfilm);
            this.Controls.Add(this.takephoto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saturation);
            this.Controls.Add(this.contrast);
            this.Controls.Add(this.brightness);
            this.Controls.Add(this.camoutput);
            this.Controls.Add(this.camlist);
            this.Controls.Add(this.refresh);
            this.Name = "Form1";
            this.Text = "Aplikacja do obsługi kamery";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.camoutput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixdiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixcount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delayframe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.ComboBox camlist;
        private System.Windows.Forms.PictureBox camoutput;
        private System.Windows.Forms.TrackBar brightness;
        private System.Windows.Forms.TrackBar contrast;
        private System.Windows.Forms.TrackBar saturation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button takephoto;
        private System.Windows.Forms.Button startfilm;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button stopfilm;
        private System.Windows.Forms.Button startcamera;
        private System.Windows.Forms.Button stopcamera;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button detectstart;
        private System.Windows.Forms.Button detectstop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox label9;
        private System.Windows.Forms.TrackBar pixdiff;
        private System.Windows.Forms.TrackBar pixcount;
        private System.Windows.Forms.TrackBar delayframe;
    }
}

