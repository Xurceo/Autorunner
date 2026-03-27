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

        //Delete autoruns later!
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
            // Ensure tray icon is valid; fallback to a system icon if resource is missing
            trayIcon.Icon = this.Icon ?? SystemIcons.Application;

            trayIcon.Text = "Autorunner";

            // Context menu for tray icon
            var menu = new ContextMenuStrip();
            menu.Items.Add("Відкрити", null, (s, e) => ShowWindow());
            menu.Items.Add("Вийти", null, (s, e) => Application.Exit());
            trayIcon.ContextMenuStrip = menu;

            trayIcon.DoubleClick += (s, e) => ShowWindow();

            // Make the tray icon visible after configuration
            trayIcon.Visible = true;

            LoadAutoruns();
            this.Load += (s, e) => AddSavedAutorunsToFlowPanel();
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

        // Rerender panel with tasks width of the panel
        private void MainForm_Resize(object sender, EventArgs e)
        {
            foreach (AutoRunTaskItem item in panelAutorunTaskItems.Controls)
            {
                item.Width = panelAutorunTaskItems.ClientSize.Width;
                item.labelName.MaximumSize = new Size(item.Width - item.buttonEdit.Width - item.buttonDelete.Width - 40, 0);
            }

        }

        // Add autoruns to panel
        public void AddSavedAutorunsToFlowPanel()
        {
            foreach (var autorun in Autoruns)
            {
                AddAutorunToFlowPanel(autorun);
            }
        }

        // Add autorun to panel
        public void AddAutorunToFlowPanel(Autorun autorun)
        {
            var itemControl = new AutoRunTaskItem(autorun);
            itemControl.EditClicked += EditTask;
            itemControl.DeleteClicked += DeleteTask;
            panelAutorunTaskItems.Controls.Add(itemControl);
            itemControl.Width = panelAutorunTaskItems.ClientSize.Width;
            panelAutorunTaskItems.Refresh();
        }

        public void LoadAutoruns()
        {
            if (!File.Exists(save_path))
            {
                return;
            }

            var json = File.ReadAllText(save_path);
            try
            {
                var deserialized = JsonSerializer.Deserialize<List<Autorun>>(json);
                Autoruns = new BindingList<Autorun>(deserialized ?? new List<Autorun>());

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при завантаженні авторанів: {ex.Message}");
            }
        }

        private void RunAutoruns()
        {
            // If there are no autoruns, we simply return without doing anything
            if (Autoruns == null || Autoruns.Count == 0)
            {
                return;
            }

            // Iterate through each autorun task and execute it based on its type
            foreach (var autorun in Autoruns)
            {
                try
                {
                    StartAutorun(autorun);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при запуску '{autorun.Name}': {ex.Message}");
                }
            }
        }

        // Starts autorun task silently without showing any windows or errors (used for autoruns that should run in the background)
        private void StartAutorun(Autorun autorun)
        {
            if (autorun == null)
                return;

            if (autorun.Type == AutorunType.Application)
            {
                // Start an application executable. UseShellExecute = true so shell handles file associations and GUI apps show as expected.
                if (string.IsNullOrWhiteSpace(autorun.Path) || !File.Exists(autorun.Path))
                    return;

                var startInfo = new ProcessStartInfo
                {
                    FileName = autorun.Path,
                    WindowStyle = autorun.Minimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal,
                    UseShellExecute = true,
                    // Set working directory to the exe folder so launched app resolves relative resources correctly
                    WorkingDirectory = Path.GetDirectoryName(autorun.Path) ?? string.Empty
                };

                Process.Start(startInfo);
            }
            else if (autorun.Type == AutorunType.Command)
            {
                // Run a command via cmd.exe using /C so it executes and exits.
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {autorun.Command}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false
                };

                Process.Start(startInfo);
            }
        }

        // When the user tries to close the form, hide form in tray
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

        // Open the form when the user clicks on the tray icon
        private void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        // When "Add autorun" clicked, open the form to add new autorun task
        private void AddAutorun_Click(object sender, EventArgs e)
        {
            panelAutorunTaskItems.Invalidate();
            // Open dialog form to add new autorun task
            using (var dialog = new AutorunForm())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.Result == null)
                    {
                        MessageBox.Show("Помилка: Не вдалося отримати дані з форми.");
                        return;
                    }

                    // Add autorun to the list
                    var autorun = dialog.Result;
                    Autoruns.Add(autorun);

                    AddAutorunToFlowPanel(autorun);

                    MessageBox.Show($"Додано: {dialog.Result.Name}");

                    // Save updated list of autoruns to file after adding new task to json file
                    SaveAutoruns(Autoruns.ToList());
                }
            }
        }

        // When "Edit" clicked for a specific autorun task, open the form to edit that task
        private void EditTask(Autorun autorun)
        {
            // Same form as for adding new autorun, but with pre-filled data of the selected task
            using (var dialog = new AutorunForm())
            {
                // Pre-filled data
                dialog.TextBox_Name.Text = autorun.Name;
                dialog.ApplicationPath = autorun.Path;
                dialog.OpenApp.Text = Path.GetFileName(autorun.Path);
                dialog.TextBox_Command.Text = autorun.Command;
                dialog.radioButtonApp.Checked = autorun.Type == AutorunType.Application;
                dialog.radioButtonCommand.Checked = autorun.Type == AutorunType.Command;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.Result == null)
                    {
                        MessageBox.Show("Помилка: Не вдалося отримати дані з форми.");
                        return;
                    }

                    // Refresh data
                    autorun.Name = dialog.Result.Name;
                    autorun.Command = dialog.Result.Command;
                    autorun.Path = dialog.Result.Path;

                    // Refresh view after editing
                    foreach (AutoRunTaskItem task in panelAutorunTaskItems.Controls)
                    {
                        if (task.Task == autorun)
                        {
                            task.RefreshView();
                            break;
                        }
                    }
                    MessageBox.Show($"Завдання оновлено: {dialog.Result.Name}");

                    // Save updated list of autoruns to file after editing task to json file
                    SaveAutoruns(Autoruns.ToList());
                }
            }
        }

        // When "Delete" clicked for a specific autorun task, ask for confirmation and delete that task if confirmed
        private void DeleteTask(Autorun task)
        {
            var confirmResult = MessageBox.Show($"Ви впевнені, що хочете видалити завдання '{task.Name}'?", "Підтвердження видалення", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Deleting from BindingList
                Autoruns.Remove(task);

                // Deleting from the panel
                foreach (AutoRunTaskItem item in panelAutorunTaskItems.Controls)
                {
                    if (item.Task == task)
                    {
                        panelAutorunTaskItems.Controls.Remove(item);
                        item.Dispose();
                        panelAutorunTaskItems.Invalidate();
                        break;
                    }
                }
                MessageBox.Show($"Завдання видалено: {task.Name}");

                // Save updated list of autoruns to file after deleting task to json file
                SaveAutoruns(Autoruns.ToList());
            }
        }
    }
}
