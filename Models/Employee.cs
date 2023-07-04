using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Years { get; set; }

        [JsonPropertyName("Job Title")]
        public string JobTitle { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Employee>(this);
   
    }
}
