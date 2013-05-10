using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    using ActivityDiagramTools.Classes;

    [TestClass]
    public class LoopFinderTest
    {
        [TestMethod]
        public void FindsLoopsInSequences()
        {
            var sequenceWithLoop = "Flow(Act5, Dec(Dec2, Act5, END))";

            Assert.AreEqual("Loop(Dec2, Act5, END)", LoopFinder.FindLoopsInSequence(sequenceWithLoop));
        }
    }
}
