using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class WhoLine : IrcLine
    {
        private Boolean IsAwayValue;
        private Mode[] ModesValue;
        private Boolean IsOperValue;
        private int HopCountValue;
        private String RealNameValue;

        private UserInfo UserValue;

        public WhoLine(IrcLine baseLine) : base(baseLine)
        {
            if(baseLine.Numeric != 352)
            	throw new ArgumentOutOfRangeException("baseLine", "RPL_WHOREPLY 352 expected");
            if(Parameters.Length < 8)
            	throw new ArgumentOutOfRangeException("baseLine", "Need a minimum of 8 parameters");
            
            UserValue = new UserInfo(Parameters[5], Parameters[2], Parameters[3], Client);
            List<Mode> modes = new List<Mode>();
            int i = 1;
            
            IsAwayValue = Parameters[6][0] == 'G';
            IsOperValue = Parameters[6][i] == '*';
            
            if(IsOper)
            	i++;
            
            for(; i < Parameters[6].Length; i++)
            {
                if(Client.Standard.UserPrefixFlags.ContainsKey(Parameters[6][i]))
                {
                    modes.Add(new Mode(Client.Standard.UserPrefixFlags[Parameters[6][i]], FlagArt.Set, User.NickName));
                }
            }

            ModesValue = modes.ToArray();
            
            RealNameValue = Parameters[7];

            if(!int.TryParse(RealNameValue.Substring(1, RealNameValue.IndexOf(" ")), out HopCountValue))
            	throw new ArgumentOutOfRangeException("baseLine", "Invalid hop count, integer expected");
            
            RealNameValue = RealNameValue.Substring(RealNameValue.IndexOf(" ") + 1);
        }

        public string Channel
        {
            get { return Parameters[1]; }
        }

        public string Server
        {
            get { return Parameters[4]; }
        }

        public bool IsAway
        {
            get { return IsAwayValue; }
        }

        public bool IsOper
        {
            get { return IsOperValue; }
        }

        public int HopCount
        {
            get { return HopCountValue; }
        }

        public Mode[] Modes
        {
            get { return (Mode[])ModesValue.Clone(); }
        }

        public UserInfo User
        {
            get { return UserValue; }
        }
    }
}
