using System.Runtime.InteropServices;

namespace opcode_To_strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inputs
            string binFile = "";
            string wordlistFile = ""; 
            string outFile = ".\\output.cs";
            List<string> largs = [.. args]; // Convert from string[] to List


            // Display help menu
            if (args.Contains("-h") || largs.Count < 1)
            {
                Console.WriteLine(
                "This tool will transform binary data into a byte table of strings, and save to output file.\n\n" +
                "-b <file>\tPath to .bin that will be encoded\n" +
                "-w <file>\tPath to wordlist\n" +
                "-o <file>\tOutput file path [optional]\n" +
                "\n\nExample:\nopcode-To-strings.exe -b \"C:\\Users\\You\\Desktop\\fun.bin\" -w .\\wordlist.txt -o .\\result\\cool.cs"
                );
                return;
            }

            // Apply arguments if they're found
            largs.ForEach(x =>
            {
                try
                {
                    if (x == "-b")
                    {
                        var index = largs.IndexOf("-b");
                        binFile = args.ElementAt(index + 1);
                    }

                    if (x == "-w")
                    {
                        var index = largs.IndexOf("-w");
                        wordlistFile = args.ElementAt(index + 1);
                    }

                    if (x == "-o")
                    {
                        var index = largs.IndexOf("-o");
                        outFile = args.ElementAt(index + 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[-] Issue parsing input parameters." + ex.Message);
                    return;
                }
            });

            Console.WriteLine("[.] Reading wordlist and binary file");
            try
            {
                var wat = File.GetAttributes(binFile);
                wat.GetHashCode();

                string[] wordsForEncoder = File.ReadAllLines(wordlistFile);
                byte[] rawBinaryToEncode = File.ReadAllBytes(binFile);

                Console.WriteLine("[.] Creating string byte map");
                StringByteMap stringBitMap = new StringByteMap(wordsForEncoder, rawBinaryToEncode);
                Console.WriteLine("[.] Formatting output");
                OutputFormatter outForm = new OutputFormatter(stringBitMap.encodedPayload, stringBitMap.keyWords);
            

                Console.WriteLine("[.] Saving key words and encoded payload");
                using (StreamWriter sw = File.CreateText(outFile))
                {
                    sw.Write(outForm.GetFormattedKeyWords());
                    sw.Flush();
                    sw.Write("\n\n" + outForm.GetFormattedPayload());
                    sw.Flush();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("[-] ERROR:\n\n" + ex.Message);
            }

            Console.WriteLine("[+] Saved words key and encoded payload to " + outFile);
        }

    }

}
