using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace SendInputHelper
{
    class SendInputHelper
    {
        public static uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        //If specified, the scan code was preceded by a prefix byte that has the value 0xE0 (224).
        public static uint KEYEVENTF_KEYUP = 0x0002;
        public static uint KEYEVENTF_SCANCODE = 0x0008;
        public static uint KEYEVENTF_UNICODE = 0x0004;

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint nInput, ref INPUT pInput, int cbSize);
        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUT
        {
            [FieldOffset(0)]
            internal int type;//0:mouse event;1:keyboard event;2:hardware event
            [FieldOffset(4)]
            internal MOUSEINPUT mi;
            [FieldOffset(4)]
            internal KEYBDINPUT ki;
            [FieldOffset(4)]
            internal HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            internal int uMsg;
            internal short wParamL;
            internal short wParamH;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            internal ushort wVk;
            internal ushort wScan;
            internal uint dwFlags;
            internal uint time;
            internal IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal int dwFlags;
            internal int time;
            internal IntPtr dwExtraInfo;
        }

        //按住
        public static void KeyDown(int vkCode)
        {
            INPUT input = new INPUT();
            input.type = 1; //keyboard_input
            input.ki.wVk = (ushort)vkCode; //按键的vkCode
            input.ki.dwFlags = 0;//按下按键
            SendInput(1, ref input, Marshal.SizeOf(input));
        }
	   
	    //抬起
        public static void KeyUp(int vkCode)
        {
            INPUT input = new INPUT();
            input.type = 1;
            input.ki.wVk = (ushort)vkCode;//按键的vkCode
            input.ki.dwFlags = KEYEVENTF_KEYUP;//抬起按键
            SendInput(1, ref input, Marshal.SizeOf(input));
        }

        //DirectInput按住
        public static void DirectInputKeyDown(int vScanCode)
        {
            INPUT input = new INPUT();
            input.type = 1; //keyboard_input
            input.ki.wScan = (ushort)vScanCode; //按键的vScanCode
            input.ki.dwFlags = KEYEVENTF_SCANCODE;//按下ScanCode
            SendInput(1, ref input, Marshal.SizeOf(input));
        }

        //DirectInput弹起
        public static void DirectInputKeyUp(int vScanCode)
        {
            INPUT input = new INPUT();
            input.type = 1; //keyboard_input
            input.ki.wScan = (ushort)vScanCode; //按键的vScanCode
            input.ki.dwFlags = KEYEVENTF_KEYUP | KEYEVENTF_SCANCODE;//按下ScanCode
            SendInput(1, ref input, Marshal.SizeOf(input));
        }
    }
}
