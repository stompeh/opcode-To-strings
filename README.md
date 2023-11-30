# Info
Created using .NET 8  
The project transforms bytecode into a string byte table (size 256), attempting static and possibly dynamic evasion. Binary information is usually represented with strings using Base64.  
A problem with storing Base64 in an implant is how random it may seem, increasing file entropy, and can indicate there's encrypted data. 
Encryption can sometimes trigger AV and EDR products to throw a flag on the binary if the object entropy is high enough.  
What about plain old English?

Default output is in C# style, but can be used in nearly all languages.  

# Getting Started
Create bytecode and save into a .bin
This tool will transcode binary data into a byte table of strings  
`-b <file> Path to .bin that will be encoded`  
`-w <file> Path to wordlist`  
`-o <file> Output file path [optional]`  
Example:  
`opcode-To-strings.exe -b "C:\Users\You\Desktop\fun.bin" -w .\wordlist.txt -o .\repo\cool.cs`  

# TODO:
- Replace specific classes/objects for better efficiency  
- Polish
