using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoPackageJson._sunamo;
internal class CATo
{
    internal static List<T> ToList<T>(params T[] t)
    {
        return t.ToList();
    }

    internal static T[] ToArray<T>(params T[] t)
    {
        return t;
    }

    internal static List<string> ToListString(params object[] t)
    {
        return t.ToList().ConvertAll(d => d.ToString());
    }
}
