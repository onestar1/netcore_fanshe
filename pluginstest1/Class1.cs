using System;
using core;
namespace pluginstest12
{
    public class Class1 : Ipluginsbase
    {
        public string getname()
        {
            return "接口1";
        }

        public void show()
        {
            Console.WriteLine("这里是接口1的实现方法");
           
        }
    }
}
