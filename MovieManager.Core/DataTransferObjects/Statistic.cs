using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    public class Statistic
    {
        public string Categorie { get; set; }

        public int Count { get; set; }

        public double Duration { get; set; }

        //TODO
        public double Hours => Duration / 60;
        //TODO
        public double Minutes => Duration % 60;
        //TODO
        public double Seconds => (Minutes * 60) % 60;
        //TODO
        public string GetTimeForOutput(bool withSeconds = false)
        {
            if(withSeconds)
            return $"{(int)Hours:D2} h {(int)Minutes:D2} min {(int)Seconds:D2} sec";
            else
            return $"{(int)Hours:D2} h {(int)Minutes:D2} min";

        }
    }
}
