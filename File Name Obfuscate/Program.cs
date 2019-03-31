using System;
using System.IO;
using System.Text;

namespace File_Name_Obfuscate
{
    class Program
    {
        private static Random _rnd;
        private static readonly char[] ValidCharsFilename = {'a', 'b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A', 'B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z','1','2','3','4','5','6','7','8','9'};
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "-h")
            {
                Console.WriteLine("Developed by Hirbod Behnam");
                Console.WriteLine("Source at https://github.com/HirbodBehnam/File-Name-Obfuscate");
                Console.WriteLine();
                Console.WriteLine("A simple program to change all filenames in a folder.");
                Console.WriteLine("How to obfuscate: Just run the program in the folder you wish to obfuscate it's filenames. Program creates a file named FileInfo.db");
                Console.WriteLine("How to de-obfuscate: Run program in the folder with obfuscated files in with argument of FileInfo.db path.");
            }
            if (args.Length == 1)//Decrypt
            {
                foreach (string fileNames in File.ReadAllLines(args[0]))
                {
                    try
                    {
                        string[] f = fileNames.Split('|');
                        File.Move(f[1],f[0]);
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Error on file name " + fileNames + " : ");
                        Console.WriteLine(ex.Message);
                    }
                }
                return;
            }
            StringBuilder database = new StringBuilder();
            string[] filesWithPath = Directory.GetFiles(Environment.CurrentDirectory);
            string[] newFileNames = new string[filesWithPath.Length];
            _rnd = new Random();
            {//Generate random file names and do not include this file in it
                int myIndexFileName = 0;
                string thisName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                for (int i = 0; i < filesWithPath.Length; i++)
                {
                    if (filesWithPath[i] == thisName)
                    {
                        myIndexFileName = i;
                        continue;
                    }
                    do
                    {
                        newFileNames[i] = RandomFileName();
                    } while (ExistsInArray(newFileNames, newFileNames[i], i));
                }
                newFileNames = RemoveAt(newFileNames, myIndexFileName);
                filesWithPath = RemoveAt(filesWithPath, myIndexFileName);
            }
            for (int i=0;i<newFileNames.Length;i++)//Build the database
            {
                try
                {
                    //Rename files
                    File.Move(filesWithPath[i], newFileNames[i]);
                    //Write file to database
                    filesWithPath[i] = Path.GetFileName(filesWithPath[i]);
                    database.Append(filesWithPath[i]);
                    database.Append('|');
                    database.AppendLine(newFileNames[i]);
                }
                catch (Exception ex)
                {
                    Console.Write("Error on file name " + filesWithPath[i] + " : ");
                    Console.WriteLine(ex.Message);
                }
            }
            //Save the database
            File.WriteAllText("FileInfo.db",database.ToString());
        }
        /// <summary>
        /// Generates random file name with 12 letters long
        /// </summary>
        /// <returns>A random file name</returns>
        private static string RandomFileName()
        {
            char[] name = new char[12];
            for (int i = 0; i < 12; i++)
                name[i] = ValidCharsFilename[_rnd.Next(ValidCharsFilename.Length)];
            name[9] = '.';
            return new string(name);
        }
        /// <summary>
        /// Check if an element exists in an array up to an index
        /// </summary>
        /// <param name="array">The array to search in</param>
        /// <param name="toCheck">The element to check</param>
        /// <param name="to">Index to search up to it</param>
        /// <returns>True if exists</returns>
        private static bool ExistsInArray(string[] array,string toCheck ,int to)
        {
            for(int i = 0;i<to;i++)
                if (array[i] == toCheck)
                    return true;
            return false;
        }
        /// <summary>
        /// Removes an element from array https://stackoverflow.com/a/457501/4213397
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The array to remove from</param>
        /// <param name="index">The index to remove</param>
        /// <returns></returns>
        private static T[] RemoveAt<T>(T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if( index > 0 )
                Array.Copy(source, 0, dest, 0, index);
            if( index < source.Length - 1 )
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            return dest;
        }
    }
}
