using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timetable.View
{
    class DataGridViewNumberedRow : DataGridViewRow
    {
        public DataGridViewNumberedRow()
        {
        }

        protected override void PaintHeader(Graphics graphics, 
            Rectangle clipBounds, Rectangle rowBounds, int rowIndex, 
            DataGridViewElementStates rowState, bool isFirstDisplayedRow, 
            bool isLastVisibleRow, DataGridViewPaintParts paintParts)
        {
            base.PaintHeader(graphics, clipBounds, rowBounds, 
                rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow,
                paintParts);

            graphics.DrawString(Index.ToString(), SystemFonts.MenuFont, 
                Brushes.Black, rowBounds);
        }
    }
}
