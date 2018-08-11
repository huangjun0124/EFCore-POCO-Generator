using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GeneratePOCO
{
    public class CommonGrid : DataGridView
    {
        private bool autoResize = true;

        private bool _mouseUpCommit = false;

        private bool _inSorting = false;

        private bool _autoFilter = false;

        private bool _mouseUpInCell = false;

        private bool _herderCheckBoxClick = false;

        private bool _sortable = true;

        private bool _setHeaderText = true;

        public bool AutoCheckBox
        {
            get;
            set;
        }

        public bool AutoFilter
        {
            get
            {
                return this._autoFilter;
            }
            set
            {
                this._autoFilter = value;
            }
        }

        public bool AutoResize
        {
            get
            {
                return this.autoResize;
            }
            set
            {
                this.autoResize = value;
            }
        }

        public bool BigSize
        {
            get;
            set;
        }

        public bool CheckBoxSyncSelectedRows
        {
            get;
            set;
        }

        private string ColumnInEdit
        {
            get;
            set;
        }

        public bool InSorting
        {
            get
            {
                return this._inSorting;
            }
        }

        public bool MouseUpCommit
        {
            get
            {
                return this._mouseUpCommit;
            }
            set
            {
                this._mouseUpCommit = value;
            }
        }

        public bool SetHeaderText
        {
            get
            {
                return this._setHeaderText;
            }
            set
            {
                this._setHeaderText = value;
            }
        }

        public bool ShowHeaderCheckBox
        {
            get;
            set;
        }

        public bool Sortable
        {
            get
            {
                return this._sortable;
            }
            set
            {
                this._sortable = value;
            }
        }

        public CommonGrid()
        {
            base.RowHeadersVisible = false;
            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.LightGreen);
            base.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            base.CellDoubleClick += new DataGridViewCellEventHandler(this.CommonGrid_CellDoubleClick);
            base.AllowUserToResizeRows = false;
            base.CellMouseUp += new DataGridViewCellMouseEventHandler(this.CommonGrid_CellMouseUp);
            base.MouseUp += new MouseEventHandler(this.CommonGrid_MouseUp);
            base.Sorted += new EventHandler(this.CommonGrid_Sorted);
            base.CurrentCellDirtyStateChanged += new EventHandler(this.CommonGrid_CurrentCellDirtyStateChanged);
            base.CellEnter += new DataGridViewCellEventHandler(this.CommonGrid_CellEnter);
            base.EditMode = DataGridViewEditMode.EditOnEnter;
            base.BindingContextChanged += new EventHandler(this.CommonGrid_BindingContextChanged);
            base.TabStop = true;
        }

        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            try
            {
                base.SuspendLayout();
                this._herderCheckBoxClick = true;
                base.CurrentCell = null;
                Console.WriteLine(base.Rows.Count);
                foreach (DataGridViewRow row in (IEnumerable)base.Rows)
                {
                    row.Cells["CheckBoxColumn"].Value = state;
                }
                if (this.CheckBoxSyncSelectedRows)
                {
                    this.SyncSelectionWithCheckBox();
                }
                base.ResumeLayout();
            }
            catch (ArgumentOutOfRangeException argumentOutOfRangeException)
            {
            }
        }

        private void CommonGrid_BindingContextChanged(object sender, EventArgs e)
        {
            if (this.AutoFilter)
            {
                if (base.DataSource != null)
                {
                    base.AutoResizeColumns();
                }
            }
        }

        private void CommonGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                object value = base.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (value != null)
                {
                    Clipboard.SetDataObject(value.ToString(), true);
                }
            }
            catch
            {
            }
        }

        private void CommonGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.ColumnInEdit = base.Columns[e.ColumnIndex].Name;
        }

        private void CommonGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this._herderCheckBoxClick)
                {
                    return;
                }
                else if ((!this.AutoCheckBox ? false : this.CheckBoxSyncSelectedRows))
                {
                    if ((e.RowIndex <= -1 ? false : e.ColumnIndex > -1))
                    {
                        this._mouseUpInCell = true;
                        if (!(base.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType == typeof(bool)))
                        {
                            this.SyncCheckBoxWithSelection();
                        }
                        else
                        {
                            this.SyncSelectionWithCheckBox();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void CommonGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.ColumnInEdit.Equals("CheckBoxColumn"))
            {
                if (base.IsCurrentCellDirty)
                {
                    base.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        } 

        private void CommonGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (this._herderCheckBoxClick)
                {
                    return;
                }
                else if ((this._mouseUpInCell || !this.AutoCheckBox ? false : this.CheckBoxSyncSelectedRows))
                {
                    if ((base.Rows.Count <= 0 ? false : base.Columns.Count > 0))
                    {
                        this.SyncCheckBoxWithSelection();
                    }
                }
            }
            finally
            {
                this._mouseUpInCell = false;
                this._herderCheckBoxClick = false;
            }
        }

        private void CommonGrid_Sorted(object sender, EventArgs e)
        {
            try
            {
                this._inSorting = true;
                if ((!this.AutoCheckBox || !this.CheckBoxSyncSelectedRows ? false : base.Rows.Count > 0))
                {
                    this.SyncSelectionWithCheckBox();
                }
                this.OnDataSourceChanged(e);
            }
            finally
            {
                this._inSorting = false;
            }
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            if (this._setHeaderText)
            {
                string str = Resource.GetString(e.Column.Name);
                if (!string.IsNullOrEmpty(str))
                {
                    e.Column.HeaderText = str;
                }
            }
            if (this.AutoResize)
            {
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                base.AutoResizeColumn(e.Column.Index);
            }
            if (e.Column.Name.Equals("CheckBoxColumn"))
            {
                e.Column.DisplayIndex = 0;
                e.Column.ReadOnly = false;
                e.Column.MinimumWidth = 30;
                if (this.ShowHeaderCheckBox)
                {
                    CommonGirdCheckBoxHeaderCell commonGirdCheckBoxHeaderCell = new CommonGirdCheckBoxHeaderCell();
                    commonGirdCheckBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.cbHeader_OnCheckBoxClicked);
                    e.Column.HeaderCell = commonGirdCheckBoxHeaderCell;
                }
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                e.Column.HeaderText = string.Empty;
                base.AutoResizeColumn(e.Column.Index);
            }
            else if (e.Column.Name.Equals("+++"))
            {
                e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                e.Column.HeaderText = string.Empty;
                base.AutoResizeColumn(e.Column.Index);
                e.Column.MinimumWidth = 30;
                e.Column.ReadOnly = true;
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            if (base.DataSource != null)
            {
                DataTable dataSource = base.DataSource as DataTable;
                if ((!this.AutoCheckBox ? false : !dataSource.Columns.Contains("CheckBoxColumn")))
                {
                    DataColumn dataColumn = new DataColumn("CheckBoxColumn", typeof(bool))
                    {
                        DefaultValue = false
                    };
                    dataSource.Columns.Add(dataColumn);
                    dataColumn.SetOrdinal(0);
                    dataColumn.ReadOnly = false;
                }
                base.OnDataSourceChanged(e);
                if (base.CurrentCell != null)
                {
                    base.CurrentCell.Selected = false;
                }
                base.TabStop = false;
                if (!this.Sortable)
                {
                    foreach (DataGridViewColumn column in base.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                }
            }
            else
            {
                base.OnDataSourceChanged(e);
            }
        }

        public void SetStyle()
        {
            if (!this.BigSize)
            {
                this.Font = new System.Drawing.Font("Verdana", 8.75f, FontStyle.Regular);
            }
            else
            {
                this.Font = new System.Drawing.Font("Verdana", 18.25f, FontStyle.Regular);
            }
            base.BackgroundColor = ColorTranslator.FromHtml("#F8F8F8");
            base.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        public void SyncCheckBoxWithSelection()
        {
            base.SuspendLayout();
            if ((!this.AutoCheckBox ? false : this.CheckBoxSyncSelectedRows))
            {
                foreach (DataGridViewRow row in (IEnumerable)base.Rows)
                {
                    row.Cells["CheckBoxColumn"].Value = row.Selected;
                }
            }
            base.ResumeLayout();
        }

        public void SyncSelectionWithCheckBox()
        {
            base.SuspendLayout();
            if ((!this.AutoCheckBox ? false : this.CheckBoxSyncSelectedRows))
            {
                foreach (DataGridViewRow row in (IEnumerable)base.Rows)
                {
                    row.Selected = (bool)row.Cells["CheckBoxColumn"].Value;
                }
            }
            base.ResumeLayout();
        }
    }
}