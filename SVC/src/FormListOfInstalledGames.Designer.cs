namespace SVC
{
    partial class FormListOfInstalledGames
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
            this.listOfGamesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listOfGamesTextBox
            // 
            this.listOfGamesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listOfGamesTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.listOfGamesTextBox.CausesValidation = false;
            this.listOfGamesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listOfGamesTextBox.Location = new System.Drawing.Point(12, 12);
            this.listOfGamesTextBox.Multiline = true;
            this.listOfGamesTextBox.Name = "listOfGamesTextBox";
            this.listOfGamesTextBox.ReadOnly = true;
            this.listOfGamesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.listOfGamesTextBox.Size = new System.Drawing.Size(776, 426);
            this.listOfGamesTextBox.TabIndex = 0;
            this.listOfGamesTextBox.TabStop = false;
            this.listOfGamesTextBox.TextChanged += new System.EventHandler(this.listOfGamesTextBox_TextChanged);
            // 
            // FormListOfInstalledGames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listOfGamesTextBox);
            this.Name = "FormListOfInstalledGames";
            this.Text = "List of installed games";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox listOfGamesTextBox;
    }
}