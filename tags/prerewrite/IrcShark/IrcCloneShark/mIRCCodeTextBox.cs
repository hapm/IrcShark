using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IrcCloneShark
{
    public partial class mIRCCodeTextBox : RichTextBox
    {
        private int DefaultForegroundColorValue;
        private int DefaultBackgroundColorValue;
        private ColorTable ColorTableValue;

        public mIRCCodeTextBox()
        {
            InitializeComponent();
            ColorTableValue = ColorTable.mIRCDefaultColorTable;
            DefaultBackgroundColorValue = 1;
            DefaultForegroundColorValue = 2;
        }

        public mIRCCodeTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            ColorTableValue = ColorTable.mIRCDefaultColorTable;
            DefaultBackgroundColorValue = 1;
            DefaultForegroundColorValue = 2;
        }
        public override string SelectedText
        {
            get
            {
                return base.SelectedText;
            }
            set
            {
                int selStart = SelectionStart;
                base.SelectedText = value;
                Select(selStart, value.Length);
                String rtf = base.SelectedRtf;
                rtf = mIRCCodeToRTF(rtf);
                rtf = ColorTable.mIRCDefaultColorTable.ToRtf(rtf);
                base.SelectedRtf = rtf;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {

                base.Text = value;
                /*String rtf = base.Rtf;
                rtf = mIRCCodeToRTF(rtf);
                rtf = ColorTable.mIRCDefaultColorTable.ToRtf(rtf);
                base.Rtf = rtf;*/
            }
        }

        private String mIRCCodeToRTF(String Line)
        {
            StringBuilder result = new StringBuilder();
            bool BoldSet = false;
            bool UnderlineSet = false;
            int currentBG = DefaultBackgroundColor;
            int currentFG = DefaultForegroundColor;
            int newFG;
            int newBG;
            Regex ColorCode = new Regex(@"(?<=[^\\])\\'..");
            Line = ColorCode.Replace(Line, mIRCCodeRTFReplace);

            for (int i = 0; i < Line.Length; i++)
            {
                switch (Line[i])
                {
                    case '\x02':
                        if (BoldSet)
                            result.Append(@"\b0 ");
                        else
                            result.Append(@"\b ");
                        BoldSet = !BoldSet;
                        break;

                    case '\x03':
                        i++;
                        if (i >= Line.Length) break;
                        newFG = Line[i] - 48;
                        if (newFG >= 0 && newFG <= 9)
                        {
                            i++;
                            if (i >= Line.Length) break;
                            if ((newFG == 0 && (Line[i] - 48 >= 1 && Line[i] - 48 <= 9))
                              || (newFG == 1 && (Line[i] - 48 >= 0 && Line[i] - 48 <= 5)))
                            {
                                newFG = newFG * 10 + Line[i] - 48;
                                i++;
                            }
                            if (i >= Line.Length) break;
                            newFG++;
                            result.AppendFormat(@"\cf{0} ", newFG);
                            currentFG = newFG;
                            if (Line[i] == ',')
                            {
                                i++;
                                if (i >= Line.Length)
                                {
                                    result.Append(',');
                                    break;
                                }
                                newBG = Line[i] - 48;
                                if (newBG >= 0 && newBG <= 9)
                                {
                                    i++;
                                    if (i >= Line.Length) break;
                                    if (Line[i] - 48 >= 0 && Line[i] - 48 <= 5)
                                    {
                                        newBG = newBG * 10 + Line[i] - 48;
                                        i++;
                                    }
                                    if (i >= Line.Length) break;
                                    newBG++;
                                    result.AppendFormat(@"\highlight{0} ", newBG);
                                    currentBG = newBG;
                                    i++;
                                    if (i >= Line.Length) break;
                                }
                                i--;
                            }
                            i--;
                        }
                        else
                        {
                            //hier Farbe zurücksetzen da keine Farbe angegeben
                            if (currentFG != DefaultForegroundColor)
                            {
                                result.AppendFormat(@"\cf{0} ", DefaultForegroundColor);
                                currentFG = DefaultForegroundColor;
                            }
                            if (currentBG != DefaultBackgroundColor)
                            {
                                result.AppendFormat(@"\highlight{0} ", DefaultBackgroundColor);
                                currentBG = DefaultBackgroundColor;
                            }
                            i--;
                        }
                        break;

                    case '\x16':
                        int temp = currentBG;
                        currentBG = currentFG;
                        currentFG = temp;
                        result.AppendFormat(@"\cf{0} \highlight{1} ", currentFG, currentBG);
                        break;

                    case '\x1F':
                        if (UnderlineSet)
                            result.Append(@"\ulnone ");
                        else
                            result.Append(@"\ul ");
                        UnderlineSet = !UnderlineSet;
                        break;

                    case '\x0F':
                        if (BoldSet)
                            result.Append(@"\b0 ");
                        if (UnderlineSet)
                            result.Append(@"\ulnone ");
                        if (currentFG != DefaultForegroundColor)
                        {
                            result.AppendFormat(@"\cf{0} ", DefaultForegroundColor);
                            currentFG = DefaultForegroundColor;
                        }
                        if (currentBG != DefaultBackgroundColor)
                        {
                            result.AppendFormat(@"\highlight{0} ", DefaultBackgroundColor);
                            currentBG = DefaultBackgroundColor;
                        }
                        BoldSet = false;
                        UnderlineSet = false;
                        break;

                    default:
                        result.Append(Line[i]);
                        break;
                }
            }
            return result.ToString();
        }

        private String mIRCCodeRTFReplace(Match m)
        {
            if (m.Value == "\\'02") return "\x02";
            if (m.Value == "\\'03") return "\x03";
            if (m.Value == "\\'0f") return "\x0f";
            if (m.Value == "\\'16") return "\x16";
            if (m.Value == "\\'1f") return "\x1f";
            return m.Value;
        }

        public int DefaultBackgroundColor
        {
            get { return DefaultBackgroundColorValue; }
        }

        public int DefaultForegroundColor
        {
            get { return DefaultForegroundColorValue; }
        }
    }
}
