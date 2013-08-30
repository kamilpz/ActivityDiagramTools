using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityDiagramTools.Classes
{
    class Sequence
    {
        public string Name;
        public string Argument1;
        public string Argument2;
        public string Argument3;

        public void Describe()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Arg1: " + Argument1);
            Console.WriteLine("Arg2: " + Argument2);
            Console.WriteLine("Arg3: " + Argument3);
        }

        public static Sequence GetSequenceFromString(string stringToParse)
        {
            Sequence seq = new Sequence();

            string sequence = stringToParse.Replace(" ", "");
            int braceIndex = sequence.IndexOf("(");

            //Console.WriteLine("Try to parse: " + stringToParse);

            seq.Name = sequence.Substring(0, braceIndex);

            if (seq.Name == "Flow")
            {
                int comaIndex = sequence.IndexOf(",");
                string arg1 = sequence.Substring(braceIndex + 1, sequence.Length - comaIndex - 2);

                int bIndex = arg1.IndexOf("(");

                if (comaIndex > bIndex)
                {
                    comaIndex = sequence.LastIndexOf(",");
                }

                seq.Argument1 = sequence.Substring(braceIndex + 1, comaIndex - braceIndex - 1);
                seq.Argument2 = sequence.Substring(comaIndex + 1, sequence.Length - comaIndex - 2);
            }
            else
            {
                int comaIndex1 = sequence.IndexOf(",");

                string arg2 = sequence.Substring(comaIndex1 + 1, sequence.Length - comaIndex1 - 1);
                int comaIndex2 = arg2.IndexOf(",");

                seq.Argument1 = sequence.Substring(braceIndex + 1, comaIndex1 - braceIndex - 1);
                seq.Argument2 = arg2.Substring(0, comaIndex2);
                seq.Argument3 = arg2.Substring(comaIndex2 + 1, arg2.Length - comaIndex2 - 2);
            }
            //seq.Describe();   

            return seq;
        }
    }
}
