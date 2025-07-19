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
            this.ButtonActivate = new System.Windows.Forms.Button();
            this.ButtonListVoiceCommands = new System.Windows.Forms.Button();
            this.ButtonListInstalledGames = new System.Windows.Forms.Button();
            this.currentVoiceCommandLabel = new System.Windows.Forms.Label();
            this.ButtonSaveKeybind = new System.Windows.Forms.Button();
            this.TextBoxKeybind = new System.Windows.Forms.TextBox();
            this.ButtonClearKeybindInput = new System.Windows.Forms.Button();
            this.LabelSpecifyKeybind = new System.Windows.Forms.Label();
            this.LabelSavedKeybind = new System.Windows.Forms.Label();
            this.CheckBoxAutoListen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ButtonActivate
            // 
            this.ButtonActivate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ButtonActivate.Location = new System.Drawing.Point(0, 304);
            this.ButtonActivate.Name = "ButtonActivate";
            this.ButtonActivate.Size = new System.Drawing.Size(800, 76);
            this.ButtonActivate.TabIndex = 0;
            this.ButtonActivate.Text = "Stop voice commands";
            this.ButtonActivate.UseVisualStyleBackColor = true;
            this.ButtonActivate.Click += new System.EventHandler(this.ActivateButton_Click);
            // 
            // ButtonListVoiceCommands
            // 
            this.ButtonListVoiceCommands.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ButtonListVoiceCommands.Location = new System.Drawing.Point(12, 12);
            this.ButtonListVoiceCommands.Name = "ButtonListVoiceCommands";
            this.ButtonListVoiceCommands.Size = new System.Drawing.Size(208, 93);
            this.ButtonListVoiceCommands.TabIndex = 1;
            this.ButtonListVoiceCommands.Text = "Open list of currently available voice commands";
            this.ButtonListVoiceCommands.UseVisualStyleBackColor = true;
            this.ButtonListVoiceCommands.Click += new System.EventHandler(this.ButtonListVoiceCommands_Click);
            // 
            // ButtonListInstalledGames
            // 
            this.ButtonListInstalledGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ButtonListInstalledGames.Location = new System.Drawing.Point(580, 12);
            this.ButtonListInstalledGames.Name = "ButtonListInstalledGames";
            this.ButtonListInstalledGames.Size = new System.Drawing.Size(208, 93);
            this.ButtonListInstalledGames.TabIndex = 2;
            this.ButtonListInstalledGames.Text = "Open list of installed games";
            this.ButtonListInstalledGames.UseVisualStyleBackColor = true;
            this.ButtonListInstalledGames.Click += new System.EventHandler(this.ButtonListInstalledGames_Click);
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
            // ButtonSaveKeybind
            // 
            this.ButtonSaveKeybind.Location = new System.Drawing.Point(298, 129);
            this.ButtonSaveKeybind.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSaveKeybind.Name = "ButtonSaveKeybind";
            this.ButtonSaveKeybind.Size = new System.Drawing.Size(86, 33);
            this.ButtonSaveKeybind.TabIndex = 4;
            this.ButtonSaveKeybind.Text = "Save keybind";
            this.ButtonSaveKeybind.UseVisualStyleBackColor = true;
            this.ButtonSaveKeybind.Click += new System.EventHandler(this.SaveKeybindButton_Click);
            // 
            // TextBoxKeybind
            // 
            this.TextBoxKeybind.Location = new System.Drawing.Point(12, 136);
            this.TextBoxKeybind.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxKeybind.Name = "TextBoxKeybind";
            this.TextBoxKeybind.ReadOnly = true;
            this.TextBoxKeybind.Size = new System.Drawing.Size(200, 20);
            this.TextBoxKeybind.TabIndex = 5;
            this.TextBoxKeybind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeybindTextBox_KeyDown);
            // 
            // ButtonClearKeybindInput
            // 
            this.ButtonClearKeybindInput.Location = new System.Drawing.Point(212, 129);
            this.ButtonClearKeybindInput.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonClearKeybindInput.Name = "ButtonClearKeybindInput";
            this.ButtonClearKeybindInput.Size = new System.Drawing.Size(82, 33);
            this.ButtonClearKeybindInput.TabIndex = 6;
            this.ButtonClearKeybindInput.Text = "Clear input";
            this.ButtonClearKeybindInput.UseVisualStyleBackColor = true;
            this.ButtonClearKeybindInput.Click += new System.EventHandler(this.ButtonClearKeybindInput_Click);
            // 
            // LabelSpecifyKeybind
            // 
            this.LabelSpecifyKeybind.AutoSize = true;
            this.LabelSpecifyKeybind.Location = new System.Drawing.Point(12, 120);
            this.LabelSpecifyKeybind.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSpecifyKeybind.Name = "LabelSpecifyKeybind";
            this.LabelSpecifyKeybind.Size = new System.Drawing.Size(85, 13);
            this.LabelSpecifyKeybind.TabIndex = 7;
            this.LabelSpecifyKeybind.Text = "Specify keybind:";
            // 
            // LabelSavedKeybind
            // 
            this.LabelSavedKeybind.AutoSize = true;
            this.LabelSavedKeybind.Location = new System.Drawing.Point(12, 166);
            this.LabelSavedKeybind.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSavedKeybind.Name = "LabelSavedKeybind";
            this.LabelSavedKeybind.Size = new System.Drawing.Size(81, 13);
            this.LabelSavedKeybind.TabIndex = 8;
            this.LabelSavedKeybind.Text = "Saved keybind:";
            // 
            // CheckBoxAutoListen
            // 
            this.CheckBoxAutoListen.AutoSize = true;
            this.CheckBoxAutoListen.Location = new System.Drawing.Point(486, 118);
            this.CheckBoxAutoListen.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxAutoListen.Name = "CheckBoxAutoListen";
            this.CheckBoxAutoListen.Size = new System.Drawing.Size(300, 17);
            this.CheckBoxAutoListen.TabIndex = 9;
            this.CheckBoxAutoListen.Text = "Automatically start listening for voice commands on launch";
            this.CheckBoxAutoListen.UseVisualStyleBackColor = true;
            this.CheckBoxAutoListen.CheckedChanged += new System.EventHandler(this.AutoListenCheckBox_CheckedChanged);
            // 
            // SvcWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.CheckBoxAutoListen);
            this.Controls.Add(this.LabelSavedKeybind);
            this.Controls.Add(this.LabelSpecifyKeybind);
            this.Controls.Add(this.ButtonClearKeybindInput);
            this.Controls.Add(this.TextBoxKeybind);
            this.Controls.Add(this.ButtonSaveKeybind);
            this.Controls.Add(this.currentVoiceCommandLabel);
            this.Controls.Add(this.ButtonListInstalledGames);
            this.Controls.Add(this.ButtonListVoiceCommands);
            this.Controls.Add(this.ButtonActivate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SvcWindow";
            this.Text = "Steam Voice Commands";
            this.Load += new System.EventHandler(this.SvcWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonListVoiceCommands;
        private System.Windows.Forms.Button ButtonActivate;
        private System.Windows.Forms.Button ButtonListInstalledGames;
        private System.Windows.Forms.Label currentVoiceCommandLabel;
        private System.Windows.Forms.Button ButtonSaveKeybind;
        private System.Windows.Forms.TextBox TextBoxKeybind;
        private System.Windows.Forms.Button ButtonClearKeybindInput;
        private System.Windows.Forms.Label LabelSpecifyKeybind;
        private System.Windows.Forms.Label LabelSavedKeybind;
        private System.Windows.Forms.CheckBox CheckBoxAutoListen;
    }
}

