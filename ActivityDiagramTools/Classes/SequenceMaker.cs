namespace ActivityDiagramTools.Classes
{
    using System.Linq;

    using ActivityDiagramTools.Classes.NodeClasses;

    public class SequenceMaker
    {
        public static string GetSequence(string FilePath)
        {
            var xmlNodes = XMLNodesGetter.GetXMLNodes(FilePath);
            var nodes = NodesMaker.GetNodes(xmlNodes);
            NodeElement.AllElements = nodes.ToList();
            var initialNode = nodes.Single(n => n.Name.Contains("Initial"));
            var sequence = initialNode.GetSequence();
            return LoopFinder.FindLoopsInSequence(sequence);
        }
    }
}
