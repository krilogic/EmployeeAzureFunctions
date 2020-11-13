using Employee.Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employee.Azure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employees> _empList = new List<Employees>();
        private int _Id = 1;
        public EmployeeRepository()
        {
            this.Add(new Employees() { empName = "Raj Beniwal", designation = "Asst. Project Manager", address = "Noida" });
            this.Add(new Employees() { empName = "Dev Malhotra", designation = "Tech Lead", address = "Banglore" });
            this.Add(new Employees() { empName = "Neelesh Arora", designation = "System Analyst", address = "Pune" });
            this.Add(new Employees() { empName = "Samy Verma", designation = "Project Manager", address = "Washingtom DC" });
            this.Add(new Employees() { empName = "Ravinder Malik", designation = "Team Lead", address = "Gurgaon" });
        }
        public Employees Add(Employees employee)
        {
            employee.empId = _Id++;
            _empList.Add(employee);
            return employee;
        }

        public void DeleteEmployee(int id)
        {
            Employees emp = _empList.Find(e => e.empId == id);
            _empList.Remove(emp);
        }

        public bool EditEmployee(Employees employee)
        {
            int index = _empList.FindIndex(e => e.empId == employee.empId);
            if (index==-1)
            {
                return false;
            }
            _empList.RemoveAt(index);
            _empList.Add(employee);
            return true;
        }

        public Employees GetEmployee(int id)
        {
            return _empList.Find(e => e.empId == id);
        }

        public IList<Employees> GetList()
        {
            return _empList.ToList();
        }
    }
}
