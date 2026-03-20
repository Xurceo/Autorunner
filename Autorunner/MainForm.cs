using FontAwesome.Sharp;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Autorunner
{
    public partial class MainForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<Autorun> Autoruns { get; set; } = new BindingList<Autorun>();


        NotifyIcon trayIcon = new NotifyIcon();

        private string save_path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "AutorunApp",
            "autoruns.json"
        );

        public MainForm()
        {
            InitializeComponent();
            trayIcon.Icon = MainForm.Main;
            trayIcon.Text = "Autorunner";
            trayIcon.Visible = true;

            // Контекстне меню трею
            var menu = new ContextMenuStrip();
            menu.Items.Add("Відкрити", null, (s, e) => ShowWindow());
            menu.Items.Add("Вийти", null, (s, e) => Application.Exit());
            trayIcon.ContextMenuStrip = menu;

            trayIcon.DoubleClick += (s, e) => ShowWindow();

            RunAutoruns();
        }

        public void SaveAutoruns(List<Autorun> tasks)
        {
            var dir = Path.GetDirectoryName(save_path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(save_path, json);
        }
        public List<Autorun> LoadAutoruns()
        {
            if (!File.Exists(save_path))
                return new List<Autorun>();

            var json = File.ReadAllText(save_path);
            return JsonSerializer.Deserialize<List<Autorun>>(json)
                   ?? new List<Autorun>();
        }

        private void RunAutoruns()
        {
            Autoruns = new BindingList<Autorun>(LoadAutoruns());
            foreach (var autorun in Autoruns)
            {
                try
                {
                    if (autorun.Type == AutorunType.Application)
                    {
                        Process.Start(autorun.Path);
                    }
                    else if (autorun.Type == AutorunType.Command)
                    {
                        Process.Start("cmd.exe", autorun.Command);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при запуску '{autorun.Name}': {ex.Message}");
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void AddAutorun_Click(object sender, EventArgs e)
        {
            using (var dialog = new AutorunForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.Result == null)
                    {
                        MessageBox.Show("Помилка: Не вдалося отримати дані з форми.");
                        return;
                    }

                    // Додаємо об’єкт у список
                    var autorun = dialog.Result;
                    Autoruns.Add(autorun);

                    var itemControl = new AutoRunTaskItem(autorun);
                    itemControl.EditClicked += EditTask;
                    itemControl.DeleteClicked += DeleteTask;

                    panelAutorunTaskItems.Controls.Add(itemControl);

                    MessageBox.Show($"Додано: {dialog.Result.Name}");
                }
            }
        }

        private void EditTask(Autorun task)
        {
            using (var dialog = new AutorunForm())
            {
                dialog.ApplicationPath = task.Path;
                dialog.OpenApp.Text = Path.GetFileName(task.Path);
                dialog.TextBox_Command.Text = task.Command;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.Result == null)
                    {
                        MessageBox.Show("Помилка: Не вдалося отримати дані з форми.");
                        return;
                    }

                    // Оновлюємо дані завдання
                    task.Name = dialog.Result.Name;
                    task.Command = dialog.Result.Command;
                    task.Path = dialog.Result.Path;
                    // Оновлюємо відображення
                    foreach (AutoRunTaskItem item in panelAutorunTaskItems.Controls)
                    {
                        if (item.Task == task)
                        {
                            item.RefreshView();
                            break;
                        }
                    }
                    MessageBox.Show($"Завдання оновлено: {dialog.Result.Name}");
                }
            }
        }

        private void DeleteTask(Autorun task)
        {
            var confirmResult = MessageBox.Show($"Ви впевнені, що хочете видалити завдання '{task.Name}'?", "Підтвердження видалення", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Видаляємо з BindingList
                Autoruns.Remove(task);
                // Видаляємо відповідний контрол з панелі
                foreach (AutoRunTaskItem item in panelAutorunTaskItems.Controls)
                {
                    if (item.Task == task)
                    {
                        panelAutorunTaskItems.Controls.Remove(item);
                        item.Dispose();
                        break;
                    }
                }
                MessageBox.Show($"Завдання видалено: {task.Name}");
            }
        }
    }
}
