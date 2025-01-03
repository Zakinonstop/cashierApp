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
    public class ProductController
    {
        private ProductRepository _repository;
        public int Create(ProductEntity product)
        {
            int result = 0;

            if (string.IsNullOrEmpty(product.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new ProductRepository(context);
                result = _repository.Create(product);
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

        public int Update(ProductEntity product)
        {
            int result = 0;

            if (string.IsNullOrEmpty(product.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _repository = new ProductRepository(context);
                result = _repository.Update(product);
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

        public List<ProductEntity> ReadAll()
        {
            // membuat objek collection
            List<ProductEntity> list = new List<ProductEntity>();

            // membuat objek context menggunakan blok using
            using (DbContext context = new DbContext())
            {
                // membuat objek dari class repository
                _repository = new ProductRepository(context);

                //panggil method ReadAll yang ada di dalam class repository
                list = _repository.ReadAll();
            }

            return list;
        }

        public int Delete(ProductEntity product)
        {
            int result = 0;

            using (DbContext context = new DbContext())
            {
                _repository = new ProductRepository(context);
                result = _repository.Delete(product);
            }

            if (result > 0)
            {
                MessageBox.Show("Product deleted successfully!", "Delete Product", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Product deleted failed!", "Confirm Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }
    }
}
