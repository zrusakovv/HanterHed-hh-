﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.Core
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }

        public ICollection<Summary> Summaries { get; set; }
    }
}
