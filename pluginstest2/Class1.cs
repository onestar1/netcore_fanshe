using System;
using core;
namespace pluginstest2
{
    public class Class1 : Ipluginsbase
    {
        public string getname()
        {
            return "接口2";
        }

        public void show()
        {
            Console.WriteLine("这里是接口2的实现");
        }
    }
}
