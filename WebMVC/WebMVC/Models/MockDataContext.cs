using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebMVC.Models
{
    public class MockDataContext : DbContext 
    {
        public DbSet<MockData> MockDatas { get; set; }
    }
}