namespace Autorunner
{
    partial class AutorunForm
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
            Add = new Button();
            TextBox_Command = new TextBox();
            OpenApp = new Button();
            TextBox_Name = new TextBox();
            Label_Name = new Label();
            radioButtonApp = new RadioButton();
            radioButtonCommand = new RadioButton();
            SuspendLayout();
            // 
            // Add
            // 
            Add.Location = new Point(542, 9);
            Add.Margin = new Padding(3, 2, 3, 2);
            Add.Name = "Add";
            Add.Size = new Size(147, 28);
            Add.TabIndex = 0;
            Add.Text = "Додати";
            Add.UseVisualStyleBackColor = true;
            Add.Click += Add_Click;
            // 
            // TextBox_Command
            // 
            TextBox_Command.Location = new Point(10, 121);
            TextBox_Command.Margin = new Padding(3, 2, 3, 2);
            TextBox_Command.Name = "TextBox_Command";
            TextBox_Command.Size = new Size(471, 23);
            TextBox_Command.TabIndex = 3;
            // 
            // OpenApp
            // 
            OpenApp.AutoSize = true;
            OpenApp.Location = new Point(10, 71);
            OpenApp.Margin = new Padding(3, 2, 3, 2);
            OpenApp.Name = "OpenApp";
            OpenApp.Size = new Size(136, 25);
            OpenApp.TabIndex = 4;
            OpenApp.Text = "Обрати додаток";
            OpenApp.UseVisualStyleBackColor = true;
            OpenApp.Click += OpenApp_Click;
            // 
            // TextBox_Name
            // 
            TextBox_Name.Location = new Point(10, 24);
            TextBox_Name.Margin = new Padding(3, 2, 3, 2);
            TextBox_Name.Name = "TextBox_Name";
            TextBox_Name.Size = new Size(161, 23);
            TextBox_Name.TabIndex = 5;
            // 
            // Label_Name
            // 
            Label_Name.AutoSize = true;
            Label_Name.Location = new Point(10, 7);
            Label_Name.Name = "Label_Name";
            Label_Name.Size = new Size(108, 15);
            Label_Name.TabIndex = 6;
            Label_Name.Text = "Назва автозапуска";
            // 
            // radioButtonApp
            // 
            radioButtonApp.AutoSize = true;
            radioButtonApp.Location = new Point(10, 49);
            radioButtonApp.Margin = new Padding(3, 2, 3, 2);
            radioButtonApp.Name = "radioButtonApp";
            radioButtonApp.Size = new Size(88, 19);
            radioButtonApp.TabIndex = 9;
            radioButtonApp.TabStop = true;
            radioButtonApp.Text = "Застосунок";
            radioButtonApp.UseVisualStyleBackColor = true;
            radioButtonApp.CheckedChanged += radioButtonApp_CheckedChanged;
            // 
            // radioButtonCommand
            // 
            radioButtonCommand.AutoSize = true;
            radioButtonCommand.Location = new Point(10, 98);
            radioButtonCommand.Margin = new Padding(3, 2, 3, 2);
            radioButtonCommand.Name = "radioButtonCommand";
            radioButtonCommand.Size = new Size(73, 19);
            radioButtonCommand.TabIndex = 10;
            radioButtonCommand.TabStop = true;
            radioButtonCommand.Text = "Команда";
            radioButtonCommand.UseVisualStyleBackColor = true;
            radioButtonCommand.CheckedChanged += radioButtonCommand_CheckedChanged;
            // 
            // AutorunForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 146);
            Controls.Add(radioButtonCommand);
            Controls.Add(radioButtonApp);
            Controls.Add(Label_Name);
            Controls.Add(TextBox_Name);
            Controls.Add(OpenApp);
            Controls.Add(TextBox_Command);
            Controls.Add(Add);
            Margin = new Padding(3, 2, 3, 2);
            Name = "AutorunForm";
            Text = "Autorun";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Button Add;
        public TextBox TextBox_Command;
        public Button OpenApp;
        public TextBox TextBox_Name;
        public Label Label_Name;
        public RadioButton radioButtonApp;
        public RadioButton radioButtonCommand;
    }
}