namespace DareToEscape.Helpers
{
    partial class ResolutionChooser
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
            this._startGameBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._resolutionComboBox = new System.Windows.Forms.ComboBox();
            this.rememberCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _startGameBtn
            // 
            this._startGameBtn.Location = new System.Drawing.Point(64, 101);
            this._startGameBtn.Name = "_startGameBtn";
            this._startGameBtn.Size = new System.Drawing.Size(123, 23);
            this._startGameBtn.TabIndex = 0;
            this._startGameBtn.Text = "Start";
            this._startGameBtn.UseVisualStyleBackColor = true;
            this._startGameBtn.Click += new System.EventHandler(this.StartGameBtnClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._resolutionComboBox);
            this.groupBox1.Controls.Add(this.rememberCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(26, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 83);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resolution";
            // 
            // _resolutionComboBox
            // 
            this._resolutionComboBox.FormattingEnabled = true;
            this._resolutionComboBox.Items.AddRange(new object[] {
            "320x240 \t(Fullscreen)",
            "320x240 \t(Windowed)",
            "640x480 \t(Windowed)",
            "960x720 \t(Windowed)",
            "1280x960 \t(Windowed)",
            "1600x1200 (Windowed)",
            "1920x1440 (Windowed)"});
            this._resolutionComboBox.Location = new System.Drawing.Point(7, 30);
            this._resolutionComboBox.Name = "_resolutionComboBox";
            this._resolutionComboBox.Size = new System.Drawing.Size(187, 21);
            this._resolutionComboBox.TabIndex = 1;
            this._resolutionComboBox.SelectedIndexChanged += new System.EventHandler(this.ResolutionComboBoxSelectedIndexChanged);
            // 
            // rememberCheckbox
            // 
            this.rememberCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rememberCheckbox.AutoSize = true;
            this.rememberCheckbox.Location = new System.Drawing.Point(7, 60);
            this.rememberCheckbox.Name = "rememberCheckbox";
            this.rememberCheckbox.Size = new System.Drawing.Size(118, 17);
            this.rememberCheckbox.TabIndex = 0;
            this.rememberCheckbox.Text = "Remember Settings";
            this.rememberCheckbox.UseVisualStyleBackColor = true;
            this.rememberCheckbox.CheckedChanged += new System.EventHandler(this.RememberCheckboxCheckedChanged);
            // 
            // ResolutionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(255, 131);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._startGameBtn);
            this.Name = "ResolutionChooser";
            this.Text = "Dare To Escape - Config";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _startGameBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox rememberCheckbox;
        private System.Windows.Forms.ComboBox _resolutionComboBox;
    }
}