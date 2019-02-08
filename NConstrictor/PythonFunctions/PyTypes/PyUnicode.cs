using System.Runtime.InteropServices;
using System.Security;

namespace NConstrictor
{
    public class PyUnicode
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(@"Python3.dll", EntryPoint = "PyUnicode_DecodeFSDefault", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern PyObject DecodeFSDefault(string s);
    }
}
