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
            this.autoListenCheckBox = new System.Windows.Forms.CheckBox();
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
            this.ActivateButton.Click += new System.EventHandler(this.ActivateButton_Click);
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
            this.buttonListVoiceCommands.Click += new System.EventHandler(this.ButtonListVoiceCommands_Click);
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
            this.buttonListInstalledGames.Click += new System.EventHandler(this.ButtonListInstalledGames_Click);
            // 
            // currentVoiceCommandLabel
            // 
            this.currentVoiceCommandLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.currentVoiceCommandLabel.AutoSize = true;
            this.currentVoiceCommandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentVoiceCommandLabel.Location = new System.Drawing.Point(131, 235);
            this.currentVoiceCommandLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.currentVoiceCommandLabel.Name = "currentVoiceCommandLabel";
            this.currentVoiceCommandLabel.Size = new System.Drawing.Size(230, 25);
            this.currentVoiceCommandLabel.TabIndex = 3;
            this.currentVoiceCommandLabel.Text = "Current voice command: ";
            // 
            // saveKeybindButton
            // 
            this.saveKeybindButton.Location = new System.Drawing.Point(298, 129);
            this.saveKeybindButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.saveKeybindButton.Name = "saveKeybindButton";
            this.saveKeybindButton.Size = new System.Drawing.Size(86, 33);
            this.saveKeybindButton.TabIndex = 4;
            this.saveKeybindButton.Text = "Save keybind";
            this.saveKeybindButton.UseVisualStyleBackColor = true;
            this.saveKeybindButton.Click += new System.EventHandler(this.SaveKeybindButton_Click);
            // 
            // keybindTextBox
            // 
            this.keybindTextBox.Location = new System.Drawing.Point(12, 136);
            this.keybindTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.keybindTextBox.Name = "keybindTextBox";
            this.keybindTextBox.ReadOnly = true;
            this.keybindTextBox.Size = new System.Drawing.Size(200, 20);
            this.keybindTextBox.TabIndex = 5;
            this.keybindTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeybindTextBox_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(212, 129);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "Clear input";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Specify keybind:";
            // 
            // savedKeybindLabel
            // 
            this.savedKeybindLabel.AutoSize = true;
            this.savedKeybindLabel.Location = new System.Drawing.Point(12, 166);
            this.savedKeybindLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.savedKeybindLabel.Name = "savedKeybindLabel";
            this.savedKeybindLabel.Size = new System.Drawing.Size(81, 13);
            this.savedKeybindLabel.TabIndex = 8;
            this.savedKeybindLabel.Text = "Saved keybind:";
            // 
            // autoListenCheckBox
            // 
            this.autoListenCheckBox.AutoSize = true;
            this.autoListenCheckBox.Location = new System.Drawing.Point(486, 118);
            this.autoListenCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.autoListenCheckBox.Name = "autoListenCheckBox";
            this.autoListenCheckBox.Size = new System.Drawing.Size(300, 17);
            this.autoListenCheckBox.TabIndex = 9;
            this.autoListenCheckBox.Text = "Automatically start listening for voice commands on launch";
            this.autoListenCheckBox.UseVisualStyleBackColor = true;
            this.autoListenCheckBox.CheckedChanged += new System.EventHandler(this.AutoListenCheckBox_CheckedChanged);
            // 
            // SvcWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.autoListenCheckBox);
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
            this.Name = "SvcWindow";
            this.Text = "Steam Voice Commands";
            this.Load += new System.EventHandler(this.SvcWindow_Load);
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
        private System.Windows.Forms.CheckBox autoListenCheckBox;
    }
}

