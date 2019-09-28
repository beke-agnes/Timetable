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
        public Window(Model.Model model)
        {
            InitializeComponent();
            _model = model;

            dataGridView1.RowTemplate = new DataGridViewNumberedRow();

            var lst = new List<TimetableRow>();
            foreach(var item in _model.GetTimetable().Days)
            {
                for(int i = 0; i < item.Value.Count; ++i)
                {
                    while (lst.Count <= i)
                    {
                        lst.Add(new TimetableRow());
                    }
                    lst[i][item.Key] = item.Value[i];
                }
            }

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lst;
        }

        private readonly Model.Model _model;
    }
}
