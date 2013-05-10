namespace ActivityDiagramTools.Classes
{
    using System.Text.RegularExpressions;

    public class LoopFinder
    {
        public static string FindLoopsInSequence(string Notation)
        {
            //Flow(Act5, Dec(Dec2, Act5, END))
            //should be changed to:
            //Loop(Dec2, Act5, END)

            //I look for: <<Whatever1>>Flow(ActionName, Dec(DecisionName, ActionName, <<Whatever2>>))<<Whatever3>>
            var findWhat = @"(.*)Flow\(([^\, ]+), Dec\(([^\, ]+), \2, ([^\, ]+)\)\)(.*)";

            //I replace it with: <<Whatever1>>Loop(DecisionName, ActionName, <<Whatever2>>))<<Whatever3>>
            var replaceWith = @"$1Loop($3, $2, $4)$5";

            return Regex.Replace(Notation, findWhat, replaceWith);
        }
    }
}
