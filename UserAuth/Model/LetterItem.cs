using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuth.Model
{
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
