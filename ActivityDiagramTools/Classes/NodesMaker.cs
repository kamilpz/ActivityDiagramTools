namespace ActivityDiagramTools.Classes
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Linq;
    using ActivityDiagramTools.Classes.NodeClasses;

    class NodesMaker
    {
        public static IEnumerable<NodeElement> GetNodes(IEnumerable<XElement> Nodes)
        {
            IEnumerable<NodeElement> actionAndInitialNodes = getActionAndInitialNodes(Nodes);
            IEnumerable<NodeElement> decisionNodes = getDecisionNodes(Nodes);
            IEnumerable<NodeElement> forkNodes = getForkNodes(Nodes);
            IEnumerable<NodeElement> finalNodes = getFinalNodes(Nodes);

            return actionAndInitialNodes.Concat(decisionNodes).Concat(forkNodes).Concat(finalNodes);
        }

        private static IEnumerable<ActionNode> getActionAndInitialNodes(IEnumerable<XElement> Nodes)
        {
            return from node in Nodes
                   where node.Name.LocalName == "opaqueAction" || node.Name.LocalName == "initialNode"
                   select
                       new ActionNode()
                           {
                               Name = node.Attribute("name").Value,
                               Target =
                                   node.Element("flowNodeTargets")
                                       .Element("controlFlow")
                                       .Elements().Single(e => e.Name.LocalName.Contains("Moniker"))
                                       .Attribute("LastKnownName")
                                       .Value
                           };
        }
        private static IEnumerable<DecisionNode> getDecisionNodes(IEnumerable<XElement> Nodes)
        {
            return from node in Nodes
                   where node.Name.LocalName == "decisionNode"
                   select
                       new DecisionNode()
                       {
                           Name = node.Attribute("name").Value,
                           IfTrueTarget = 
                               node.Element("flowNodeTargets")
                                   .Elements("controlFlow").Single(e => (e.Attributes("guard").Any() && e.Attribute("guard").Value == "True"))
                                   .Elements().Single(e=>e.Name.LocalName.Contains("Moniker"))
                                   .Attribute("LastKnownName")
                                   .Value,
                           ElseTarget =
                               node.Element("flowNodeTargets")
                                   .Elements("controlFlow").Single(e => e.Attributes("guard").Any() == false)
                                   .Elements().Single(e => e.Name.LocalName.Contains("Moniker"))
                                   .Attribute("LastKnownName")
                                   .Value
                       };
        }
        private static IEnumerable<ForkNode> getForkNodes(IEnumerable<XElement> Nodes)
        {
            return from node in Nodes
                   where node.Name.LocalName == "forkNode"
                   select
                       new ForkNode()
                       {
                           Name = node.Attribute("name").Value,
                           Targets = 
                               node.Element("flowNodeTargets")
                                   .Elements("controlFlow").Select(e=>e
                                       .Elements().Single(el => el.Name.LocalName.Contains("Moniker"))
                                       .Attribute("LastKnownName")
                                       .Value)
                       };
        }
        private static IEnumerable<FinalNode> getFinalNodes(IEnumerable<XElement> Nodes)
        {
            return from node in Nodes
                   where node.Name.LocalName == "activityFinalNode"
                   select
                       new FinalNode
                       {
                           Name = node.Attribute("name").Value
                       };
        }
    }
}
