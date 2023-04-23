﻿using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Information : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
    }
}