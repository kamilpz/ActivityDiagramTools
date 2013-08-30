namespace ActivityDiagramTools.Classes
{
    using System.Linq;

    using ActivityDiagramTools.Classes.NodeClasses;
    using System.Collections.Generic;
    using System;

    public sealed class SequenceMaker
    {
        public List<string> sequences = new List<string>();
        public string FilePath
        {
            get;
            private set;
        }

        public string BasedSequence
        {
            get;
            private set;
        }

        public SequenceMaker(string FilePath)
        {
            this.FilePath = FilePath;
            BasedSequence = GetSequence();
            PrepareSequencesList();
        }

        private string GetSequence()
        {
            var xmlNodes = XMLNodesGetter.GetXMLNodes(FilePath);
            var nodes = NodesMaker.GetNodes(xmlNodes);
            NodeElement.AllElements = nodes.ToList();

            // WŁAŚNIE DODAŁEM
            sequences = new List<string>();

            foreach (var nodeElement in NodeElement.AllElements)
            {
                string mySeq = nodeElement.GetSequence();
                sequences.Add(LoopFinder.FindLoopsInSequence(mySeq));

                foreach (var element in NodeElement.AllElements)
                {
                    element.Used = false;
                }
            }
            // KONIEC

            var initialNode = nodes.Single(n => n.Name.Contains("Initial"));
            var sequence = initialNode.GetSequence();
            return LoopFinder.FindLoopsInSequence(sequence);
        }

        private void PrepareSequencesList()
        {
            sequences = sequences.OrderBy(name => name.Length).ToList();

            for (int i = 0; i < sequences.Count; i++)
            {
                if (!BasedSequence.Contains(sequences[i]))
                {
                    sequences.Remove(sequences[i]);
                }
            }

            //for (int i = 0; i < sequences.Count; i++)
            //{
            //    Console.WriteLine("#" + i + ": " + sequences[i]);
            //}
        }

        private List<string> GetSortedSequences()
        {
            List<string> sequencesList = new List<string>();

            int index = -1;
            foreach (string seq1 in sequences)
            {
                if (index == -1)
                {
                    index++;
                    continue;
                }
                //Console.WriteLine(seq1);
                string finalSeq = seq1;

                for (int i = index + 1; i >= 1; i--)
                {
                    //Console.WriteLine("Check: #" + i + " " + SequenceMaker.sequences[i]);

                    if (seq1.Contains(sequences[i]) && seq1 != sequences[i])
                    {
                        //Console.WriteLine("Contains");
                        finalSeq = finalSeq.Replace(sequences[i], "#" + i);
                    }
                    //else
                    //{
                    //    Console.WriteLine("Not contains");
                    //}
                }
                //Console.WriteLine("#" + index + ": " + finalSeq);
                sequencesList.Add(finalSeq);
                index++;
            }

            return sequencesList;
        }

        public string GetLTLSequence()
        {
            string ltlSequence = "";

            List<string> sequencesList = GetSortedSequences();
            List<Sequence> mySequences = new List<Sequence>();

            foreach (string s in sequencesList)
            {
                mySequences.Add(Sequence.GetSequenceFromString(s));
            }

            foreach (Sequence seq in mySequences)
            {
                string ltl = SequenceRepresentation.GetRepresentation(this, sequencesList, seq);
                ltlSequence += ltl + Environment.NewLine + Environment.NewLine;
            }
            return ltlSequence;
        }
    
        
    }
}
