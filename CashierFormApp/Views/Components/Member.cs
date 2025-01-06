using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CashierFormApp.Controller;
using CashierFormApp.Model.Entity;
using Guna.UI2.AnimatorNS;

namespace CashierFormApp.Views.Components
{
    public partial class Member : UserControl
    {
        private MemberController controller;
        private List<MemberEntity> listOfMember = new List<MemberEntity>();
        public Member()
        {
            InitializeComponent();
            InitializeListView();
            controller = new MemberController();
            LoadDataMember();

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

            listMember.Columns.Add("No", 60, HorizontalAlignment.Left);
            listMember.Columns.Add("Name", 160, HorizontalAlignment.Left);
            listMember.Columns.Add("Address", 200, HorizontalAlignment.Left);
            listMember.Columns.Add("NIK", 160, HorizontalAlignment.Right);
            listMember.Columns.Add("Shop Count", 160, HorizontalAlignment.Right);

            listMember.Resize += (s, e) => AdjustColumnWidths();
        }

        public void LoadDataMember()
        {
            listMember.Items.Clear();

            listOfMember = controller.ReadAll();

            foreach (var value in listOfMember)
            {
                var noUrut = listOfMember.IndexOf(value) + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(value.Name);
                item.SubItems.Add(value.Address);
                item.SubItems.Add(value.Nik);
                item.SubItems.Add(value.Shopping.ToString());

                listMember.Items.Add(item);
            }
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

        public void OnCreateEventHandlerMember(MemberEntity member)
        {
            LoadDataMember();
        }

        public void OnUpdateEventHandlerMember(MemberEntity value)
        {
            int index = listMember.SelectedIndices[0];

            ListViewItem itemRow = listMember.Items[index];

            itemRow.SubItems[1].Text = value.Name;

            itemRow.SubItems[2].Text = value.Address;

            itemRow.SubItems[3].Text = value.Nik.ToString();

            itemRow.SubItems[4].Text = value.Shopping.ToString();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            MemberHandler memberHandler = new MemberHandler("Add Data", controller);

            memberHandler.OnCreate += OnCreateEventHandlerMember;

            memberHandler.ShowDialog();
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            if (listMember.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a member to edit.", "Edit Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MemberEntity member = listOfMember[listMember.SelectedIndices[0]];

                MemberHandler memberHandler = new MemberHandler("Edit Data", member, controller);

                memberHandler.OnUpdate += OnUpdateEventHandlerMember;

                memberHandler.ShowDialog();
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
                MemberEntity member = listOfMember[listMember.SelectedIndices[0]];

                var hasil = controller.Delete(member);

                if (hasil > 0) LoadDataMember();
            }
        }
    }
}
