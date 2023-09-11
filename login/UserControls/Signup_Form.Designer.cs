
namespace login.UserControls
{
    partial class Signup_Form
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.useridtx = new System.Windows.Forms.Label();
            this.uidtb = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.passwordtx = new System.Windows.Forms.Label();
            this.pwdtb = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.re_enterpw = new System.Windows.Forms.Label();
            this.rpwdtb = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.nametx = new System.Windows.Forms.Label();
            this.nametb = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.signupbtn = new System.Windows.Forms.Button();
            this.DoubleCheckbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // useridtx
            // 
            this.useridtx.AutoSize = true;
            this.useridtx.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useridtx.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.useridtx.Location = new System.Drawing.Point(116, 101);
            this.useridtx.Name = "useridtx";
            this.useridtx.Size = new System.Drawing.Size(47, 15);
            this.useridtx.TabIndex = 19;
            this.useridtx.Text = "User ID";
            // 
            // uidtb
            // 
            this.uidtb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.uidtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uidtb.Font = new System.Drawing.Font("Calibri Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uidtb.Location = new System.Drawing.Point(117, 127);
            this.uidtb.Name = "uidtb";
            this.uidtb.Size = new System.Drawing.Size(160, 23);
            this.uidtb.TabIndex = 18;
            this.uidtb.TextChanged += new System.EventHandler(this.uidtb_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.Location = new System.Drawing.Point(117, 158);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(160, 1);
            this.panel1.TabIndex = 17;
            // 
            // passwordtx
            // 
            this.passwordtx.AutoSize = true;
            this.passwordtx.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordtx.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.passwordtx.Location = new System.Drawing.Point(116, 188);
            this.passwordtx.Name = "passwordtx";
            this.passwordtx.Size = new System.Drawing.Size(99, 15);
            this.passwordtx.TabIndex = 22;
            this.passwordtx.Text = "Password(숫자만)";
            // 
            // pwdtb
            // 
            this.pwdtb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.pwdtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwdtb.Font = new System.Drawing.Font("Calibri Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwdtb.Location = new System.Drawing.Point(117, 214);
            this.pwdtb.Name = "pwdtb";
            this.pwdtb.PasswordChar = '*';
            this.pwdtb.Size = new System.Drawing.Size(220, 23);
            this.pwdtb.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel2.Location = new System.Drawing.Point(117, 245);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(220, 1);
            this.panel2.TabIndex = 20;
            // 
            // re_enterpw
            // 
            this.re_enterpw.AutoSize = true;
            this.re_enterpw.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.re_enterpw.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.re_enterpw.Location = new System.Drawing.Point(116, 275);
            this.re_enterpw.Name = "re_enterpw";
            this.re_enterpw.Size = new System.Drawing.Size(111, 15);
            this.re_enterpw.TabIndex = 25;
            this.re_enterpw.Text = "Re_enter Password";
            // 
            // rpwdtb
            // 
            this.rpwdtb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.rpwdtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rpwdtb.Font = new System.Drawing.Font("Calibri Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rpwdtb.Location = new System.Drawing.Point(117, 301);
            this.rpwdtb.Name = "rpwdtb";
            this.rpwdtb.PasswordChar = '*';
            this.rpwdtb.Size = new System.Drawing.Size(220, 23);
            this.rpwdtb.TabIndex = 24;
            this.rpwdtb.TextChanged += new System.EventHandler(this.rpwdtb_TextChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel3.Location = new System.Drawing.Point(117, 332);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(220, 1);
            this.panel3.TabIndex = 23;
            // 
            // nametx
            // 
            this.nametx.AutoSize = true;
            this.nametx.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametx.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.nametx.Location = new System.Drawing.Point(116, 362);
            this.nametx.Name = "nametx";
            this.nametx.Size = new System.Drawing.Size(38, 15);
            this.nametx.TabIndex = 28;
            this.nametx.Text = "Name";
            // 
            // nametb
            // 
            this.nametb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.nametb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nametb.Font = new System.Drawing.Font("Calibri Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nametb.Location = new System.Drawing.Point(117, 388);
            this.nametb.Name = "nametb";
            this.nametb.Size = new System.Drawing.Size(220, 23);
            this.nametb.TabIndex = 27;
            this.nametb.TextChanged += new System.EventHandler(this.nametb_TextChanged);
            this.nametb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nametb_KeyDown);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel4.Location = new System.Drawing.Point(117, 419);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(220, 1);
            this.panel4.TabIndex = 26;
            // 
            // signupbtn
            // 
            this.signupbtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.signupbtn.Enabled = false;
            this.signupbtn.FlatAppearance.BorderSize = 0;
            this.signupbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signupbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.signupbtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.signupbtn.Location = new System.Drawing.Point(147, 449);
            this.signupbtn.Name = "signupbtn";
            this.signupbtn.Size = new System.Drawing.Size(160, 40);
            this.signupbtn.TabIndex = 29;
            this.signupbtn.Text = "회원가입";
            this.signupbtn.UseVisualStyleBackColor = false;
            this.signupbtn.Click += new System.EventHandler(this.signupbtn_Click);
            // 
            // DoubleCheckbtn
            // 
            this.DoubleCheckbtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.DoubleCheckbtn.FlatAppearance.BorderSize = 0;
            this.DoubleCheckbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoubleCheckbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoubleCheckbtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DoubleCheckbtn.Location = new System.Drawing.Point(278, 127);
            this.DoubleCheckbtn.Name = "DoubleCheckbtn";
            this.DoubleCheckbtn.Size = new System.Drawing.Size(59, 32);
            this.DoubleCheckbtn.TabIndex = 30;
            this.DoubleCheckbtn.Text = "중복확인";
            this.DoubleCheckbtn.UseVisualStyleBackColor = false;
            this.DoubleCheckbtn.Click += new System.EventHandler(this.DoubleCheckbtn_Click);
            // 
            // Signup_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
            this.Controls.Add(this.DoubleCheckbtn);
            this.Controls.Add(this.signupbtn);
            this.Controls.Add(this.nametx);
            this.Controls.Add(this.nametb);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.re_enterpw);
            this.Controls.Add(this.rpwdtb);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.passwordtx);
            this.Controls.Add(this.pwdtb);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.useridtx);
            this.Controls.Add(this.uidtb);
            this.Controls.Add(this.panel1);
            this.Name = "Signup_Form";
            this.Size = new System.Drawing.Size(454, 550);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Signup_Form_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label useridtx;
        private System.Windows.Forms.TextBox uidtb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label passwordtx;
        private System.Windows.Forms.TextBox pwdtb;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label re_enterpw;
        private System.Windows.Forms.TextBox rpwdtb;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label nametx;
        private System.Windows.Forms.TextBox nametb;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button signupbtn;
        private System.Windows.Forms.Button DoubleCheckbtn;
    }
}
