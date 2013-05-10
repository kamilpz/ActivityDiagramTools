namespace ActivityDiagramTools.Classes.NodeClasses
{
    class FinalNode : NodeElement
    {
        public override string GetSequence()
        {
            return this.Name;
        }
    }
}
