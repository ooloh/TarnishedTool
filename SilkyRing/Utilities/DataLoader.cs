using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SilkyRing.Models;
using SilkyRing.Properties;

namespace SilkyRing.Utilities
{
    public static class DataLoader
    {
        public static Dictionary<string, List<WarpLocation>> GetGraces()
        {
            Dictionary<string, List<WarpLocation>> graceDict = new Dictionary<string, List<WarpLocation>>();
            string csvData = Resources.Graces;

            if (string.IsNullOrWhiteSpace(csvData))
                return graceDict;

            using (StringReader reader = new StringReader(csvData))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                    string[] parts = line.Split(',');
                    if (parts.Length < 4) continue;

                    bool isDlc = parts[0] == "1";
                    string mainArea = parts[1];
                    string name = parts[2];
                    long graceEntityId = long.Parse(parts[3], CultureInfo.InvariantCulture);
                    
                    WarpLocation warpLocation = new WarpLocation
                    {
                        IsDlc = isDlc,
                        MainArea = mainArea,
                        LocationName = name,
                        GraceEntityId = graceEntityId
                    };
                    
                    if (!graceDict.ContainsKey(mainArea))
                    {
                        graceDict[mainArea] = new List<WarpLocation>();
                    }
                    graceDict[mainArea].Add(warpLocation);
                }
            }

            return graceDict;
        }
    }
}