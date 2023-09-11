using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MonitoringSystem.Panels
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // RoundedPanel 클래스
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    partial class RoundedPanel : System.Windows.Forms.Panel
    {
        #region 변수 정의
        [Browsable(true)]
        public Color FillColor
        {
            get
            {
                return mFillColor;
            }
            set
            {
                mFillColor = value;
                mFillColorChanged(this, EventArgs.Empty);
            }
        }
        [Browsable(true)]
        public Color BorderColor { get; set; } = Color.Black;
        [Browsable(true)]
        public Color ShadowColor { get; set; } = Color.Black;
        [Browsable(true)]
        public bool IsBorder { get; set; } = false;
        [Browsable(true)]
        public int BorderLineWidth { get; set; } = 1;
        [Browsable(true)]
        public int Radius { get; set; } = 10;
        [Browsable(true)]
        public bool IsShadow { get; set; } = true;

        private EventHandler mFillColorChanged;

        private Color mFillColor = SystemColors.ControlDark;

        private int mShadowTickness = 3;
        #endregion 변수 정의

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 생성자
        public RoundedPanel()
        {
            this.DoubleBuffered = true;

            mFillColorChanged += new EventHandler(FillColorChanged);

            // UI 감빡이는 현상 제거
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
        }

        #region 이벤트 함수
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mFillColorChanged -= new EventHandler(FillColorChanged);
            }

            base.Dispose(disposing);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 이벤트 - 그리기
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e); // 파생클래스에서 Windows 배경 지우기를 요청할 수 있음

            using (BufferedGraphics bufferedgraphic = BufferedGraphicsManager.Current.Allocate(e.Graphics, this.ClientRectangle))
            {
                bufferedgraphic.Graphics.Clear(this.BackColor);

                Graphics graphics = bufferedgraphic.Graphics;



                //if (IsShadow)
                //{
                //    using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, ShadowColor)))
                //    {
                //        graphics.FillRoundedRectangle(brush, new RectangleF(new PointF(mShadowTickness, mShadowTickness),
                //                                                            new SizeF(this.Width - mShadowTickness - 1.0F,
                //                                                                      this.Height - mShadowTickness - 1.0F)), Radius);
                //    }
                //}
                // fillColor 속성의 색상값 받기
                using (SolidBrush fillBrush = new SolidBrush(FillColor))
                {
                    if (IsShadow)
                    {
                        graphics.FillRoundedRectangle(fillBrush, new RectangleF(new PointF(0.0F, 0.0F),
                                                                                new SizeF(this.Width - mShadowTickness - 1.0F, this.Height - mShadowTickness - 1.0F)), Radius);
                    }
                    else
                    {
                        graphics.FillRoundedRectangle(fillBrush, new RectangleF(new PointF(0.0F, 0.0F),
                                                                                new SizeF(this.Width - 1.0F, this.Height - 1.0F)), Radius);
                    }
                }

                // Border 속성
                if (IsBorder)
                {
                    using (Pen pen = new Pen(BorderColor, BorderLineWidth))
                    {
                        if (IsShadow)
                        {
                            graphics.DrawRoundedRectangle(pen, new RectangleF(new PointF(0.0F, 0.0F),
                                                                              new SizeF(this.Width - mShadowTickness - 1.0F, this.Height - mShadowTickness - 1.0F)), Radius);
                        }
                        else
                        {
                            graphics.DrawRoundedRectangle(pen, new RectangleF(new PointF(0.0F, 0.0F),
                                                                              new SizeF(this.Width - 1.0F, this.Height - 1.0F)), Radius);
                        }
                    }
                }

                bufferedgraphic.Render(e.Graphics);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 
        private void FillColorChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
        #endregion 이벤트 함수
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // GraphicsExtension 클래스
    static class GraphicsExtension
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GenerateRoundedRectangle
        private static GraphicsPath GenerateRoundedRectangle(this Graphics graphics, RectangleF rectangle, float radius)
        {
            GraphicsPath graphicPath = new GraphicsPath();

            float diameter = 0.0F;

            if (radius <= 0.0F)
            {
                graphicPath.AddRectangle(rectangle);
                graphicPath.CloseFigure();
            }
            else
            {
                if (radius >= (Math.Min(rectangle.Width, rectangle.Height)) / 2.0F)
                {
                    return graphics.GenerateCapsule(rectangle);
                }

                diameter = radius * 2.0F;
                SizeF size = new SizeF(diameter, diameter);
                RectangleF recArc = new RectangleF(rectangle.Location, size);
                graphicPath.AddArc(recArc, 180.0F, 90.0F);// 왼쪽위 호를 만듬

                recArc.X = rectangle.Right - diameter; // 기준점을 오른쪽 - 지름 으로 이동
                graphicPath.AddArc(recArc, 270.0F, 90.0F); // 오른쪽위 호를 만듬
                
                recArc.Y = rectangle.Bottom - diameter; // 기준점 아래에서 지름뺀거로 잡음
                graphicPath.AddArc(recArc, 0.0F, 90.0F); // 호 생성
                
                recArc.X = rectangle.Left; // 기준점 왼쪽으로 이동
                graphicPath.AddArc(recArc, 90.0F, 90.0F); // 호 생성
                
                graphicPath.CloseFigure();
            }
            return graphicPath;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // GenerateCapsule
        private static GraphicsPath GenerateCapsule(this Graphics graphics, RectangleF rectangle)
        {
            GraphicsPath graphicPath = new GraphicsPath();

            float diameter = 0.0F;
            RectangleF rectArc = new RectangleF();

            try
            {
                // 가로가 크면 양 끝에 height 지름인 반원 추가
                if (rectangle.Width > rectangle.Height)
                {
                    diameter = rectangle.Height;
                    SizeF size = new SizeF(diameter, diameter);
                    rectArc = new RectangleF(rectangle.Location, size);
                    graphicPath.AddArc(rectArc, 90.0F, 180.0F);
                    rectArc.X = rectangle.Right - diameter;
                    graphicPath.AddArc(rectArc, 270.0F, 180.0F);
                }
                // 새로가 길면 양 위에 width 지름인 반원 추가
                else if (rectangle.Width < rectangle.Height)
                {
                    diameter = rectangle.Width;
                    SizeF size = new SizeF(diameter, diameter);
                    rectArc = new RectangleF(rectangle.Location, size);
                    graphicPath.AddArc(rectArc, 180.0F, 180.0F);
                    rectArc.Y = rectangle.Bottom - diameter;
                    graphicPath.AddArc(rectArc, 0.0F, 180.0F);
                }
                else // 똑같으면 원만들어라
                {
                    graphicPath.AddEllipse(rectangle);
                }
            }
            catch (Exception)
            {
                graphicPath.AddEllipse(rectangle);
            }
            finally
            {
                graphicPath.CloseFigure();
            }

            return graphicPath;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DrawRoundedRectangle
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, RectangleF rectangle, float radius)
        {
            GraphicsPath graphicsPath = graphics.GenerateRoundedRectangle(rectangle, radius);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(pen, graphicsPath);
            graphics.SmoothingMode = old;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // DrawRoundedRectangle
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rectangle, int radius)
        {
            graphics.DrawRoundedRectangle(pen, rectangle, Convert.ToSingle(radius));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // FillRoundedRectangle
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, RectangleF rectangle, float radius)
        {
            GraphicsPath graphicPath = graphics.GenerateRoundedRectangle(rectangle, radius);
            SmoothingMode old = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, graphicPath);
            graphics.SmoothingMode = old;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // FillRoundedRectangle
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rectangle, int radius)
        {
            graphics.FillRoundedRectangle(brush, rectangle, Convert.ToSingle(radius));
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}