namespace HadoopLogGenerator
{
    partial class MainWindow
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
            this.createLogButton = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // createLogButton
            // 
            this.createLogButton.Location = new System.Drawing.Point(68, 69);
            this.createLogButton.Name = "createLogButton";
            this.createLogButton.Size = new System.Drawing.Size(116, 43);
            this.createLogButton.TabIndex = 0;
            this.createLogButton.Text = "Create Logs";
            this.createLogButton.UseVisualStyleBackColor = true;
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(68, 157);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(49, 13);
            this.message.TabIndex = 1;
            this.message.Text = "message";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 205);
            this.Controls.Add(this.message);
            this.Controls.Add(this.createLogButton);
            this.Name = "MainWindow";
            this.Text = "Log Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createLogButton;
        private System.Windows.Forms.Label message;
    }
}

