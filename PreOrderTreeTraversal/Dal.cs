using PreOrderTreeTraversal.Repositories;
using System;

namespace PreOrderTreeTraversal
{
    class Dal
    {
        public Dal()
        {

        }

        [ThreadStatic]
        private static TasksUnitOfWork context;

        public static T Db<T>(Func<TasksUnitOfWork, T> func)
        {
            if (context == null || context.IsDisposed)
            {
                using (context = new TasksUnitOfWork())
                {
                    return func(context);
                }
            }
            return func(context);
        }

        public static void Db(Action<TasksUnitOfWork> func)
        {
            Db(d =>
            {
                func(d);
                return 1;
            });

        }
    }
}
