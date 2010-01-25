using System;
using System.Collections.Generic;
using System.Text;

namespace IrcCloneShark
{
    public class IrcColor
    {
        private int BackgroundValue;
        private int ForegroundValue;

        public IrcColor(int fg, int bg)
        {
            ForegroundValue = fg;
            BackgroundValue = bg;
        }

        public int Background
        {
            get { return BackgroundValue; }
            set { BackgroundValue = value; }
        }

        public int Foreground
        {
            get { return ForegroundValue; }
            set { ForegroundValue = value; }
        }
    }
}
