using PreOrderTreeTraversal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PreOrderTreeTraversal 
{
    class Program
    {
        public static void Main(string[] args)
        {
            SetupTestData();

            using (var unitOfWork = new TasksUnitOfWork())
            {
                var parentsTest = unitOfWork.Tasks.GetParents("MoveLeg");
                IEnumerable<string> parentsTeststrings = parentsTest.Select(s => s.Name).ToList();
                var result1 = string.Join<string>(",", parentsTeststrings);
                Console.WriteLine("Parents of task MoveLeg are:{0}", result1);

                var childrenTest = unitOfWork.Tasks.GetChildren("MoveBody");
                IEnumerable<string> childrenTeststrings = childrenTest.Select(s => s.Name).ToList();
                var result2 = string.Join<string>(",", childrenTeststrings);
                Console.WriteLine("Children of task MoveBody are:{0}", result2);

                var selectTreeTest = unitOfWork.Tasks.SelectTree("Root");
                IEnumerable<string> selectTreeTeststrings = selectTreeTest.Select(s => s.Name).ToList();
                var result3 = string.Join<string>(",", selectTreeTeststrings);
                Console.WriteLine("Selected tree of Root task:{0}", result3);
            }
            Console.WriteLine("Press any key to exit ...");
            Console.ReadKey();
        }

        private static void SetupTestData()
        {
            using (var unitOfWork = new TasksUnitOfWork())
            {
                TaskModel newtask = new TaskModel();
                TaskModel test1 = new TaskModel();
                TaskModel test2 = new TaskModel();
                TaskModel test3 = new TaskModel();
                TaskModel test4 = new TaskModel();
                TaskModel test5 = new TaskModel();

                newtask.Name = "New Task";
                test1.Name = "Test 1 Name";
                test2.Name = "Test 2 Name";
                test3.Name = "Test 3 Name";
                test4.Name = "Test 4 Name";
                test5.Name = "Test 5 Name";

                newtask.Children.Add(test1);
                newtask.Children.Add(test2);
                newtask.Children.Add(test3);
                test1.Children.Add(test4);
                test1.Children.Add(test5);

                TaskModel root = new TaskModel
                {
                    Name = "Root",
                    Children = new List<TaskModel> 
                    { 
                        new TaskModel{Name ="Head"},
                        new TaskModel
                        {
                            Name="MoveBody", 
                            Children=new List<TaskModel>
                            {
                                new TaskModel
                                {
                                    Name="MoveHand", 
                                    Children=new List<TaskModel>
                                    {
                                        new TaskModel{Name="MoveHandMuscle"}
                                    }
                                },
                                new TaskModel
                                {
                                    Name="MoveLeg"
                                }
                            }
                        }
                    }
                };
                var all = unitOfWork.Tasks.GetAll();
                unitOfWork.Tasks.DeleteRange(all);
                unitOfWork.SaveChanges();
                unitOfWork.Tasks.InsertTree(root);
                unitOfWork.Tasks.InsertTree(newtask);
                unitOfWork.SaveChanges();
            }
        }
    }
}
