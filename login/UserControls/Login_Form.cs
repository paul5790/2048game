using System;
using System.Data;
using System.Windows.Forms;

namespace login.UserControls
{
    public partial class Login_Form : UserControl
    {
        SqlDB sqlDB = new SqlDB();
        DataSet dataSet = new DataSet();
        private string username;
   
        public Login_Form()
        {
            InitializeComponent();
            sqlDB = new SqlDB();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            string uid = uidtextbox.Text;
            string pwdText = pwdtextbox.Text;

            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(pwdText))
            {
                MessageBox.Show("아이디 혹은 비밀번호를 입력해주세요.");
                return;
            }

            if (!int.TryParse(pwdText, out int pwd))
            {
                MessageBox.Show("비밀번호는 숫자만 입력해주세요.");
                return;
            }

            string dpwd = sqlDB.Login_ReadData(dataSet, "UserInfo", uid);

            if (dpwd == null)
            {
                MessageBox.Show("아이디를 찾을 수 없습니다.");
            }
            else if (pwd != int.Parse(dpwd))
            {
                MessageBox.Show("비밀번호가 틀렸습니다.");
            }
            else
            {
                username = uidtextbox.Text;
                Main_Form mainForm = (Main_Form)this.FindForm();
                mainForm.ShowGameForm(username);
            }
        }

        // 회원가입창 이동
        private void signupbtn_Click(object sender, EventArgs e)
        {
            Move move = new Move(2);
        }

        // 엔터키 로그인
        private void uidtextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginbtn_Click(sender, e);
            }
        }
        private void pwdtextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginbtn_Click(sender, e);
            }
        }
    }
}
