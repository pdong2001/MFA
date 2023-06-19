using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class frmMFA : Form
    {
        public frmMFA(string userName, string password)
        {
            InitializeComponent();
            UserName = userName;
            Password = password;
        }

        public string UserName { get; }
        public string Password { get; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            Https.VerifyLogin(UserName, Password, textBox1.Text).ContinueWith(t =>
            {
                var response = t.Result;
                if (response == null || !response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Mã xác nhận không chính xác!", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                }

            });
        }
    }
}
