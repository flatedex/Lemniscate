using System;
using System.Windows.Forms;

namespace Lemniscate
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }
        bool hideFormHello = false;

        private void About_Load(object sender, EventArgs e)
        {
            if (hideFormHello)
                Close();
        }
    }
}
