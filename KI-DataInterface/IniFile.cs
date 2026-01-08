using System.Runtime.InteropServices;
using System.Text;

namespace KI_DataInterface
{
    public class IniFile
    {
        private readonly string _path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniFile(string iniPath)
        {
            _path = iniPath;
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public string Read(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, key, "", temp, 255, _path);
            return temp.ToString();
        }
    }
}