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
                if (!string.IsNullOrEmpty(user.UserImage))
                {
                    try { picUserImage.Image = System.Drawing.Image.FromFile(user.UserImage); } catch { picUserImage.Image = null; }
                }
                User = user;
            }
            else
            {
                lblMode.Text = "Thêm KH";
                dtpReg.Value = DateTime.Now;
                dtpExp.Value = DateTime.Now.AddMonths(1);
            }
            btnSelectImage.Click += (s, e) =>
            {
                using var dlg = new OpenFileDialog();
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    picUserImage.Image = System.Drawing.Image.FromFile(dlg.FileName);
                    if (User == null) User = new User();
                    User.UserImage = dlg.FileName;
                }
            };
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMoney.Text))
            {
                MessageBox.Show("Vui lòng nhập tiền/tháng.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoney.Focus();
                return;
            }
            if (!decimal.TryParse(txtMoney.Text, out var money))
            {
                MessageBox.Show("Giá trị tiền hàng tháng không đúng.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoney.Focus();
                return;
            }

            if (User == null) User = new User();
            User.Name = txtName.Text;
            User.PhoneNumber = txtPhone.Text;
            User.RegisteredAt = dtpReg.Value.Date;
            User.ExpiredAt = dtpExp.Value.Date;
            User.MoneyPerMonth = money;
            if (picUserImage.Image != null && string.IsNullOrEmpty(User.UserImage))
            {
                MessageBox.Show("Vui lòng chọn ảnh cho người dùng.");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}
