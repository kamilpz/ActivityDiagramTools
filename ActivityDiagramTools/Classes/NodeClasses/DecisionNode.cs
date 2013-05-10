namespace ActivityDiagramTools.Classes.NodeClasses
{
    using System;
    using System.Linq;

    class DecisionNode : NodeElement
    {
        public String IfTrueTarget { get; set; }
        public String ElseTarget { get; set; }

        public override string GetSequence()
        {
            if (this.Used) return this.Name;

            this.Used = true;

            return String.Format(
                "Dec({0}, {1}, {2})",
                this.Name,
                AllElements.Single(e => e.Name == this.IfTrueTarget).GetSequence(),
                AllElements.Single(e => e.Name == this.ElseTarget).GetSequence());
        }
    }
}
