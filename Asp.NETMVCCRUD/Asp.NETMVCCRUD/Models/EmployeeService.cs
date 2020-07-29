using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asp.NETMVCCRUD.Models
{
    public class EmployeeService
    {
         public List<Employee> GetAll()
         {
            using (DBModel db = new DBModel())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return empList;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (DBModel db = new DBModel())
            {
               return db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();
            }
        }
    }
}