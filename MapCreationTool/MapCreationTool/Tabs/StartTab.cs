using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Tabs
{
    internal class StartTab : TabBase
    {
        public override string Header { get => "Start"; set => throw new InvalidOperationException(); }
        public override object Content { get => null; set => throw new InvalidOperationException(); }
    }
}
