using login.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace login
{
    public partial class Game_Form : Form
    {
        Gauge gauge = new Gauge();
        Game game;
        Graphics g, G;
        Bitmap background;
        Main_Form main_form;

        public string usernow; // 현재 접속자

        public Game_Form(string username, Main_Form main_form)
        {
            InitializeComponent();
            background = new Bitmap(450, 589); // 현재 폼의 배경이미지를 비트맵 객체로 저장
            G = Graphics.FromImage(background);

            this.usernow = username;
            this.main_form = main_form;
            game = new Game(usernow);
        }
        private void UpdateGame()
        {
            game.Update();
        }

        private void Draw(Graphics g)
        {
            game.Draw(g);
        }

        private void Game_Form_Paint(object sender, PaintEventArgs e)
        {
            // 초기화
            e.Graphics.Clear(Color.FromArgb(251, 248, 239));
            UpdateGame();
            game.Draw(e.Graphics);
            e.Graphics.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            if (game.bRender)
            {
                // 다시 배경을 덮고
                Draw(G);
                // 새로운 화면을 그림
                this.CreateGraphics().DrawImage(background, new Point(0, 0));
            }
            
        }

        private void Game_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Score();
            this.main_form.Close();
            this.main_form = null; // 메모리 해제
        }  

        private void Game_Form_MouseClick(object sender, MouseEventArgs e)
        {
            game.checkButton(e.X, e.Y);
        }

        private void Game_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                game.MoveBoard(Game.Direction.kLEFT);
            }
            else if (e.KeyCode == Keys.Up)
            {
                game.MoveBoard(Game.Direction.kTOP);
            }
            else if (e.KeyCode == Keys.Right)
            {
                game.MoveBoard(Game.Direction.kRIGHT);
            }
            else if (e.KeyCode == Keys.Down)
            {
                game.MoveBoard(Game.Direction.kBOTTOM);
            }
            gauge.Invalidate(); // 게이지컨트롤 다시그리기
        }
    }
}
