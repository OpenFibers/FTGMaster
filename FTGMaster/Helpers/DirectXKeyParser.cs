using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectXKeyParser
{
    public class DirectXKeyParser
    {
        public static DirectXKeyCode DirectXKeyScanCodeFromString(String stringToParse)
        {
            DirectXKeyCode value = 0;
            String lowerCaseStringToParse = stringToParse.ToLower();

            switch (lowerCaseStringToParse)
            {
                case "escape":
                    value = DirectXKeyCode.Escape;
                    break;
                case "1":
                    value = DirectXKeyCode.D1;
                    break;
                case "2":
                    value = DirectXKeyCode.D2;
                    break;
                case "3":
                    value = DirectXKeyCode.D3;
                    break;
                case "4":
                    value = DirectXKeyCode.D4;
                    break;
                case "5":
                    value = DirectXKeyCode.D5;
                    break;
                case "6":
                    value = DirectXKeyCode.D6;
                    break;
                case "7":
                    value = DirectXKeyCode.D7;
                    break;
                case "8":
                    value = DirectXKeyCode.D8;
                    break;
                case "9":
                    value = DirectXKeyCode.D9;
                    break;
                case "0":
                    value = DirectXKeyCode.D0;
                    break;
                case "-":
                    value = DirectXKeyCode.Minus;
                    break;
                case "=":
                    value = DirectXKeyCode.Equals;
                    break;
                case "backspace":
                case "back":
                    value = DirectXKeyCode.BackSpace;
                    break;
                case "tab":
                    value = DirectXKeyCode.Tab;
                    break;
                case "q":
                    value = DirectXKeyCode.Q;
                    break;
                case "w":
                    value = DirectXKeyCode.W;
                    break;
                case "e":
                    value = DirectXKeyCode.E;
                    break;
                case "r":
                    value = DirectXKeyCode.R;
                    break;
                case "t":
                    value = DirectXKeyCode.T;
                    break;
                case "y":
                    value = DirectXKeyCode.Y;
                    break;
                case "u":
                    value = DirectXKeyCode.U;
                    break;
                case "i":
                    value = DirectXKeyCode.I;
                    break;
                case "o":
                    value = DirectXKeyCode.O;
                    break;
                case "p":
                    value = DirectXKeyCode.P;
                    break;
                case "{":
                case "[":
                    value = DirectXKeyCode.LeftBracket;
                    break;
                case "}":
                case "]":
                    value = DirectXKeyCode.RightBracket;
                    break;
                case "return":
                case "enter":
                    value = DirectXKeyCode.Return;
                    break;
                case "leftcontrol":
                case "lcontrol":
                case "lctrl":
                case "controlkey":
                case "lcontrolkey":
                    value = DirectXKeyCode.LeftControl;
                    break;
                case "a":
                    value = DirectXKeyCode.A;
                    break;
                case "s":
                    value = DirectXKeyCode.S;
                    break;
                case "d":
                    value = DirectXKeyCode.D;
                    break;
                case "f":
                    value = DirectXKeyCode.F;
                    break;
                case "g":
                    value = DirectXKeyCode.G;
                    break;
                case "h":
                    value = DirectXKeyCode.H;
                    break;
                case "j":
                    value = DirectXKeyCode.J;
                    break;
                case "k":
                    value = DirectXKeyCode.K;
                    break;
                case "l":
                    value = DirectXKeyCode.L;
                    break;
                case ";":
                case "semicolon":
                    value = DirectXKeyCode.SemiColon;
                    break;
                case "'":
                case "apostrophe":
                    value = DirectXKeyCode.Apostrophe;
                    break;
                case "`":
                case "grave":
                    value = DirectXKeyCode.Grave;
                    break;
                case "leftshift":
                case "lshift":
                case "shiftkey":
                case "lshiftkey":
                    value = DirectXKeyCode.LeftShift;
                    break;
                case "\\":
                case "backslash"://反斜杠\
                    value = DirectXKeyCode.BackSlash;
                    break;
                case "z":
                    value = DirectXKeyCode.Z;
                    break;
                case "x":
                    value = DirectXKeyCode.X;
                    break;
                case "c":
                    value = DirectXKeyCode.C;
                    break;
                case "v":
                    value = DirectXKeyCode.V;
                    break;
                case "b":
                    value = DirectXKeyCode.B;
                    break;
                case "n":
                    value = DirectXKeyCode.N;
                    break;
                case "m":
                    value = DirectXKeyCode.M;
                    break;
                case ",":
                case "comma":
                    value = DirectXKeyCode.Comma;
                    break;
                case ".":
                case "period":
                    value = DirectXKeyCode.Period;
                    break;
                case "/":
                case "slash":
                    value = DirectXKeyCode.Slash;
                    break;
                case "rightshift":
                case "rshift":
                case "rshiftkey":
                    value = DirectXKeyCode.RightShift;
                    break;
                case "*":
                case "numpadstar":
                case "multiply":
                    value = DirectXKeyCode.NumPadStar;
                    break;
                case "leftmenu":
                case "lmenu":
                case "leftalt":
                case "lalt":
                case "altkey":
                    value = DirectXKeyCode.LeftAlt;
                    break;
                case "space":
                    value = DirectXKeyCode.Space;
                    break;
                case "capital":
                case "capsLock":
                    value = DirectXKeyCode.CapsLock;
                    break;
                case "f1":
                    value = DirectXKeyCode.F1;
                    break;
                case "f2":
                    value = DirectXKeyCode.F2;
                    break;
                case "f3":
                    value = DirectXKeyCode.F3;
                    break;
                case "f4":
                    value = DirectXKeyCode.F4;
                    break;
                case "f5":
                    value = DirectXKeyCode.F5;
                    break;
                case "f6":
                    value = DirectXKeyCode.F6;
                    break;
                case "f7":
                    value = DirectXKeyCode.F7;
                    break;
                case "f8":
                    value = DirectXKeyCode.F8;
                    break;
                case "f9":
                    value = DirectXKeyCode.F9;
                    break;
                case "f10":
                    value = DirectXKeyCode.F10;
                    break;
                case "numlock":
                    value = DirectXKeyCode.Numlock;
                    break;
                case "scroll":
                case "scrolllock":
                    value = DirectXKeyCode.Scroll;
                    break;
                case "numpad7":
                    value = DirectXKeyCode.NumPad7;
                    break;
                case "numpad8":
                    value = DirectXKeyCode.NumPad8;
                    break;
                case "numpad9":
                    value = DirectXKeyCode.NumPad9;
                    break;
                case "numpad-":
                case "numpadminus":
                case "subtract":
                    value = DirectXKeyCode.NumPadMinus;
                    break;
                case "numpad4":
                    value = DirectXKeyCode.NumPad4;
                    break;
                case "numpad5":
                    value = DirectXKeyCode.NumPad5;
                    break;
                case "numpad6":
                    value = DirectXKeyCode.NumPad6;
                    break;
                case "numpadplus":
                case "numpad+":
                case "add":
                    value = DirectXKeyCode.NumPadPlus;
                    break;
                case "numpad1":
                    value = DirectXKeyCode.NumPad1;
                    break;
                case "numpad2":
                    value = DirectXKeyCode.NumPad2;
                    break;
                case "numpasd3":
                    value = DirectXKeyCode.NumPad3;
                    break;
                case "numpad0":
                    value = DirectXKeyCode.NumPad0;
                    break;
                case "numpad.":
                case "numpadperiod":
                case "decimal":
                    value = DirectXKeyCode.NumPadPeriod;
                    break;
                case "oem102":
                    value = DirectXKeyCode.OEM102;
                    break;
                case "f11":
                    value = DirectXKeyCode.F11;
                    break;
                case "f12":
                    value = DirectXKeyCode.F12;
                    break;
                case "f13":
                    value = DirectXKeyCode.F13;
                    break;
                case "f14":
                    value = DirectXKeyCode.F14;
                    break;
                case "f15":
                    value = DirectXKeyCode.F15;
                    break;
                case "kana":
                case "kanamode":
                    value = DirectXKeyCode.Kana;
                    break;
                case "abntc1":
                    value = DirectXKeyCode.AbntC1;
                    break;
                case "convert":
                case "imeconvert"://IME convert key
                    value = DirectXKeyCode.Convert;
                    break;
                case "noconvert":
                case "imenoconvert": //IME no convert keyDirectXKeyCode
                    value = DirectXKeyCode.NoConvert;
                    break;
                case "yen":
                    value = DirectXKeyCode.Yen;
                    break;
                case "abntc2":
                    value = DirectXKeyCode.AbntC2;
                    break;
                case "numpadequals":
                case "numpad=":
                    value = DirectXKeyCode.NumPadEquals;
                    break;
                case "circumflex":
                case "prevtrack":
                case "previoustrack":
                case "mediaprevioustrack":
                    value = DirectXKeyCode.Circumflex;
                    break;
                case "at":
                case "@":
                    value = DirectXKeyCode.At;
                    break;
                case "colon":
                    value = DirectXKeyCode.Colon;
                    break;
                case "underline":
                case "_":
                    value = DirectXKeyCode.Underline;
                    break;
                case "kanji":
                case "kanjimode":
                    value = DirectXKeyCode.Kanji;
                    break;
                case "stop":
                    value = DirectXKeyCode.Stop;
                    break;
                case "ax":
                    value = DirectXKeyCode.AX;
                    break;
                case "unlabeled":
                    value = DirectXKeyCode.Unlabeled;
                    break;
                case "nexttrack":
                case "medianexttrack":
                    value = DirectXKeyCode.NextTrack;
                    break;
                case "numpadenter":
                    value = DirectXKeyCode.NumPadEnter;
                    break;
                case "rightcontrol":
                case "rcontrol":
                case "rightctrl":
                case "rctrl":
                case "rcontrolkey":
                    value = DirectXKeyCode.RightControl;
                    break;
                case "mute":
                case "volumemute":
                    value = DirectXKeyCode.Mute;
                    break;
                case "calculator":
                    value = DirectXKeyCode.Calculator;
                    break;
                case "playpause":
                    value = DirectXKeyCode.PlayPause;
                    break;
                case "mediastop":
                    value = DirectXKeyCode.MediaStop;
                    break;
                case "volumedown":
                    value = DirectXKeyCode.VolumeDown;
                    break;
                case "volumeup":
                    value = DirectXKeyCode.VolumeUp;
                    break;
                case "webhome":
                    value = DirectXKeyCode.WebHome;
                    break;
                case "numpadcomma":
                case "numpad,":
                    value = DirectXKeyCode.NumPadComma;
                    break;
                case "divide":
                    value = DirectXKeyCode.Divide;
                    break;
                case "sysrq":
                    value = DirectXKeyCode.SysRq;
                    break;
                case "rightmenu":
                case "rmenu":
                case "rightalt":
                case "ralt":
                    value = DirectXKeyCode.RightAlt;
                    break;
                case "pause":
                    value = DirectXKeyCode.Pause;
                    break;
                case "home":
                    value = DirectXKeyCode.Home;
                    break;
                case "uparrow":
                case "up":
                    value = DirectXKeyCode.UpArrow;
                    break;
                case "prior":
                case "pageup":
                    value = DirectXKeyCode.PageUp;
                    break;
                case "left":
                case "leftarrow":
                    value = DirectXKeyCode.LeftArrow;
                    break;
                case "right":
                case "rightarrow":
                    value = DirectXKeyCode.RightArrow;
                    break;
                case "end":
                    value = DirectXKeyCode.End;
                    break;
                case "down":
                case "downarrow":
                    value = DirectXKeyCode.DownArrow;
                    break;
                case "next":
                case "pagedown":
                    value = DirectXKeyCode.Next;
                    break;
                case "insert":
                    value = DirectXKeyCode.Insert;
                    break;
                case "delete":
                    value = DirectXKeyCode.Delete;
                    break;
                case "leftwindows":
                case "lwindows":
                    value = DirectXKeyCode.LeftWindows;
                    break;
                case "rightwindows":
                case "rwindows":
                    value = DirectXKeyCode.RightWindows;
                    break;
                case "apps":
                case "application":
                case "app":
                    value = DirectXKeyCode.Apps;
                    break;
                case "power":
                    value = DirectXKeyCode.Power;
                    break;
                case "sleep":
                    value = DirectXKeyCode.Sleep;
                    break;
                case "wake":
                    value = DirectXKeyCode.Wake;
                    break;
                case "websearch":
                case "browsersearch":
                    value = DirectXKeyCode.WebSearch;
                    break;
                case "webfavorites":
                case "webfav":
                case "browserfavorites":
                case "browserfav":
                    value = DirectXKeyCode.WebFavorites;
                    break;
                case "webrefresh":
                case "browserrefresh":
                    value = DirectXKeyCode.WebRefresh;
                    break;
                case "webstop":
                case "browserstop":
                    value = DirectXKeyCode.WebStop;
                    break;
                case "webforward":
                case "browserforward":
                    value = DirectXKeyCode.WebForward;
                    break;
                case "webback":
                case "browserback":
                    value = DirectXKeyCode.WebBack;
                    break;
                case "mycomputer":
                    value = DirectXKeyCode.MyComputer;
                    break;
                case "mail":
                    value = DirectXKeyCode.Mail;
                    break;
                case "mediaselect":
                    value = DirectXKeyCode.MediaSelect;
                    break;
                default:
                    value = DirectXKeyCode.None;
                    break;
            }
            return value;
        }
    }

    public enum DirectXKeyCode
    {
        None = 0,
        Escape = 1,
        D1 = 2,
        D2 = 3,
        D3 = 4,
        D4 = 5,
        D5 = 6,
        D6 = 7,
        D7 = 8,
        D8 = 9,
        D9 = 10,
        D0 = 11,
        Minus = 12,
        Equals = 13,
        BackSpace = 14,
        Back = 14,
        Tab = 15,
        Q = 16,
        W = 17,
        E = 18,
        R = 19,
        T = 20,
        Y = 21,
        U = 22,
        I = 23,
        O = 24,
        P = 25,
        LeftBracket = 26,
        RightBracket = 27,
        Return = 28,
        LeftControl = 29,
        A = 30,
        S = 31,
        D = 32,
        F = 33,
        G = 34,
        H = 35,
        J = 36,
        K = 37,
        L = 38,
        SemiColon = 39,
        Apostrophe = 40,
        Grave = 41,
        LeftShift = 42,
        BackSlash = 43,
        Z = 44,
        X = 45,
        C = 46,
        V = 47,
        B = 48,
        N = 49,
        M = 50,
        Comma = 51,
        Period = 52,
        Slash = 53,
        RightShift = 54,
        NumPadStar = 55,
        Multiply = 55,
        LeftMenu = 56,
        LeftAlt = 56,
        Space = 57,
        Capital = 58,
        CapsLock = 58,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        Numlock = 69,
        Scroll = 70,
        NumPad7 = 71,
        NumPad8 = 72,
        NumPad9 = 73,
        NumPadMinus = 74,
        Subtract = 74,
        NumPad4 = 75,
        NumPad5 = 76,
        NumPad6 = 77,
        NumPadPlus = 78,
        Add = 78,
        NumPad1 = 79,
        NumPad2 = 80,
        NumPad3 = 81,
        NumPad0 = 82,
        NumPadPeriod = 83,
        Decimal = 83,
        OEM102 = 86,
        F11 = 87,
        F12 = 88,
        F13 = 100,
        F14 = 101,
        F15 = 102,
        Kana = 112,
        AbntC1 = 115,
        Convert = 121,
        NoConvert = 123,
        Yen = 125,
        AbntC2 = 126,
        NumPadEquals = 141,
        Circumflex = 144,
        PrevTrack = 144,
        At = 145,
        Colon = 146,
        Underline = 147,
        Kanji = 148,
        Stop = 149,
        AX = 150,
        Unlabeled = 151,
        NextTrack = 153,
        NumPadEnter = 156,
        RightControl = 157,
        Mute = 160,
        Calculator = 161,
        PlayPause = 162,
        MediaStop = 164,
        VolumeDown = 174,
        VolumeUp = 176,
        WebHome = 178,
        NumPadComma = 179,
        Divide = 181,
        NumPadSlash = 181,
        SysRq = 183,
        RightMenu = 184,
        RightAlt = 184,
        Pause = 197,
        Home = 199,
        UpArrow = 200,
        Up = 200,
        Prior = 201,
        PageUp = 201,
        Left = 203,
        LeftArrow = 203,
        Right = 205,
        RightArrow = 205,
        End = 207,
        DownArrow = 208,
        Down = 208,
        Next = 209,
        PageDown = 209,
        Insert = 210,
        Delete = 211,
        LeftWindows = 219,
        RightWindows = 220,
        Apps = 221,
        Power = 222,
        Sleep = 223,
        Wake = 227,
        WebSearch = 229,
        WebFavorites = 230,
        WebRefresh = 231,
        WebStop = 232,
        WebForward = 233,
        WebBack = 234,
        MyComputer = 235,
        Mail = 236,
        MediaSelect = 237,
    }
}
