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
            this.currentVoiceCommandLabel = new System.Windows.Forms.Label();
            this.saveKeybindButton = new System.Windows.Forms.Button();
            this.keybindTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.savedKeybindLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ActivateButton
            // 
            this.ActivateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ActivateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ActivateButton.Location = new System.Drawing.Point(0, 585);
            this.ActivateButton.Margin = new System.Windows.Forms.Padding(6);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.Size = new System.Drawing.Size(1600, 146);
            this.ActivateButton.TabIndex = 0;
            this.ActivateButton.Text = "Stop voice commands";
            this.ActivateButton.UseVisualStyleBackColor = true;
            this.ActivateButton.Click += new System.EventHandler(this.activateButton_Click);
            // 
            // buttonListVoiceCommands
            // 
            this.buttonListVoiceCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonListVoiceCommands.Location = new System.Drawing.Point(24, 23);
            this.buttonListVoiceCommands.Margin = new System.Windows.Forms.Padding(6);
            this.buttonListVoiceCommands.Name = "buttonListVoiceCommands";
            this.buttonListVoiceCommands.Size = new System.Drawing.Size(416, 179);
            this.buttonListVoiceCommands.TabIndex = 1;
            this.buttonListVoiceCommands.Text = "Open list of currently available voice commands";
            this.buttonListVoiceCommands.UseVisualStyleBackColor = true;
            this.buttonListVoiceCommands.Click += new System.EventHandler(this.buttonListVoiceCommands_Click);
            // 
            // buttonListInstalledGames
            // 
            this.buttonListInstalledGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.buttonListInstalledGames.Location = new System.Drawing.Point(1160, 23);
            this.buttonListInstalledGames.Margin = new System.Windows.Forms.Padding(6);
            this.buttonListInstalledGames.Name = "buttonListInstalledGames";
            this.buttonListInstalledGames.Size = new System.Drawing.Size(416, 179);
            this.buttonListInstalledGames.TabIndex = 2;
            this.buttonListInstalledGames.Text = "Open list of installed games";
            this.buttonListInstalledGames.UseVisualStyleBackColor = true;
            this.buttonListInstalledGames.Click += new System.EventHandler(this.buttonListInstalledGames_Click);
            // 
            // currentVoiceCommandLabel
            // 
            this.currentVoiceCommandLabel.AutoSize = true;
            this.currentVoiceCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentVoiceCommandLabel.Location = new System.Drawing.Point(428, 523);
            this.currentVoiceCommandLabel.Name = "currentVoiceCommandLabel";
            this.currentVoiceCommandLabel.Size = new System.Drawing.Size(466, 46);
            this.currentVoiceCommandLabel.TabIndex = 3;
            this.currentVoiceCommandLabel.Text = "Current voice command: ";
            this.currentVoiceCommandLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // saveKeybindButton
            // 
            this.saveKeybindButton.Location = new System.Drawing.Point(596, 248);
            this.saveKeybindButton.Name = "saveKeybindButton";
            this.saveKeybindButton.Size = new System.Drawing.Size(173, 64);
            this.saveKeybindButton.TabIndex = 4;
            this.saveKeybindButton.Text = "Save keybind";
            this.saveKeybindButton.UseVisualStyleBackColor = true;
            this.saveKeybindButton.Click += new System.EventHandler(this.saveKeybindButton_Click);
            // 
            // keybindTextBox
            // 
            this.keybindTextBox.Location = new System.Drawing.Point(24, 262);
            this.keybindTextBox.Name = "keybindTextBox";
            this.keybindTextBox.ReadOnly = true;
            this.keybindTextBox.Size = new System.Drawing.Size(395, 31);
            this.keybindTextBox.TabIndex = 5;
            this.keybindTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.keybindTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keybindTextBox_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(425, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(165, 64);
            this.button1.TabIndex = 6;
            this.button1.Text = "Clear input";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Specify keybind:";
            // 
            // savedKeybindLabel
            // 
            this.savedKeybindLabel.AutoSize = true;
            this.savedKeybindLabel.Location = new System.Drawing.Point(24, 320);
            this.savedKeybindLabel.Name = "savedKeybindLabel";
            this.savedKeybindLabel.Size = new System.Drawing.Size(160, 25);
            this.savedKeybindLabel.TabIndex = 8;
            this.savedKeybindLabel.Text = "Saved keybind:";
            this.savedKeybindLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // SvcWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 731);
            this.Controls.Add(this.savedKeybindLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.keybindTextBox);
            this.Controls.Add(this.saveKeybindButton);
            this.Controls.Add(this.currentVoiceCommandLabel);
            this.Controls.Add(this.buttonListInstalledGames);
            this.Controls.Add(this.buttonListVoiceCommands);
            this.Controls.Add(this.ActivateButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "SvcWindow";
            this.Text = "Steam Voice Commands";
            this.Load += new System.EventHandler(this.svcWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.svcWindow_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonListVoiceCommands;
        private System.Windows.Forms.Button ActivateButton;
        private System.Windows.Forms.Button buttonListInstalledGames;
        private System.Windows.Forms.Label currentVoiceCommandLabel;
        private System.Windows.Forms.Button saveKeybindButton;
        private System.Windows.Forms.TextBox keybindTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label savedKeybindLabel;
    }
}

