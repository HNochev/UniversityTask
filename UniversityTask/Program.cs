using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniversityTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            bool selectionSorted = false;

            PrintMenu();

            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Name of the student:");
                            string name = Console.ReadLine();

                            Console.WriteLine("Faculty number of the student:");
                            string facNumber = Console.ReadLine();

                            Console.WriteLine("Number of grades:");
                            int countOfGrades = int.Parse(Console.ReadLine());

                            Console.WriteLine($"Write all grades of the student be sure their count is equal to ({countOfGrades}).");
                            Console.WriteLine($"If grades are more than {countOfGrades}, the last unnecessary grades will not be saved for the student.");
                            Console.WriteLine($"(Example of input: 5, 4, 5, 3        if count is 3 only 5, 4 and 5 will be saved.)");
                            List<double> grades = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToList();

                            Student student = new Student(name, facNumber);

                            for (int i = 0; i < countOfGrades; i++)
                            {
                                student.AddNewGrade(grades[i]);
                            }

                            students.Add(student);

                            Console.WriteLine("Successfully added new student!");

                            PrintMenu();
                            break;
                        }
                    case "2":
                        {
                            if (students.Count == 0)
                            {
                                Console.WriteLine("There are no students listed");
                                PrintMenu();
                                break;
                            }

                            Console.WriteLine("Facuty number of the student of which you want to add grades.");
                            string neededFacNumber = Console.ReadLine();

                            Student neededStudent = students.Where(x => x.FacNumber == neededFacNumber).FirstOrDefault();

                            if (neededStudent == null)
                            {
                                Console.WriteLine("There is no student with such faculty number!");
                                PrintMenu();
                                break;
                            }

                            Console.WriteLine($"You have choosen: {neededStudent.PrintWithAllGrades()}");

                            Console.WriteLine("Write all the grades you want to add:");
                            Console.WriteLine($"(Example of input: 5, 4, 5, 3)");
                            List<double> grades = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToList();

                            for (int i = 0; i < grades.Count; i++)
                            {
                                neededStudent.AddNewGrade(grades[i]);
                            }

                            Console.WriteLine("Grade/s added successfully!");

                            PrintMenu();
                            break;
                        }
                    case "3":
                        {
                            for (int i = 0; i < students.Count; i++)
                            {
                                Console.WriteLine($"{students[i]}");
                            }

                            string result = students.Count > 0 ? "Success!" : "No students to print.";

                            Console.WriteLine(result);

                            PrintMenu();
                            break;
                        }
                    case "4":
                        {
                            TextWriter tw = new StreamWriter("../../../AllStudentsList.txt");

                            tw.WriteLine("Students List:");

                            foreach (Student student in students)
                            {
                                tw.WriteLine($"{student.PrintWithAllGrades()}");
                            }

                            tw.Close();
                            Console.WriteLine("Successfully saved in txt file!");

                            PrintMenu();
                            break;
                        }
                    case "5":
                        {
                            using (StreamReader sr = File.OpenText("../../../FileWithStudentsToReadFrom.txt"))
                            {
                                string s = "";
                                while ((s = sr.ReadLine()) != null)
                                {
                                    string[] input = s.Split(" --- ");

                                    string name = input[0].Replace("Name: ", "");
                                    string facNum = input[1].Replace("Faculty Number: ", "");
                                    string grades = input[2].Replace("Grades: ", "");

                                    Student student = new Student(name, facNum);

                                    double[] gradesArray = grades.Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();

                                    for (int i = 0; i < gradesArray.Length; i++)
                                    {
                                        student.AddNewGrade(gradesArray[i]);
                                    }

                                    students.Add(student);
                                }
                            }
                            Console.WriteLine("Successfully readed from txt file!");

                            PrintMenu();
                            break;
                        }
                    case "6":
                        {
                            SelectionSort(students);
                            selectionSorted = true;

                            Console.WriteLine("Sorted by faculty number successfully!");

                            PrintMenu();
                            break;
                        }
                    case "7":
                        {
                            InsertionSort(students);
                            selectionSorted = false;

                            Console.WriteLine("Sorted by average grade successfully!");

                            PrintMenu();
                            break;
                        }
                    case "8":
                        {
                            if (students.Count == 0)
                            {
                                Console.WriteLine("There are no students listed");
                                PrintMenu();
                                break;
                            }

                            Console.WriteLine("Facuty number of the student of which you want find.");
                            string neededFacNumber = Console.ReadLine();


                            while (!students.Any(x => x.FacNumber == neededFacNumber))
                            {
                                Console.WriteLine("Student with such faculty number doesn't exist.");
                                Console.WriteLine("Try write down the faculty number again:");

                                neededFacNumber = Console.ReadLine();
                            }

                            Student student = null;

                            if (selectionSorted)
                            {
                                student = students[(int)BinarySearch(students.ToArray(), neededFacNumber)];
                            }
                            else
                            {
                                student = students.Where(x => x.FacNumber == neededFacNumber).FirstOrDefault();

                            }
                            Console.WriteLine("Information about the student:");
                            Console.WriteLine($"{student.PrintMainRecord()}");

                            Console.WriteLine("Success!");

                            PrintMenu();
                            break;
                        }
                    case "9":
                        {
                            Console.Clear();
                            PrintMenu();
                            break;
                        }
                    case "10":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Write correct number from the menu!");
                            break;
                        }
                }
            }

        }
        public static void PrintMenu()
        {
            string menu = Environment.NewLine +
                "Program Menu:" + Environment.NewLine +
                "1. Add new student." + Environment.NewLine +
                "2. Add grades to student." + Environment.NewLine +
                "3. Print all students." + Environment.NewLine +
                "4. Extract my whole students list in a file." + Environment.NewLine +
                "5. Read students from a file." + Environment.NewLine +
                "6. Sort students by their faculty number with Selection Sort." + Environment.NewLine +
                "7. Sort students by their average grade with Insertion Sort." + Environment.NewLine +
                "8. View all data for one student with its faculty number" + Environment.NewLine +
                "9. Clear the Console." + Environment.NewLine +
                "10. Exit." + Environment.NewLine;

            Console.WriteLine(menu);
        }

        private static void SelectionSort(List<Student> collection)
        {
            int n = collection.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (String.Compare(collection[j].FacNumber, collection[min_idx].FacNumber) < 0)
                    {
                        min_idx = j;
                    }

                Student temp = collection[min_idx];
                collection[min_idx] = collection[i];
                collection[i] = temp;
            }
        }

        private static void InsertionSort(List<Student> collection)
        {
            int n = collection.Count;
            for (int i = 1; i < n; ++i)
            {
                Student key = collection[i];
                int j = i - 1;

                while (j >= 0 && collection[j].AverageGrade > key.AverageGrade)
                {
                    collection[j + 1] = collection[j];
                    j = j - 1;
                }
                collection[j + 1] = key;
            }
        }

        private static object BinarySearch(Student[] inputArray, string key)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (key == inputArray[mid].FacNumber)
                {
                    return mid;
                }
                else if (String.Compare(key, inputArray[mid].FacNumber) < 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return "Nil";
        }
    }
}

