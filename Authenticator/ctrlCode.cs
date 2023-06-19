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

namespace Authenticator
{
    public partial class ctrlCode : UserControl
    {
        public string? UserName { get; set; }
        private string? _token;
        public string? Token
        {
            get => _token; set
            {
                _token = value;
                if (_token != null)
                {
                    var userName = Https.VerifyToken(_token).Result;
                    UserName = userName!;
                    if (string.IsNullOrEmpty(UserName)) { Dispose(); }
                    lblUserName.Text = "Người dùng : " + userName;
                }
            }
        }
        public DateTime? Expire { get; set; }
        public ctrlCode()
        {
            InitializeComponent();
        }

        private void ctrlCode_Load(object sender, EventArgs e)
        {
            RefreshCode();
        }

        public void RefreshCode()
        {
            if (Token != null)
                Https.GenerateCode(Token).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        textBox1.Text = t.Result?.Code;
                        Expire = t.Result?.CodeExpireTime;
                        tmrRefresh.Enabled = true;
                    });
                });
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (Expire.HasValue)
            {
                if (Expire <= DateTime.UtcNow) RefreshCode();
                else progressBar1.Value = (int)((Expire.Value - DateTime.UtcNow).TotalSeconds / 60 * 100);
            }
        }
    }
}
