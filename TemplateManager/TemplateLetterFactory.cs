using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateManager.Manager;

namespace TemplateManager
{
    public class TemplateLetterFactory
    {
        private readonly string _pathDirectory;
        private readonly string _extension;

        public TemplateLetterFactory(string pathDirectory, string extension)
        {
            this._pathDirectory = pathDirectory;
            this._extension = extension;
        }

        public Dictionary<LetterMangmentType, string[]> ReadManagmentTemplates()
        {
            return new ManagmentManager().GetTemplates(_pathDirectory, _extension);
        }

        public Dictionary<LetterTaskType, string[]> ReadTaskTemplates()
        {
            return new TaskManager().GetTemplates(_pathDirectory, _extension);
        }
    }
}
