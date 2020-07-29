using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;
using System.Threading;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeService employeeService = new EmployeeService();

        //
        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            //return Json(new { data = employeeService.GetAll() }, JsonRequestBehavior.AllowGet);
			            using (DBModel db = new DBModel())
            {
                List<Employee> empList = db.Employees.ToList<Employee>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee());
            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Employee emp)
        {

            if (emp.IsValidate() == false)
            {
                return Json(new { success = false, message = "An error occurred" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Thread.Sleep(200);
                using (DBModel db = new DBModel())
                {
                    if (emp.EmployeeID == 0)
                    {
                        db.Employees.Add(emp);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Add Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        // Lấy dữ liệu theo Id trên form
                        var check = db.Employees.Where(x => x.EmployeeID == emp.EmployeeID).FirstOrDefault<Employee>();

                        //Kiểm tra ID trên form có tồn tại không? Có thì cho phép Update, ngược lại báo lỗi
                        if (check != null)
                        {
                           // db.Entry(emp).State = EntityState.Modified;
                            db.Entry(check).CurrentValues.SetValues(emp);
                            db.SaveChanges();
                            return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, message = "Data does not exist. Please reload page!" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                // Lấy dữ liệu theo Id trên form
                var emp = db.Employees.Where(x => x.EmployeeID == id).FirstOrDefault<Employee>();

                //Kiểm tra ID trên form có tồn tại không? Có thì cho phép Update, ngược lại báo lỗi
                if (emp!=null){
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else{
                    return Json(new { success = false, message = "Data does not exist. Please reload page!" }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }

    public static class EmployeeExt
    {
        public static bool IsValidate(this Employee employee)
        {
            if (String.IsNullOrEmpty(employee.Office) || String.IsNullOrEmpty(employee.Position) || String.IsNullOrEmpty(employee.Name))
                return false;
            return true;
        }
    }
}