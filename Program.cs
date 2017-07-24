using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL;
using IDAL;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            //  SqlHelper sqlHelper = new SqlHelper();
            try
            {
                IDBHelper sqlHelper = SimpleFactory.CreateInstance();
                //测试SqlHelper GetById方法
                User user = sqlHelper.GetById<User>(1);
                Console.WriteLine("显示Id=1的User数据：");
                Show(user);

                List<Company> companys = sqlHelper.GetALL<Company>();
                ShowALL(companys);

                #region 对用户进行排序、获取用户类型UserType最大、最小、平均值等其他Linq尝试
                UserLinq ul = new UserLinq();
                var orderUsers = ul.GetUserOrderByUserType();
                ShowALL(orderUsers);
                Console.WriteLine("显示UserType的最大值：");
                Show(ul.GetMaxUserType());
                Console.WriteLine("显示UserType的最小值：");
                Show(ul.GetMinUserType());
                Console.WriteLine("显示UserType的平均值：");
                Show(ul.GetAvgUserType());
                Console.WriteLine("公司名等部分用户信息");
                Show(ul.GetViewFirstUser());
                #endregion

                #region 测试泛型的数据库实体插入、实体更新、ID删除数据的数据库访问方法
                User u = new User
                {
                    Name = "老杨",
                    Account = "yy",
                    Password = "1",
                    Email = "2645660419@qq.com",
                    CompanyId = 1,
                    State = 1,
                    LastLoginTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    LastModifyTime = DateTime.Now,
                    CreatorId = 1
                };
                if (sqlHelper.Add(u) > 0)
                {
                    Console.WriteLine("插入成功");
                }
                else
                {
                    Console.WriteLine("插入失败");
                }

                Company c = new Company()
                {
                    Id = 2,
                    Name = "宁波新天地",
                    CreatorId = 1,
                    CreateTime = DateTime.Now,
                    LastModifyTime = DateTime.Now,
                };

                if (sqlHelper.Update(c) > 0)
                {
                    Console.WriteLine("更新成功");
                }
                else
                {
                    Console.WriteLine("更新失败");
                }

                if (sqlHelper.Delete<User>(1008) > 0)
                {
                    Console.WriteLine("删除成功");
                }
                else
                {
                    Console.WriteLine("删除失败");
                }



                #endregion
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Common.LogHelper.Log(ex.StackTrace);
            }
        }

        private static void ShowALL<T>(List<T> companys)
        {
            Console.WriteLine($"显示所有的{typeof(T).Name}数据：");
            foreach (var item in companys)
            {
                Show(item);
            }
        }

        private static void Show(int t)
        {
            Console.WriteLine(t);
        }

        private static void Show(double t)
        {
            Console.WriteLine(t);
        }

        private static void Show<T>(T t)
        {
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                Console.Write("{0}\t", item.GetValue(t));
            }
            Console.WriteLine();
        }
    }
}
