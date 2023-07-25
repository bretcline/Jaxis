using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LogViewer.Properties;

namespace LogViewer
{
    public partial class MainWindow : Form
    {
        private readonly List<LogEntry> m_data;

        public MainWindow()
        {
            InitializeComponent();
            m_data = new List<LogEntry>();
        }

        private void MenuItemImportLogsClick(object _sender, EventArgs _e)
        {
            HandledEvent(() =>
            {
                var dlg = new FolderBrowserDialog();
                var result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    ImportLogs(dlg.SelectedPath);
                    m_gridControlLog.DataSource = m_data;
                }
            });
        }

        private void HandledEvent(Action _action)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _action();
                Cursor = Cursors.Arrow;
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.Message, Settings.Default.ApplicationTitle);
            }
        }

        private void ImportLogs(string _path)
        {
            var filePaths = Directory.GetFiles(_path, Settings.Default.FileSearchPattern);
            foreach (var filePath in filePaths)
            {
                ImportLogFile(filePath);
            }
        }

        private void ImportLogFile(string _filePath)
        {
            using (var rdr = new StreamReader(_filePath))
            {
                while (!rdr.EndOfStream)
                {
                    var line = rdr.ReadLine();
                    ImportLine(line, _filePath);
                }
            }
        }

        private void ImportLine(string _line, string _filePath)
        {
            var regex = new Regex(Settings.Default.LogLineRegex);
            var match = regex.Match(_line);
            if (match.Success)
            {
                var fileName = Path.GetFileName(_filePath);
                var dateTimeText = match.Groups[1].Value;
                var typeText = match.Groups[2].Value;
                var threadText = match.Groups[3].Value;
                var messageText = match.Groups[4].Value;

                var logEntry = new LogEntry(fileName, dateTimeText, typeText, threadText, messageText);
                m_data.Add(logEntry);
            }
        }

        private void MenuItemClearClick(object sender, EventArgs e)
        {
            HandledEvent(() =>
            {
                m_data.Clear();
                m_gridControlLog.DataSource = m_data;
            });
        }
    }
}
