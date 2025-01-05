using System;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using CashierFormApp.Controller;
using CashierFormApp.Model.Entity;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CashierFormApp.Views.Components
{
    public delegate void CreateUpdateEventHandlerMember(MemberEntity member);
    public partial class MemberHandler : Form
    {
        public event CreateUpdateEventHandlerMember OnCreate;

        public event CreateUpdateEventHandlerMember OnUpdate;

        private MemberController controller;
        public bool IsEditMode { get; set; } = false;

        private MemberEntity member;

        private int memberId;

        public string MemberName
        {
            get => txtName.Text;
            set => txtName.Text = value;
        }

        public string MemberNIK
        {
            get => txtNIK.Text;
            set => txtNIK.Text = value;
        }

        public string MemberAddress
        {
            get => txtAddress.Text;
            set => txtAddress.Text = value;
        }

        public MemberHandler()
        {
            InitializeComponent();
        }

        public MemberHandler(string title, MemberController controller) : this()
        {
            this.Text = title;
            this.controller = controller;
        }

        public MemberHandler(string title, MemberEntity obj, MemberController controller) : this()
        {
            this.Text = title;
            this.controller = controller;
            IsEditMode = true;
            member = obj;

            memberId = member.MemberId;
            txtNIK.Text = member.Nik;
            txtName.Text = member.Name;
            txtAddress.Text = member.Address.ToString();
            //txtShopping.Text = member.Shopping.ToString();

        }

        private void MemberHandler_Load(object sender, EventArgs e)
        {
            // Set txtNIK to read-only if in Edit mode
            txtNIK.ReadOnly = IsEditMode;
        }

        private void txtNIK_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numeric input for NIK
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate that required fields are filled
            if (string.IsNullOrWhiteSpace(MemberName))
            {
                MessageBox.Show("Member name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MemberNIK))
            {
                MessageBox.Show("NIK is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(MemberAddress))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsEditMode) member = new MemberEntity();

            member.MemberId = memberId;
            member.Nik = txtNIK.Text;
            member.Name = txtName.Text;
            member.Address = txtAddress.Text;

            int result = 0;

            if (!IsEditMode)
            {
                result = controller.Create(member);

                if (result > 0)
                {
                    OnCreate(member);
                    txtNIK.Clear();
                    txtName.Clear();
                    txtAddress.Clear();
                }
            }
            else
            {
                result = controller.Update(member);

                if (result > 0)
                {
                    OnUpdate(member);
                    this.Close();
                }
            }
        }
    }
}
