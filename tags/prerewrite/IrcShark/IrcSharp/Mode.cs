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
        private FlagDefinition FlagValue;
        private String ParameterValue;
        private FlagArt ArtValue;

        public Mode(FlagDefinition Flag, FlagArt Art)
        {
            FlagValue = Flag;
            ArtValue = Art;
        }

        public Mode(FlagDefinition Flag, FlagArt Art, String Parameter)
        {
            FlagValue = Flag;
            ArtValue = Art;
            ParameterValue = Parameter;
        }

        public FlagDefinition Flag
        {
            get { return FlagValue; }
        }

        public String Parameter
        {
            get { return ParameterValue; }
        }

        public FlagArt Art
        {
            get { return ArtValue; }
        }
    }
}
