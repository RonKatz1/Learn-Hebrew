using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace Domain.Repositories
{
    abstract public class Repository : IDisposable
    {
        protected Entity.LearnHebrewDbContext LearnHebrewDB { get; set; }

        public Repository()
        {
            this.LearnHebrewDB = new Entity.LearnHebrewDbContext();

            ((IObjectContextAdapter)this.LearnHebrewDB).ObjectContext.ContextOptions.LazyLoadingEnabled = false;

        }

        void IDisposable.Dispose()
        {
            this.LearnHebrewDB.Dispose();
        }
    }
}
