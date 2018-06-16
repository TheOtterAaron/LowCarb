using System;
using System.IO;
using System.Text;

namespace FileBaker
{
    class FillprojectCommand : ICommand
    {
        public int Execute(Invocation invocation)
        {
            if (invocation.Arguments.Count == 0)
            {
                Console.WriteLine("bake: 'fillproject' command requires a project argument.");
                return 302;
            }
            
            if (!invocation.OptionsContainsAlias("-F", "--file"))
            {
                Console.WriteLine("bake: 'fillproject' command requires the '--file' option.");
                return 303;
            }

            string project = invocation.Arguments[0];
            string file = invocation.OptionsAtAlias("-F", "--file");
            EProjectFiletype filetype = EProjectFiletype.Contract;

            if (invocation.OptionsContainsAlias("-t", "--filetype"))
            {
                string filetypeString = invocation.OptionsAtAlias("-t", "--filetype");
                switch (filetypeString)
                {
                    case "contract":
                        filetype = EProjectFiletype.Contract;
                        break;
                    case "unittest":
                        filetype = EProjectFiletype.UnitTest;
                        break;
                    default:
                        Console.WriteLine(String.Format("bake: '{0}' is not a valid '--filetype' value.", filetypeString));
                        return 303;
                }
            }

            if (!File.Exists(project))
            {
                Console.WriteLine(String.Format("bake: '{0}' could not be found.", project));
                return 304;
            }
            
            try
            {
                CSProjectRewriter.RewriteProject(project, file, filetype);
                Console.WriteLine(String.Format("Filled the project at '{0}' with '{1}' and a little jelly.", project, file));
            }
            catch (Exception e)
            {
                Console.WriteLine("bake: " + e.Message);
                return 301;
            }

            return 0;
        }

        public string GetHelpMessage()
        {
            StringBuilder help = new StringBuilder();
            help.AppendLine("bake fillproject [-t | --filetype=(contract|unittest)] -F | --file=<filepath> <project>");
            help.AppendLine();
            help.AppendLine("     Adds the specified file to a given project.  File may be a source file or");
            help.AppendLine("     a content file and should be provided as a relative path from the project's");
            help.AppendLine("     source directory.");

            return help.ToString();
        }
    }
}
