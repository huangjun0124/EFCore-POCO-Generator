using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratePOCO
{
    public partial class Form1 : Form
    {
        private const string COL_TABLENAME = "TableName";
        private const string COL_TYPE = "Type";
        private const string COL_NameHumanCase = "NameHumanCase";
        private const string COL_Schema = "Schema";
        private const string COL_ClassName = "ClassName";
        private const string COL_Suffix = "Suffix";
        private const string COL_IsMapping = "IsMapping";
        private const string COL_IsView = "IsView";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Utils.InitConnectionString();
            txtConnectionString.Text = Settings.ConnectionString;
            LoadTables();
        }

        private void LoadTables()
        {
            var config = TablesToGenerateConfig.TableNamesConfig;
            Processer pro = new Processer();
            pro.LoadAllTablesToSetting();

            DataTable dtTo = new DataTable();
            dtTo.Columns.Add(COL_TABLENAME);
            dtTo.Columns.Add(COL_IsView);
            foreach (var c in config.tables)
            {
                var row = dtTo.NewRow();
                row[COL_TABLENAME] = c;
                row[COL_IsView] = Resource.GetString("false");
                dtTo.Rows.Add(row);
            }
            foreach (var c in config.views)
            {
                var row = dtTo.NewRow();
                row[COL_TABLENAME] = c;
                row[COL_TYPE] = Resource.GetString("true");
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
                if ((bool) row.Cells[0].Value && !config.TableHashSet.Contains(row.Cells[COL_TABLENAME].Value.ToString()))
                {
                    config.tables.Add(row.Cells[COL_TABLENAME].Value.ToString());
                    config.TableHashSet.Add(row.Cells[COL_TABLENAME].Value.ToString());
                    var dtTo = grdTo.DataSource as DataTable;
                    var rowNew = dtTo.NewRow();
                    rowNew[COL_TABLENAME] = row.Cells[COL_TABLENAME].Value.ToString();
                    rowNew[COL_IsView] = Resource.GetString(row.Cells[COL_IsView].Value.ToString().ToLower());
                    dtTo.Rows.Add(rowNew);
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            

        }

       
    }
}
