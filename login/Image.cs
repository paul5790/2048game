using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login
{
    internal class Image
    {
        private int iXPos, iYPos, iWidth, iHeight, imgID;
        private bool btn = false;

        // 부동 이미지
        public Image(int iXPos, int iYPos, int iWidth, int iHeight, int imgID)
        {
            this.iXPos = iXPos;
            this.iYPos = iYPos;
            this.iWidth = iWidth;
            this.iHeight = iHeight;
            this.imgID = imgID; // 이미지 이름
        }

        // 버튼 이미지
        public Image(int iXPos, int iYPos, int iWidth, int iHeight, int imgID, bool btn)
        {
            this.iXPos = iXPos;
            this.iYPos = iYPos;
            this.iWidth = iWidth;
            this.iHeight = iHeight;
            this.imgID = imgID;
            this.btn = btn;

        }

        public void Draw(Graphics g, Bitmap oB)
        {
            g.DrawImage(oB, new Point(iXPos, iYPos));
        }

        public int getimgID()
        {
            return imgID;
        }

        public int getXpos()
        {
            return iXPos;
        }

        public int getYPos()
        {
            return iYPos;
        }

        public int getWidth()
        {
            return iWidth;
        }

        public int getHeight()
        {
            return iHeight;
        }

        public bool getBtn()
        {
            return btn;
        }
    }
}
