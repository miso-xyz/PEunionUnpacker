# PEunionUnpacker
Manual Unpacker for PEunion (tested on 3.1.5), kinda unstable, can be improved a lot more

The main application works, you can successfully unpack & decrypt strings from protected applications by PEunion

Reading the documentation is highly recommended before/while using the application.
# Tools provided in the app
* Info about the protected file
  * Protected File(s) name
  * Packed File(s) Size (in bytes)
  * Extraction Folder (returns type & path)
  * Process Arguments
  * Shows if internal file(s) are encrypted &/or compressed
  * Shows what anti's are enabled
  * Shows parameters

* String Decryptor
* RBU (Raw Bytes Unpacker)

# Possible Improvements
* Better size measurement of packed file
* Automatically extract the bytes out of the packed application instead of manually selecting the packed bytes and pasting them in the RBU
* Info prompt can struggle if instructions arent at expected indexes

# Credits
[UweKeim (.NET Transitions)](https://github.com/UweKeim/dot-net-transitions),
[0xd4d (dnlib)](https://github.com/0xd4d/dnlib) & 
[miyako (idk)](https://github.com/miyakoyakota)
