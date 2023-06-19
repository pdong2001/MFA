using Library.Common;

namespace Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text;
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng điền tài khoản và mật khẩu để đăng nhập!", "Thông báo");
                return;
            }
            var loginResult = Https.Login(userName, password).Result;
            if (loginResult)
            {
                var frm = new frmMFA(userName, password);
                Hide();
                frm.ShowDialog();
                ShowDialog();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Thông báo");
                txtPassword.Text = "";
            }
        }
    }
}