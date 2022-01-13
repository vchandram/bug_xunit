using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Bug
{
    public class ExampleTestCase : Tuple<int, int>
    {
        public string Name { get; set; }

        public ExampleTestCase(int first, int second)
            : base(first, second) { }

        public override string ToString()
        {
            return Name ?? base.ToString();
        }
    }

    public class ExampleTestData : TheoryData<ExampleTestCase>
    {
        private int _counter = 0;

        public string GetUniqueName() => $"{GetType().Name}_{_counter++}_xyz";
    }

    public class ExampleClassData : ExampleTestData
    {
        
        public ExampleClassData()
        {
            Add(new ExampleTestCase(1, 2)
            { Name = GetUniqueName() });

            Add(new ExampleTestCase(3, 4)
            { Name = GetUniqueName() });            
        }
    }

    public class TestFixture
    {

        [Theory]
        [ClassData(typeof(ExampleClassData))]
        public void Example_Test(ExampleTestCase data)
        {
            (int first, int second) = data;

            first.Should().BeLessThan(second);
        }
    }
}
