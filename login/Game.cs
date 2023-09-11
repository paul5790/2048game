using login.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace login
{
    class Game
    {
        DataSet dataSet = new DataSet();
        SqlDB sqldb = new SqlDB();
        Gauge gauge = new Gauge();
        Random random = new Random();
        private enum GameState // 화면 상태
        {
            eGame,
            eAbout
        };
        public enum Direction // 방향키
        {
            kTOP,
            kRIGHT,
            kBOTTOM,
            kLEFT,
        };

        int[][] iBoard; // 게임판
        int iScore = 0, iBest, maxVal = 0, maxgauge = -30;
        int addNum = 2; // 추가되는 블럭 갯수

        // 이미지
        List<Image> orec = new List<Image>(); // 고정 이미지
        List<Bitmap> oBitmap = new List<Bitmap>(); // 동적 이미지
        Bitmap closebm = new Bitmap("close.png");

        // 폰트
        Font fFontS2 = new Font("Clear Sans", 10, FontStyle.Bold);
        Font fFontS = new Font("Clear Sans", 16, FontStyle.Bold);
        Font fFont = new Font("Clear Sans", 22, FontStyle.Bold);
        SolidBrush rankingbrush = new SolidBrush(Color.FromArgb(120, 110, 101));
        SizeF stringSize = new SizeF();

        public Boolean bRender = true; // ture면 새로운 화면을 그림
        Boolean gameOver = false;
        
        string usernow; // 현재 게임 접속자 id

        GameState currentGameState = GameState.eGame;// 게임 상태 초기화

        //생성자 함수
        //2차원배열, Bitmap객체, 버튼, 화면크기 객체를 초기화
        public Game(string usernow)
        {
            this.usernow = usernow;
            int bestScore = sqldb.Score_ReadData(dataSet, "UserInfo", usernow);
            this.iBest = bestScore;

            // 2차원 배열 [4][4] 할당
            this.iBoard = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                iBoard[i] = new int[4];
            }

            // bin 폴더 안에 이미지
            oBitmap.Add(new Bitmap("1.png"));
            oBitmap.Add(new Bitmap("2.png"));
            oBitmap.Add(new Bitmap("3.png"));
            oBitmap.Add(new Bitmap("4.png"));
            oBitmap.Add(new Bitmap("5.png"));
            oBitmap.Add(new Bitmap("6.png"));
            oBitmap.Add(new Bitmap("7.png"));
            oBitmap.Add(new Bitmap("8.png"));
            oBitmap.Add(new Bitmap("9.png"));
            oBitmap.Add(new Bitmap("k0.png"));
            oBitmap.Add(new Bitmap("10.png"));
            oBitmap.Add(new Bitmap("11.png"));
            oBitmap.Add(new Bitmap("12.png"));
            oBitmap.Add(new Bitmap("13.png"));
            oBitmap.Add(new Bitmap("14.png"));
            oBitmap.Add(new Bitmap("15.png"));
            oBitmap.Add(new Bitmap("16.png"));
            oBitmap.Add(new Bitmap("17.png"));
            oBitmap.Add(new Bitmap("18.png"));

            orec.Add(new Image(37, 18, 230, 130, 0)); // -- 왼쪽 사각형
            orec.Add(new Image(180, 18, 100, 66, 1)); // -- iScore
            orec.Add(new Image(298, 18, 100, 66, 1)); // -- iBest

            orec.Add(new Image(180, 96, 100, 38, 2, true)); // -- 새게임
            orec.Add(new Image(298, 96, 100, 38, 2, true)); // -- 랭킹
        }

        // 새로운 블록 생성
        public void Update()
        {
            // 게임 종료상태가 아닌데 addNum이 남아있으면
            while (!gameOver && addNum > 0)
            {
                int nX = random.Next(0, 4), nY = random.Next(0, 4); // 0~3 랜덤값

                if (iBoard[nX][nY] == 0) // 비어있는 공간에 랜덤값 생성
                {
                    iBoard[nX][nY] = random.Next(0, 15) == 0 ? 4 : 2; // 4블록은 15/1 확률
                    --addNum;
                }
            }
        }

        public void Draw(Graphics g)
        {
            switch (currentGameState)
            {
                case GameState.eGame: // 게임화면위에 게임오버화면
                    DrawGame(g);
                    if (gameOver)
                    {
                        GameOverDraw(g);
                    }

                    bRender = false;
                    break;
                case GameState.eAbout: // 게임화면위에 게임정보화면
                    DrawGame(g);
                    DrawRanking(g);
                    bRender = false;
                    break;
            }
        }

        // 게임화면 그리는 함수
        private void DrawGame(Graphics g)
        {
            g.Clear(Color.FromArgb(251, 248, 239));
            // 이미지만 그리는 함수
            for (int i = 0; i < orec.Count; i++)
            {
                orec[i].Draw(g, oBitmap[orec[i].getimgID()]);
            }
            
            DrawText(g, "점수", fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 230, 40);
            DrawText(g, iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(255, 241, 224)), 230, 65);

            DrawText(g, "최고점수", fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 348, 40);
            DrawText(g, iBest.ToString(), fFontS, new SolidBrush(Color.FromArgb(255, 241, 224)), 348, 65);

            DrawText(g, "새 게임", fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 230, 115);
            DrawText(g, "랭킹보기", fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 348, 115);

            g.DrawImage(oBitmap[3], new Point(37, 166));

            // 버튼과 게임판을 그려주고
            // iBoard는 현재 게임판의 상태가 저장
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {                                                 // iBoard배열의 각 값에 해당하는 이미지를 가져오기 위함(getBitmapID)
                    g.DrawImage(oBitmap[getBitmapID(iBoard[i][j])], new Point(49 + 87 * i, 178 + 87 * j)); // 뼈대와 각 블록 색깔들
                    if (iBoard[i][j] > 0) // 블록값이 0보다 클때만 그림으로 그려진다
                    {
                        DrawText(g, iBoard[i][j].ToString(), fFont, (new SolidBrush(Color.FromArgb(120, 110, 101))), 87 + 87 * i, 217 + 87 * j);
                    }
                }
            }
            maxVal = iBoard.SelectMany(row => row).Max();
            gauge.DrawGauge(g);

            switch (maxVal)
            {
                case 0:
                    break;
                case 2:
                    maxgauge = -10;
                    break;
                case 4:
                    maxgauge = 10;
                    break;
                case 8: 
                    maxgauge = 30;
                    break;
                case 16:
                    maxgauge = 50;
                    break;
                case 32:
                    maxgauge = 70;
                    break;
                case 64:
                    maxgauge = 90;
                    break;
                case 128:
                    maxgauge = 110;
                    break;
                case 256:
                    maxgauge = 130;
                    break;
                case 512:
                  maxgauge = 150;
                    break;
                case 1024:
                    maxgauge = 170;
                    break;
                case 2048:
                    maxgauge = 190;
                    break;
                case 4096:
                    maxgauge = 210;
                    break;
            }
            gauge.DrawArrow(g, maxgauge);
            DrawText(g, maxVal.ToString(), fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 99, 130);

        }

        // 게임오버 화면
        private void GameOverDraw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 251, 248, 239)), new Rectangle(0, 0, 484, 660));
            DrawText(g, "GAME OVER", fFontS, new SolidBrush(Color.FromArgb(164, 10, 10, 10)), 215, 250);
            DrawText(g, "SCORE: " + iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(164, 10, 10, 10)), 215, 282);
            orec[3].Draw(g, oBitmap[orec[3].getimgID()]);
            DrawText(g, "새 게임", fFontS2, new SolidBrush(Color.FromArgb(255, 241, 224)), 230, 115);
        }

        // 랭킹
        private void DrawRanking(Graphics g)
        {
            string[] Ranking = sqldb.GetAllScores(dataSet, "UserInfo");
            g.FillRectangle(new SolidBrush(Color.FromArgb(235, 251, 229, 214)), new Rectangle(55, 75, 324, 430));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(75, 145, 280, 50));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(75, 215, 280, 50));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(75, 285, 280, 50));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(75, 355, 280, 50));
            g.FillRectangle(new SolidBrush(Color.White), new Rectangle(75, 425, 280, 50));
            DrawText(g,"Ranking", fFont, rankingbrush, 215, 110);
            DrawText(g,"1.  " + Ranking[0].Trim(), fFontS, rankingbrush, 215, 170);
            DrawText(g,"2.  " + Ranking[1].Trim(), fFontS, rankingbrush, 215, 240);
            DrawText(g,"3.  " + Ranking[2].Trim(), fFontS, rankingbrush, 215, 310);
            DrawText(g,"4.  " + Ranking[3].Trim(), fFontS, rankingbrush, 215, 380);
            DrawText(g,"5.  " + Ranking[4].Trim(), fFontS, rankingbrush, 215, 450);
            g.DrawImage(closebm, new Point(335, 90));
        }

        // 텍스트 
        private void DrawText(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, int X, int Y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(X - stringSize.Width / 2, Y - stringSize.Height / 2));
        }

        // 방향키 움직이는 알고리즘
        // 합쳐지는 알고리즘
        public void MoveBoard(Direction nDirection)
        {
            Boolean bAdd = false;

            if (currentGameState == GameState.eAbout) currentGameState = GameState.eGame;

            switch (nDirection)
            {
                case Direction.kTOP:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (iBoard[i][k] == 0) // 아랫열의 점수가 0일 때
                                {
                                    continue;
                                }
                                else if (iBoard[i][k] == iBoard[i][j]) // 윗열과 아랫열의 점수가 같을 때
                                {
                                    iScore += iBoard[i][j]; // 점수 추가
                                    iBoard[i][j] *= 2;
                                    iBoard[i][k] = 0;
                                    bAdd = true; // 화면 새로고침
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0) // 윗열의 점수가 0이고 아랫열에 점수가 있을 때
                                    {
                                        iBoard[i][j] = iBoard[i][k]; // 윗열에 점수를 할당
                                        iBoard[i][k] = 0;
                                        j--;  // 같은 줄 한번더 확인
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.kRIGHT:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 3; i >= 1; i--)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iScore += iBoard[i][j];
                                    iBoard[i][j] *= 2;

                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.kBOTTOM:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j >= 1; j--)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (iBoard[i][k] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[i][k] == iBoard[i][j])
                                {
                                    iScore += iBoard[i][j];
                                    iBoard[i][j] *= 2;

                                    iBoard[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0)
                                    {
                                        iBoard[i][j] = iBoard[i][k];
                                        iBoard[i][k] = 0;
                                        j++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.kLEFT:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iScore += iBoard[i][j];

                                    iBoard[i][j] *= 2;
                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            // 최고 점수 현제점수에서 적용
            if (iScore > iBest)
            {
                iBest = iScore;
            }

            // bAdd는 이전 상태와 다른 상태를 만들었는지 여부를 나타내는 변수
            // 2,2,0,0이었던 한 행을 왼쪽으로 이동했을 때 4,0,0,0으로 변하면 bAdd는 true가 됩니다.
            if (bAdd)
            {
                ++addNum; // 빈공간 추가
            }

            /* ----- GAME OVER ----- */

            checkGameOver();
            bRender = true; // 새로운 화면을 그림
        }

        // 게임오버 상태인지 체크

        private void checkGameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // 아래, 위, 양옆에 붙어있는 숫자가 같은지 체크
                    if (i - 1 >= 0)
                    {
                        if (iBoard[i - 1][j] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (iBoard[i][j - 1] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (iBoard[i][j] == 0)
                    {
                        return;
                    }
                }
            }
            // 모든 조건에 부합하지 않으면
            gameOver = true;
            Score();
        }

        // 점수 저장
        public void Score()
        {
            sqldb.InsertData(dataSet, "UserInfo", usernow, iBest);

        }

        // [i][j]값이 0이면 4번 그림 리턴, 2이면 5번그림 리턴...
        private int getBitmapID(int iNum)
        {
            switch (iNum)
            {
                case 0:
                    return 4;
                case 2:
                    return 5;
                case 4:
                    return 6;
                case 8:
                    return 7;
                case 16:
                    return 8;
                case 32:
                    return 10;
                case 64:
                    return 11;
                case 128:
                    return 12;
                case 256:
                    return 13;
                case 512:
                    return 14;
                case 1024:
                    return 15;
                case 2048:
                    return 16;
                case 4096:
                case 8192:
                case 16384:
                    return 17;
            }

            return 4;
        }

        // MouseClick 이벤트의 좌표를 가져와 위치 비교
        public void checkButton(int nXPos, int nYPos) // 클릭이 되고
        {
            for (int i = 0; i < orec.Count; i++)
            {
                if (orec[i].getBtn()) // 버튼이 참이면
                {
                    // (이미지 x좌표 < 버튼 < x좌표 + 이미지 길이) && (이미지 y좌표 < 버튼 < y좌표 + 이미지 길이)
                    if (nXPos >= orec[i].getXpos() && nXPos <= orec[i].getXpos() + orec[i].getWidth() && nYPos >= orec[i].getYPos() && nYPos <= orec[i].getYPos() + orec[i].getHeight())
                    {
                        actionButton(i);
                    }
                }

            }
        }


        // 버튼 클릭 이벤트랑 똑같음
        private void actionButton(int iButtonID)
        {
            switch (iButtonID)
            {
                case 3: // 새 게임 버튼
                    Score();
                    resetGameData();
                    break;
                case 4: // 랭킹보기 & 닫기
                    if (currentGameState == GameState.eGame) currentGameState = GameState.eAbout;
                    else currentGameState = GameState.eGame;
                    break;
            }
            bRender = true;
        }

        // 새 게임 시작
        private void resetGameData()
        { 
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.iBoard[i][j] = 0; // 16개행 전부 초기화
                }
            }
            this.addNum = 2;
            this.iScore = 0;
            this.gameOver = false;
            this.currentGameState = GameState.eGame;
            this.bRender = true; // 화면 새로고침
        }
    }
}