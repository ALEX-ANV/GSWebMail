using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateManager.Manager
{
    internal class ManagmentManager : Manager<LetterMangmentType>
    {
        internal override Dictionary<LetterMangmentType, string[]> GetTemplates(string path, string extension)
        {
            if (!Directory.Exists(path))
            {
                return null;
            }
            var files = new DirectoryInfo(path).GetFiles($"*.{extension}");

            var source = new Dictionary<LetterMangmentType, string[]>();

            foreach (var file in files)
            {
                LetterMangmentType type;
                if (Enum.TryParse(file.Name, true, out type))
                {
                    source.Add(type, File.ReadAllLines(file.FullName));
                }
            }
            return source;
        }
    }
}
