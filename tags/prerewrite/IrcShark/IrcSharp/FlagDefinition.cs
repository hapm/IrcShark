// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
    }

    public class FlagDefinition
    {
        private char flagChar;
        private ModeArt art;
        private FlagParameter unsetParameter;
        private FlagParameter setParameter;
        private Regex parameterCheck;

        public FlagDefinition(char FlagChar, ModeArt Art)
        {
            flagChar = FlagChar;
            art = Art;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter Parameters)
        {
            flagChar = FlagChar;
            art = Art;
            setParameter = Parameters;
            unsetParameter = Parameters;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter SetParameter, FlagParameter UnsetParameter)
        {
            flagChar = FlagChar;
            art = Art;
            setParameter = SetParameter;
            unsetParameter = UnsetParameter;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter Parameters, Regex ParameterChecker)
        {
            flagChar = FlagChar;
            art = Art;
            setParameter = Parameters;
            unsetParameter = Parameters;
            parameterCheck = ParameterChecker;
        }

        public FlagDefinition(char FlagChar, ModeArt Art, FlagParameter SetParameter, FlagParameter UnsetParameter, Regex ParameterChecker)
        {
            flagChar = FlagChar;
            art = Art;
            setParameter = SetParameter;
            unsetParameter = UnsetParameter;
            parameterCheck = ParameterChecker;
        }

        public ModeArt Art
        {
            get { return art; }
        }

        public FlagParameter SetParameter
        {
            get { return setParameter; }
        }

        public FlagParameter UnsetParameter
        {
            get { return unsetParameter; }
        }

        public Regex ParameterCheck
        {
            get { return parameterCheck; }
        }

        public bool IsParameter(FlagArt Art, String Parameter)
        {
            if (Art == FlagArt.Set && SetParameter == FlagParameter.NotAllowed)
            	return false;
            else if (Art == FlagArt.Unset && UnsetParameter == FlagParameter.NotAllowed)
            	return false;
            
            if (ParameterCheck == null)
            	return true;
            return ParameterCheck.IsMatch(Parameter);
        }

        public bool NeedParameter(FlagArt Art)
        {
            if (Art == FlagArt.Set && SetParameter == FlagParameter.Needed)
            	return true;
            else if (Art == FlagArt.Unset && UnsetParameter == FlagParameter.Needed)
            	return true;
            else
            	return false;
        }

        public char Char
        {
            get { return flagChar; }
        }
    }
}
