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
using Google.Protobuf.WellKnownTypes;

namespace CashierFormApp.Controller
{
    public class TransactionDetailController
    {
        private TransactionDetailRepository _repository;

        public int Create(int transactionDetailId, int productId, int qty, float price)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);
                result = _repository.Create(transactionDetailId, productId, qty, price);
            }

            //if (result > 0)
            //{
            //    MessageBox.Show("Data berhasil disimpan !", "Informasi",
            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //    MessageBox.Show("Data gagal disimpan !!!", "Peringatan",
            //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return result;
        }
        public List<TransactionDetailEntity> ReadAll()
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);

                list = _repository.ReadAll();
            }

            return list;
        }

        public List<TransactionDetailEntity> ReadByAnything(string keyword)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);

                list = _repository.ReadByAnything(keyword);
            }

            return list;
        }

        public List<TransactionDetailEntity> ReadByTransactionDetailId(int transactionDetailId)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);

                list = _repository.ReadByTransactionDetailId(transactionDetailId);
            }

            return list;
        }

        public List<TransactionDetailEntity> ReadDetailByTransactionDetailId(int transactionDetailId)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);

                list = _repository.ReadDetailByTransactionDetailId(transactionDetailId);
            }

            return list;
        }

        //public List<TransactionDetailEntity> GetMaxtransactionDetailDetailId()
        //{
        //    List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

        //    using (DbContext context = new DbContext())
        //    {
        //        _repository = new TransactionDetailRepository(context);

        //        list = _repository.GetMaxtransactionDetailDetailId();
        //    }

        //    return list;
        //}
        public int Update(TransactionDetailEntity transactionDetail)
        {
            int result = 0;

            //if (string.IsNullOrEmpty(transactionDetail.Name))
            //{
            //    MessageBox.Show("Nama harus diisi !!!", "Peringatan",
            //    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return 0;
            //}

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);
                result = _repository.Update(transactionDetail);
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
        public int Delete(TransactionDetailEntity transactionDetail)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new TransactionDetailRepository(context);
                result = _repository.Delete(transactionDetail);
            }

            if (result > 0)
            {
                MessageBox.Show("transactionDetail deleted successfully!", "Delete transactionDetail",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("transactionDetail deleted failed!", "Confirm Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

    }
}
