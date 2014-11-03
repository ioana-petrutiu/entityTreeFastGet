using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;

namespace PreOrderTreeTraversal
{
    public class Context : DbContext
    {
       
        public Context()
            : base("x")
        {
            
        }

        public DbSet<TaskModel> Tasks { get; set; }



    }

    [DebuggerDisplay("{Name} {lft} {rgt}")]
    public class TaskModel
    {
        
        public TaskModel() { Children = new List<TaskModel>(); }

        [Key]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? RootId { get; set; }

        
        public virtual TaskModel Root { get; set; }

        public virtual TaskModel Parent { get; set; }

        [NotMapped]
        public List<TaskModel> Children { get; set; }

        public int? lft { get; set; }

        public int? rgt { get; set; }

        public string Name { get; set; }

    }
}
