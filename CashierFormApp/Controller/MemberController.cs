using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using CashierFormApp.Model.Repository;
using System.Windows.Forms;
using System.Web.Compilation;

namespace CashierFormApp.Controller
{
    public class MemberController
    {
        private MemberRepository _repository;

        public int Create(MemberEntity member)
        {
            int result = 0;

            if (string.IsNullOrEmpty(member.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);
                result = _repository.Create(member);
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
        public List<MemberEntity> ReadAll()
        {
            List<MemberEntity> list = new List<MemberEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);

                list = _repository.ReadAll();
            }

            return list;
        }

        public List<MemberEntity> ReadByAnything(string keyword)
        {
            List<MemberEntity> list = new List<MemberEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);

                list = _repository.ReadByAnything(keyword);
            }

            return list;
        }

        public List<MemberEntity> ReadByNik(string keyword)
        {
            List<MemberEntity> list = new List<MemberEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);

                list = _repository.ReadByNik(keyword);
            }

            return list;
        }
        public int Update(MemberEntity member)
        {
            int result = 0;

            if (string.IsNullOrEmpty(member.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);
                result = _repository.Update(member);
            }

            if (result > 0)
            {
                MessageBox.Show("Data berhasil diperbarui !", "Informasi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Data gagal diperbarui !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
        public int Delete(MemberEntity member)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new MemberRepository(context);
                result = _repository.Delete(member);
            }

            if (result > 0)
            {
                MessageBox.Show("Member deleted successfully!", "Delete Member",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Member deleted failed!", "Confirm Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }
    }
}
