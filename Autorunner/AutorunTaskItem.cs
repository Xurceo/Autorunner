using System;
using System.Collections.Generic;
using System.Text;
using FontAwesome.Sharp;

namespace Autorunner
{
    public partial class AutoRunTaskItem : UserControl
    {
        public Autorun Task { get; private set; }

        // Події для натискання кнопок
        public event Action<Autorun> EditClicked;
        public event Action<Autorun> DeleteClicked;

        public AutoRunTaskItem(Autorun task)
        {
            this.BackColor = Color.LightBlue;

            InitializeComponent();
            Task = task;

            if (task.Path != null)
            {
                System.Drawing.Icon appIcon = System.Drawing.Icon.ExtractAssociatedIcon(task.Path)!;
                Image.Image = appIcon.ToBitmap();
            }
            labelName!.Text = task.Name;

            buttonEdit!.Click += (s, e) => EditClicked?.Invoke(Task);
            buttonDelete!.Click += (s, e) => DeleteClicked?.Invoke(Task);
        }

        // Можна оновити відображення після редагування
        public void RefreshView()
        {
            labelName.Text = Task.Name;
        }

        private void InitializeComponent()
        {
            labelName = new Label();
            buttonEdit = new IconButton();
            buttonDelete = new IconButton();
            Image = new PictureBox();
            buttonRun = new IconButton();
            ((System.ComponentModel.ISupportInitialize)Image).BeginInit();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(57, 20);
            labelName.Name = "labelName";
            labelName.Size = new Size(38, 15);
            labelName.TabIndex = 0;
            labelName.Text = "label1";
            // 
            // buttonEdit
            // 
            buttonEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonEdit.IconChar = IconChar.Wrench;
            buttonEdit.IconColor = Color.Black;
            buttonEdit.IconFont = IconFont.Auto;
            buttonEdit.Location = new Point(258, 3);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(48, 48);
            buttonEdit.TabIndex = 4;
            buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDelete.IconChar = IconChar.Trash;
            buttonDelete.IconColor = Color.Black;
            buttonDelete.IconFont = IconFont.Auto;
            buttonDelete.Location = new Point(312, 3);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(48, 48);
            buttonDelete.TabIndex = 5;
            buttonDelete.UseVisualStyleBackColor = true;
            // 
            // Image
            // 
            Image.Location = new Point(3, 3);
            Image.Name = "Image";
            Image.Size = new Size(48, 48);
            Image.TabIndex = 6;
            Image.TabStop = false;
            // 
            // buttonRun
            // 
            buttonRun.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRun.IconChar = IconChar.Play;
            buttonRun.IconColor = Color.Black;
            buttonRun.IconFont = IconFont.Auto;
            buttonRun.Location = new Point(204, 3);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(48, 48);
            buttonRun.TabIndex = 7;
            buttonRun.UseVisualStyleBackColor = true;
            buttonRun.Click += buttonRun_Click;
            // 
            // AutoRunTaskItem
            // 
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(labelName);
            Controls.Add(Image);
            Controls.Add(buttonRun);
            Controls.Add(buttonEdit);
            Controls.Add(buttonDelete);
            Margin = new Padding(0, 0, 0, 2);
            Name = "AutoRunTaskItem";
            Size = new Size(363, 52);
            ((System.ComponentModel.ISupportInitialize)Image).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        public Label labelName;
        public IconButton buttonEdit;
        private PictureBox Image;
        public IconButton buttonRun;
        public IconButton buttonDelete;

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Task.Run();
        }
    }
}
