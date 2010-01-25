using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

namespace IrcCloneShark
{
    public class ColorTable : List<Color>
    {
        private static Regex ColorTableRegex = new Regex(@"\{\\colortbl;((?:\\(?:(red|green|blue|cshade|ctint)(\d*)|(caccentone)))*;[^\\]*)*\}");

        public static ColorTable mIRCDefaultColorTable
        {
            get
            {
                ColorTable c = new ColorTable();
                c.Add(Color.White);
                c.Add(Color.Black);
                c.Add(Color.DarkBlue);
                c.Add(Color.DarkGreen);
                c.Add(Color.Red);
                c.Add(Color.Brown);
                c.Add(Color.Violet);
                c.Add(Color.Orange);
                c.Add(Color.Yellow);
                c.Add(Color.Green);
                c.Add(Color.Turquoise);
                c.Add(Color.LightBlue);
                c.Add(Color.Blue);
                c.Add(Color.Pink);
                c.Add(Color.DarkGray);
                c.Add(Color.Gray);
                return c;
            }
        }

        public String ToRtfColorTable()
        {
            StringBuilder result = new StringBuilder();
            result.Append(@"{\colortbl ;");
            foreach (Color c in this)
            {
                result.AppendFormat(@"\red{0}\green{1}\blue{2};", c.R, c.G, c.B);
            }
            result.Append('}');
            return result.ToString();
        }

        public String ToRtf(String Rtf)
        {

            // Search for colour table info. If it exists (it shouldn't,
            // but we'll check anyway) remove it and replace with our one
            int iCTableStart = Rtf.IndexOf(@"{\colortbl ;");


            if (iCTableStart != -1) //then colortbl exists
            {
                //find end of colortbl tab by searching
                //forward from the colortbl tab itself
                int iCTableEnd = Rtf.IndexOf('}', iCTableStart) + 1;

                //remove the existing colour table
                Rtf = Rtf.Remove(iCTableStart, iCTableEnd - iCTableStart);

                //now insert new colour table at index of old colortbl tag
                Rtf = Rtf.Insert(iCTableStart, ToRtfColorTable());
            }

            //colour table doesn't exist yet, so let's make one
            else
            {
                // find index of start of header
                int iRTFLoc = Rtf.IndexOf("\\rtf");
                // get index of where we'll insert the colour table
                // try finding opening bracket of first property of header first                
                int iInsertLoc = Rtf.IndexOf("{", iRTFLoc);

                // if there is no property, we'll insert colour table
                // just before the end bracket of the header
                if (iInsertLoc == -1) iInsertLoc = Rtf.IndexOf(' ', iRTFLoc);

                // insert the colour table at our chosen location                
                Rtf = Rtf.Insert(iInsertLoc, Environment.NewLine + ToRtfColorTable());
            }
            return Rtf;
        }

        /*public static ColorTable FromRtfColorTable()
        {
        }*/
    }
}
