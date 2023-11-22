# Info
The project transforms byte code into string representation using a byte table, attempting static and possibly dynamic evasion. Binary information is usually represented with strings using Base64. The problem with storing Base64 in an implant is how random it may seem. This increases file entropy, the measurement of randomization, and can indicate there's encryption. Encryption can trigger AV and EDR products to throw a flag on the binary. What about plain old English?

# Getting Started
This tool will transcode binary data into a byte table of strings  
`-b <file> Path to .bin that will be encoded`  
`-w <file> Path to wordlist`  
`-o <file> Output file path [optional]`  
Example:  
`opcode-To-strings.exe -b "C:\Users\You\Desktop\fun.bin" -w .\wordlist.txt -o .\repo\cool.cs`  

# TODO:
- format code to output filetype style
- Implement decryption project  
- Replace specific objects for better efficiency  
- Polish  
