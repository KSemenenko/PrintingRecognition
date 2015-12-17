using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserAuth.Model;

namespace UserAuth
{
    public class Detector
    {
        private readonly List<LetterItem> chars = new List<LetterItem>();
        private readonly Stopwatch globalStopwatch = new Stopwatch();
        private readonly Stopwatch pressStopwatch = new Stopwatch();
        public TotalInfo Total = new TotalInfo();

        public void Start()
        {
            globalStopwatch.Reset();
            pressStopwatch.Reset();
            globalStopwatch.Start();
            chars.Clear();
            Total = new TotalInfo();
        }

        public void Stop()
        {
            globalStopwatch.Stop();
            pressStopwatch.Stop();
            Total.TotalTime = globalStopwatch.ElapsedMilliseconds;

            if (chars.Count > 0)
            {
                if (chars.Last()?.Letter == "\r")
                {
                    chars.Remove(chars.Last());
                }

                var prevtime = chars[0].TimeMarker;
                Total.DelayTime = prevtime;
                Total.CharPerMinute = Math.Round(chars.Count/(double) (Total.TotalTime - Total.DelayTime)*1000d*60d);
                foreach (var item in chars)
                {
                    Total.Word += item.Letter;

                    Total.DifferentTime.Add(item.TimeMarker - prevtime);
                    Total.KeyPressTime.Add(item.PressItme);
                    Total.Time.Add(item.TimeMarker);
                    prevtime = item.TimeMarker;
                }
            }

            chars.Clear();
            globalStopwatch.Reset();
        }

        public void Add(string letter)
        {
            if (pressStopwatch.IsRunning)
            {
                pressStopwatch.Stop();
                chars.Add(new LetterItem(letter, globalStopwatch.ElapsedMilliseconds, pressStopwatch.ElapsedTicks));
                pressStopwatch.Reset();
                return;
            }

            chars.Add(new LetterItem(letter, globalStopwatch.ElapsedMilliseconds));
        }

        public void PreAdd()
        {
            pressStopwatch.Start();
        }

        public bool IsEnter()
        {
            return chars.Count > 0;
        }
    }
}