namespace GymStore1
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.DateTimePicker dtpReg;
        private System.Windows.Forms.DateTimePicker dtpExp;
        private System.Windows.Forms.TextBox txtMoney;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picUserImage;
        private System.Windows.Forms.Button btnSelectImage;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblMode = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.dtpReg = new System.Windows.Forms.DateTimePicker();
            this.dtpExp = new System.Windows.Forms.DateTimePicker();
            this.txtMoney = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.picUserImage = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMode
            // 
            this.lblMode.Location = new System.Drawing.Point(20, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(360, 30);
            this.lblMode.Text = "Chế độ";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(20, 40);
            this.txtName.Name = "txtName";
            this.txtName.PlaceholderText = "Tên khách hàng";
            this.txtName.Size = new System.Drawing.Size(250, 30);
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(20, 80);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 30);
            this.txtPhone.PlaceholderText = "Số điện thoại";
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // dtpReg
            // 
            this.dtpReg.Location = new System.Drawing.Point(20, 120);
            this.dtpReg.Name = "dtpReg";
            this.dtpReg.Size = new System.Drawing.Size(250, 30);
            this.dtpReg.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReg.CustomFormat = "dd/MM/yyyy";
            this.dtpReg.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // dtpExp
            // 
            this.dtpExp.Location = new System.Drawing.Point(20, 160);
            this.dtpExp.Name = "dtpExp";
            this.dtpExp.Size = new System.Drawing.Size(250, 30);
            this.dtpExp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExp.CustomFormat = "dd/MM/yyyy";
            this.dtpExp.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // txtMoney
            // 
            this.txtMoney.Location = new System.Drawing.Point(20, 200);
            this.txtMoney.Name = "txtMoney";
            this.txtMoney.Size = new System.Drawing.Size(250, 30);
            this.txtMoney.PlaceholderText = "Tiền/tháng";
            this.txtMoney.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(80, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 40);
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Text = "Hủy";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // picUserImage
            // 
            this.picUserImage.Location = new System.Drawing.Point(290, 40);
            this.picUserImage.Name = "picUserImage";
            this.picUserImage.Size = new System.Drawing.Size(90, 90);
            this.picUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUserImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(290, 140);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(90, 30);
            this.btnSelectImage.Text = "Chọn ảnh";
            this.btnSelectImage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // UserForm
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.dtpReg);
            this.Controls.Add(this.dtpExp);
            this.Controls.Add(this.txtMoney);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.picUserImage);
            this.Controls.Add(this.btnSelectImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Khách hàng";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
