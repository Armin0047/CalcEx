using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcEx
{
    public partial class frm_ListKala : Form
    {
        public string CodeDr;
        public frm_ListKala()
        {
            InitializeComponent();
        }

        private void frm_ListKala_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable Kala = Database.TableSqlQuery("select K.code  کدکالا,K.FaName نام ,K.GenCode کدژنریک ,K.price قیمت,M.Mojodi موجودی from Kala K Left join Mojodi M on K.Code=M.Code and AnbCode=1 Order by K.Code");
                dgvDr.DataSource = Kala;
                dgvDr.Columns[0].Width = 100;
                dgvDr.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvDr.Columns[2].Width = 70;
                dgvDr.Columns[3].Width = 100;
                dgvDr.Columns[4].Width = 70;
                dgvDr.Columns[3].DefaultCellStyle.Format = "N0";
                dgvDr.Columns[1].DefaultCellStyle.Font = new Font("IRANSans", 9);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ((System.Data.DataTable)dgvDr.DataSource).DefaultView.RowFilter = string.Format("[کدکالا]+[نام]  like '%{0}%'", txtSearch.Text.Trim().Replace("'", "''").Replace("ی", "ي"));

            }
            catch (Exception) { }
        }

        private void dgvDr_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            CodeDr = dgvDr.CurrentRow.Cells[0].Value.ToString();
            this.Close();
        }

        private void dgvDr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                CodeDr = dgvDr.CurrentRow.Cells[0].Value.ToString();
                this.Close();
            }
        }

        private void frm_ListKala_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Close(); }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int i = dgvDr.CurrentRow.Index;

                CodeDr = dgvDr.Rows[i].Cells[0].Value.ToString();
                this.Close();
            }
        }
    }
}
