using ShopApp_C300.Model;
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
    public partial class Login : Form
    {
        ShopDB db = new ShopDB();
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (email!="" && password!="")
            {
                Client selectClient = db.Clients.FirstOrDefault(cl => cl.Email == email);
                if (selectClient!=null)
                {
                    if (selectClient.Password==Extensions.HashMe(password))
                    {
                        Dashboard dsh = new Dashboard(selectClient);
                     
                        this.Close();
                        dsh.ShowDialog();
                    }
                    else
                    {
                        lblError.Text = "Password is not correct!";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Email is not correct!";
                    lblError.Visible = true;
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
