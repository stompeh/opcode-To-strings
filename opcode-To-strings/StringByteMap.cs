using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace opcode_To_strings
{
    internal class StringByteMap
    {
        public List<string> keyWords { get; }
        public List<string> encodedPayload { get; }

        public StringByteMap(string[] wordlist, byte[] rawBinaryToEncode)
        {
            keyWords = new List<string>();
            encodedPayload = new List<string>();

            if (!SelectKeyWordsFromWordlist(wordlist))
            {
                Console.WriteLine("[-] Error. Stopping operation at word selection from wordlist.");
                return;
            }

            EncodePayload(rawBinaryToEncode);
        }

        private bool SelectKeyWordsFromWordlist(string[] wordlist)
        {
            try
            {
                if (wordlist.Length == 0)
                {
                    Console.WriteLine("Supplied wordlist is empty!");
                    return false;
                }

                string currentword = "";
                Random random = new Random();
                foreach (var dummy in Enumerable.Range(0, 256))
                {
                    // CHECK FOR DUPLICATES
                    dupecheck:
                    currentword = wordlist.ElementAt(random.Next(wordlist.Length));
                    if (keyWords.Contains(currentword))
                    {
                        goto dupecheck;
                    }
                    keyWords.Add(currentword);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }
        private void EncodePayload(byte[] rawBinToEncode)
        {
            foreach (byte index in rawBinToEncode)
            {
                encodedPayload.Add(keyWords[index]);
            }
        }

    }
}
