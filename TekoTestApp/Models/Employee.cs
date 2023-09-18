using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TekoTestApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
    }
}