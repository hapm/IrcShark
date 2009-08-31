using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{

    public enum ModeArt
    {
        User,
        Channel
    }

    public class Mode
    {
        private FlagDefinition flag;
        private String parameter;
        private FlagArt art;

        public Mode(FlagDefinition Flag, FlagArt Art)
        {
            flag = Flag;
            art = Art;
        }

        public Mode(FlagDefinition Flag, FlagArt Art, String Parameter)
        {
            flag = Flag;
            art = Art;
            parameter = Parameter;
        }

        public FlagDefinition Flag
        {
            get { return flag; }
        }

        public String Parameter
        {
            get { return parameter; }
        }

        public FlagArt Art
        {
            get { return art; }
        }
    }
}
