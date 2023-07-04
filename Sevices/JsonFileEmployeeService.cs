using Employees.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Employees.Sevices
{
    public class JsonFileEmployeeService
    {
        private string JsonFileName
        {
            get { return Path.Combine("C:\\Projects\\Employees\\Data\\employeesTable.json"); }
        }

        public void SerializeFile<Employee>(Employee employees)
        {
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                try
                {
                    outputStream.SetLength(0);
                    JsonSerializer.SerializeAsync(outputStream, employees, options);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Employee[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            
        }

        public Employee Get(string lastName)
        {
            var employees = GetAllEmployees();

            return employees.FirstOrDefault(emp => emp.LastName == lastName);
        }
        
        public void UpdateEmployee(string targetName,string firstName, string lastName, int years, string jobTitle)
        {
            var employees = GetAllEmployees().ToList();
            var existingEmployee = employees.FirstOrDefault(emp => emp.LastName == targetName);
            
            if (existingEmployee !=null)
            {
                if (!string.IsNullOrEmpty(firstName))
                {
                    existingEmployee.FirstName = firstName;
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                   existingEmployee.LastName = lastName;
                }
                if (years != 0)
                {
                    existingEmployee.Years = years;
                }
                if (!string.IsNullOrEmpty(jobTitle))
                {
                    existingEmployee.JobTitle = jobTitle;
                }
            }else{
                //throw new ArgumentException("invalid id");
                return;
            }
            SerializeFile(employees);
           
        }

        public void CreateEmployee(string firstName, string lastName, int years, string jobTitle)
        {
            var employee = new Employee();
            var employees = GetAllEmployees().ToList();
            if (employees.Count == 0)
            {
                employee.Id = 0;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Years = years;
                employee.JobTitle = jobTitle;
            }
            else
            {
                var lastID = employees.Count;
                employee.Id = employees[lastID -1].Id + 1;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Years = years;
                employee.JobTitle = jobTitle;
            }

            employees.Add(employee);
            SerializeFile(employees);
        }

        public void DeleteEmployee(string targetName)
        {
            var employees = GetAllEmployees().ToList();
            var existingEmployee = employees.FirstOrDefault(emp => emp.LastName == targetName);

            if (employees != null)
            {
                employees.Remove(existingEmployee);

            }
            else
            {
                return;
            }

            SerializeFile(employees);
        }
    }
}
