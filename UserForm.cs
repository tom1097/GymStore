using System;
using System.Windows.Forms;

namespace GymStore1
{
    public partial class UserForm : Form
    {
        public User User { get; private set; }

        public UserForm(User user = null)
        {
            InitializeComponent();
            // Ensure label is brought to front and visible
            lblMode.Visible = true;
            lblMode.BringToFront();
            if (user != null)
            {
                lblMode.Text = "Sửa KH";
                txtName.Text = user.Name;
                txtPhone.Text = user.PhoneNumber;
                dtpReg.Value = user.RegisteredAt;
                dtpExp.Value = user.ExpiredAt;
                txtMoney.Text = user.MoneyPerMonth.ToString();
                User = user;
            }
            else
            {
                lblMode.Text = "Thêm KH";
                dtpReg.Value = DateTime.Now;
                dtpExp.Value = DateTime.Now.AddMonths(1);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtMoney.Text, out var money))
            {
                if (User == null) User = new User();
                User.Name = txtName.Text;
                User.PhoneNumber = txtPhone.Text;
                User.RegisteredAt = dtpReg.Value.Date;
                User.ExpiredAt = dtpExp.Value.Date;
                User.MoneyPerMonth = money;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Giá trị tiền hàng tháng không đúng.");
            }
        }
    }
}
