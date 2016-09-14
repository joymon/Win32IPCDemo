﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    class Win32
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        [DllImport("User32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        public const int WM_USER = 0x0400;
        public const int WM_COPYDATA = 0x004a;

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            [MarshalAs(UnmanagedType.I4)]
            public int dwData;
            [MarshalAs(UnmanagedType.I4)]
            public int cbData;
            [MarshalAs(UnmanagedType.SysInt)]
            public IntPtr lpData;
        }
    }
    class Win32Helper
    {
        public void SendString(IntPtr handle,string message)
        {
            string s = message;
            IntPtr lpData = Marshal.StringToHGlobalUni(s);

            Win32.COPYDATASTRUCT data = new Win32.COPYDATASTRUCT();
            data.dwData = 0;
            data.cbData = s.Length * 2;
            data.lpData = lpData;

            IntPtr lpStruct = Marshal.AllocHGlobal(
                Marshal.SizeOf(data));

            Marshal.StructureToPtr(data, lpStruct, false);


            Win32.SendMessage(handle, Win32.WM_COPYDATA,
                IntPtr.Zero, lpStruct);
        }
    }
}
