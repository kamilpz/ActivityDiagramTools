namespace ActivityDiagramTools.Classes.NodeClasses
{
    using System.Collections.Generic;
    using System.Linq;

    class ForkNode : NodeElement
    {
        public IEnumerable<string> Targets { get; set; }

        public override string GetSequence()
        {
            if (this.Used) return this.Name;
            
            this.Used = true;

            var notation = "Fork(" + this.Name;

            foreach (var target in this.Targets) notation += ", " + AllElements.Single(e => e.Name == target).GetSequence();

            notation += ")";

            return notation;
        }
    }
}
