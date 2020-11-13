using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Employee.Azure.Repository;
using System.Linq;
using Employee.Azure.Models;

namespace Employee.Azure.Functions
{
    public class EmployeeFunction
    {
        //private readonly EmployeeRepository _dbContext;

        //public EmployeeFunction(EmployeeRepository dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        [FunctionName("PostEmp")]
        public async Task<IActionResult> Post(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "emp")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Create Employee request processed started.");

            EmployeeRepository _dbContext = new EmployeeRepository();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Employees>(requestBody);
            Employees emp = _dbContext.Add(data);

            return new OkObjectResult(emp);
        }

        [FunctionName("GetEmpList")]
        public static IActionResult GetEmps(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "emplist")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting todo list items");

            EmployeeRepository _dbContext = new EmployeeRepository();
            return new OkObjectResult(_dbContext.GetList());
        }

        [FunctionName("GetEmpById")]
        public static IActionResult GetEmpById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "emp/{id}")] HttpRequest req, ILogger log, string id)
        {
            EmployeeRepository _dbContext = new EmployeeRepository();

            var emp = _dbContext.GetList().Where(e => e.empId == Convert.ToInt32(id));
            if (emp == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(emp);
        }

        [FunctionName("DeleteEmpById")]
        public static IActionResult DeleteEmpById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route =   "emp/{id}")] HttpRequest req, ILogger log, string id)
        {
            EmployeeRepository _dbContext = new EmployeeRepository();
            _dbContext.DeleteEmployee(Convert.ToInt32(id));
            return new OkResult();
        }

        [FunctionName("UpdateEmpById")]
        public static async Task<IActionResult> UpdateEmpById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route =  "emp/{id}")] HttpRequest req, ILogger log, string id)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Employees>(requestBody);

            Employees emp = new Employees();
            emp.empId = data.empId;
            emp.empName = data.empName;
            emp.empSalary = data.empSalary;
            emp.designation = data.designation;
            emp.address = data.address;

            EmployeeRepository _dbContext = new EmployeeRepository();
            _dbContext.EditEmployee(emp);
            return new OkResult();
        }
    }
}
