namespace SVC
{
    partial class SvcWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SvcWindow));
            this.ActivateButton = new System.Windows.Forms.Button();
            this.buttonListVoiceCommands = new System.Windows.Forms.Button();
            this.buttonListInstalledGames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ActivateButton
            // 
            this.ActivateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ActivateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ActivateButton.Location = new System.Drawing.Point(0, 304);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.Size = new System.Drawing.Size(800, 76);
            this.ActivateButton.TabIndex = 0;
            this.ActivateButton.Text = "Stop voice commands";
            this.ActivateButton.UseVisualStyleBackColor = true;
            this.ActivateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // buttonListVoiceCommands
            // 
            this.buttonListVoiceCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonListVoiceCommands.Location = new System.Drawing.Point(12, 12);
            this.buttonListVoiceCommands.Name = "buttonListVoiceCommands";
            this.buttonListVoiceCommands.Size = new System.Drawing.Size(208, 93);
            this.buttonListVoiceCommands.TabIndex = 1;
            this.buttonListVoiceCommands.Text = "Open list of currently available voice commands";
            this.buttonListVoiceCommands.UseVisualStyleBackColor = true;
            this.buttonListVoiceCommands.Click += new System.EventHandler(this.buttonListVoiceCommands_Click);
            // 
            // buttonListInstalledGames
            // 
            this.buttonListInstalledGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonListInstalledGames.Location = new System.Drawing.Point(580, 12);
            this.buttonListInstalledGames.Name = "buttonListInstalledGames";
            this.buttonListInstalledGames.Size = new System.Drawing.Size(208, 93);
            this.buttonListInstalledGames.TabIndex = 2;
            this.buttonListInstalledGames.Text = "Open list of installed games";
            this.buttonListInstalledGames.UseVisualStyleBackColor = true;
            this.buttonListInstalledGames.Click += new System.EventHandler(this.buttonListInstalledGames_Click);
            // 
            // SvcWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.buttonListInstalledGames);
            this.Controls.Add(this.buttonListVoiceCommands);
            this.Controls.Add(this.ActivateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SvcWindow";
            this.Text = "Steam Voice Commands";
            this.Load += new System.EventHandler(this.svcWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.svcWindow_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonListVoiceCommands;
        private System.Windows.Forms.Button ActivateButton;
        private System.Windows.Forms.Button buttonListInstalledGames;
    }
}

