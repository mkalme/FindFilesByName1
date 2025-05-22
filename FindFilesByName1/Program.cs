using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FindFilesByName1
{
    class Program
    {
        private static string filenameFolderPath = @"D:\Recovered Files\Found\.class";
        private static string destinationFolderPath = @"D:\Delete\Known\Compare With";

        private static string[] filePaths = { };
        private static string[] destinationAllPaths = { };

        private static List<string> allNames = new List<string>();

        private static List<string> foundFiles = new List<string>();

        static void Main(string[] args)
        {
            loadFilePaths();
            loadAllNames();
            findFiles();

            printAllPaths();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void loadFilePaths() {
            filePaths = Directory.GetFiles(filenameFolderPath, "*.*", SearchOption.AllDirectories);
            destinationAllPaths = Directory.GetFiles(destinationFolderPath, "*.*", SearchOption.AllDirectories);
        }
        static void loadAllNames() {
            for (int i = 0; i < filePaths.Length; i++) {
                string name = Path.GetFileNameWithoutExtension(filePaths[i]).ToLower();

                if (!allNames.Contains(name)) {
                    allNames.Add(name);
                }
            }
        }
        static void findFiles() {
            for (int i = 0; i < destinationAllPaths.Length; i++) {
                for (int b = 0; b < allNames.Count; b++) {
                    string fileName = Path.GetFileNameWithoutExtension(destinationAllPaths[i]).ToLower();

                    if (allNames[b].Contains(fileName) || fileName.Contains(allNames[b])) {
                        //if (allNames[b].Equals(fileName)) {
                        foundFiles.Add(fileName + " || " + allNames[b] + "\t\t\t\t" + destinationAllPaths[i]);
                        b = allNames.Count;
                    }
                }

                if (i % 500 == 0) {
                    Console.WriteLine(i + " / " + destinationAllPaths.Length);
                }
            }
        }

        static void printAllPaths() {
            for (int i = 0; i < foundFiles.Count; i++) {
                Console.WriteLine(foundFiles[i]);
            }

            using (StreamWriter sw = File.CreateText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\allfiles.txt"))
            {
                sw.WriteLine(filenameFolderPath);
                sw.WriteLine(destinationFolderPath + "\r\n\r\n\r\n");
                sw.WriteLine(String.Join("\r\n", foundFiles.ToArray()));
            }
        }
    }
}
