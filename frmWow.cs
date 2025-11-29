using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RZVD
{
    public partial class frmWow : Form
    {
        private int countdown;
        public frmWow()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void frmWow_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countdown++;
            if(countdown > 10)
            {
                timer1.Stop();
                Close();
            }
        }
    }
}
