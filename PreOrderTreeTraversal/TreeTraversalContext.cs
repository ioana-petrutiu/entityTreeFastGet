using System.Data.Entity;

namespace PreOrderTreeTraversal
{
    public partial class TreeTraversalContext : System.Data.Entity.DbContext
    {
        public TreeTraversalContext()
            : base("name=x")
        {
        }
    
        public System.Data.Entity.DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>().
              HasOptional(e => e.Parent).
              WithMany().
              HasForeignKey(m => m.ParentId);
        }
       
    }
}
