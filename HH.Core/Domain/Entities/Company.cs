using System;
using System.Collections.Generic;
using HH.Core.Domain;

namespace HH.Core
{
    public class Company : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Vacancy> Vacancies { get; set; }
    }
}
