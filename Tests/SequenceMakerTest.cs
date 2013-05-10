namespace Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ActivityDiagramTools.Classes;

    [TestClass]
    public class SequenceMakerTest
    {
        [TestMethod]
        public void MakesCorrectSequence()
        {
            var correctSequence =
                "Flow(Initial1, Flow(Act1, Flow(Act2, Dec(Decision1, Flow(Act4, Loop(Decision2, Act5, ActivityFinal1)), Flow(Act3, Fork(Fork1, Flow(Act6, Flow(Act8, ActivityFinal1)), Flow(Act7, ActivityFinal1)))))))";
            Assert.AreEqual(correctSequence, SequenceMaker.GetSequence(@"TestActivityDiagram.xml"));
        }

    }
}
