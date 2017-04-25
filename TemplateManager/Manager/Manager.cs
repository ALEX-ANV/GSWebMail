using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TemplateManager.Manager
{
    internal abstract class Manager<TSource> where TSource : struct 
    {
        internal abstract Dictionary<TSource, string[]> GetTemplates(string path, string extension);
    }
}
