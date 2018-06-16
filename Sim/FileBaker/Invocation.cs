using System.Collections.Generic;

namespace FileBaker
{
    internal class Invocation
    {
        public Invocation()
        {
            Command = "";
            Arguments = new List<string>();
            Options = new Dictionary<string, string>();
        }

        public bool OptionsContainsAlias(params string[] list)
        {
            bool keyFound = false;

            foreach (string s in list)
            {
                if (Options.ContainsKey(s))
                {
                    keyFound = true;
                    break;
                }
            }

            return keyFound;
        }

        public string OptionsAtAlias(params string[] list)
        {
            string value = "";

            foreach (string s in list)
            {
                if (Options.ContainsKey(s))
                {
                    value = Options[s];
                    break;
                }
            }

            return value;
        }

        public string Command { get; set; }
        public List<string> Arguments { get; }
        public Dictionary<string, string> Options { get; }
    }
}
