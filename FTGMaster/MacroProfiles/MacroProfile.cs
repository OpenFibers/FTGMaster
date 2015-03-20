using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FTGMaster.MacroProfiles
{
    class MacroProfile
    {
        private List<SingleMacro> _macros;

        private MacroProfile(String profileString)
        {
            _macros = new List<SingleMacro>();
            String[] macroStrings = profileString.Split(':');
            foreach(String macroString in macroStrings)
            {
                if (macroString != null && macroString.Length != 0)
                {
                    SingleMacro macro = SingleMacro.SingleMacroWithString(macroString);
                    if (macro != null)
                    {
                        _macros.Add(macro);
                    }
                }
            }
        }

        public SingleMacro[] AllMacros()
        {
            SingleMacro[] allMacros = _macros.ToArray();
            return allMacros;
        }

        //工厂方法，从relative path生成MacroProfile
        public static MacroProfile ProfileFromFileRelativePath(String relativeFilePath)
        {
            String fileFullPathString = System.Windows.Forms.Application.StartupPath + "\\" + relativeFilePath;
            MacroProfile profile = ProfileFromFileFullPath(fileFullPathString);
            return profile;
        }

        //工厂方法，从full path生成MacroProfile
        private static MacroProfile ProfileFromFileFullPath(String fullFilePath)
        {
            String content = ContentStringFromFilePath(fullFilePath);
            if (content == null)
            {
                return null;
            }
            MacroProfile profile = new MacroProfile(content);
            return profile;
        }

        //读取文件内容，trim空白、换行符、去注释、去掉end
        private static String ContentStringFromFilePath(String filePath)
        {
            String content = null;
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filePath, true);
                String line = "";
                StringBuilder stringBuilder = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    //去掉注释
                    int commentIndex = line.IndexOf("//");
                    if (commentIndex != -1)//-1是未找到的返回值
                    {
                        line = line.Substring(0, commentIndex);
                    }
                    //去掉空白符
                    line = line.Trim();
                    //如果是函数结尾的标记，扔掉
                    if (line == "end")
                    {
                        continue;
                    }
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    
                    stringBuilder.Append(line);
                }
                content = stringBuilder.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return content;
        }
    }
}
