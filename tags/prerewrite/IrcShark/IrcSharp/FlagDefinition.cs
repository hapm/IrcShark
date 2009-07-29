using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace IrcSharp
{
    public enum FlagArt
    {
        Set,
        Unset
    }

    public enum FlagParameter
    {
        Optional,
        Needed,
        NotAllowed
    };

    public class FlagDefinition
    {
        private char CharValue;
        private ModeArt ArtValue;
        private FlagParameter UnsetParameterValue;
        private FlagParameter SetParameterValue;
        private Regex ParameterCheckValue;

        public FlagDefinition(char FlagChar, ModeArt Art)
        {
            CharValue = FlagChar;
            ArtValue = Art;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter Parameters)
        {
            CharValue = FlagChar;
            ArtValue = Art;
            SetParameterValue = Parameters;
            UnsetParameterValue = Parameters;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter SetParameter, FlagParameter UnsetParameter)
        {
            CharValue = FlagChar;
            ArtValue = Art;
            SetParameterValue = SetParameter;
            UnsetParameterValue = UnsetParameter;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter Parameters, Regex ParameterChecker)
        {
            CharValue = FlagChar;
            ArtValue = Art;
            SetParameterValue = Parameters;
            UnsetParameterValue = Parameters;
            ParameterCheckValue = ParameterChecker;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter SetParameter, FlagParameter UnsetParameter, Regex ParameterChecker)
        {
            CharValue = FlagChar;
            ArtValue = Art;
            SetParameterValue = SetParameter;
            UnsetParameterValue = UnsetParameter;
            ParameterCheckValue = ParameterChecker;
        }

        public ModeArt Art
        {
            get { return ArtValue; }
        }

        public FlagParameter SetParameter
        {
            get { return SetParameterValue; }
        }

        public FlagParameter UnsetParameter
        {
            get { return UnsetParameterValue; }
        }

        public Regex ParameterCheck
        {
            get { return ParameterCheckValue; }
        }

        public bool IsParameter(FlagArt Art, String Parameter)
        {
            if (Art == FlagArt.Set && SetParameter == FlagParameter.NotAllowed) return false;
            else if (Art == FlagArt.Unset && UnsetParameter == FlagParameter.NotAllowed) return false;
            if (ParameterCheck == null) return true;
            return ParameterCheck.IsMatch(Parameter);
        }

        public bool NeedParameter(FlagArt Art)
        {
            if (Art == FlagArt.Set && SetParameter == FlagParameter.Needed) return true;
            else if (Art == FlagArt.Unset && UnsetParameter == FlagParameter.Needed) return true;
            return false;
        }

        public char Char
        {
            get { return CharValue; }
        }
    }
}
