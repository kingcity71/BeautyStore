using System;
using System.Linq;

namespace TestConsole
{
    class Cl1
    {
        public Enum1 Enum{ get; set; }
    }
    class Cl2
    {
        public Enum2 Enum { get; set; }
    }
    enum Enum1
    {
        enum1val1,
        enum1val2,
    }
    enum Enum2
    {
        enum2val1,
        enum2val2,
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cl1Props = typeof(Cl1).GetProperties();
            var cl1 = new Cl1 { Enum = Enum1.enum1val2 };
            var cl2Props = typeof(Cl2).GetProperties();
            var cl2 = new Cl2();

            foreach (var cl1prop in cl1Props)
            {
                var cl2Prop = cl2Props.FirstOrDefault(x => x.Name == cl1prop.Name);
                if (cl2Prop.PropertyType.IsEnum && cl1prop.PropertyType.IsEnum)
                    cl2Prop.SetValue(cl2, cl1prop.GetValue(cl1));
            }
        }
    }
}
