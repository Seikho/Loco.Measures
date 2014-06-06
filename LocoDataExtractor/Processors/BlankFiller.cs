using System;

namespace LocoDataExtractor.Processors
{
    public class BlankFiller : Processor
    {
        protected DateTime Pred;
        protected DateTime Succ;
        protected DateTime Curr;
        protected string PredByte;
        protected bool ReadFirst = false;
        protected string Line;
        public BlankFiller(string fileLocation, string newFile) : base(fileLocation, newFile)
        {
            ReadFirst = false;
            Pred = new DateTime();
            Succ = new DateTime();
            PredByte = ""; // previous sample byte
        }

        protected override void Execute()
        {
            while ((Line = Original.ReadLine()) != null)
            {
                if (Line.Length < 5) continue;
                var split = Line.Split(' ');
                if (!ReadFirst) GenerateInitialValues(split);
                else Curr = GetTime(Line);
                if (Pred == Curr) continue;
                if (Succ != Curr) // we're missing a second or two. fill in the gap(s).
                {
                    while (Succ != Curr)
                    {
                        Target.WriteLine("{0} {1} {2} {3}", Succ.ToShortDateString(), Succ.ToLongTimeString(), split[3],
                            PredByte);
                        Succ = Succ.AddSeconds(1);
                    }
                }
                // we do this regardless.
                Succ = Curr.AddSeconds(1);
                Pred = Curr;
                PredByte = split[4];
                Target.WriteLine(Line);
            }
        }

        private void GenerateInitialValues(string[] split)
        {
            ReadFirst = true;
            PredByte = split[4];
            Curr = GetTime(Line);
            Pred = Curr.AddSeconds(-1);
            Succ = Curr;
        }
    }
}
