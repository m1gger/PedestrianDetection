namespace PedestrianDetection
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnLoadImages = new System.Windows.Forms.Button();
            this.btnRecognize = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.pictureBoxBefore = new System.Windows.Forms.PictureBox();
            this.pictureBoxAfter = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImages
            // 
            this.btnLoadImages.Location = new System.Drawing.Point(16, 15);
            this.btnLoadImages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoadImages.Name = "btnLoadImages";
            this.btnLoadImages.Size = new System.Drawing.Size(171, 39);
            this.btnLoadImages.TabIndex = 0;
            this.btnLoadImages.Text = "Загрузить изображения";
            this.btnLoadImages.UseVisualStyleBackColor = true;
            this.btnLoadImages.Click += new System.EventHandler(this.BtnLoadImages_Click);
            // 
            // btnRecognize
            // 
            this.btnRecognize.Location = new System.Drawing.Point(16, 62);
            this.btnRecognize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRecognize.Name = "btnRecognize";
            this.btnRecognize.Size = new System.Drawing.Size(171, 39);
            this.btnRecognize.TabIndex = 1;
            this.btnRecognize.Text = "Распознать";
            this.btnRecognize.UseVisualStyleBackColor = true;
            this.btnRecognize.Click += new System.EventHandler(this.BtnRecognize_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(16, 108);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(171, 39);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 318);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1035, 28);
            this.progressBar.TabIndex = 3;
            // 
            // pictureBoxBefore
            // 
            this.pictureBoxBefore.Location = new System.Drawing.Point(195, 15);
            this.pictureBoxBefore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxBefore.Name = "pictureBoxBefore";
            this.pictureBoxBefore.Size = new System.Drawing.Size(427, 295);
            this.pictureBoxBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBefore.TabIndex = 4;
            this.pictureBoxBefore.TabStop = false;
            // 
            // pictureBoxAfter
            // 
            this.pictureBoxAfter.Location = new System.Drawing.Point(616, 15);
            this.pictureBoxAfter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxAfter.Name = "pictureBoxAfter";
            this.pictureBoxAfter.Size = new System.Drawing.Size(427, 295);
            this.pictureBoxAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAfter.TabIndex = 5;
            this.pictureBoxAfter.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.pictureBoxAfter);
            this.Controls.Add(this.pictureBoxBefore);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRecognize);
            this.Controls.Add(this.btnLoadImages);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Recognition";
            this.Load += new System.EventHandler(this.RecognitionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAfter)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnLoadImages;
        private System.Windows.Forms.Button btnRecognize;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.PictureBox pictureBoxBefore;
        private System.Windows.Forms.PictureBox pictureBoxAfter;
    }
}
