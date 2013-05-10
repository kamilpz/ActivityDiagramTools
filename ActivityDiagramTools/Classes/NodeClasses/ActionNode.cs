namespace ActivityDiagramTools.Classes.NodeClasses
{
    using System;
    using System.Linq;

    class ActionNode : NodeElement
    {
        public String Target { get; set; }

        public override string GetSequence()
        {
            if (this.Used) return this.Name;
            
            this.Used = true;
            return String.Format(
                "Flow({0}, {1})", this.Name, AllElements.Single(e => e.Name == this.Target).GetSequence());
        }
    }
}
