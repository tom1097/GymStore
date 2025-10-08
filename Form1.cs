using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GymStore1
{
    public partial class Form1 : Form
    {
        private List<User> users = new List<User>();
        private BindingSource bindingSource = new BindingSource();
        private User selectedUser = null;
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;
        private Label lblPage;
        private Button btnPrev;
        private Button btnNext;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Auto full screen on first load
            UserRepository.InitializeDatabase();
            users = UserRepository.GetAllUsers();
            InitializeGymManagement();
        }

        private void RefreshUsers()
        {
            users = UserRepository.GetAllUsers();
            totalPages = (int)Math.Ceiling(users.Count / (double)pageSize);
            if (currentPage > totalPages) currentPage = totalPages > 0 ? totalPages : 1;
            UpdatePage();
        }

        private void UpdatePage()
        {
            if (users.Count == 0)
            {
                bindingSource.DataSource = null;
            }
            else
            {
                bindingSource.DataSource = users.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            lblPage.Text = $"Trang {currentPage} / {totalPages}";
        }

        private void InitializeGymManagement()
        {
            var dgv = Controls.Find("dgvUsers", true).FirstOrDefault() as DataGridView;
            if (dgv == null) return;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            bindingSource.DataSource = users;
            dgv.DataSource = bindingSource;
            // Set Vietnamese header text
            if (dgv.Columns["Id"] != null) dgv.Columns["Id"].HeaderText = "#No";
            if (dgv.Columns["Name"] != null) dgv.Columns["Name"].HeaderText = "Tên";
            if (dgv.Columns["PhoneNumber"] != null) dgv.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
            if (dgv.Columns["RegisteredAt"] != null) dgv.Columns["RegisteredAt"].HeaderText = "Ngày đăng ký";
            if (dgv.Columns["ExpiredAt"] != null) dgv.Columns["ExpiredAt"].HeaderText = "Ngày hết hạn";
            if (dgv.Columns["MoneyPerMonth"] != null) dgv.Columns["MoneyPerMonth"].HeaderText = "Tiền/tháng";
            dgv.SelectionChanged += (s, e) =>
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    var row = dgv.SelectedRows[0].DataBoundItem as User;
                    if (row != null)
                    {
                        selectedUser = row;
                    }
                }
            };

            // Format money column for display only
            dgv.CellFormatting += (s, e) =>
            {
                var colName = dgv.Columns[e.ColumnIndex].Name;
                var user = dgv.Rows[e.RowIndex].DataBoundItem as User;
                if (user != null)
                {
                    var today = DateTime.Today;
                    if (colName == "MoneyPerMonth" && e.Value is decimal money)
                    {
                        e.Value = string.Format("{0:N0} VND", money);
                        e.FormattingApplied = true;
                    }
                    else if ((colName == "ExpiredAt" || colName == "RegisteredAt") && e.Value is DateTime dt)
                    {
                        e.Value = dt.ToString("dd/MM/yyyy");
                        e.FormattingApplied = true;
                    }
                    // Color logic for expired/expiring users
                    if (colName == "Name" || colName == "PhoneNumber" || colName == "ExpiredAt")
                    {
                        if (user.ExpiredAt.Date < today)
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.MistyRose; // light red
                        }
                        else if ((user.ExpiredAt.Date - today).TotalDays >= 0 && (user.ExpiredAt.Date - today).TotalDays <= 3)
                        {
                            e.CellStyle.BackColor = System.Drawing.Color.LightYellow; // yellow
                        }
                    }
                }
            };

            // Clear panel controls and set up layout
            panel.Controls.Clear();
            panel.FlowDirection = FlowDirection.TopDown;
            panel.WrapContents = false;
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.Dock = DockStyle.Top;

            // Create a main vertical panel for centering (search + pagination)
            var panelMain = new FlowLayoutPanel {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.None,
                Anchor = AnchorStyles.None,
                Padding = new Padding(0, 10, 0, 10)
            };

            

            // Add the main panel to the form panel (top)
            panel.Controls.Add(panelMain);

            // Create a new bottomPanel for the footer
            var bottomPanel = new Panel {
                Dock = DockStyle.Bottom,
                Height = 60
            };
            // Search row at bottom left
            var searchRow = new FlowLayoutPanel {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Left,
                Anchor = AnchorStyles.Left,
                Padding = new Padding(10, 10, 0, 10)
            };
            var txtSearch = new TextBox {
                Width = 200,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
                PlaceholderText = "Tìm theo tên hoặc số điện thoại"
            };
            var btnSearch = new Button {
                Text = "Tìm kiếm",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            };
            searchRow.Controls.Add(txtSearch);
            searchRow.Controls.Add(btnSearch);
            bottomPanel.Controls.Add(searchRow);
            // Pagination and action buttons at bottom right
            var rightRow = new FlowLayoutPanel {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Right,
                Anchor = AnchorStyles.Right,
                Padding = new Padding(0, 10, 10, 10)
            };
            btnPrev = new Button {
                Text = "Trước",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            };
            btnNext = new Button {
                Text = "Sau",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            };
            lblPage = new Label {
                Text = $"Trang {currentPage} / {totalPages}",
                AutoSize = false,
                Width = 150,
                Height = 40,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            };
            rightRow.Controls.Add(btnPrev);
            rightRow.Controls.Add(lblPage);
            rightRow.Controls.Add(btnNext);
            var btnAdd = new Button {
                Text = "Thêm KH",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            };
            btnAdd.Click += (s, e) => {
                using var form = new UserForm();
                if (form.ShowDialog() == DialogResult.OK) {
                    UserRepository.AddUser(form.User);
                    RefreshUsers();
                }
            };
            rightRow.Controls.Add(btnAdd);
            var btnEdit = new Button {
                Text = "Sửa KH",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
            };
            btnEdit.Click += (s, e) => {
                if (selectedUser != null) {
                    using var form = new UserForm(selectedUser);
                    if (form.ShowDialog() == DialogResult.OK) {
                        UserRepository.UpdateUser(form.User);
                        RefreshUsers();
                    }
                }
            };
            rightRow.Controls.Add(btnEdit);
            var btnDelete = new Button {
                Text = "Xóa KH",
                AutoSize = false,
                Size = new System.Drawing.Size(110, 40),
                Padding = new Padding(10, 5, 10, 5),
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            btnDelete.Click += (s, e) => {
                if (selectedUser != null) {
                    var confirm = MessageBox.Show($"Xóa KH: {selectedUser.Name} - SDT: {selectedUser.PhoneNumber}?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (confirm == DialogResult.OK) {
                        UserRepository.DeleteUser(selectedUser.Id);
                        RefreshUsers();
                    }
                }
            };
            rightRow.Controls.Add(btnDelete);
            bottomPanel.Controls.Add(rightRow);
            Controls.Add(bottomPanel);

            btnSearch.Click += (s, e) => {
                var keyword = txtSearch.Text.Trim().ToLower();
                if (string.IsNullOrEmpty(keyword)) {
                    users = UserRepository.GetAllUsers();
                } else {
                    users = UserRepository.GetAllUsers()
                        .Where(u => u.Name.ToLower().Contains(keyword) || u.PhoneNumber.ToLower().Contains(keyword)).ToList();
                }
                currentPage = 1;
                totalPages = (int)Math.Ceiling(users.Count / (double)pageSize);
                UpdatePage();
            };

            dgv.AllowUserToOrderColumns = true;

            // Track sort direction
            string lastSortColumn = null;
            bool lastSortAsc = true;

            dgv.ColumnHeaderMouseClick += (s, e) =>
            {
                var col = dgv.Columns[e.ColumnIndex].Name;
                // Only allow sorting for ExpiredAt column
                if (col == "ExpiredAt")
                {
                    if (lastSortColumn == col)
                        lastSortAsc = !lastSortAsc;
                    else
                        lastSortAsc = true;
                    lastSortColumn = col;

                    if (lastSortAsc)
                        users = users.OrderBy(u => u.ExpiredAt).ToList();
                    else
                        users = users.OrderByDescending(u => u.ExpiredAt).ToList();

                    currentPage = 1;
                    totalPages = (int)Math.Ceiling(users.Count / (double)pageSize);
                    UpdatePage();

                    // Set sort glyph for ExpiredAt only, after UpdatePage (which resets glyphs)
                    foreach (DataGridViewColumn c in dgv.Columns)
                        c.HeaderCell.SortGlyphDirection = SortOrder.None;
                    dgv.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = lastSortAsc ? SortOrder.Ascending : SortOrder.Descending;
                }
                else
                {
                    // Always show the glyph on ExpiredAt if sorted
                    foreach (DataGridViewColumn c in dgv.Columns)
                        c.HeaderCell.SortGlyphDirection = SortOrder.None;
                    if (lastSortColumn == "ExpiredAt" && dgv.Columns["ExpiredAt"] != null)
                    {
                        var idx = dgv.Columns["ExpiredAt"].Index;
                        dgv.Columns[idx].HeaderCell.SortGlyphDirection = lastSortAsc ? SortOrder.Ascending : SortOrder.Descending;
                    }
                }
            };

            dgv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgv.Rows.Count)
                {
                    var row = dgv.Rows[e.RowIndex].DataBoundItem as User;
                    if (row != null)
                    {
                        using var form = new UserForm(row);
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            UserRepository.UpdateUser(form.User);
                            RefreshUsers();
                        }
                    }
                }
            };

            RefreshUsers();
        }
    }
}
