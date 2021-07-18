using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityTask
{
    public interface IStudent
    {
        string Name { get; }

        string FacNumber { get; }

        List<double> Grades { get; }

        double AverageGrade { get; }
    }
}

