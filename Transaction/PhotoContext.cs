using System;
using System.Collections.Generic;
using System.Data.Entity;
using Transaction.model;

namespace Transaction
{
    class PhotoContext : DbContext
    {
        public PhotoContext()
            : base("DbConnection")
        { }

        public DbSet<Photo> Photos { get; set; }
    }
}