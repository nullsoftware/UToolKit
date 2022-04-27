using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace NullSoftware.ToolKit
{
    public static class WindowPlacementManager
    {
        // RECT structure required by WINDOWPLACEMENT structure
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}, {2}, {3}", Left, Top, Right, Bottom);
            }
        }

        // POINT structure required by WINDOWPLACEMENT structure
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public override string ToString()
            {
                return string.Format("{0}, {1}", X, Y);
            }
        }

        // WINDOWPLACEMENT stores the position, size, and state of a window
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT minPosition;
            public POINT maxPosition;
            public RECT normalPosition;
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        private static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;

        public static void SetPlacement(Window window, WINDOWPLACEMENT placement)
        {
            placement.length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
            placement.flags = 0;
            placement.showCmd = (placement.showCmd == SW_SHOWMINIMIZED ? SW_SHOWNORMAL : placement.showCmd);
            SetWindowPlacement(new WindowInteropHelper(window).Handle, ref placement);
        }

        public static WINDOWPLACEMENT GetPlacement(Window window)
        {
            WINDOWPLACEMENT wp;
            GetWindowPlacement(new WindowInteropHelper(window).Handle, out wp);

            return wp;
        }

        public static byte[] Serialize(WINDOWPLACEMENT placement)
        {
            int size = Marshal.SizeOf(placement);
            byte[] result = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(placement, ptr, true);
            Marshal.Copy(ptr, result, 0, size);
            Marshal.FreeHGlobal(ptr);

            return result;
        }

        public static WINDOWPLACEMENT Deserialize(byte[] data)
        {
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();

            int size = Marshal.SizeOf(wp);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(data, 0, ptr, size);

            wp = (WINDOWPLACEMENT)Marshal.PtrToStructure(ptr, wp.GetType());
            Marshal.FreeHGlobal(ptr);

            return wp;
        }
    }
}