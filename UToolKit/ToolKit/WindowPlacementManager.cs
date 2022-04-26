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
            byte[] result;

            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(placement.length);
                writer.Write(placement.flags);
                writer.Write(placement.showCmd);

                writer.Write(placement.minPosition.X);
                writer.Write(placement.minPosition.Y);

                writer.Write(placement.maxPosition.X);
                writer.Write(placement.maxPosition.Y);

                writer.Write(placement.normalPosition.Left);
                writer.Write(placement.normalPosition.Top);
                writer.Write(placement.normalPosition.Right);
                writer.Write(placement.normalPosition.Bottom);

                result = stream.ToArray();
            }

            return result;
        }

        public static WINDOWPLACEMENT Deserialize(byte[] data)
        {
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();

            using (MemoryStream stream = new MemoryStream(data))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                wp.length = reader.ReadInt32();
                wp.flags = reader.ReadInt32();
                wp.showCmd = reader.ReadInt32();

                wp.minPosition.X = reader.ReadInt32();
                wp.minPosition.Y = reader.ReadInt32();

                wp.maxPosition.X = reader.ReadInt32();
                wp.maxPosition.Y = reader.ReadInt32();

                wp.normalPosition.Left = reader.ReadInt32();
                wp.normalPosition.Top = reader.ReadInt32();
                wp.normalPosition.Right = reader.ReadInt32();
                wp.normalPosition.Bottom = reader.ReadInt32();
            }

            return wp;
        }
    }
}