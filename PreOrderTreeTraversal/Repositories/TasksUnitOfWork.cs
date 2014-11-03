using PreOrderTreeTraversal.GenericRepository;

namespace PreOrderTreeTraversal.Repositories
{
    public class TasksUnitOfWork : UnitOfWork
    {

        public TasksUnitOfWork() : base(new TreeTraversalContext())
        {
            Tasks = new TaskRepository(context);
        }

        public TaskRepository Tasks { get; set; }

        public override void SaveChanges()
        {
            base.SaveChanges();
           
        }

    }
}
