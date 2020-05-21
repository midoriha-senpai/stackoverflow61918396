using System;
using System.Collections.Generic;

namespace stackoverflow61918396
{
    class Program
    {
        static bool Again { get; set; }
        static Manager Manager { get; set; }

        static void Main()
        {            
            Again = true;
            Manager = new Manager();

            do
            {
                Console.WriteLine("Enter a command (type 'help' for options):");
                var selection = Console.ReadLine();
                Console.WriteLine("\n");

                CommandHandler(selection);
            } while (Again);
        }

        static void CommandHandler(string command)
        {
            switch (command)
            {
                case var s when s.Trim().ToLower() == "help":
                    Console.WriteLine("Commands:");
                    Console.WriteLine("'quit' - exits the program");
                    Console.WriteLine("'students' - lists all students");
                    Console.WriteLine("'subjects' - lists all subjects");                    
                    Console.WriteLine("'student [id]' - shows this student's record");
                    Console.WriteLine("'subject [id]' - shows this subject's record");
                    Console.WriteLine("'all students by subject [id]' - lists all students in this subject or not");
                    Console.WriteLine("'alternatives for subject [id]' - lists all class numbers of a subject");
                    Console.WriteLine("'new student [name]' - adds a new student");
                    Console.WriteLine("'new subject [name] [class number]' - adds a new subject");
                    Console.WriteLine("'edit student [id] [name]' - edits an existing student");
                    Console.WriteLine("'edit subject [id] [name] [class number]' - edits an existing subject");
                    Console.WriteLine("'delete student [id]' - deletes an existing student");
                    Console.WriteLine("'delete subject [id]' - deletes an existing subject");
                    Console.WriteLine("'enroll [student id] [subject id]' - enrolls a student into a subject");
                    Console.WriteLine("'disenroll [student id] [subject id]' - disenrolls a student from a subject");
                    Console.WriteLine("\n");
                    break;
                case var s when s.Trim().ToLower() == "quit":
                    Again = false;
                    break;
                case var s when s.Trim().ToLower() == "students":
                    try
                    {
                        List<Student> studentList = Manager.ListStudents();

                        if (studentList == null) throw new Exception();

                        Console.WriteLine("Students:");
                        foreach (Student student in studentList)
                        {
                            Console.WriteLine("Id: " + student.Id + ", Name: " + student.Name);
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower() == "subjects":
                    try
                    {
                        List<Subject> subjectList = Manager.ListSubjects();

                        if (subjectList == null) throw new Exception();

                        Console.WriteLine("Subjects:");
                        foreach (Subject subject in subjectList)
                        {
                            Console.WriteLine(
                                "Id: " + subject.Id +
                                ", Class Number: " + subject.ClassNumber +
                                ", Name: " + subject.Name
                            );
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("student"):
                    try
                    {
                        var studentId = s.Split(' ')[1];
                        var student = Manager.GetStudent(studentId);

                        if (student == null) throw new Exception();

                        Console.WriteLine("Id: " + student.Id + ", Name: " + student.Name);
                        
                        Console.WriteLine("Subjects Enrolled In:");
                        foreach (string subjectId in student.SubjectsEnrolledIn)
                        {
                            var subject = Manager.GetSubject(subjectId);
                            Console.WriteLine(
                                "Id: " + subject.Id + 
                                ", Class Number: " + subject.ClassNumber + 
                                ", Name: " + subject.Name
                            );
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("subject"):
                    try
                    {
                        var subjectId = s.Split(' ')[1];
                        var subject = Manager.GetSubject(subjectId);

                        if (subject == null) throw new Exception();

                        Console.WriteLine("Id: " + subject.Id + ", Name: " + subject.Name);

                        Console.WriteLine("Students In Subject:");
                        foreach (string studentId in subject.StudentsInSubject)
                        {
                            var student = Manager.GetStudent(studentId);
                            Console.WriteLine(
                                "Id: " + student.Id +
                                ", Name: " + student.Name
                            );
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("all"):
                    try
                    {
                        var subjectId = s.Split(' ')[4];
                        var subjectName = Manager.GetSubject(subjectId)?.Name;
                        var subjectWithAll = Manager.GetSubjectAllStudents(subjectId);

                        if (subjectWithAll == null) throw new Exception();

                        Console.WriteLine("All students with respect to " + (subjectName ?? subjectId) + ":");
                        foreach (var group in subjectWithAll)
                        {
                            foreach (var item in group)
                            {
                                Console.WriteLine(
                                    "\tName: " + item.Item1 +
                                    ", \t\tClass Number: " + item.Item2 +
                                    ", \t\tEnrolled: " + item.Item3
                                );
                            }
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("alternatives"):
                    try
                    {
                        var subjectId = s.Split(' ')[3];
                        List<Subject> subjectAlternatives = Manager.GetSubjectAlternatives(subjectId);

                        if (subjectAlternatives == null) throw new Exception();

                        Console.WriteLine("Alternatives for " + subjectAlternatives[0].Name + ":");
                        foreach (Subject subject in subjectAlternatives)
                        {
                            Console.WriteLine("Id: " + subject.Id + ", Class Number: " + subject.ClassNumber);
                        }
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("new student"):
                    try
                    {
                        var name = s.Split(' ')[2];
                        var student = Manager.AddStudent(name);

                        if (student == null) throw new Exception();

                        Console.WriteLine("Student " + student.Id + " created");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("new subject"):
                    try
                    {
                        var name = s.Split(' ')[2];
                        var classNumber = s.Split(' ')[3];
                        var subject = Manager.AddSubject(name, int.Parse(classNumber));

                        if (subject == null) throw new Exception();

                        Console.WriteLine("Subject " + subject.Id + " created");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("edit student"):
                    try
                    {
                        var studentId = s.Split(' ')[2];
                        var studentName = s.Split(' ')[3];
                        var student = Manager.EditStudent(studentId, studentName);

                        if (student == null) throw new Exception();

                        Console.WriteLine("Student " + student.Id + " was edited");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("edit subject"):
                    try
                    {
                        var subjectId = s.Split(' ')[2];
                        var subjectName = s.Split(' ')[3];
                        var subjectClassNumber = s.Split(' ')[4];
                        var subject = Manager.EditSubject(subjectId, subjectName, int.Parse(subjectClassNumber));

                        if (subject == null) throw new Exception();

                        Console.WriteLine("Subect " + subject.Id + " was edited");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("delete student"):
                    try
                    {
                        var studentId = s.Split(' ')[2];
                        var deleted = Manager.DeleteStudent(studentId);

                        Console.WriteLine("Student was " + (deleted ? "" : "not") + " deleted");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("delete subject"):
                    try
                    {
                        var subjectId = s.Split(' ')[2];
                        var deleted = Manager.DeleteSubject(subjectId);

                        Console.WriteLine("Subject was " + (deleted ? "" : "not") + " deleted");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("enroll"):
                    try
                    {
                        var studentId = s.Split(' ')[1];
                        var subjectId = s.Split(' ')[2];
                        var enrolled = Manager.Enroll(studentId, subjectId);

                        Console.WriteLine("Student was " + (enrolled ? "" : "not") + " enrolled");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                case var s when s.Trim().ToLower().StartsWith("disenroll"):
                    try
                    {
                        var studentId = s.Split(' ')[1];
                        var subjectId = s.Split(' ')[2];
                        var disenrolled = Manager.Disenroll(studentId, subjectId);

                        Console.WriteLine("Student was " + (disenrolled ? "" : "not") + " disenrolled");
                        Console.WriteLine("\n");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Unrecognized command. Please try again.");
                        Console.WriteLine("\n");
                    }
                    break;
                default:
                    Console.WriteLine("Unrecognized command. Please try again.");
                    Console.WriteLine("\n");
                    break;
            }
        }
    }
}
