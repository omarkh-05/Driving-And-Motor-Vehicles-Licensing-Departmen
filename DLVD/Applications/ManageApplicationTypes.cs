using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppTypeBussiness;

namespace DLVD.Applications
{
    public partial class ManageApplications : Form
    {
        public ManageApplications()
        {
            InitializeComponent();
        }

        private void _FillAppTypesData()
        {
            dgvApplicationTypes.DataSource = ApplicationTypeBussiness.GetAllAppTypes();
            lblRecordsCount.Text = dgvApplicationTypes.RowCount.ToString();
        }

        private void ManageApplications_Load(object sender, EventArgs e)
        {
            _FillAppTypesData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationType editApplicationType = new EditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            editApplicationType.Show();
        }
    }
}
