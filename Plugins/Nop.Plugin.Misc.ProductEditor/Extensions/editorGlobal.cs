using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.ProductEditor.Extensions
{
    public static class EditorGlobal
    {
        public static Dictionary<string, string> CurrentEtag = new Dictionary<string, string>();

        public static void GenerateEtag(string language)
        {
            CurrentEtag[language] = language + Guid.NewGuid().ToString().Substring(0, 20);
            //currentEtag[language] = Guid.NewGuid().ToString().Substring(0, 20);
        }
    }
}
