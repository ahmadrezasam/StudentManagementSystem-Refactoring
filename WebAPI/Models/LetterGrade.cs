using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public static class LetterGrade
    {
        private static readonly Dictionary<string, double> LetterToDigitGrades = new Dictionary<string, double>
        {
            { "A+", 4.0 },
            { "A", 4.0 },
            { "A-", 3.7 },
            { "B+", 3.3 },
            { "B", 3.0 },
            { "B-", 2.7 },
            { "C+", 2.3 },
            { "C", 2.0 },
            { "C-", 1.7 },
            { "D+", 1.3 },
            { "D", 1.0 },
            { "D-", 0.7 },
            { "F", 0 }
        };

        public static string GetLetterGrade(String letterGrade)
        {
            if(LetterToDigitGrades.ContainsKey(letterGrade))
                return letterGrade;
            return "NaN";
        }
        public static double GetDigitGrade(string letterGrade)
        {
            if (LetterToDigitGrades.TryGetValue(letterGrade, out double digitGrade))
            {
                return digitGrade;
            }

            return -1; // Indicate invalid letter grade
        }
    }
}