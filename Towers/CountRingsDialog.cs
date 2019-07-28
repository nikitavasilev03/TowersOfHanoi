using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Towers
{
    public partial class CountRingsDialog : Form
    {
        public CountRingsDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainWindow form2 = new MainWindow((int)numericUpDown1.Value);
            Hide();
            if (form2.ShowDialog(this) != DialogResult.OK)
                Application.Exit();
            Show();
        }
    }
}
