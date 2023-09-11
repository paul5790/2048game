using login.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    
    public partial class Main_Form : Form
    {
        Login_Form login_form;
        Signup_Form signup_form;
        Game_Form gameForm;

        public Main_Form()
        {
            InitializeComponent();
            login_form = new Login_Form();
            signup_form = new Signup_Form();
        }

        public void ShowGameForm(string username)
        {
            gameForm = new Game_Form(username, this);
            this.Hide();
            gameForm.ShowDialog();
        }
        
        // 처음에는 uPanel을 login_form으로 지정
        private void Main_Form_Load(object sender, EventArgs e)
        {
            uPanel.Controls.Add(login_form); //Panel
        }

        public void select_Form(int num)
        {
            if (num == 1)
            {
                uPanel.Controls.Clear();
                uPanel.Controls.Add(login_form);
            }
            else if (num == 2)
            {
                uPanel.Controls.Clear();
                uPanel.Controls.Add(signup_form);
            }
            else { }
        }
    }
}
