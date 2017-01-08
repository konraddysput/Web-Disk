using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDisk.BusinessLogic.Common
{
    public static class ZipHelper
    {
        public static string GetFileName(string currentName, IEnumerable<ZipEntry> entries)
        {
            if (entries.Any(n => n.FileName == currentName))
            {
                return GetFileName(GetNewFileNameVersion(currentName),entries);
            }
            return currentName;
        }
        private static string GetNewFileNameVersion(string fileName)
        {
            var realName = Path.GetFileNameWithoutExtension(fileName);
            var pathWithoutFileName = Path.GetDirectoryName(fileName);
            var extension = Path.GetExtension(fileName);
            int currentVersion;
            if (realName.Contains('(') && realName.EndsWith(")"))
            {
                string previousVersion = realName.Substring(realName.LastIndexOf('(') + 1, realName.LastIndexOf(')') - realName.LastIndexOf('(') - 1);
                if (int.TryParse(previousVersion, out currentVersion))
                {
                    return $"{pathWithoutFileName}/{realName.Substring(0, realName.LastIndexOf('('))}({currentVersion+1}){extension}";

                }
            }
            return $"{pathWithoutFileName}/{realName}(1){extension}";

        }
    }
}
