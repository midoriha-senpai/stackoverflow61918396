using System.Collections.Generic;

namespace stackoverflow61918396
{
    class Subject
    {
        public string Id { get; set; }

        public int ClassNumber { get; set; }
        
        public string Name { get; set; }

        public List<string> StudentsInSubject { get; set; }

        public Subject()
        {
            StudentsInSubject = new List<string>();
        }
    }
}
