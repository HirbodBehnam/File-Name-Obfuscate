# File Name Obfuscate
A program to obfuscate all file names in a folder
## What?
This program generates random file names and renames all files in a folder (not subfolders) to them. It creates a file named `FileInfo.db`. This file stores all old filenames in it. You can de-obfuscate your files by passing `FileInfo.db` path to the program.

File Name Obfuscate *does not* change file context. It only renames the files.
### How to use
At first download the executable file from [releases](https://github.com/HirbodBehnam/File-Name-Obfuscate/releases). Then place the exe file inside the folder you wish to obfuscate it files. Then simply run the program.

To de-obfuscate a folder's file names, place `File Name Obfuscate.exe` inside that folder and pass `FileInfo.db` location as an argument to the program.
#### Example
Before:

![Before](https://i.imgur.com/6rBLZ9L.png)

After:

![After](https://i.imgur.com/6KTvzSa.png)
