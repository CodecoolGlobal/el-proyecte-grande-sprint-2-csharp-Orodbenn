using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Marks;

namespace Core.Utils
{
   public class Util
    {
        
        public Subject checkSubject(string subjectString)
        {
            switch (subjectString)
            {
                case "Art":
                    return Subject.Art;
                   
                case "Literature":
                    return Subject.Literature;

                case "Grammar":
                    return Subject.Grammar;

                case "Biology":
                    return Subject.Biology;

                case "Physics":
                    return Subject.Physics;

                case "Mathematics":
                    return Subject.Mathematics;

                case "ComputerScience":
                    return Subject.ComputerScience;

                case "English":
                    return Subject.English;

                case "German":
                    return Subject.German;

                case "Sociology:":
                    return Subject.Sociology;

                case "Sport":
                    return Subject.Sport;

                case "Music":
                    return Subject.Music;

                default:
                    return Subject.VampireHunting;
            }
        }
        public MarkWeight checkMarkweight(string mw)
        {
            switch (mw)
            {
                case "HalfWeight":
                    return MarkWeight.HalfWeight;
                case "NormalWeight":
                    return MarkWeight.NormalWeight;
                case "DoubleWeight":
                    return MarkWeight.DoubleWeight;
                case "ExamWeight":
                    return MarkWeight.ExamWeight;

                default:
                    return MarkWeight.NormalWeight;
            }
        }
    }
}
