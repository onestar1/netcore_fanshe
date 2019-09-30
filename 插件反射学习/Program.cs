using System;
using System.IO;
using System.Reflection;
using core;
namespace 插件反射学习
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootdir = AppContext.BaseDirectory;
            DirectoryInfo Dir = Directory.GetParent(rootdir);
            string root = Dir.Parent.Parent.FullName;
            //获取指定目录的dll文件列表
            string[] dllFiles = System.IO.Directory.GetFiles(root, "plugins*.dll", System.IO.SearchOption.AllDirectories);
            //加载对应程序集信息
            // var dllfull = Assembly.Load(AssemblyName.GetAssemblyName(dllFiles[0]));//Instance.InstallDll(dllFiles[0]);
            Assembly dllfull;
            Type basel;
            foreach (var dll in dllFiles)
            {
                ////加载对应程序集信息
                dllfull = Assembly.Load(AssemblyName.GetAssemblyName(dll));
                foreach (var item in dllfull.GetTypes())//遍历程序集的里面的类信息
                {
                    var isfrom = typeof(Ipluginsbase).IsAssignableFrom(item);//判断对应type类是否实现某个接口
                    //typeof(B).isSubClassOf(typeof(I)); // false  B类是否是I的子类
                    // Console.WriteLine(item.FullName);
                    if (isfrom)
                    {
                        //实现反射类
                        Ipluginsbase ipluginsbase = (Ipluginsbase)Activator.CreateInstance(item);//根据完整的类名称 去实例化
                                                                                                 // Ipluginsbase ipluginsbase = Instance.Get<Ipluginsbase>(dllfull.GetTypes()[0].FullName+","+ assemblyname);
                                                                                                 //记住 实例化 ：  classfullname ：   类的完整名称xxx.xx,程序集名称

                      
                        //typeof(IFoo).IsAssignableFrom(typeof(BarClass));
                        Console.WriteLine("是否继承"+isfrom);
                        Console.WriteLine(ipluginsbase.getname());
                        ipluginsbase.show();
                    }
                }
            }
        //    Console.ReadKey();
              //   var name1= dllfull.GetTypes()[0].FullName;
        //    var assemblyname = dllfull.GetName().Name;        

            Console.WriteLine(root);
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
