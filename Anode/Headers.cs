using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Anode
{
    public static class Headers
    {
        // EXPORT INT CraftExecutable(LPCSTR lpszDest, LPBYTE lpStubData, DWORD dwStubSize, LPBYTE lpInterpreterData, DWORD dwInterpreterSize, LPBYTE lpProgramText, DWORD dwProgramSize)

        [DllImport("ImageSurgery.dll")]
        public static extern unsafe int CraftExecutable(string lpszDest,
            string lpszStubLocation,
            string lpszInterpreterPath,
            string lpszProgramPath);
    }
}
