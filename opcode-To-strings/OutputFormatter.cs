using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opcode_To_strings
{
    internal class OutputFormatter
    {
        
        string midFunc = "";
        string bottomFunc = "\n};";
        List<string> encodedPayload;
        List<string> keyWords;
        List<string> formattingTemp = new List<string>();

        public string formattedPayload = "";
        public string formattedKeyWords = "";

        public OutputFormatter(List<string> encodedPayload, List<string> keyWords)
        {
            this.encodedPayload = encodedPayload;
            this.keyWords = keyWords;

            FormatList(encodedPayload, "encodedPayload", ref formattedPayload);
            FormatList(keyWords, "keyWords", ref formattedKeyWords);
        }

        private void FormatList(List<string> inputList, string bufName, ref string outFormat)
        {
            int wordcount = 0;
            formattingTemp.Clear();
            formattingTemp.Add("readonly string[] " + bufName + " = new string[] {\n");

            int newlineTrigger = 0;
            inputList.ForEach(x =>
            {
                newlineTrigger++;
                if (newlineTrigger == 8) {
                    midFunc += "\n";
                    newlineTrigger = 0;
                }

                wordcount++;
                if (inputList.Count == wordcount)
                {
                    midFunc += ("\"" + x + "\""); // End of list, don't need comma
                }
                else
                {
                    midFunc += ("\"" + x + "\"" + ", ");
                }
                
            });
            formattingTemp.Add(midFunc);
            formattingTemp.Add(bottomFunc);

            outFormat = string.Join("", formattingTemp);
            formattingTemp.Clear();
            midFunc = "";
        }

        public string GetFormattedPayload()
        {
            return formattedPayload;
        }

        public string GetFormattedKeyWords()
        {
            return formattedKeyWords;
        }
    }
}
