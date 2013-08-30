using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagramTools.Classes
{
    class SequenceRepresentation
    {
        public static string GetRepresentation(SequenceMaker maker, List<string> sequences, Sequence sequence)
        {
            string result = "";

            sequence.Argument1 = PrepareArgument(maker, sequences, sequence.Argument1);
            sequence.Argument2 = PrepareArgument(maker, sequences, sequence.Argument2);

            if (sequence.Name == "Flow")
            {
                result = GetFlowRepresentation(sequence.Argument1, sequence.Argument2);
            }
            else if (sequence.Name == "Dec")
            {
                sequence.Argument3 = PrepareArgument(maker, sequences, sequence.Argument3);
                result = GetDecisionRepresentation(sequence.Argument1, sequence.Argument2, sequence.Argument3);
            }
            else if (sequence.Name == "Fork")
            {
                sequence.Argument3 = PrepareArgument(maker, sequences, sequence.Argument3);
                result = GetForkRepresentation(sequence.Argument1, sequence.Argument2, sequence.Argument3);
            }
            else if (sequence.Name == "Loop")
            {
                sequence.Argument3 = PrepareArgument(maker, sequences, sequence.Argument3);
                result = GetLoopRepresentation(sequence.Argument1, sequence.Argument2, sequence.Argument3);
            }

            return result;
        }

        private static string GetFlowRepresentation(string arg1, string arg2)
        {
            string representation = @"Flow(#Act1, #Act2)
ini= #Act1 / fin= #Act2
<>#Act1 
<>#Act2
#Act1 => <>#Act2 
#Act2 => ~<>#Act2";

            string result = representation.Replace(@"#Act1", arg1);
            result = result.Replace(@"#Act2", arg2);
            return result;
        }

        private static string GetDecisionRepresentation(string dec, string arg2, string arg3)
        {
            string representation = @"Dec(#Dec1, #Act1, #Act2):
ini= #Dec1 / fin= (#Act1 & ~#Act2) | (~#Act1 & #Act2)
<>#Dec1
<>#Act1 | <>#Act2
#Dec1 => ((<>#Act1 & ~<>#Act2) | (~<>#Act1 & <>#Act2))
#Act1 | #Act2 => ~<>#Dec1";
            string result = representation.Replace("#Dec1", dec);
            result = result.Replace("#Act1", arg2);
            result = result.Replace("#Act2", arg3);
            return result;
        }

        private static string GetForkRepresentation(string fork, string arg2, string arg3)
        {
            string representation = @"Fork(#Fork1, #Act1, #Act2):
ini= #Fork1 / fin= #Act1 & #Act2
<>#Fork1 
<>#Act1 
<>#Act2
#Fork1 => <>#Act1 & <>#Act2
(#Act1 | #Act2) => ~<>#Fork1
[]~(#Fork1 & (#Act1 | #Act2))";

            string result = representation.Replace("#Fork1", fork);
            result = result.Replace("#Act1", arg2);
            result = result.Replace("#Act2", arg3);
            return result;
        }

        private static string GetLoopRepresentation(string loop, string arg2, string arg3)
        {
            string representation = @"Loop(#Dec1,#Act1,#Act2):
ini= #Dec1 / fin= #Act1
<>#Dec1 
<>#Act1 
#Dec1 => <>#Act1
#Act2 => <>#Act1
(#Act1 | #Act2) => ~<>#Dec1";

            string result = representation.Replace("#Dec1", loop);
            result = result.Replace("#Act1", arg2);
            result = result.Replace("#Act2", arg3);
            return result;
        }

        private static string PrepareArgument(SequenceMaker maker, List<string> sequences, string argument)
        {
             //Console.WriteLine("Prepare: " + argument);
            if (argument.StartsWith("#"))
            {
                string number = argument.Substring(1);
                int index = Int32.Parse(number);
                argument = maker.sequences[index];
            }
            //Console.WriteLine("Final arg: " + argument);
            return argument;
        }
    }
}
