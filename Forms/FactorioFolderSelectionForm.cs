using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Overseer
{
    public partial class FactorioFolderSelectionForm : Form
    {
        public FactorioFolderSelectionForm()
        {
            InitializeComponent();
        }

        private void NavigateFolderButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            FactorioPathTextBox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if(Directory.Exists(FactorioPathTextBox.Text))
            {
                StreamWriter sw = new StreamWriter("settings.cfg", false);
                sw.WriteLine(FactorioPathTextBox.Text);
                sw.Close();
                Settings.FactorioPath = FactorioPathTextBox.Text;
                FactorioFolderSelectionForm form = (FactorioFolderSelectionForm)Form.ActiveForm;
                form.Close();
            }
        }
    }
}
