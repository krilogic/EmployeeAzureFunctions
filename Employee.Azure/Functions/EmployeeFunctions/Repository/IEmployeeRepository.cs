using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Employee.Azure.Models;


namespace Employee.Azure.Repository
{
    public interface IEmployeeRepository
    {
        Employees Add(Employees employee);
        IList<Employees> GetList();
        Employees GetEmployee(int id);
        bool EditEmployee(Employees teacher);
        void DeleteEmployee(int id);
    }
}
