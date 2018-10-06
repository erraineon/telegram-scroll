namespace TelegramScroll
{
    partial class MainForm
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
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBox
            // 
            this.checkBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new System.Drawing.Point(114, 75);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(49, 23);
            this.checkBox.TabIndex = 0;
            this.checkBox.Text = "enable";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.OnCheckBoxCheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TelegramScroll.Properties.Resources.DdtLrNDVwAAUi91;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(286, 174);
            this.Controls.Add(this.checkBox);
            this.Name = "MainForm";
            this.Text = "stop that shit";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox;
    }
}

