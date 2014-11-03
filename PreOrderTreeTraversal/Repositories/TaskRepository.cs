using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PreOrderTreeTraversal.Repositories
{
    public class TaskRepository : PreOrderTreeTraversal.GenericRepository.GenericRepository<TaskModel>
    {
        public TaskRepository (DbContext dbContext)
            : base(dbContext)
        {
            FillRootIdsForRoots();
            dbContext.SaveChanges();
        }


        public void InsertTree(TaskModel rootNode)
        {
            InsertLeftAndRight(rootNode, 0);
            FillParentsAndRoot(rootNode);
            Add(rootNode);
        }

        private void FillParentsAndRoot(TaskModel node)
        {
            foreach (var child in node.Children)
            {
                child.Parent = node;
                if (node.lft == 1)
                {
                    child.Root = node;
                }
                else
                {
                    child.Root = node.Root;
                }
                Add(child);
                FillParentsAndRoot(child);
            }
        }

        public int counter;
        private void InsertLeftAndRight(TaskModel node, int level)
        {
            if (level == 0)
            {
                counter = 0;
            }
            counter++;
            node.lft = counter;
            foreach (var child in node.Children)
            {
                InsertLeftAndRight(child, level + 1);
            }
            counter++;
            node.rgt = counter;
        }


        public List<TaskModel> GetParents(string taskName)
        {
            var currentTask = GetTaskByName(taskName);
            if (currentTask.RootId != currentTask.Id)
            {
                return dbSet.Where(x => x.lft < currentTask.lft && x.rgt > currentTask.rgt && x.RootId == currentTask.RootId)
                .ToList();
            }
            else
            {
                return new List<TaskModel>();
            }
        }

        public List<TaskModel> GetChildren(string taskName)
        {
            var currentTask = GetTaskByName(taskName);
                return dbSet.Where(x => x.lft > currentTask.lft && x.lft < currentTask.rgt && x.RootId == currentTask.RootId)
                    .ToList();
        }


        public List<TaskModel> SelectTree(string taskName)
        {
            var listOfChildren = GetChildren(taskName);
            var listOfParents = GetParents(taskName);
            var treeTaskModels = listOfChildren.Concat(listOfParents);
            var orderedTree = treeTaskModels.OrderBy(x => x.lft).ToList();
            return orderedTree;
        }


        internal List<TaskModel> GetRoots()
        {
            return dbSet.Where(t => t.ParentId == null).ToList();
        }

        internal void FillRootIdsForRoots()
        {
            dbSet.Where(t => t.ParentId == null && t.RootId == null).ToList().ForEach(root=>root.Root = root);
        }

        private TaskModel GetTaskByName(string taskName)
        {
            return dbSet.First(t => t.Name == taskName);
        }

    }
}
