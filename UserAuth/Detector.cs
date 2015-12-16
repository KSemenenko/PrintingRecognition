using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UserAuth
{
    public class Detector
    {
        private readonly List<LetterItem> Chars = new List<LetterItem>();
        private readonly Stopwatch globalStopwatch = new Stopwatch();

        private readonly Stopwatch pressStopwatch = new Stopwatch();

        public TotalInfo Total = new TotalInfo();

        public double CharPerMinute { get; set; }

        public void Run()
        {
            globalStopwatch.Start();
        }

        public void Stop()
        {
            globalStopwatch.Stop();
            Total.TotalTime = globalStopwatch.ElapsedMilliseconds;

            var prevtime = Chars[0].TimeMarker;
            Total.DifferentTime.Clear();
            Total.DelayTime = prevtime;

            CharPerMinute = Math.Round(Chars.Count/(double) (Total.TotalTime - Total.DelayTime)*1000d*60d);

            foreach (var item in Chars)
            {
                Total.Word += item.Letter;

                Total.DifferentTime.Add(item.TimeMarker - prevtime);
                prevtime = item.TimeMarker;
            }

            Chars.Clear();
            globalStopwatch.Reset();
        }

        public void Add(string letter)
        {
            if (pressStopwatch.IsRunning)
            {
                pressStopwatch.Stop();
                Chars.Add(new LetterItem(letter, globalStopwatch.ElapsedMilliseconds, pressStopwatch.ElapsedTicks));
                pressStopwatch.Reset();
                return;
            }

            Chars.Add(new LetterItem(letter, globalStopwatch.ElapsedMilliseconds));
        }

        public void PreAdd()
        {
            pressStopwatch.Start();
        }

        public List<bool> GetList()
        {
            var list = new List<bool>();

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
            var list = new List<double>();

            long preview = 0;
            foreach (var item in Total.DifferentTime)
            {
                if (item == 0 || preview == 0)
                {
                    preview = item;
                    continue;
                }


                list.Add(preview/(double) item);


                preview = item;
            }

            return list;
        }
    }

    public class TotalInfo
    {
        public long TotalTime { get; set; }

        public long DelayTime { get; set; }

        public List<long> DifferentTime { get; set; } = new List<long>();

        public string Word { get; set; } = string.Empty;
    }

    public class LetterItem
    {
        public LetterItem(string letter, long timeMarker, long pressTime = 0)
        {
            Letter = letter;
            TimeMarker = timeMarker;
            PressItme = pressTime;
        }

        public string Letter { get; }
        public long TimeMarker { get; }

        public long PressItme { get; }
    }


}