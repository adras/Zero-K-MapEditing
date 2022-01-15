using MapCreationTool.WPF;

namespace MapCreationTool.Tabs
{
    public abstract class TabBase : PropertyChangedBase
    {
        string header;
        object content;

        public abstract string Header { get;  }
        public abstract object Content { get;  }

    }
}
