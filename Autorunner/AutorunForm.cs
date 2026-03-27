using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Autorunner
{
    public partial class AutorunForm : Form
    {
        public Autorun? Result { get; private set; }
        public string ApplicationPath = string.Empty;
        public AutorunForm()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var type = radioButtonApp.Checked ? AutorunType.Application : AutorunType.Command;
            var minimized = checkBoxMinimized.Checked;
            Result = new Autorun(TextBox_Name.Text, ApplicationPath, TextBox_Command.Text, type, minimized);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void OpenApp_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ApplicationPath = dialog.FileName;
                }
            }

            string fileName = Path.GetFileName(ApplicationPath);

            OpenApp.Text = fileName;
        }

        private void radioButtonApp_CheckedChanged(object sender, EventArgs e)
        {
            OpenApp.Enabled = true;
            TextBox_Command.Enabled = false;
            Add.Enabled = ValidateInput();
        }

        private void radioButtonCommand_CheckedChanged(object sender, EventArgs e)
        {
            OpenApp.Enabled = false;
            TextBox_Command.Enabled = true;
            Add.Enabled = ValidateInput();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(TextBox_Name.Text))
                return false;
            if (!radioButtonApp.Checked && !radioButtonCommand.Checked)
                return false;
            if (string.IsNullOrEmpty(ApplicationPath) && string.IsNullOrEmpty(TextBox_Command.Text))
                return false;
            return true;
        }

        private void TextBox_Command_TextChanged(object sender, EventArgs e)
        {
            Add.Enabled = ValidateInput();
        }

        private void TextBox_Name_TextChanged(object sender, EventArgs e)
        {
            Add.Enabled = ValidateInput();
        }

        private void OpenApp_TextChanged(object sender, EventArgs e)
        {
            Add.Enabled = ValidateInput();
        }
    }
}
