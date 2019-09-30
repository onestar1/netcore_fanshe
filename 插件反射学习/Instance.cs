using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace 插件反射学习
{
    /// <summary>
    /// 实例化 对应类
    /// </summary>
    public class Instance
    {
        public static T Get<T>(string classFullName)
        {
            try
            {
                Type sourceType = Type.GetType(classFullName);
                return (T)Activator.CreateInstance(sourceType);
            }
            catch (Exception ex)
            {
                //throw new InstanceCreateException("创建实例异常", ex);
                Console.WriteLine("创建实例异常");
                return default(T);
            }
        }

        /// <summary>
        /// 加载(安装)dll 
        /// </summary>
        /// <param name="dllFileName"></param>
        /// <returns></returns>
       public static Assembly InstallDll(string dllFileName)
        {
            string newFileName = dllFileName;
            FileInfo fileInfo = new FileInfo(dllFileName);
            DirectoryInfo copyFolder;
            if (!string.IsNullOrWhiteSpace(AppDomain.CurrentDomain.DynamicDirectory))
            {
                //获取asp.net dll运行目录
                copyFolder = new DirectoryInfo(AppDomain.CurrentDomain.DynamicDirectory);
            }
            else
                copyFolder = new DirectoryInfo(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.FullName);// string root = Dir.Parent.Parent.FullName;new DirectoryInfo(IOHelper.GetMapPath(""));
            newFileName = copyFolder.FullName + "\\" + fileInfo.Name;

            Assembly assembly = null;
         //   PluginInfo pluginfo = null;
            try
            {
                //try
                //{
                //    System.IO.File.Copy(dllFileName, newFileName, true);
                //}
                //catch
                //{
                //    //在某些情况下会出现"正由另一进程使用，因此该进程无法访问该文件"错误，所以先重命名再复制
                //    File.Move(newFileName, newFileName + Guid.NewGuid().ToString("N") + ".locked");
                //    System.IO.File.Copy(dllFileName, newFileName, true);
                //}
                assembly = Assembly.Load(AssemblyName.GetAssemblyName(newFileName));
                //排除以 Super.Plugin 开头的，非插件的dll，主要是插件基类
                if (assembly.FullName.StartsWith("Super.Plugin") && !assembly.FullName.Contains("Super.Plugin.Payment.Alipay.Base"))
                {

                //    pluginfo = AddPluginInfo(fileInfo);//添加插件信息

                    //向插件注入信息
                  //  IPlugin plugin = Core.Instance.Get<IPlugin>(pluginfo.ClassFullName);
                 //   plugin.WorkDirectory = fileInfo.Directory.FullName;

                }
            }
            catch (IOException ex)
            {
              //  Core.Log.Error("插件复制失败(" + dllFileName + ")！", ex);
              //  if (pluginfo != null)//插件复制失败时，移除插件安装信息
               //     RemovePlugin(pluginfo);
            }
            catch (Exception ex)
            {
               // Core.Log.Error("插件加载失败(" + dllFileName + ")！", ex);
               // if (pluginfo != null)//插件加载失败时，移除插件安装信息
               //     RemovePlugin(pluginfo);
            }
            return assembly;
        }
    }
}
