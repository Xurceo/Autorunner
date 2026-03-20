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
            Add.Location = new Point(620, 12);
            Add.Name = "Add";
            Add.Size = new Size(168, 38);
            Add.TabIndex = 0;
            Add.Text = "Додати";
            Add.UseVisualStyleBackColor = true;
            Add.Click += Add_Click;
            // 
            // TextBox_Args
            // 
            TextBox_Command.Location = new Point(12, 161);
            TextBox_Command.Name = "TextBox_Args";
            TextBox_Command.Size = new Size(538, 27);
            TextBox_Command.TabIndex = 3;
            // 
            // OpenApp
            // 
            OpenApp.AutoSize = true;
            OpenApp.Location = new Point(12, 95);
            OpenApp.Name = "OpenApp";
            OpenApp.Size = new Size(155, 30);
            OpenApp.TabIndex = 4;
            OpenApp.Text = "Обрати додаток";
            OpenApp.UseVisualStyleBackColor = true;
            OpenApp.Click += OpenApp_Click;
            // 
            // TextBox_Name
            // 
            TextBox_Name.Location = new Point(12, 32);
            TextBox_Name.Name = "TextBox_Name";
            TextBox_Name.Size = new Size(183, 27);
            TextBox_Name.TabIndex = 5;
            // 
            // Label_Name
            // 
            Label_Name.AutoSize = true;
            Label_Name.Location = new Point(12, 9);
            Label_Name.Name = "Label_Name";
            Label_Name.Size = new Size(139, 20);
            Label_Name.TabIndex = 6;
            Label_Name.Text = "Назва автозапуска";
            // 
            // radioButtonApp
            // 
            radioButtonApp.AutoSize = true;
            radioButtonApp.Location = new Point(12, 65);
            radioButtonApp.Name = "radioButtonApp";
            radioButtonApp.Size = new Size(107, 24);
            radioButtonApp.TabIndex = 9;
            radioButtonApp.TabStop = true;
            radioButtonApp.Text = "Застосунок";
            radioButtonApp.UseVisualStyleBackColor = true;
            radioButtonApp.Click += radioButtonApp_Click;
            // 
            // radioButtonCommand
            // 
            radioButtonCommand.AutoSize = true;
            radioButtonCommand.Location = new Point(12, 131);
            radioButtonCommand.Name = "radioButtonCommand";
            radioButtonCommand.Size = new Size(92, 24);
            radioButtonCommand.TabIndex = 10;
            radioButtonCommand.TabStop = true;
            radioButtonCommand.Text = "Команда";
            radioButtonCommand.UseVisualStyleBackColor = true;
            radioButtonCommand.Click += radioButtonCommand_Click;
            // 
            // AutorunForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 195);
            Controls.Add(radioButtonCommand);
            Controls.Add(radioButtonApp);
            Controls.Add(Label_Name);
            Controls.Add(TextBox_Name);
            Controls.Add(OpenApp);
            Controls.Add(TextBox_Command);
            Controls.Add(Add);
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
        private RadioButton radioButtonApp;
        private RadioButton radioButtonCommand;
    }
}