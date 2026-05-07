using System;
using System.Collections.Generic;
using System.Text;
using BuilderHacker.Abstraction.Attributes;

namespace BuilderHacker.Console
{
    public class IO
    {
        protected int Id { get; set; }
    }

    [GenerateBuilderHacker]
    public class TestClass : IO
    {
        private int a;

        public int A { get { return a; } }

        public string Name { get; set; }
        public int Age { get; set; }

        public int GetId
        {
            get { return Id; }
        }
    }
}
