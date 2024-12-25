using System;
using System.Windows.Forms;

namespace CashierFormApp.Views.Components
{
    public partial class Member : UserControl
    {
        public Member()
        {
            InitializeComponent();
            InitializeListView();

            // Enable double buffering for smooth rendering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }

        private void InitializeListView()
        {
            listMember.View = System.Windows.Forms.View.Details;
            listMember.FullRowSelect = true;
            listMember.GridLines = false;
            listMember.HeaderStyle = ColumnHeaderStyle.None;

            listMember.Columns.Add("Name", 160, HorizontalAlignment.Left);
            listMember.Columns.Add("Address", 200, HorizontalAlignment.Left);
            listMember.Columns.Add("NIK", 160, HorizontalAlignment.Right);
            listMember.Columns.Add("Shop Count", 160, HorizontalAlignment.Right);

            listMember.Resize += (s, e) => AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            if (listMember.Columns.Count > 1)
            {
                int totalWidth = listMember.ClientSize.Width - 24;
                int fixedWidth = 160 + 160 + 160;

                listMember.Columns[1].Width = totalWidth - fixedWidth;
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            MemberHandler memberHandler = new MemberHandler
            {
                IsEditMode = false,
                Text = "Add Member"
            };

            memberHandler.MemberName = "";
            memberHandler.MemberAddress = "";
            memberHandler.MemberNIK = "";

            if (memberHandler.ShowDialog() == DialogResult.OK)
            {
                ListViewItem newItem = new ListViewItem(new[]
                {
                    memberHandler.MemberName,
                    memberHandler.MemberAddress,
                    memberHandler.MemberNIK,
                });

                listMember.Items.Add(newItem);

                MessageBox.Show("Member added successfully!", "Add Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            if (listMember.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a member to edit.", "Edit Member", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = listMember.SelectedItems[0];

            MemberHandler memberHandler = new MemberHandler
            {
                IsEditMode = true,
                MemberName = selectedItem.SubItems[0].Text,
                MemberAddress = selectedItem.SubItems[1].Text,
                MemberNIK = selectedItem.SubItems[2].Text,
            };

            memberHandler.Text = "Edit Member";

            if (memberHandler.ShowDialog() == DialogResult.OK)
            {
                selectedItem.SubItems[0].Text = memberHandler.MemberName;
                selectedItem.SubItems[1].Text = memberHandler.MemberAddress;
                selectedItem.SubItems[2].Text = memberHandler.MemberNIK;

                MessageBox.Show("Member updated successfully!", "Edit Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (listMember.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a member to delete.", "Delete Member", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = listMember.SelectedItems[0];
            string memberName = selectedItem.SubItems[0].Text;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete the member '{memberName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                listMember.Items.Remove(selectedItem);

                MessageBox.Show("Member deleted successfully!", "Delete Member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
