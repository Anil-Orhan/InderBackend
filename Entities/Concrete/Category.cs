using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }


}