using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GeneratePOCO
{
    public class CommonGirdCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        private Point checkBoxLocation;

        private System.Drawing.Size checkBoxSize;

        private bool _checked = false;

        private Point _cellLocation = new Point();

        private CheckBoxState _cbState = CheckBoxState.UncheckedNormal;

        public CommonGirdCheckBoxHeaderCell()
        {
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point point = new Point(e.X + this._cellLocation.X, e.Y + this._cellLocation.Y);
            if ((point.X < this.checkBoxLocation.X || point.X > this.checkBoxLocation.X + this.checkBoxSize.Width || point.Y < this.checkBoxLocation.Y ? false : point.Y <= this.checkBoxLocation.Y + this.checkBoxSize.Height))
            {
                this._checked = !this._checked;
                if (this.OnCheckBoxClicked != null)
                {
                    this.OnCheckBoxClicked(this._checked);
                    base.DataGridView.InvalidateCell(this);
                }
            }
            base.OnMouseClick(e);
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            Point x = new Point();
            System.Drawing.Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
            Point location = cellBounds.Location;
            x.X = location.X + cellBounds.Width / 2 - glyphSize.Width / 2;
            location = cellBounds.Location;
            x.Y = location.Y + cellBounds.Height / 2 - glyphSize.Height / 2;
            this._cellLocation = cellBounds.Location;
            this.checkBoxLocation = x;
            this.checkBoxSize = glyphSize;
            if (!this._checked)
            {
                this._cbState = CheckBoxState.UncheckedNormal;
            }
            else
            {
                this._cbState = CheckBoxState.CheckedNormal;
            }
            CheckBoxRenderer.DrawCheckBox(graphics, this.checkBoxLocation, this._cbState);
        }

        public event CheckBoxClickedHandler OnCheckBoxClicked;
    }
}