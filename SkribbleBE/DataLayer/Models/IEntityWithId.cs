using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public interface IEntityWithId
    {
        public int Id { get; set; }
    }
}
