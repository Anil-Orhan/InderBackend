
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PostRepository : EfEntityRepositoryBase<Post, ProjectDbContext>, IPostRepository
    {
        public PostRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
