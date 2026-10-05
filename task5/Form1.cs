using System.Diagnostics.Eventing.Reader;

namespace task5
{
    public partial class Form1 : Form
    {
        private decimal totalAmount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string productName = btn.Tag.ToString().Split(';')[0];
            decimal productPrice = Convert.ToDecimal(btn.Tag.ToString().Split(";")[1]);

            lstCart.Items.Add($"{productName} - {productPrice}€");
            totalAmount += productPrice;
            refreshTotal();

        }

        private void calculateChange()
        {

            if (decimal.TryParse(txtAmount.Text, out decimal paidAmount))
            {
                decimal change = paidAmount - totalAmount;
                if (change >= 0)
                {
                    lblChange.Text = $"{change:0.00} €";
                }
                else
                {
                    lblChange.Text = $"No change left";
                }
            }
            else
            {
                lblChange.Text = $"0.00€";
            }

        }

        private void refreshForm()
        {
            lstCart.Items.Clear();
            totalAmount = 0;
            txtAmount.Text = "";
            lblChange.Text = "0.00€";
            lblTotal.Text = "0.00€";
        }

        private void refreshTotal()
        {
            lblTotal.Text = $"{totalAmount:0.00}€";
            calculateChange();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal paidAmount) && paidAmount >= totalAmount && totalAmount > 0)
            {
                MessageBox.Show("Purchase succsessful", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshForm();
            }
            else
            {
                MessageBox.Show("Please check the amount and cart!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Do you want refresh form?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                refreshForm();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstCart.SelectedIndex != -1)
            {
                string selected = lstCart.SelectedItem.ToString();
                string[] parts = selected.Split('-');

                if (parts.Length > 1)
                {
                    string priceText = parts[1].Replace("€", "").Trim();
                    if (decimal.TryParse(priceText, out decimal price))
                    {
                        totalAmount -= price;
                        refreshTotal();
                    }
                }
                string selectedText = lstCart.SelectedItem.ToString();
                string name = selectedText.Split('-')[0].Trim();
                lstCart.Items.RemoveAt(lstCart.SelectedIndex);
                MessageBox.Show($"{name} removed", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an item!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            refreshTotal();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = "";
            lblChange.Text = "0";
        }


        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            calculateChange();
        }
    }
}
