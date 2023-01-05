using System.Text;
using System.Text.RegularExpressions;

namespace RemoteFinder.BLL.Helpers
{
    public static class TransliterationHelper
    {
        public static string Transliterate(string stringToTransliterate)
        {
            var stringTransliterated = new StringBuilder();

            if (stringToTransliterate.StartsWith("зг"))
            {
                stringTransliterated.Append("zgh");
                stringToTransliterate = stringToTransliterate.Remove(0, 3);
            }
            
            if (stringToTransliterate.StartsWith("Зг"))
            {
                stringTransliterated.Append("Zgh");
                stringToTransliterate = stringToTransliterate.Remove(0, 3);
            }
            
            if (stringToTransliterate.StartsWith("ЗГ"))
            {
                stringTransliterated.Append("ZGH");
                stringToTransliterate = stringToTransliterate.Remove(0, 3);
            }

            var transliterationMap = GetTransliterationMap();
            
            foreach (var str in stringToTransliterate)
            {
                var stringPosition = stringToTransliterate.IndexOf(str) == 0 
                    ? TransliterationPositionType.BeginOfWord 
                    : TransliterationPositionType.RestOfPositions;
                
                var isUpper = char.IsUpper(str);
                var transliteratedString = transliterationMap
                    .FirstOrDefault(t => 
                        string.Equals(t.UkrainianLetter, str.ToString(), StringComparison.InvariantCultureIgnoreCase) &&
                        (t.Position == stringPosition || t.Position == TransliterationPositionType.RestOfPositions)
                    )?.LatinLetter;

                if (string.IsNullOrEmpty(transliteratedString))
                {
                    transliteratedString = CheckIsEnglishString(str.ToString()) ? str.ToString() : string.Empty;
                }

                transliteratedString = isUpper ? transliteratedString?.ToUpper() : transliteratedString;
                
                stringTransliterated.Append(transliteratedString);
            }
            
            return stringTransliterated.ToString();
        }
        
        private static List<TransliterateItem> GetTransliterationMap()
        {
            return new List<TransliterateItem>()
            {
                new TransliterateItem {UkrainianLetter = "зг", LatinLetter = "zgh"},
                new TransliterateItem {UkrainianLetter = "а", LatinLetter = "a"},
                new TransliterateItem {UkrainianLetter = "б", LatinLetter = "b"},
                new TransliterateItem {UkrainianLetter = "в", LatinLetter = "v"},
                new TransliterateItem {UkrainianLetter = "г", LatinLetter = "h"},
                new TransliterateItem {UkrainianLetter = "ґ", LatinLetter = "g"},
                new TransliterateItem {UkrainianLetter = "д", LatinLetter = "d"},
                new TransliterateItem {UkrainianLetter = "е", LatinLetter = "e"},
                new TransliterateItem {UkrainianLetter = "є", LatinLetter = "ye", Position = TransliterationPositionType.BeginOfWord},
                new TransliterateItem {UkrainianLetter = "є", LatinLetter = "ie"},
                new TransliterateItem {UkrainianLetter = "ж", LatinLetter = "zh"},
                new TransliterateItem {UkrainianLetter = "з", LatinLetter = "z"},
                new TransliterateItem {UkrainianLetter = "и", LatinLetter = "y"},
                new TransliterateItem {UkrainianLetter = "і", LatinLetter = "i"},
                new TransliterateItem {UkrainianLetter = "ї", LatinLetter = "yi", Position = TransliterationPositionType.BeginOfWord},
                new TransliterateItem {UkrainianLetter = "ї", LatinLetter = "i"},
                new TransliterateItem {UkrainianLetter = "й", LatinLetter = "y", Position = TransliterationPositionType.BeginOfWord},
                new TransliterateItem {UkrainianLetter = "й", LatinLetter = "i"},
                new TransliterateItem {UkrainianLetter = "к", LatinLetter = "k"},
                new TransliterateItem {UkrainianLetter = "л", LatinLetter = "l"},
                new TransliterateItem {UkrainianLetter = "м", LatinLetter = "m"},
                new TransliterateItem {UkrainianLetter = "н", LatinLetter = "n"},
                new TransliterateItem {UkrainianLetter = "о", LatinLetter = "o"},
                new TransliterateItem {UkrainianLetter = "п", LatinLetter = "p"},
                new TransliterateItem {UkrainianLetter = "р", LatinLetter = "r"},
                new TransliterateItem {UkrainianLetter = "с", LatinLetter = "s"},
                new TransliterateItem {UkrainianLetter = "т", LatinLetter = "t"},
                new TransliterateItem {UkrainianLetter = "у", LatinLetter = "u"},
                new TransliterateItem {UkrainianLetter = "ф", LatinLetter = "f"},
                new TransliterateItem {UkrainianLetter = "х", LatinLetter = "kh"},
                new TransliterateItem {UkrainianLetter = "ц", LatinLetter = "ts"},
                new TransliterateItem {UkrainianLetter = "ч", LatinLetter = "ch"},
                new TransliterateItem {UkrainianLetter = "ш", LatinLetter = "sh"},
                new TransliterateItem {UkrainianLetter = "щ", LatinLetter = "shch"},
                new TransliterateItem {UkrainianLetter = "ю", LatinLetter = "yu", Position = TransliterationPositionType.BeginOfWord},
                new TransliterateItem {UkrainianLetter = "ю", LatinLetter = "iu"},
                new TransliterateItem {UkrainianLetter = "я", LatinLetter = "ya", Position = TransliterationPositionType.BeginOfWord},
                new TransliterateItem {UkrainianLetter = "я", LatinLetter = "ia"},
                new TransliterateItem {UkrainianLetter = "ь", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "'", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "`", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "`", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "‘", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "’", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "«", LatinLetter = ""},
                new TransliterateItem {UkrainianLetter = "»", LatinLetter = ""},
            };
        }

        private static bool CheckIsEnglishString(string strToCheck)
        {
            return Regex.IsMatch(strToCheck, "^[a-zA-Z0-9-_]*$");
        }
    }

    public class TransliterateItem
    {
        public TransliterateItem()
        {
            Position = TransliterationPositionType.RestOfPositions;
        }
        
        public string UkrainianLetter { get; set; }
        public string LatinLetter { get; set; }
        public TransliterationPositionType Position { get; set; }
    }

    public enum TransliterationPositionType
    {
        BeginOfWord,
        RestOfPositions
    }
}