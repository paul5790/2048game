using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace login.UserControls
{
    public partial class Signup_Form : UserControl
    {
        SqlDB sqldb;
        DataSet dataSet;
        bool bid, brpw, bname = false; // 3가지 조건

        public Signup_Form()
        {
            InitializeComponent();
            dataSet = new DataSet();
            sqldb = new SqlDB();
        }

        // 모든 조건이 완성되고 마우스를 움직였을 때
        private void Signup_Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (bid && brpw && bname)
            {
                signupbtn.Enabled = true;
            }
        }

        private void signupbtn_Click(object sender, EventArgs e)
        {
            string newid = uidtb.Text;
            string newpw = pwdtb.Text;
            string name = nametb.Text;

            sqldb.InsertData(dataSet, "UserInfo", newid, newpw, name); // 회원정보 저장
            MessageBox.Show("회원가입 완료");

            uidtb.Text = "";
            pwdtb.Text = "";
            rpwdtb.Text = "";
            nametb.Text = "";
            Move move = new Move(1); // 로그인 창으로 이동
        }

        private void DoubleCheckbtn_Click(object sender, EventArgs e)
        {
            string uid = uidtb.Text.Trim(); // 공백 제거
            if (uid != "")
            {
                string cid = sqldb.Check_ReadData(dataSet, "UserInfo", uid); // 같은 아이디 찾아서 상태저장
                if (cid == "possible")
                {
                    MessageBox.Show("중복된 아이디가 없습니다.");
                    bid = true;
                    useridtx.ForeColor = Color.Black;
                }
                else
                {
                    MessageBox.Show("아이디가 존재합니다.");
                    bid = false;
                    useridtx.ForeColor = Color.Gray;
                }
            }
            else
            {
                MessageBox.Show("아이디를 입력해주세요.");
                bid = false;
                useridtx.ForeColor = Color.Gray;
            }
        }

        // name 칸에서 엔터키를 눌렀을 때
        private void nametb_KeyDown(object sender, KeyEventArgs e)
        {
            if (bid && brpw && bname)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    signupbtn_Click(sender, e);
                }
            }
        }

        // 중복확인 후 다른 아이디 사용할 경우
        private void uidtb_TextChanged(object sender, EventArgs e)
        {
            // 글자를 치거나 비어있을 때
            signupbtn.Enabled = string.IsNullOrWhiteSpace(uidtb.Text) && uidtb.Text.Trim() != "";
            bid = false;
        }

        private void rpwdtb_TextChanged(object sender, EventArgs e)
        {
            if(pwdtb.Text != rpwdtb.Text)
            {
                re_enterpw.Text = "Re_enter Password(not correct)";
                re_enterpw.ForeColor = Color.Red;
                brpw = false;
            }
            else
            {
                re_enterpw.Text = "Re_enter Password(correct)";
                re_enterpw.ForeColor = Color.Green;
                passwordtx.ForeColor = Color.Black;
                brpw = true;
            }
        }

        private void nametb_TextChanged(object sender, EventArgs e)
        {
            if (nametb.Text.Trim() != "")
            {
                nametx.ForeColor = Color.Black;
                bname = true;
            }
            else
            {
                nametx.ForeColor = Color.Gray;
                bname = false;
            }
        }
    }
}
