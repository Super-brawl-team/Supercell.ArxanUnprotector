# Supercell.ArxanFucker - A tool to remove and re-protect Arxan protection from Supercell's games
Forked from https://github.com/Mimi8298/Supercell.ArxanUnprotector tweaked by jordi with luv :3

**This fork can print the xor key and allows you to kill strings encryption if you want to (you still need to update-crc once you edited)**

This is a tool to remove and re-protect Arxan protection from Supercell's games. It facilitates the reversal of Arxan Protection in Supercell Games on Android (arm and arm64) and iOS (arm64) platforms.

## Requirements
* .Net 7 (https://dotnet.microsoft.com/download/dotnet/7.0)
* Windows or Mac OS X (arm64) / any platform that supports .Net 7 but you need to compile Capstone yourself

## Usage
```
Usage: Supercell.ArxanUnprotector -a <action> -i <original> -m <modified> [-o <output>] -e (kill strings encryption / reimplement it)
Actions:
    verify-crc - Verify checksums in modified file
    update-crc - Update checksums in modified file and save to output file
    decrypt - Decrypt strings in original file and save to output file
    encrypt - Encrypt strings in modified file and save to output file
```
## Tutorial
To use Supercell.ArxanUnprotector you firstly need to decrypt the strings of the lib, then open it in ida/ghidra and do your modification. Then reencrypt strings (if you havent killed string encryption) and once its done you need to update the checksums and you should be done.

## Examples
- ```Supercell.ArxanUnprotector -a verify-crc -i liboriginal.so -m libmodified.so```: Verify if the checksums in libmodified.so are correct
- ```Supercell.ArxanUnprotector -a update-crc -i liboriginal.so -m libmodified.so -o libg.so```: Update the checksums in libmodified.so to get the final libg.so
- ```Supercell.ArxanUnprotector -a decrypt -i liboriginal.so -o liboriginal.so.decrypted -e (if u want do kill strings encryption)```: Decrypt strings in liboriginal.so and save to liboriginal.so.decrypted. Note that you need to reencrypt strings if you havent killed it, and you will also need to update the checksums
- ```Supercell.ArxanUnprotector -a encrypt -i liboriginal.so -m liboriginal.so.decrypted -o liboriginal.so.encrypted -e (if you want to bring back string encryption)```: Encrypt strings in liboriginal.so.decrypted and save to liboriginal.so.encrypted. Note that you need to update the checksums after encrypting the strings.

## Contact
You can contact the original owner on Discord: ```mimi8297```.
I don't provide any support for this tool but I'm happy to answer any questions you may have.
But you can also dm me ```depresiveprimo``` if you need any support for the tool
