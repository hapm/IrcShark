using System;
using System.Collections.Generic;
using System.Text;
using IrcSharp;

namespace IrcCloneShark
{
    public class TextTheme
    {
        private IrcColor Color1Value;
        private IrcColor Color2Value;
        private IrcColor Color3Value;

        private String JoinValue;
        private String PartValue;
        private String QuitValue;
        private String TopicValue;
        private String SelfJoinValue;
        private String SelfPartValue;
        private String ChannelMessageValue;
        private String ChannelNoticeValue;
        private String ChannelActionValue;
        private String MotdBeginValue;
        private String MotdEndValue;

        public String Join
        {
            get { return JoinValue; }
            set { JoinValue = value; }
        }

        public String Part
        {
            get { return PartValue; }
            set { PartValue = value; }
        }

        public String Quit
        {
            get { return QuitValue; }
            set { QuitValue = value; }
        }

        public String Topic
        {
            get { return TopicValue; }
            set { TopicValue = value; }
        }

        public String SelfJoin
        {
            get { return JoinValue; }
            set { JoinValue = value; }
        }

        public String SelfPart
        {
            get { return PartValue; }
            set { PartValue = value; }
        }
    }
}
