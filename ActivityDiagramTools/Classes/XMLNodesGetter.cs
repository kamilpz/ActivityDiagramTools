namespace ActivityDiagramTools.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    class XMLNodesGetter
    {
        public static IEnumerable<XElement> GetXMLNodes(string FilePath)
        {
            try
            {
                var document = XDocument.Load(FilePath);
                var e =
                    document.Element("activityRootModel")
                            .Element("packagedElements")
                            .Element("activityRootModelHasActivity")
                            .Element("activity")
                            .Element("nodes");

                if (!e.HasElements) throw new Exception();

                return e.Elements();
            }
            catch (Exception)
            {
                throw new Exception("Incorrect activity diagram file.");
            }
        }
    }
}
