using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppLINQ
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("https://www.tutorialsteacher.com/codeeditor?cid=cs-9yg2ep");

            Console.WriteLine("\nUse of 'func(type, bool)' in LINQ");
            Func<Student, bool> isStudentTeenAger = s => s.Age > 12 && s.Age < 20;

            Student std = new Student() { Age = 21, Name = "Jan" };
            bool isTeen = isStudentTeenAger(std);// returns false
            Console.WriteLine("\t" + std.Name + " :  isTeen --> " + isTeen);

            Student.studentList.ToList().ForEach(s => Console.WriteLine("\t" + s.Name + " :  isTeen --> " + isStudentTeenAger(s)));

            Func<int, string> tekst = i => "Function met txt: " + i.ToString("0.00") + " hallo world";
            Console.WriteLine(tekst(1));

            // string collection
            Console.WriteLine("\nUse of 'ForEach(s =>....)' in LINQ");
            var strList = Student.stringList;
            // LINQ Query Syntax
            var result = from s in strList
                         where s.Contains("Tutorials")
                         select s;
            result.ToList().ForEach(s => Console.WriteLine("\t" + s));

            //OfType
            Console.WriteLine("\nUse of 'OfType' in LINQ");
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { Id = 1, Name = "Bill" });

            var stringResult = from s in mixedList.OfType<string>()
                               select s;
            stringResult.ToList().ForEach(s => Console.WriteLine("\tstringResult : " + s));

            var intResult = from s in mixedList.OfType<int>()
                            select s;
            intResult.ToList().ForEach(s => Console.WriteLine("\tintResult : " + s));

            var objectResult = from s in mixedList.OfType<Student>()
                               select s;
            objectResult.ToList().ForEach(s => Console.WriteLine("\tStudentObjectResult : " + s.Name));

            Console.WriteLine("\nUse of .Where met functie...");

            var filteredResult = Student.studentList
                .Where((s, i) =>
                { // if it is even element
                    if (i % 2 == 0) return true;
                    return false;
                })
                .Where((s, n) =>
                {
                    if ("\t" + s.Name == "Bill") return true;
                    return false;
                });

            foreach (var stu in filteredResult)
                Console.WriteLine(stu.Name);

            var groupedResult = Student.studentList.OrderBy(s => s.Age).GroupBy(s => s.Age);

            foreach (var ageGroup in groupedResult)
            {
                Console.WriteLine("\tAge Group: {0}", ageGroup.Key);  //Each group has a key

                foreach (Student s in ageGroup)  //Each group has a inner collection
                    Console.WriteLine("\tStudent Name: {0}", s.Name);
            }

            Student testEqualStu = new Student() { Id = 3, Name = "Bill", Age = 18 };
            Student.studentList.ToList().ForEach(stu => Console.WriteLine(testEqualStu + "is gelijk aan TestEqualStu " + stu + ": " + testEqualStu.Equals(stu)));
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }

            // override object.Equals
            public override bool Equals(object obj)
            {
                //
                // See the full list of guidelines at
                //   http://go.microsoft.com/fwlink/?LinkID=85237
                // and also the guidance for operator== at
                //   http://go.microsoft.com/fwlink/?LinkId=85238
                //

                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                // TODO: write your implementation of Equals() here
                Student stu = (Student)obj;
                return (stu.Age == this.Age && stu.Name == this.Name && stu.Id == this.Id);
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {
                return base.GetHashCode() * 5;
            }

            public static IList<Student> studentList = new List<Student>() {
                new Student() { Id = 1, Name = "John", Age = 13} ,
                new Student() { Id = 2, Name = "Moin",  Age = 21 } ,
                new Student() { Id = 3, Name = "Bill",  Age = 18 } ,
                new Student() { Id = 4, Name = "Ram" , Age = 20} ,
                new Student() { Id = 5, Name = "Ron" , Age = 15 }
                };

            public static IList<string> stringList = new List<string>() {
                "C# Tutorials",
                "VB.NET Tutorials",
                "Learn C++",
                "MVC Tutorials" ,
                "Java"};
        }
    }
}