using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopApp_C300
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pcImage.Location = new Point((this.ClientSize.Width - pcImage.Size.Width) / 2,
                (this.ClientSize.Height - pcImage.Size.Height) / 2
                );
            lblWelcome.Text = "C300 Mağazasına xoş gəlmisiniz";
            lblWelcome.Location = new Point((this.ClientSize.Width - pcImage.Size.Width) / 2,30);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Register rg = new Register();
            rg.ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.ShowDialog();
        }
    }
}
