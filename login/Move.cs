using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public class Move
    {
        public Move(int num)
        {
            // Main_Form을 찾아 변수에 객체 할당
            Form mForm = Application.OpenForms["Main_Form"];
            if (mForm is Main_Form mainForm)
            {
                mainForm.select_Form(num); // select_Form() 함수 호출
            }
        }
    }
}
