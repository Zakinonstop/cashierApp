using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using CashierFormApp.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashierFormApp.Controller
{
    public class RoleController
    {
        private RoleRepository _repository;
        public int Create(Role role)
        {
            int result = 0;
            
            if (string.IsNullOrEmpty(role.name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            
            using (DbContext context = new DbContext())
            {
                _repository = new RoleRepository(context);
                result = _repository.Create(role);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil disimpan !", "Informasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal disimpan !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }

        public int Delete(Role role)
        {
            int result = 0;

            DialogResult dialogResult = MessageBox.Show("Anda yakin akan menghapus data?", "Peringatan", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.No)
            {
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new RoleRepository(context);
                result = _repository.Delete(role);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil dihapus !", "Informasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal dihapus !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
    }
}
