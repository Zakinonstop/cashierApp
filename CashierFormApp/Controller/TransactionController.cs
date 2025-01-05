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
    public class TransactionController
    {
        private TransactionRepository _repository;

        public int Create(TransactionEntity transaction)
        {
            int result = 0;

            //if (string.IsNullOrEmpty(transaction.Name))
            //{
            //    MessageBox.Show("Nama harus diisi !!!", "Peringatan",
            //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return 0;
            //}

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);
                result = _repository.Create(transaction);
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
        public List<TransactionEntity> ReadAll()
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);

                list = _repository.ReadAll();
            }

            return list;
        }

        public List<TransactionEntity> ReadByAnything(string keyword)
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);

                list = _repository.ReadByAnything(keyword);
            }

            return list;
        }

        public List<TransactionEntity> GetMaxTransactionDetailId()
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);

                list = _repository.GetMaxTransactionDetailId();
            }

            return list;
        }
        public int Update(TransactionEntity transaction)
        {
            int result = 0;

            //if (string.IsNullOrEmpty(transaction.Name))
            //{
            //    MessageBox.Show("Nama harus diisi !!!", "Peringatan",
            //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return 0;
            //}

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);
                result = _repository.Update(transaction);
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
        public int Delete(TransactionEntity transaction)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionRepository(context);
                result = _repository.Delete(transaction);
            }

            if (result > 0)
            {
                MessageBox.Show("transaction deleted successfully!", "Delete transaction",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("transaction deleted failed!", "Confirm Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }
    }
}
