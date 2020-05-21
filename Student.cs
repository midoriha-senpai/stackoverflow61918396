using System.Collections.Generic;

namespace stackoverflow61918396
{
    class Student
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> SubjectsEnrolledIn { get; set; }

        public Student()
        {
            SubjectsEnrolledIn = new List<string>();
        }
    }
}
