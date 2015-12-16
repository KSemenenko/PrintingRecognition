using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuth
{
    public class Detector
    {
        private Stopwatch globalStopwatch = new Stopwatch();

        private List<LetterItem> Chars = new List<LetterItem>();

        public TotalInfo Total = new TotalInfo();

        public void Run()
        {
            globalStopwatch.Start();
        }

        public void Stop()
        {
            globalStopwatch.Stop();
            Total.TotalTime = globalStopwatch.ElapsedMilliseconds;

            long prevtime = Chars[0].TimeMarker;
            Total.DifferentTime.Clear();

            foreach (var item in Chars)
            {
                Total.Word += item.Letter;
                
                Total.DifferentTime.Add(item.TimeMarker - prevtime);
                prevtime = item.TimeMarker;
            }

            Chars.Clear();
            globalStopwatch.Reset();
        }

        public void NewLetter(string letter)
        {
            Chars.Add(new LetterItem(letter, globalStopwatch.ElapsedMilliseconds));
        }

        public List<bool> GetList()
        {
            List<bool> list = new List<bool>();

            long preview = 0;
            foreach (var item in Total.DifferentTime)
            {
                list.Add(item > preview);
                preview = item;
            }

            return list;


        }

        public List<double> GetListFloat()
        {
            List<double> list = new List<double>();

            long preview = 0;
            foreach (var item in Total.DifferentTime)
            {

                if (item == 0 || preview == 0)
                {
                    preview = item;
                    continue;
                }

               
                list.Add((double)preview / (double)item);
               

                preview = item;

            }

            return list;


        }
        
    }

    public class TotalInfo
    {
        public long TotalTime { get; set; }

        public List<long> DifferentTime { get; set; } = new List<long>();

        public string Word { get; set; } = string.Empty;
    }

    public class LetterItem
    {
        public string Letter { get; }
        public long TimeMarker { get; }

        public LetterItem(string letter, long timeMarker)
        {
            Letter = letter;
            TimeMarker = timeMarker;
        }
    }

    public class LetterTuple
    {
        private List<LetterItem> Chars = new List<LetterItem>();
    }
}
