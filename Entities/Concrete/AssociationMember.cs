using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class AssociationMember : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }

  
}