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
    public partial class Register : Form
    {
        ShopDB db = new ShopDB();
        public Register()
        {
            InitializeComponent();
        }
        //private void ClearRegisterInput()
        //{
        //    txtFullname.Text = "";
        //    txtEmail.Text = "";
        //    txtPassword.Text = "";
        //    txtConfirmPassword.Text = "";
        //    txtPhone.Text = "";

        //}
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            
            string email  = txtEmail.Text;
            string password = txtPassword.Text;
            string phone = txtPhone.Text;
            string confirmPassword = txtConfirmPassword.Text;
            if (fullname!="" && email!="" && password!="" && phone!=""
                  && confirmPassword!="")
            {
                if (password == confirmPassword)
                {
                    
                    if (password.Length >= 5)
                    {


                        db.Clients.Add(new Client {
                            Email = email,
                            Password = password.HashMe(),
                            Phone = phone,
                            Fullname = fullname
                        });
                        db.SaveChanges();
                        MessageBox.Show(fullname + " was successfully created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Login lg = new Login();
                        this.Close();
                        lg.ShowDialog();
                    }
                    else
                    {
                        lblError.Text = "Password length should be greater than 5";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Password and Confirm Password must be same!";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Please all the Fill!";
                lblError.Visible = true;
            }
        }

        private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar<48 || e.KeyChar>57) && e.KeyChar!=8)
            {
                e.Handled = true;
            }
        }

        private void TxtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void LblError_Click(object sender, EventArgs e)
        {

        }
    }
}
