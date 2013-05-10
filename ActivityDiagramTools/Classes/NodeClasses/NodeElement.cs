namespace ActivityDiagramTools.Classes.NodeClasses
{
    using System;
    using System.Collections.Generic;

    public abstract class NodeElement
    {
        public static List<NodeElement> AllElements { get; set; }

        public String Name { get; set; }

        public bool Used { get; set; }

        public abstract string GetSequence();
    }
}
