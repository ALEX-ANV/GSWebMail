using System.Collections.Generic;

namespace TemplateManager.StringConverters
{
    public class TemplateStringSettings
    {
        private IDictionary<string, object> _values;
        public char QuotesStart { get; private set; }
        public char QuoteEnd { get; private set; }

        public TemplateStringSettings(char quoteStart = '{', char quoteEnd = '}')
        {
            _values = new Dictionary<string, object>();
            QuotesStart = quoteStart;
            QuoteEnd = quoteEnd;
        }

        public object FindValue(string key)
        {
            return _values.ContainsKey(key) ? _values[key] : null;
        }

        public void AddValue(string key, object value)
        {
            _values.Add(key, value);
        }

        public void AddValue(Dictionary<string, object> values)
        {
            foreach (var value in values)
            {
                if (!_values.ContainsKey(value.Key))
                {
                    _values.Add(value.Key, value.Value);
                }
                else
                {
                    _values[value.Key] = value.Value;
                }
            }
        }

    }
}
