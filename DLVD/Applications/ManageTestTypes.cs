using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestTypesBussiness;

namespace DLVD.Applications
{
    public partial class ManageTestTypes: Form
    {
        public ManageTestTypes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestType editTestType = new EditTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            editTestType.Show();
        }

        private void _FillTestTypesData()
        {
            dataGridView1.DataSource = TestTypeBussiness.GetAllTestTypes();
        }


        private void ManageTestTypes_Load(object sender, EventArgs e)
        {
            _FillTestTypesData();
        }
    }
}
