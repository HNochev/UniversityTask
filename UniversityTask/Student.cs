using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityTask
{
    class Student : IStudent
    {
        public Student(string name, string facNumber)
        {
            this.Name = name;
            this.FacNumber = facNumber;
            this.Grades = new List<double>();
        }

        public string Name { get; private set; }

        public string FacNumber { get; private set; }

        public List<double> Grades { get; private set; }

        public double AverageGrade => this.Grades.Average();

        public void AddNewGrade(double grade)
        {
            this.Grades.Add(grade);
        }

        public string PrintWithAllGrades()
        {
            return $"Name: {this.Name} --- Faculty Number: {this.FacNumber} --- Grades: {String.Join(", ", this.Grades)}";
        }
        
        public string PrintMainRecord()
        {
            return $"Name: {this.Name} --- Faculty Number: {this.FacNumber} --- Average Grade: {this.AverageGrade:f2} --- Grades: {String.Join(", ", this.Grades)}";
        }

        public override string ToString()
        {
            return $"Name: {this.Name} --- Faculty Number: {this.FacNumber} --- Average Grade: {this.AverageGrade:f2}";
        }
    }
}
