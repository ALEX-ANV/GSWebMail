using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateManager.Manager;

namespace TemplateManager.StringConverters
{
    internal class TemplateToString
    {
        internal string[] FormatString(string[] pattern, TemplateStringSettings settings)
        {
            var stringFile = new List<string>();
            int replaceStartIndex = -1;
            int replaceEndIndex = -1;
            bool fullKey = true;

            for (var i = 0; i < pattern.Length; i++)
            {
                stringFile.Add("");
                do
                {
                    replaceStartIndex = fullKey ? pattern[i].IndexOf('{') : replaceStartIndex;
                    replaceEndIndex = fullKey ? pattern[i].IndexOf('}') : pattern[i].IndexOf('}', replaceEndIndex + 1);
                    if (replaceStartIndex == -1 || replaceEndIndex == -1)
                    {
                        stringFile[i] += $"{pattern[i]}";
                        pattern[i] = String.Empty;
                        continue;
                    }
                    stringFile[i] += fullKey ? pattern[i].Substring(0, replaceStartIndex) : String.Empty;
                    var key = pattern[i].Substring(replaceStartIndex, replaceEndIndex - replaceStartIndex + 1);
                    var tempKey = key;
                    var leftCountQuote = key.Count(item => item == '{');
                    var rightCountQuote = key.Count(item => item == '}');
                    if (leftCountQuote == rightCountQuote)
                    {
                        fullKey = true;
                        var firstSymbol = pattern[i].TrimStart()[0];
                        var leftIndent = pattern[i].Substring(0, pattern[i].IndexOf(firstSymbol));
                        pattern[i] = pattern[i].Remove(0, replaceEndIndex + 1);
                        key = key.Substring(1, key.Length - 2);
                        if (key.ToLower().Contains("list:"))
                        {
                            var nameListStart = key.IndexOf('"');
                            var nameListEnd = key.IndexOf('"', nameListStart + 1);
                            var nameList = key.Substring(nameListStart + 1, nameListEnd - nameListStart - 1).ToLower();
                            var itemPattern = String.Empty;

                            if (key.ToLower().Contains("item:"))
                            {
                                var itemTemplateStart = key.IndexOf('"', nameListEnd + 1);
                                var itemTemplateEnd = key.IndexOf('"', itemTemplateStart + 1);
                                itemPattern = key.Substring(itemTemplateStart + 1,
                                    itemTemplateEnd - itemTemplateStart - 1);
                            }

                            var baseModels = values[nameList] as IDictionary<string, object>;
                            if (baseModels == null)
                            {
                                stringFile += tempKey;
                                continue;
                            }

                            var index = 1;
                            foreach (var baseModel in baseModels)
                            {
                                if (string.IsNullOrEmpty(itemPattern))
                                {
                                    stringFile += baseModel.GetValues(baseModel)
                                        .Aggregate(index == 1 ? String.Empty : leftIndent,
                                            (current, item) =>
                                                (String.IsNullOrEmpty(current) ? String.Empty : current + " ") +
                                                item.Value);
                                    index++;
                                }
                                else
                                {
                                    stringFile += FormatString((index == 1 ? String.Empty : leftIndent) + itemPattern,
                                        baseModel.GetValues(baseModel));
                                    index++;
                                }
                            }

                        }
                        else
                        {
                            var formatToString = String.Empty;
                            if (key.Contains(':'))
                            {
                                formatToString = key.Substring(key.IndexOf(':') + 1);
                                key = key.Substring(0, key.IndexOf(':'));
                            }
                            if (!values.ContainsKey(key.ToLower()))
                            {
                                Logging.Write($"Не правильное название параметра {key}");
                                continue;
                            }
                            var value = values[key.ToLower()];
                            string stringValue;
                            var cultureValue = new CultureInfo("EN-us");
                            if (value is DateTime)
                            {
                                stringValue = ((DateTime) value).ToString(formatToString, cultureValue);
                            }
                            else if (value is TimeSpan)
                            {
                                stringValue = ((TimeSpan) value).ToString(formatToString, cultureValue);
                            }
                            else if (value == null)
                            {
                                stringValue = tempKey;
                            }
                            else
                            {
                                stringValue = value.ToString();
                            }
                            stringFile[i] += stringValue;
                        }
                        else
                        {
                            fullKey = false;
                        }
                    }
                    while (!string.IsNullOrEmpty(pattern[i])) ;
                } 
                return stringFile.ToArray();
            }

        }

        internal 
    }
}
