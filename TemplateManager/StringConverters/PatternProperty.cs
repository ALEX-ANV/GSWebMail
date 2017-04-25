using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TemplateManager.StringConverters
{
    internal class PatternProperty
    {
        private string _pattern;

        private string _value;

        private List<ItemPatternProperty> _patternProperties;

        public bool Parse(string pattern, TemplateStringSettings settings, out string value)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                value = null;
                return false;
            }
            _pattern = pattern;
            _patternProperties = new List<ItemPatternProperty>();
            int replaceStart, replaceEnd;
            do
            {
                replaceStart = pattern.IndexOf(settings.QuotesStart);
                replaceEnd = pattern.IndexOf(settings.QuoteEnd);
                if (replaceStart > -1 && replaceEnd > -1)
                {
                    _value = pattern.Substring(0, replaceStart);
                    var key = pattern.Substring(replaceStart, replaceEnd - replaceStart + 1);
                    while (!FullKey(key, settings))
                    {
                        replaceEnd = pattern.IndexOf(settings.QuoteEnd, replaceEnd + 1);
                    }

                }
            } while (!string.IsNullOrEmpty(pattern));
        }

        private bool FullKey(string key, TemplateStringSettings settings)
        {
            return key.Count(t => t.Equals(settings.QuotesStart)) == key.Count(t => t.Equals(settings.QuoteEnd));
        }
    }


    internal class ItemPatternProperty
    {
        internal TypeProperty Type { get; set; }

        internal string Key { get; set; }

        internal List<ItemPatternProperty> Items { get; set; }
    }

    internal enum TypeProperty
    {
        Value, List
    }
}
