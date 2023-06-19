using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authenticator
{
    public partial class frmMain : Form
    {
        public string[] Tokens { get; set; }
        public frmMain()
        {
            InitializeComponent();
            if (!File.Exists("data")) File.Create("data");
            using var reader = new StreamReader("data");
            Tokens = reader.ReadToEnd().Split('\n', '\r', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var rowIndex = 0;
            foreach (var item in Tokens)
            {
                var frmCode = new ctrlCode();
                frmCode.Token = item;
                if (!string.IsNullOrEmpty(frmCode.UserName))
                {
                    frmCode.Visible = true;
                    frmCode.Dock = DockStyle.Top;
                    Controls.Add(frmCode);
                    rowIndex++;
                }
            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}
