
using System;

public enum GradeEnum
{
    GRADE_6TH, 
    GRADE_7TH, 
    GRADE_8TH, 
}

static class GradeEnumExtensions
{
    public static string GetString(this GradeEnum grade)
    {
        switch (grade)
        {
            case GradeEnum.GRADE_6TH: return "6th Grade";
            case GradeEnum.GRADE_7TH: return "7th Grade";
            case GradeEnum.GRADE_8TH: return "8th Grade";
            default: throw new ArgumentOutOfRangeException("none");
        }
    }
}