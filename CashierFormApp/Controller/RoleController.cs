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
            
            // cek nama yang diinputkan tidak boleh kosong
            if (string.IsNullOrEmpty(role.name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }
            
            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek class repository
                _repository = new RoleRepository(context);
                // panggil method Create class repository untuk menambahkan data
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
    }
}
