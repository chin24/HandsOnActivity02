using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class frmAddProduct : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;

        BindingSource showProductList;
        public frmAddProduct()
        {
            showProductList = new BindingSource();
             
            InitializeComponent();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.SelectedItem.ToString();
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            Product_Name(txtProductName.Text);
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            Quantity(txtQuantity.Text);   
        }

 

        private void txtSellPrice_Leave(object sender, EventArgs e)
        {
            SellingPrice(txtSellPrice.Text);
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProducts = new string[]{
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Goods",
                "Meat",
                "Personal Care",
                "Other",
            };
        
            for (int i = 0; i < 6; i++)
            {
                cbCategory.Items.Add(ListOfProducts[i].ToString());
            }
        }
        public string Product_Name(string name)
        {
            try
            {
                if (txtProductName.Text.Length == 0)
                {
                    throw new ArgumentNullException();
                }
                else if (!Regex.IsMatch(txtProductName.Text, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException("It cannot contain any numbers.");
                }


            }
            catch (StringFormatException h)
            {
                MessageBox.Show(h.Message);
                txtProductName.Focus();
            }
            catch (ArgumentNullException n)
            {
                MessageBox.Show("Product name  cannot be empty.");
                txtProductName.Focus();
            }
                
                return name;
        }
        public int Quantity(string qty)
        {
            try
            {

                if (qty.Length == 0)
                {
                    throw new ArgumentNullException();
                }
                else if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException("It cannot contain letters.");
                }

                else if (Regex.IsMatch(qty, @"^[0-9]"))
                {

                    _Quantity = Convert.ToInt32(qty);

                }
            }
            catch (NumberFormatException e)
            {
                MessageBox.Show(e.Message);
                txtQuantity.Clear();
                txtQuantity.Focus();
            }
            catch (ArgumentNullException n)
            {
                MessageBox.Show("Quantity number cannot be empty.");
                txtQuantity.Focus();
            }
            return _Quantity;


    }
        public double SellingPrice(string price)
        {
            try
            {

                if (price.Length == 0)
                {
                    throw new ArgumentNullException();
                }
                else if (!Regex.IsMatch(price.ToString(), @"^[0-9]"))
                {
                    throw new CurrentFormatException("The format of the price should be (example) 123.12");
                }

                else if (Regex.IsMatch(price, @"^[0-9]"))
                {

                    _SellPrice = Convert.ToDouble(price);

                }
            }
            catch (CurrentFormatException e)
            {
                MessageBox.Show(e.Message);
                txtSellPrice.Focus();
            }
            catch (ArgumentNullException n)
            {
                MessageBox.Show("Price number cannot be empty.");
                txtSellPrice.Focus();
            }

            return _SellPrice;
        }
    }
    class NumberFormatException : Exception
    {
        public NumberFormatException(string a) : base(a)
        {

        }
    }
    class StringFormatException : Exception
    {
        public StringFormatException(string b) : base(b)
        {

        }
    }
    class CurrentFormatException : Exception
    {
        public CurrentFormatException(string b) : base(b)
        {

        }
    }
}
