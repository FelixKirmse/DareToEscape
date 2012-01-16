namespace MapEditor
{
    partial class TransitionDialog
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
            this._mapNameInputBox = new System.Windows.Forms.TextBox();
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _mapNameInputBox
            // 
            this._mapNameInputBox.Location = new System.Drawing.Point(12, 29);
            this._mapNameInputBox.Name = "_mapNameInputBox";
            this._mapNameInputBox.Size = new System.Drawing.Size(381, 20);
            this._mapNameInputBox.TabIndex = 0;
            this._mapNameInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapNameInputBoxKeyDown);
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.AutoSize = true;
            this._descriptionLabel.Location = new System.Drawing.Point(13, 13);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(380, 13);
            this._descriptionLabel.TabIndex = 1;
            this._descriptionLabel.Text = "Input the map name without file extension and starting from the maps/ directory:";
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(161, 55);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // TransitionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 82);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._descriptionLabel);
            this.Controls.Add(this._mapNameInputBox);
            this.MaximizeBox = false;
            this.Name = "TransitionDialog";
            this.Text = "Transition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _mapNameInputBox;
        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.Button _okButton;
    }
}