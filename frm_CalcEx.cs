using FontAwesome.Sharp;
using Stimulsoft.Base;
using Stimulsoft.Controls.Win.DotNetBar.Controls;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
//using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
//using static System.Windows.Forms.LinkLabel;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace CalcEx
{
    public partial class frm_CalcEx : Form
    {
        public string srvname, dbname , uname , psw ;        
        string Link;
        string fcode = "0";
        string tcode = "99999999";
        bool load = false;
        bool Continue = true;
        DataTable dtList = new DataTable(); 
        Color clrrows = Color.White;   
        

        public frm_CalcEx()
        {
            dtList.Columns.Add(new DataColumn("CodeKala", typeof(string)));
            dtList.Columns.Add(new DataColumn("faname", typeof(string)));
            dtList.Columns.Add(new DataColumn("tedadForosh", typeof(float)));
            dtList.Columns.Add(new DataColumn("avrg", typeof(float)));
            dtList.Columns.Add(new DataColumn("shFactor", typeof(string)));
            dtList.Columns.Add(new DataColumn("datefac", typeof(int)));
            dtList.Columns.Add(new DataColumn("fardName", typeof(string)));
            dtList.Columns.Add(new DataColumn("Place", typeof(string)));
            dtList.Columns.Add(new DataColumn("exFactor", typeof(int)));
            dtList.Columns.Add(new DataColumn("ExValue", typeof(int)));
            dtList.Columns.Add(new DataColumn("Quantity", typeof(float)));
            dtList.Columns.Add(new DataColumn("forsales", typeof(float)));
            dtList.Columns.Add(new DataColumn("Status", typeof(string)));
            dtList.Columns.Add(new DataColumn("ExStartDate", typeof(int)));
            dtList.Columns.Add(new DataColumn("ExEndDate", typeof(int)));
            dtList.Columns.Add(new DataColumn("st", typeof(string)));
            dtList.Columns.Add(new DataColumn("enname", typeof(string)));
            dtList.Columns.Add(new DataColumn("buy", typeof(float)));
            dtList.Columns.Add(new DataColumn("codem", typeof(int)));
            dtList.Columns.Add(new DataColumn("CodeRadif", typeof(int)));
            dtList.Columns.Add(new DataColumn("Datedif", typeof(float)));
            dtList.Columns.Add(new DataColumn("Clr", typeof(string)));
            dtList.Columns.Add(new DataColumn("Mojodi", typeof(float)));

            InitializeComponent();
        }

        private void frm_CalcEx_Load(object sender, EventArgs e)
        {
            try
            {
                dgvShow.ColumnHeadersDefaultCellStyle.Font = new Font("IRANSans", 8);
                dgvShow.DefaultCellStyle.Font = dgvShow.ColumnHeadersDefaultCellStyle.Font;
                foreach (DataGridViewColumn column in dgvShow.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                cboxNoeAnb.SelectedIndex = 0;
                lblValuePer.Text = null ;
                DatabaseSQL.InitConnectionString(srvname, dbname, uname, psw);
                DatabaseSQL.InitConnectionStringSQLConnection(srvname, dbname, uname, psw);
                DataTable dt = Database.TableSqlQuery("Select ID,NameDrug from DrugLink");
                cboxLink.DataSource = dt;
                cboxLink.ValueMember = "ID";
                cboxLink.DisplayMember = "NameDrug";
                cboxLink.SelectedItem = "ID";
                cboxLink.SelectedIndex = -1;
                load= true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "frm_CalcEx_Load");
            }                       
        }

        private void cboxLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxLink.SelectedIndex > -1 && load)
            {
                ThreadStart ts = new ThreadStart(getList);
                Thread Tr = new Thread(ts);
                Tr.Start();
            }
        }

        public void getList()
        {
            Invoke((MethodInvoker)delegate
            {
                pLoading.Visible = true;
                ptop.Enabled = false;
                
            });
            try
            {
                int IDLink = 0;
                Invoke((MethodInvoker)delegate
                {
                    IDLink = Convert.ToInt32(cboxLink.SelectedValue.ToString());
                });
                DataTable table = Database.TableSqlQuery("select Link from DrugLink Where id=" + IDLink + "");
                Link = table.Rows[0][0].ToString();
                DataTable dastedaro = Database.TableSqlQuery("select idDastdaroie as id,Dastdaroie as Name from " + Link + "Dastedaroie");
                DataTable dastedaro2 = Database.TableSqlQuery("select idDastdaroie as id,Dastdaroie as Name from " + Link + "Dastedaroie");
                Invoke((MethodInvoker)delegate
                {
                    cboxFdaste.DataSource = dastedaro;
                    cboxFdaste.ValueMember = "id";
                    cboxFdaste.DisplayMember = "Name";
                    cboxFdaste.SelectedItem = "id";
                    cboxFdaste.SelectedIndex = -1;
                    cboxFdaste.SelectedIndex = 0;

                    cboxTdaste.DataSource = dastedaro2;
                    cboxTdaste.ValueMember = "id";
                    cboxTdaste.DisplayMember = "Name";
                    cboxTdaste.SelectedItem = "id";
                    cboxTdaste.SelectedIndex = -1;
                    cboxTdaste.SelectedIndex = cboxTdaste.Items.Count - 1;
                });

                DataTable codegroup = Database.TableSqlQuery("select CodeGroup as id,DrugGroup as Name from " + Link + "DrugGroup");
                DataTable codegroup2 = Database.TableSqlQuery("select CodeGroup as id,DrugGroup as Name from " + Link + "DrugGroup");
                Invoke((MethodInvoker)delegate
                {
                    cboxFGroup.DataSource = codegroup;
                    cboxFGroup.ValueMember = "id";
                    cboxFGroup.DisplayMember = "Name";
                    cboxFGroup.SelectedItem = "id";
                    cboxFGroup.SelectedIndex = -1;
                    cboxFGroup.SelectedIndex = 0;

                    cboxTGroup.DataSource = codegroup2;
                    cboxTGroup.ValueMember = "id";
                    cboxTGroup.DisplayMember = "Name";
                    cboxTGroup.SelectedItem = "id";
                    cboxTGroup.SelectedIndex = -1;
                    cboxTGroup.SelectedIndex = cboxTGroup.Items.Count - 1;
                });

                DataTable noekala = Database.TableSqlQuery("select idNoeKala as id,Noekala as Name from " + Link + "Noekala");
                DataTable noekala2 = Database.TableSqlQuery("select idNoeKala as id,Noekala as Name from " + Link + "Noekala");
                Invoke((MethodInvoker)delegate
                {
                    cboxFNoe.DataSource = noekala;
                    cboxFNoe.ValueMember = "id";
                    cboxFNoe.DisplayMember = "Name";
                    cboxFNoe.SelectedItem = "id";
                    cboxFNoe.SelectedIndex = -1;
                    cboxFNoe.SelectedIndex = 0;

                    cboxTNoe.DataSource = noekala2;
                    cboxTNoe.ValueMember = "id";
                    cboxTNoe.DisplayMember = "Name";
                    cboxTNoe.SelectedItem = "id";
                    cboxTNoe.SelectedIndex = -1;
                    cboxTNoe.SelectedIndex = cboxTNoe.Items.Count - 1;
                });

                Invoke((MethodInvoker)delegate
                {
                    dtList.Clear();
                });

            }
            catch(ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("خطا در واکشی اطلاعات از مقصد \n","خطا");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Invoke((MethodInvoker)delegate
                {
                    pLoading.Visible = false;
                    ptop.Enabled = true;

                });
            }
        }        

        private void txtFCodeKala_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frm_ListKala _ListDr = new frm_ListKala();
                _ListDr.ShowDialog();
                try
                {
                    txtFCodeKala.Text = _ListDr.CodeDr;
                }
                catch
                {

                }
            }
        }

        private void txtTCodeKala_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frm_ListKala _ListDr = new frm_ListKala();
                _ListDr.ShowDialog();
                try
                {
                    txtTCodeKala.Text = _ListDr.CodeDr;
                }
                catch
                {

                }
            }
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            dtList.Clear();
            if (cboxLink.SelectedIndex > -1 && load)
            {
                ThreadStart ts = new ThreadStart(getReport);
                Thread Tr = new Thread(ts);
                Tr.Start();
            }
            else
            {
                MessageBox.Show("در گام اول مقصد خود را انتخاب کنید \n بعدا مجددا برای دریافت گزارش تلاش کنید", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public async void getReport()
        {
           
            string fdaste = "null";
            string tdaste = "null";
            string fgroup = "null";
            string tgroup = "null";
            string fnoe = "null";
            string tnoe = "null";
            string namecode = "null";
            int noeAnb = 0;
            Invoke((MethodInvoker)delegate
            {
                dgvShow.Rows.Clear();
                dtList.Rows.Clear();
                txtSearch.Text = null;
                this.ControlBox = false;
                pLoading.Location = new Point(PMain.Width / 3, PMain.Height / 3);
                Continue = true;
                pLoading.Visible = true;
                btnCancelRecived.Visible = true;
                PMain.Controls.Add(pLoading);
                pLoading.BringToFront();
                label13.Focus();
                pLoading.Anchor = AnchorStyles.Top;
                PtopMain.Visible = true;
                ptop.Enabled = false;
                dgvShow.Enabled = pBottom.Enabled = false;
                namecode = cboxLink.Text;
            });
            try
            {
                int DateDif = 0;
                Invoke((MethodInvoker)delegate
                {
                    fdaste = cboxFdaste.SelectedValue.ToString() == "0" ? "null" : cboxFdaste.SelectedValue.ToString();
                    tdaste = cboxTdaste.SelectedValue.ToString() == "0" ? "null" : cboxTdaste.SelectedValue.ToString();
                    fgroup = cboxFGroup.SelectedValue.ToString() == "0" ? "null" : cboxFGroup.SelectedValue.ToString();
                    tgroup = cboxTGroup.SelectedValue.ToString() == "0" ? "null" : cboxTGroup.SelectedValue.ToString();
                    fnoe = cboxFNoe.SelectedValue.ToString() == "0" ? "null" : cboxFNoe.SelectedValue.ToString();
                    tnoe = cboxTNoe.SelectedValue.ToString() == "0" ? "null" : cboxTNoe.SelectedValue.ToString();
                    fcode = string.IsNullOrEmpty(txtFCodeKala.Text) ? "0" : txtFCodeKala.Text;
                    tcode = string.IsNullOrEmpty(txtTCodeKala.Text) ? "99999999" : txtTCodeKala.Text;
                    noeAnb = cboxNoeAnb.SelectedIndex == 0 ? 1 : cboxTNoe.SelectedIndex + 1;
                });

                DataTable datatdiff = Database.TableSqlQuery($@" SELECT  DATEDIFF(DAY, dbo.Shams2Milady('{txtAzdate.Text.Replace("/", "")}'), dbo.Shams2Milady( '{txtTaDate.Text.Replace("/", "")}'))+1 ");
                DateDif = Convert.ToInt32(datatdiff.Rows[0][0].ToString());

                DataTable listKala = Database.TableSqlQuery($@"Select Code,FaName From Kala K
                                                                Where code Between isnull({fcode},0) and isnull({tcode},99999999)
                                                                and isnull(DasteDaroo,0) between isnull({fdaste},0) and isnull({tdaste},99999999)
                                                                and isnull(CodeGroup,0) between isnull({fgroup},0) and isnull({tgroup},99999999)
                                                                and isnull(Type,0) between isnull({fnoe},0) and isnull({tnoe},99999999)");

                int AllKala = listKala.Rows.Count;
                int irows = 0;
                Invoke((MethodInvoker)delegate
                {
                    progressBar1.Maximum = AllKala;
                });


                int i = 0;
                foreach (DataRow row in listKala.Rows)
                {
                    if (Continue)
                    {


                        Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Value = irows + 1;
                        });

                        Invoke((MethodInvoker)delegate
                        {
                            lblNameValue.Text = listKala.Rows[irows][0].ToString() + " - " + listKala.Rows[irows][1].ToString();
                        });
                        Invoke((MethodInvoker)delegate
                        {
                            lblValuePer.Text = (irows + 1).ToString() + '/' + AllKala.ToString();
                        });
                        Application.DoEvents();

                        DataTable Kalaexfinall = CalcEpired(noeAnb, listKala.Rows[irows][0].ToString());



                        if (Kalaexfinall != null && Kalaexfinall.Rows.Count > 0)
                        {
                            string codelast = "",fanamelast="";
                            foreach (DataRow item in Kalaexfinall.Rows)
                            {
                                codelast = item[0].ToString();
                                fanamelast = item[1].ToString();
                                dtList.Rows.Add(item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7], item[8], item[9], item[10],
                                                item[11], item[12], item[13], item[14], item[15], item[16], item[17], item[18], item[19], item[20],
                                                item[21], item[22]);
                                Invoke((MethodInvoker)delegate
                                {
                                    var groupRow = dgvShow.Rows.Add(item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7], item[8], item[9], item[10],
                                               item[11], item[12], item[13], item[14], item[15], item[16], item[17], item[18], item[19], item[20],
                                               item[21], item[22]);                                    
                                     dgvShow.Rows[groupRow].DefaultCellStyle.BackColor = clrrows;
                                    //dgvShow.Rows[groupRow].DefaultCellStyle.Font = new Font("IRANSans",8);
                                    dgvShow.Rows[groupRow].Cells[12].Style.BackColor = Color.FromName(item[21].ToString());

                                });
                            }                                                       
                            i++;
                            Invoke((MethodInvoker)delegate
                            {
                                var rowslast = dgvShow.Rows.Add(codelast, fanamelast);
                                dgvShow.Rows[rowslast].DefaultCellStyle.ForeColor = Color.Teal;
                                dgvShow.Rows[rowslast].DefaultCellStyle.BackColor = Color.Teal;
                            });

                            if (clrrows.Name == "White")
                                clrrows = Color.LightGray;
                            else
                                clrrows = Color.White;
                        }
                        else
                        {
                           
                        }
                        irows++;
                        Application.DoEvents();

                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Invoke((MethodInvoker)delegate
                {
                    pLoading.Visible = false;
                    btnCancelRecived.Visible = false;
                    PtopMain.Visible = false;
                    ptop.Enabled = true;
                    dgvShow.Enabled = pBottom.Enabled = true;
                    this.ControlBox = true;
                });
            }
        }        

        public DataTable CalcEpired(int noeAnb, string idKala)
        {
            DataTable dt = new DataTable();
            try
            {
               // dt = Database.TableSqlQuery($"exec Sp_CalcuEnghezaMandehNEw {noeAnb},{txtAzdate.Text.Replace("/","")},{txtTaDate.Text.Replace("/", "")},'{Link}',{idKala}");
                dt = Database.TableSqlQuery($"exec {Link}Sp_CalcuEnghezaMandehNEw {noeAnb},{txtAzdate.Text.Replace("/", "")},{txtTaDate.Text.Replace("/", "")},'{Link}',{idKala}");

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }            
        }

        private void btnCancelRecived_Click(object sender, EventArgs e)
        {
            Continue = false;
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable tblPrint = new DataTable();

            tblPrint.Columns.Add(new DataColumn("CodeKala", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("faname", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("tedadForosh", typeof(float)));
            tblPrint.Columns.Add(new DataColumn("avrg", typeof(float)));
            tblPrint.Columns.Add(new DataColumn("shFactor", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("datefac", typeof(int)));
            tblPrint.Columns.Add(new DataColumn("fardName", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("Place", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("exFactor", typeof(int)));
            tblPrint.Columns.Add(new DataColumn("ExValue", typeof(int)));
            tblPrint.Columns.Add(new DataColumn("Quantity", typeof(float)));
            tblPrint.Columns.Add(new DataColumn("forsales", typeof(float)));
            tblPrint.Columns.Add(new DataColumn("Status", typeof(string)));
            tblPrint.Columns.Add(new DataColumn("ExStartDate", typeof(int)));
            tblPrint.Columns.Add(new DataColumn("ExEndDate", typeof(int)));


            if (dgvShow.Rows.Count>0)
            {
                StiReport StiReport = new StiReport();
                try
                {
                    foreach (DataGridViewRow item in dgvShow.Rows)
                    {
                        if (item.Cells[2].Value != null)
                        {
                            DataRow dr = tblPrint.NewRow();
                            dr["CodeKala"] = item.Cells[0].Value.ToString();
                            dr["faname"] = item.Cells[1].Value.ToString();
                            dr["tedadForosh"] = item.Cells[2].Value;
                            dr["avrg"] = item.Cells[3].Value;
                            dr["shFactor"] = item.Cells[4].Value;
                            dr["datefac"] = item.Cells[5].Value;
                            dr["fardName"] = item.Cells[6].Value.ToString();
                            dr["Place"] = item.Cells[7].Value.ToString();
                            dr["exFactor"] = item.Cells[8].Value;
                            dr["ExValue"] = item.Cells[9].Value;
                            dr["Quantity"] = item.Cells[10].Value.ToString();
                            dr["forsales"] = item.Cells[11].Value.ToString();
                            dr["Status"] = item.Cells[12].Value.ToString();
                            dr["ExStartDate"] = item.Cells[13].Value.ToString();
                            dr["ExEndDate"] = item.Cells[14].Value.ToString();

                            tblPrint.Rows.Add(dr);
                        }
                    }
                    StiReport.RegData("tblPrint", tblPrint);
                    StiReport.Load(@"ReportCalcEx\RP_Ex.mrt");

                    StiReport.Compile();
                    StiReport.Show();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("فایل مورد نظر یافت نشد \n " + ex.ToString(), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();

            // اگر متن جستجو خالی است، همه ردیف‌ها را نمایش بده.
            if (string.IsNullOrWhiteSpace(searchText))
            {
                foreach (DataGridViewRow row in dgvShow.Rows)
                {
                    // مطمئن می‌شویم که ردیف تکراری (NewRow) را بررسی نکنیم
                    if (!row.IsNewRow)
                    {
                        row.Visible = true;
                    }
                }
                return;
            }

            // تبدیل متن جستجو به حروف کوچک برای جستجوی غیرحساس به بزرگی و کوچکی حروف
            string lowerSearchText = searchText.ToLower().Replace("ی","ي");

            // تکرار بر روی تمام ردیف‌های موجود در DataGridView
            foreach (DataGridViewRow row in dgvShow.Rows)
            {
                // اجتناب از بررسی ردیف خالی جدید (NewRow)
                if (row.IsNewRow)
                    continue;

                // فرض می‌کنیم داده‌ای که می‌خواهید فیلتر کنید در ستون اول (ایندکس 0) قرار دارد.
                // اگر این سلول مقدار null داشته باشد، از خطا جلوگیری می‌کنیم.
                object cellValue = row.Cells[1].Value;

                if (cellValue != null)
                {
                    string rowText = cellValue.ToString().ToLower();

                    // اعمال فیلتر: ردیف نمایش داده می‌شود اگر حاوی متن جستجو باشد.
                    if (rowText.Contains(lowerSearchText))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                else
                {
                    // اگر سلول مقدار null داشت، آن را پنهان می‌کنیم (مگر اینکه متن جستجو هم خالی باشد که در بالا کنترل شد)
                    row.Visible = false;
                }
            }
        }                               
    }
}
