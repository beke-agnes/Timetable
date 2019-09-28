using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Timetable.View
{
    public partial class Window : Form
    {
        private static readonly DayOfWeek[] DISPLAYED_DAYS = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        public Window(Model.Model model)
        {
            InitializeComponent();
            _model = model;

            dataGridView1.RowTemplate = new DataGridViewNumberedRow();

            var lst = new List<TimetableRow>();

            int tableHeight = _model.DayLength(_model.LongestDay());

            for (int i = 0; i < tableHeight; ++i)
            {
                lst.Add(new TimetableRow());
                foreach(var day in DISPLAYED_DAYS)
                {
                    lst[i][day] = _model[day, i];
                }
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lst;
        }

        private readonly Model.Model _model;
    }
}
