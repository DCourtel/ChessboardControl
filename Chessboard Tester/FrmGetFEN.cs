using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessboardTester
{
    public partial class FrmGetFEN : Form
    {
        public FrmGetFEN()
        {
            InitializeComponent();
            TxtBxFENString.Focus();
        }

        public string FENString { get { return TxtBxFENString.Text; } }

        private void TxtBxFENString_TextChanged(object sender, EventArgs e)
        {
            BtnOk.Enabled = !string.IsNullOrWhiteSpace(TxtBxFENString.Text);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
