using System;
using System.Collections.Generic;
using System.Text;

namespace IrcSharp
{
    public class ModeReceivedEventArgs : IrcEventArgs
    {
        private String setter;
        private String AimValue;
        private Mode[] ModesValue;
        private ModeArt AimArtValue;

        public ModeReceivedEventArgs(IrcLine BaseLine) : base(BaseLine)
        {
            long currentParam;
            FlagArt currentArt = FlagArt.Set;
            List<Mode> modes = new List<Mode>();
            List<FlagDefinition> flags = new List<FlagDefinition>();
            setter = BaseLine.Prefix;
            AimValue = BaseLine.Parameters[0];
            if (Client.Standard.IsAllowedChannel(AimValue))
            {
                flags.AddRange(Client.Standard.ChannelFlags);
                flags.AddRange(Client.Standard.UserPrefixFlags.Values);
                AimArtValue = ModeArt.Channel;
            }
            else
            {
                flags.AddRange(Client.Standard.UserFlags);
                AimArtValue = ModeArt.User;
            }
            currentParam = 2;

            foreach (char c in BaseLine.Parameters[1])
            {
                if (c == '+') currentArt = FlagArt.Set;
                else if (c == '-') currentArt = FlagArt.Unset;
                else
                {
                    foreach (FlagDefinition currentFlag in flags)
                    {
                        if (currentFlag.Char == c)
                        {
                            if (currentParam < BaseLine.Parameters.Length && currentFlag.IsParameter(currentArt, BaseLine.Parameters[currentParam]))
                            {
                                modes.Add(new Mode(currentFlag, currentArt, BaseLine.Parameters[currentParam]));
                                currentParam++;
                            }
                            else if (!currentFlag.NeedParameter(currentArt))
                            {
                                modes.Add(new Mode(currentFlag, currentArt));
                            }
                            break;
                        }
                    }
                }
            }
            ModesValue = modes.ToArray();
        }

        public Mode[] Modes
        {
            get { return (Mode[])ModesValue.Clone(); }
        }

        public String Setter
        {
            get { return setter; }
        }

        public String Aim
        {
            get { return AimValue; }
        }

        public ModeArt AimArt
        {
            get { return AimArtValue; }
        }
    }
}
