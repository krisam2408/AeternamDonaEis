using AeternamDonaEis.Classes.Serializables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeternamDonaEis.Classes
{
    public static class Generator
    {
        #region Generate
        public static string Generate(GenDataTransfer options)
        {
            string output = string.Empty;

            switch (options.Output)
            {
                case TextOutput.XML:
                case TextOutput.Json:
                    output = GenerateObject(options);
                    break;
                default:
                    output = GenerateText(options);
                    break;
            }

            return output;
        }

        private static string GenerateText(GenDataTransfer options)
        {
            string output = string.Empty;
            for (int i = 0; i < options.Quantity; i++)
            {
                output += GenerateSegment(options, i);
            }

            return StringEndFormat(output);
        }

        private static string GenerateObject(GenDataTransfer options)
        {
            Output output = new Output();

            switch (options.Type)
            {
                case GenerateType.Paragraphs:
                case GenerateType.Lists:
                    string title = string.Empty;
                    List<string> list = new List<string>();
                    for (int i = 0; i < options.Quantity; i++)
                    {
                        int iterationIndex = i;
                        int textIndex = 0;
                        while (iterationIndex >= Count(Texts.text[textIndex], options.Type))
                        {
                            iterationIndex -= Count(Texts.text[textIndex], options.Type);
                            if (textIndex < Texts.text.Length - 1) textIndex++;
                            else textIndex = 0;
                        }
                        if (iterationIndex == 0)
                        {
                            list = new List<string>();
                            if (options.TitleOption == TitleOptions.WithTitles) title = Texts.titles[textIndex];
                        }
                        if (options.Type == GenerateType.Paragraphs) list.Add(GetParagraph(Texts.text[textIndex], iterationIndex));
                        else list.Add(GetTextLine(Texts.text[textIndex], iterationIndex));
                        if (iterationIndex == Count(Texts.text[textIndex], options.Type) - 1 || i == options.Quantity - 1)
                            if (options.Type == GenerateType.Paragraphs)
                            {
                                Paragraphs item = new Paragraphs
                                {
                                    Title = title,
                                    Body = list
                                };
                                output.Text.Add(item);
                            }
                            else
                            {
                                Lists item = new Lists
                                {
                                    Title = title,
                                    ListItems = list
                                };
                                output.Text.Add(item);
                            }
                    }
                    break;
                default:
                    string body = string.Empty;
                    for (int i = 0; i < options.Quantity; i++)
                    {
                        body += GenerateSegment(options, i);
                    }
                    switch (options.Type)
                    {
                        case GenerateType.Words:
                            output.Text.Add(new Words
                            {
                                Body = StringEndFormat(body)
                            });
                            break;
                        case GenerateType.Letters:
                            output.Text.Add(new Letters
                            {
                                Body = StringEndFormat(body)
                            });
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("Option - Type", "Not a valid text type.");
                    }
                    break;
            }


            switch (options.Output)
            {
                case TextOutput.XML:
                    using (StringWriter reader = new StringWriter())
                    {
                        Type[] childrenTypes = { typeof(Letters), typeof(Words), typeof(Paragraphs), typeof(Lists) };
                        XmlSerializer xml = new XmlSerializer(typeof(Output), childrenTypes);
                        xml.Serialize(reader, output);
                        return reader.ToString();
                    }
                case TextOutput.Json:
                    return JsonConvert.SerializeObject(output, Formatting.Indented);
                default:
                    throw new ArgumentOutOfRangeException("Options - Output Format", "Not a valid output format.");
            }
        }

        private static string GenerateSegment(GenDataTransfer options, int index)
        {
            string output = string.Empty;

            int iterationIndex = index;
            int textIndex = 0;

            while (iterationIndex >= Count(Texts.text[textIndex], options.Type))
            {
                iterationIndex -= Count(Texts.text[textIndex], options.Type);
                if (textIndex < Texts.text.Length - 1) textIndex++;
                else textIndex = 0;
            }

            if (options.TitleOption == TitleOptions.WithTitles)
            {
                if (iterationIndex == 0) output += FormatTitle(Texts.titles[textIndex], index, options);
            }

            switch (options.Type)
            {
                case GenerateType.Letters:
                    output += FormatLetters(Texts.text[textIndex][iterationIndex], index, options);
                    break;
                case GenerateType.Lists:
                    bool start = iterationIndex == 0 ? true : false;
                    bool end = iterationIndex == CountLines(Texts.text[textIndex]) - 1 || index == options.Quantity - 1 ? true : false;
                    output += FormatLists(GetTextLine(Texts.text[textIndex], iterationIndex), index, start, end, options);
                    break;
                case GenerateType.Paragraphs:
                    output += FormatParagraphs(GetParagraph(Texts.text[textIndex], iterationIndex), index, options);
                    break;
                default:
                    output += FormatWords(GetWord(Texts.text[textIndex], iterationIndex), index, options);
                    break;
            }

            return output;
        }

        private static IOutput GenerateTextItem(GenDataTransfer options, int index)
        {
            IOutput output;

            int iterationIndex = index;
            int textIndex = 0;

            while (iterationIndex >= Count(Texts.text[textIndex], options.Type))
            {
                iterationIndex -= Count(Texts.text[textIndex], options.Type);
                if (textIndex < Texts.text.Length - 1) textIndex++;
                else textIndex = 0;
            }

            switch (options.Type)
            {
                case GenerateType.Lists:
                    Lists lists = new Lists();
                    if (options.TitleOption == TitleOptions.WithTitles)
                    {
                        if (iterationIndex == 0) lists.Title = Texts.titles[textIndex];
                    }
                    lists.ListItems.Add(GetTextLine(Texts.text[textIndex], iterationIndex));
                    output = lists;
                    break;
                case GenerateType.Paragraphs:
                    Paragraphs paragraph = new Paragraphs();
                    if (options.TitleOption == TitleOptions.WithTitles)
                    {
                        if (iterationIndex == 0) paragraph.Title = Texts.titles[textIndex];
                    }
                    paragraph.Body.Add(GetParagraph(Texts.text[textIndex], iterationIndex));
                    output = paragraph;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Options - Type", "Not a valid text type.");
            }

            return output;
        }
        #endregion

        #region Getters
        private static string GetWord(string text, int index)
        {
            char[] separator = { ' ' };
            string[] words = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return string.Format("{0} ", words[index]);
        }

        private static string GetTextLine(string text, int index)
        {
            char[] separator = { '.' };
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return string.Format("{0}.", lines[index].Trim());
        }

        private static string GetParagraph(string text, int index)
        {
            string[] separator = { "\n" };
            string[] paragraphs = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return paragraphs[index];
        }

        #endregion

        #region Output Format
        private static string StringEndFormat(string text)
        {
            char[] seqs = { ' ', ',' };
            foreach (char lc in seqs)
            {
                while (text[text.Length - 1] == lc)
                {
                    text = text.Substring(0, text.Length - 1);
                }
            }

            return text;
        }

        private static string FormatTitle(string text, int index, GenDataTransfer options)
        {
            string output = string.Empty;

            switch (options.Output)
            {
                case TextOutput.HTML:
                    output += string.Format("<h2>{0}</h2>\n", text);
                    break;
                case TextOutput.Raw:
                    output += string.Format("{0}\n", text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Options - Output Format", "Not a valid output format.");
            }

            return output;
        }

        private static string FormatLetters(char character, int index, GenDataTransfer options)
        {
            string output = string.Empty;

            if (index == 0)
            {
                switch (options.Output)
                {
                    case TextOutput.HTML:
                        output += "<p>";
                        break;
                    default:
                        break;
                }
            }

            output += character;

            if (index == options.Quantity - 1)
            {
                switch (options.Output)
                {
                    case TextOutput.HTML:
                        output += "</p>";
                        break;
                    default:
                        break;
                }
            }

            return output;
        }

        private static string FormatLists(string text, int index, bool isListStart, bool isListEnd, GenDataTransfer options)
        {
            string output = string.Empty;

            switch (options.Output)
            {
                case TextOutput.HTML:
                    output += string.Format("{1}\t<li>{0}</li>\n{2}", text, isListStart ? "<ul>\n" : "", isListEnd ? "</ul>\n" : "");
                    break;
                case TextOutput.Raw:
                    output += string.Format("- {0}\n", text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Options - Output Format", "Not a valid output format.");

            }

            return output;
        }

        private static string FormatParagraphs(string text, int index, GenDataTransfer options)
        {
            string output = string.Empty;

            switch (options.Output)
            {
                case TextOutput.HTML:
                    output += string.Format("{1}<p>{0}</p>\n", text, options.TitleOption == TitleOptions.WithTitles ? "\t" : "");
                    break;
                case TextOutput.Raw:
                    output += string.Format("{0}\n", text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Options - Output Format", "Not a valid output format.");
            }

            return output;
        }

        private static string FormatWords(string text, int index, GenDataTransfer options)
        {
            string output = string.Empty;

            if (index == 0)
            {
                switch (options.Output)
                {
                    case TextOutput.HTML:
                        output += "<p>";
                        break;
                    default:
                        break;
                }
            }

            output += string.Format("{0}{1}", text, index == options.Quantity - 1 ? "" : " ");

            if (index == options.Quantity - 1)
            {
                switch (options.Output)
                {
                    case TextOutput.HTML:
                        output += "</p>";
                        break;
                    default:
                        break;
                }
            }

            return output;
        }

        #endregion

        #region Count
        public static int Count(string text, GenerateType type)
        {
            int cnt = 0;
            switch (type)
            {
                case GenerateType.Lists:
                    cnt = CountLines(text);
                    return cnt;
                case GenerateType.Letters:
                    cnt = text.Length;
                    return cnt;
                case GenerateType.Paragraphs:
                    cnt = CountParagraphs(text);
                    return cnt;
                default:
                    cnt = CountWords(text);
                    return cnt;
            }
        }

        private static int CountWords(string text)
        {
            char[] separator = { ' ' };
            string[] words = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private static int CountLines(string text)
        {
            char[] separator = { '.' };
            string[] lines = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return lines.Length;
        }

        private static int CountParagraphs(string text)
        {
            string[] separator = { "\n" };
            string[] paragraphs = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return paragraphs.Length;
        }

        #endregion

        #region Expression Format
        public static string Minify(string stringChain, TextOutput output)
        {
            while (stringChain.Contains("\r"))
            {
                stringChain = stringChain.Replace("\r", "");
            }

            while (stringChain.Contains("\n"))
            {
                stringChain = stringChain.Replace("\n", "");
            }

            switch (output)
            {
                case TextOutput.Json:
                    string[] seqs = { "{ ", "} ", "[ ", "] ", ": ", "  ", "\" ", ", {", ", [", ", \"" };
                    foreach (string str in seqs)
                    {
                        while (stringChain.Contains(str))
                        {
                            stringChain = stringChain.Replace(str, str.Length == 2 ? str[0].ToString() : str[0].ToString() + str.Substring(2));
                        }
                    }
                    break;
                case TextOutput.XML:
                    while (stringChain.Contains("> "))
                    {
                        stringChain = stringChain.Replace("> ", ">");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Output Format", "Not a valid output format.");
            }


            return stringChain;
        }
        #endregion
    }
}
