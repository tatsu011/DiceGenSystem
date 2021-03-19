using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceGenerator
{
    public class SequenceSettings
    {
        public static SequenceSettings ActiveSettings
        {
            get;
            private set;
        }

        public Dictionary<string, int> GenSettings;
        
        public SequenceSettings()
        {
            GenSettings = new Dictionary<string, int>();
        }

        public int GetSettingValue(string setting)
        {
            if (GenSettings != null && GenSettings.ContainsKey(setting))
                return GenSettings[setting];
            //no value key or GenSettings is empty.
            return 0;
        }

        public static void ActivateSettings(SequenceSettings settings)
        {
            ActiveSettings = settings;
        }


    }
}
