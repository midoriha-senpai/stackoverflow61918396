using System;
using System.Collections.Generic;
using System.Linq;

namespace stackoverflow61918396
{
    class Manager
    {
        public static List<Student> Students { get; set; }

        public static List<Subject> Subjects { get; set; }

        public Manager()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();

            // Seed some students
            var ben = new Student() { Id = Guid.NewGuid().ToString(), Name = "Ben" };
            var andrew = new Student() { Id = Guid.NewGuid().ToString(), Name = "Andrew" };
            var amanda = new Student() { Id = Guid.NewGuid().ToString(), Name = "Amanda" };
            var greg = new Student() { Id = Guid.NewGuid().ToString(), Name = "Greg" };
            var peter = new Student() { Id = Guid.NewGuid().ToString(), Name = "Peter" };
            var jessica = new Student() { Id = Guid.NewGuid().ToString(), Name = "Jessica" };
            var james = new Student() { Id = Guid.NewGuid().ToString(), Name = "James" };
            var doug = new Student() { Id = Guid.NewGuid().ToString(), Name = "Doug" };
            var cory = new Student() { Id = Guid.NewGuid().ToString(), Name = "Cory" };
            var laura = new Student() { Id = Guid.NewGuid().ToString(), Name = "Laura" };
            Students.Add(ben);
            Students.Add(andrew);
            Students.Add(amanda);
            Students.Add(greg);
            Students.Add(peter);
            Students.Add(jessica);
            Students.Add(james);
            Students.Add(doug);
            Students.Add(cory);
            Students.Add(laura);

            // Seed a couple sections of math and science
            var math2 = new Subject() { Id = Guid.NewGuid().ToString(), Name = "Math", ClassNumber = 2 };
            var math4 = new Subject() { Id = Guid.NewGuid().ToString(), Name = "Math", ClassNumber = 4 };
            var science5 = new Subject() { Id = Guid.NewGuid().ToString(), Name = "Science", ClassNumber = 5 };
            var science3 = new Subject() { Id = Guid.NewGuid().ToString(), Name = "Science", ClassNumber = 3 };
            Subjects.Add(math2);
            Subjects.Add(math4);
            Subjects.Add(science5);
            Subjects.Add(science3);

            // Seed some enrollments
            Enroll(ben.Id, math2.Id);
            Enroll(andrew.Id, math2.Id);
            Enroll(andrew.Id, science5.Id);
            Enroll(amanda.Id, math2.Id);
            Enroll(peter.Id, math2.Id);
            Enroll(jessica.Id, math4.Id);
            Enroll(james.Id, math2.Id);
            Enroll(doug.Id, math4.Id);
            Enroll(doug.Id, science5.Id);
        }

        public Student AddStudent(string name)
        {
            var student = new Student() { Id = Guid.NewGuid().ToString(), Name = name };
            Students.Add(student);

            return student;
        }

        public Subject AddSubject(string name, int classNumber)
        {
            var subject = new Subject() { Id = Guid.NewGuid().ToString(), Name = name, ClassNumber = classNumber };
            Subjects.Add(subject);

            return subject;
        }

        public bool DeleteStudent(string id)
        {
            try
            {
                var student = Students.Where(s => s.Id == id).FirstOrDefault();

                if (student == null) return false;

                Students.Remove(student);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSubject(string id)
        {
            try
            {
                var subject = Subjects.Where(s => s.Id == id).FirstOrDefault();

                if (subject == null) return false;

                Subjects.Remove(subject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Disenroll(string studentId, string subjectId)
        {
            var student = Students.Where(s => s.Id == studentId).First();
            var subject = Subjects.Where(s => s.Id == subjectId).First();

            try
            {
                student.SubjectsEnrolledIn.Remove(subjectId);
                subject.StudentsInSubject.Remove(studentId);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Student EditStudent(string id, string name)
        {
            var student = Students.Where(s => s.Id == id).FirstOrDefault();

            if (student == null) return null;

            student.Name = name;

            return student;
        }

        public Subject EditSubject(string id, string name, int classNumber)
        {
            var subject = Subjects.Where(s => s.Id == id).FirstOrDefault();

            if (subject == null) return null;

            subject.Name = name;
            subject.ClassNumber = classNumber;

            return subject;
        }

        public bool Enroll(string studentId, string subjectId)
        {
            var student = Students.Where(s => s.Id == studentId).First();
            var subject = Subjects.Where(s => s.Id == subjectId).First();

            try
            {
                student.SubjectsEnrolledIn.Add(subjectId);
                subject.StudentsInSubject.Add(studentId);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Student GetStudent(string id)
        {
            return Students.Where(s => s.Id == id).FirstOrDefault();
        }

        public Subject GetSubject(string id)
        {
            return Subjects.Where(s => s.Id == id).FirstOrDefault();
        }

        public IEnumerable<IGrouping<string, Tuple<string, int, string>>> GetSubjectAllStudents(string id)
        {
            var tupleList = new List<Tuple<string, int, string>>();
            
            var subjectName = Subjects.Where(s => s.Id == id).FirstOrDefault()?.Name;
            var subjectList = Subjects.Where(s => s.Name == subjectName).ToList();

            if (subjectName == null) return null;

            foreach (Subject subject in subjectList)
            {
                foreach (Student student in Students)
                {
                    if (subject.StudentsInSubject.Contains(student.Id))
                    {
                        tupleList.Add(new Tuple<string, int, string>(student.Name, subject.ClassNumber, "yes"));
                    }
                    else
                    {
                        tupleList.Add(new Tuple<string, int, string>(student.Name, subject.ClassNumber, "no"));
                    }
                }
            }

            var sortedTupleList = tupleList.GroupBy(x => x.Item1);

            return sortedTupleList;
        }

        public List<Subject> GetSubjectAlternatives(string id)
        {
            var subjectName = Subjects.Where(s => s.Id == id).FirstOrDefault()?.Name;

            if (subjectName == null) return null;

            return Subjects.Where(s => s.Name == subjectName).ToList();
        }

        public List<Student> ListStudents()
        {
            return Students;
        }

        public List<Subject> ListSubjects()
        {
            return Subjects;
        }
    }
}
