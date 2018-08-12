using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneratePOCO.Utils;

namespace GeneratePOCO
{
    public partial class FormMain : Form, IOutput
    {
        private const string COL_TABLENAME = "TableName";
        private const string COL_TYPE = "Type";
        private const string COL_NameHumanCase = "NameHumanCase";
        private const string COL_Schema = "Schema";
        private const string COL_ClassName = "ClassName";
        private const string COL_Suffix = "Suffix";
        private const string COL_IsMapping = "IsMapping";
        private const string COL_IsView = "IsView";

        public FormMain()
        {
            InitializeComponent();
        }

        public static void InitConnectionString()
        {
            if (!string.IsNullOrEmpty(Settings.ConnectionString))
                return;
            var section = ConfigurationManager.ConnectionStrings[Settings.ConnectionStringName];
            Settings.ProviderName = section.ProviderName;
            Settings.ConnectionString = section.ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Processer.Outputer = this;
            InitConnectionString();
            txtConnectionString.Text = Settings.ConnectionString;
            LoadTables();
            txtContextTemplate.Text = PathHelper.GetActualPath(Settings.DbContextTemplateFile);
            txtPOCOTemplate.Text = PathHelper.GetActualPath(Settings.POCOClassTemplateFile);
            Log("Load Message from database success!");
        }

        private void LoadTables()
        {
            var config = TablesToGenerateConfig.TableNamesConfig;
            Processer pro = new Processer();
            pro.LoadAllTablesToSetting();
            // pro.LoadAllProcedures();

            DataTable dtTo = new DataTable();
            dtTo.Columns.Add(COL_TABLENAME);
            foreach (var c in config.tables)
            {
                var row = dtTo.NewRow();
                row[COL_TABLENAME] = c;
                dtTo.Rows.Add(row);
            }

            grdTo.DataSource = dtTo;

            DataTable dtAll = new DataTable();
            dtAll.Columns.Add(COL_TABLENAME);
            dtAll.Columns.Add(COL_ClassName);
            dtAll.Columns.Add(COL_NameHumanCase);
            dtAll.Columns.Add(COL_IsView);
            dtAll.Columns.Add(COL_Schema);
            dtAll.Columns.Add(COL_Suffix);
            dtAll.Columns.Add(COL_IsMapping);
            foreach (var c in Settings.Tables)
            {
                var row = dtAll.NewRow();
                row[COL_TABLENAME] = c.Name;
                row[COL_ClassName] = c.ClassName;
                row[COL_NameHumanCase] = c.NameHumanCase;
                row[COL_Schema] = c.Schema;
                row[COL_Suffix] = c.Suffix;
                row[COL_IsMapping] = Resource.GetString(c.IsMapping.ToString().ToLower());
                row[COL_IsView] = Resource.GetString(c.IsView.ToString().ToLower());
                dtAll.Rows.Add(row);
            }
            grdAll.DataSource = dtAll;
            for (int i = 1; i < grdAll.ColumnCount; i++)
            {
                grdAll.Columns[i].ReadOnly = true;
            }
        }

        private void btnAddToGenerate_Click(object sender, EventArgs e)
        {
            var config = TablesToGenerateConfig.TableNamesConfig;
            foreach (DataGridViewRow row in grdAll.Rows)
            {
                if ((bool) row.Cells[0].Value && !TablesToGenerateConfig.TableHashSet.Contains(row.Cells[COL_TABLENAME].Value.ToString()))
                {
                    config.tables.Add(row.Cells[COL_TABLENAME].Value.ToString());
                    TablesToGenerateConfig.TableHashSet.Add(row.Cells[COL_TABLENAME].Value.ToString());
                    var dtTo = grdTo.DataSource as DataTable;
                    var rowNew = dtTo.NewRow();
                    rowNew[COL_TABLENAME] = row.Cells[COL_TABLENAME].Value.ToString();
                    dtTo.Rows.Add(rowNew);
                }
            }
        }

        public void Log(string message, bool isWarn = false)
        {
            if (txtMessage.InvokeRequired)
            {
                txtMessage.BeginInvoke(new MethodInvoker(() => { Log(message, isWarn); }));
            }
            else
            {
                string str = string.Format("{0}：{1}\n", DateTime.Now.ToAllInfoString(), message);
                int txtLen = txtMessage.TextLength>0?txtMessage.TextLength - 1:0;
                txtMessage.AppendText(str);
                txtMessage.SelectionStart = txtLen;
                txtMessage.SelectionLength = str.Length;
                if (isWarn)
                {
                    txtMessage.SelectionColor = Color.Red;
                }
                else
                {
                    txtMessage.SelectionColor = Color.Green;
                }
                txtMessage.ScrollToCaret();
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            ClassOutputGenerater generater = new ClassOutputGenerater(this);
            generater.WriteToFiles();
            MessageBox.Show("Generate Context and Model class success!", "POCO");
        }

        
    }
}
