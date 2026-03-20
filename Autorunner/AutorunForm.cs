using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            Result = new Autorun(TextBox_Name.Text, ApplicationPath, TextBox_Command.Text);

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

        private void radioButtonApp_Click(object sender, EventArgs e)
        {
            OpenApp.Enabled = true;
            TextBox_Command.Enabled = false;
        }

        private void radioButtonCommand_Click(object sender, EventArgs e)
        {
            OpenApp.Enabled = false;
            TextBox_Command.Enabled = true;
        }
    }
}
