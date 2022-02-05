using System.Collections.Generic;

namespace MapCreationTool.Helpers
{
    internal class ArgumentBuilder
    {
        List<string> arguments;

        public ArgumentBuilder()
        {
            arguments = new List<string>();
        }

        public void Clear()
        {
            arguments.Clear();
        }

        public void Add(string arg)
        {
            arguments.Add(arg);
        }


    }
}
