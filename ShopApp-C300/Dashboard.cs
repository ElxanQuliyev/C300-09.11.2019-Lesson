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
    public partial class Dashboard : Form
    {
        Client activeClient;
        ShopDB _db;
        #region Constructor
        public Dashboard(Client cs)
        {
            activeClient = cs;
            _db = new ShopDB();
            InitializeComponent();
        }
        #endregion

        #region Category Combobox Fill
        private void FillCategoryCombo()
        {
            cmbProduct.Text="";
            cmbCategory.Items.AddRange(_db.Categories
                .Select(ct => ct.Name).ToArray());
        }
        #endregion
        
        #region Form Load Event
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Welcome " + activeClient.Fullname;
            FillCategoryCombo();
            ProductGridBuyView();

        }
        #endregion
        private void ProductGridBuyView()
        {
            dtgBuyList.DataSource = _db.Orders.Where(ord => ord.ClientId == activeClient.Id)
                  .Select(pr => new {
                      pr.Product.Name,
                      pr.Product.Price,
                      pr.Amount,
                      pr.PurchaseDate
                  }).ToList();

        }
        #region  Product Combobox Fill
        private void ProductComboFill()
        {
            string categoryName = cmbCategory.Text;
            if (categoryName!="")
            {
                int catId = _db.Categories.FirstOrDefault(ct => ct.Name == categoryName).Id;
                cmbProduct.Items.Clear();
                cmbProduct.Items.AddRange(_db.Products.Where(pr => pr.CategoryId == catId).Select(pr => pr.Name)
                    .ToArray());
                cmbProduct.SelectedIndex = 0;

            }
            
        }
        #endregion

        #region Category Select Index Changed
        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductComboFill();
        }
        #endregion

        #region Product Select Index Changed
        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product selectProduct = _db.Products.FirstOrDefault(pr => pr.Name == cmbProduct.Text);
            if (selectProduct.Counts>0)
            {
                nmCount.Value = 1;
                lblStock.Text = selectProduct.Counts.ToString() +" ədəd";
                lblStock.Visible = true;
                lblStock.ForeColor = Color.DarkGreen;
                nmCount.Visible = true;
                button1.Enabled = true;
                lblPrice.Text = ((double)nmCount.Value * selectProduct.Price).ToString() +" Azn";
                lblPrice.Visible = true;

            }
            else
            {
                lblStock.Text = "Stockda bu mehsuldan qalmayıb";
                lblStock.ForeColor = Color.Red;
                lblStock.Visible = true;
                button1.Enabled = false;
                nmCount.Visible = false;
                lblPrice.Visible = false;

            }

        }
        #endregion
        private void NmCount_KeyUp(object sender, KeyEventArgs e)
        {
            Product selectProduct = _db.Products.FirstOrDefault(pr => pr.Name == cmbProduct.Text);
            nmCount.Maximum = (int)_db.Products.Where(pr => pr.Name == cmbProduct.Text)
                .Max(p => p.Counts);
            lblPrice.Text = ((double)nmCount.Value * selectProduct.Price).ToString() + " Azn";
            lblPrice.Visible = true;
        }

        #region Product Buy Method
        private void Button1_Click(object sender, EventArgs e)
        {
            string productname = cmbProduct.Text;
            string categoryname = cmbCategory.Text;
            Product selectProduct = _db.Products.FirstOrDefault(pr => pr.Name == productname);
            double price = (double)selectProduct.Price * (double)nmCount.Value;
            _db.Orders.Add(new Order
            {
                ClientId = activeClient.Id,
                ProductId=selectProduct.Id,
                PurchaseDate=DateTime.Now,
                Amount=(int)nmCount.Value,
                Price= price
            });
            _db.SaveChanges();
            MessageBox.Show(productname + " " + nmCount.Value + " ədəd aldınız!","Orders",MessageBoxButtons.OK,MessageBoxIcon.Information);
            ProductGridBuyView();
        }
        #endregion
    }
}

