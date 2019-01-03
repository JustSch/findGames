using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace findGames
{
    class Program
    {
        static void Main()
        {
            //Will be set to a generic Directory?
            var STEAM_DIRECTORY = "d:\\SteamLibrary\\steamapps\\common";
            try
            {
                ArrayList fileArray = new ArrayList();

                string[] dirs = Directory.GetDirectories(@STEAM_DIRECTORY); // for Steam Directory
                Console.WriteLine("The Number of Game Directories: {0}.", dirs.Length);
                foreach(String dir in dirs)
                {
                    string[] fils = Directory.GetFiles(@dir, "*.exe");
                    
                    foreach (String fl in fils)
                    {
                        
                        if (AttemptLocateGameExe(fl, dir)) fileArray.Add(fl);
                 
                    }
                }
                PrintValues(fileArray);
                Console.WriteLine("Probably " + fileArray.Count +" Executables");
            }

            catch (Exception e)
            {
                Console.WriteLine("The Process Failed: {0}", e.ToString());
            }
            Console.WriteLine("Press Any Button To Exit.........................................");
            Console.Read();


        }
        //Used to Print From ArrayList Created Earlier
        public static void PrintValues(IEnumerable myList)
        {
            foreach (Object obj in myList)
                Console.WriteLine("   {0}", obj);
            Console.WriteLine();
        }
        //Attempts to Locate Steam Executables Using Patterns Noticed in My Steam Folders
        public static bool AttemptLocateGameExe(String filename, String directory)
        {
            Regex firstLetters = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");

            bool isDemo;

            String file = filename.Substring(filename.LastIndexOf("\\") + 1);
            String direc = directory.Substring(directory.LastIndexOf("\\") + 1);
            String direcNoSpace = Regex.Replace(direc, @"\s+", String.Empty);
            String directfirstLetter = firstLetters.Replace(direc, "$1");
            String directNoThe = "______________";
            String directNoSQuote = "___________";
            String directNoDemo = "________";
            if (direcNoSpace.Contains("The"))
            {
                directNoThe = direcNoSpace.Remove(direcNoSpace.IndexOf("The"), "The".Length);

            }
            if (direcNoSpace.Contains("'"))
            {
                directNoSQuote = direcNoSpace.Remove(direcNoSpace.IndexOf("'"), "'".Length);
            }
            if (direcNoSpace.Contains("Demo"))
            {
                directNoDemo = direcNoSpace.Remove(direcNoSpace.IndexOf("Demo"), "Demo".Length);
            }
            Console.WriteLine(file);
            Console.WriteLine(direc);
            Console.WriteLine(direcNoSpace);
            Console.WriteLine(directfirstLetter+"Tag");
            Console.WriteLine(string.Equals(file, direc + ".exe"));
            Console.WriteLine(directNoThe);

            if (string.Equals(file,direc+".exe", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(file, "launcher.exe", StringComparison.OrdinalIgnoreCase)) return true;

            if (string.Equals(file,"game.exe", StringComparison.OrdinalIgnoreCase)) return true;

            if (string.Equals(file,direcNoSpace+".exe",StringComparison.OrdinalIgnoreCase)) return true;


            if (string.Equals(file, directfirstLetter + ".exe", StringComparison.OrdinalIgnoreCase)) return true;

            if (file.Contains(direcNoSpace) ||  file.Contains(directNoThe) || file.Contains(directfirstLetter) || file.Contains(directNoSQuote))
            {
        
                if (file.Contains("Unity")) return false;

                return true;
            }

            if (file.Contains(directNoDemo)) return true;

            if (file.Contains("RPG_RT.exe")) return true;

            if (file.Contains("Dagon64.exe")) return true;

            if (file.Contains("SEGAGenesisClassics.exe")) return true;

            return false;
        }
    }
}
