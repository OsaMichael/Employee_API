using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDataAccess;
using System.Web.Http.Cors;

namespace EmployeeService.Controllers
{
    //[EnableCors("*", "*","*")]
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage Get(string gender ="All")
        {
            using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
            {
                switch(gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e =>e.Gender.ToLower() == "male").ToList());

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            entities.Employees.Where(e => e.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for genger must be All, Male or Female." + gender + "is invalid.");

                }
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "ot Found");
                
            }
        }
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try { 

            using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
            {
                entities.Employees.Add(employee);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                return message;
               }
             }
            catch (Exception ex)
            {
              return  Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put([FromBody]int id, [FromUri] Employee employee)
        {
            using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + "not found to update");
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }         
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
               {
            using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
            {
                var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                if(entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + "not found to delete");
                }
                else
                {
                    entities.Employees.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
     
            }
          }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //public IEnumerable<Employee> Get()
        //{
        //    using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
        //    {
        //        return entities.Employees.ToList();
        //    }
        //}
        //public HttpResponseMessage Get(int id)
        //{
        //    using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
        //    {
        //        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //        if (entity != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + "ot Found");

        //    }
        //}
        //public HttpResponseMessage Post([FromBody] Employee employee)
        //{
        //    try
        //    {

        //        using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
        //        {
        //            entities.Employees.Add(employee);
        //            entities.SaveChanges();

        //            var message = Request.CreateResponse(HttpStatusCode.Created, employee);
        //            message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
        //            return message;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        //{
        //    using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
        //    {
        //        var entity = entities.Employees.FirstOrDefault(e => e.ID == id);

        //        if (entity == null)
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + "not found to update");
        //        }
        //        else
        //        {
        //            entity.FirstName = employee.FirstName;
        //            entity.LastName = employee.LastName;
        //            entity.Gender = employee.Gender;
        //            entity.Salary = employee.Salary;

        //            entities.SaveChanges();
        //            return Request.CreateResponse(HttpStatusCode.OK, entity);
        //        }
        //    }
        //}

        //public HttpResponseMessage Delete(int id)
        //{
        //    try
        //    {
        //        using (Emp_Web_APIEntities entities = new Emp_Web_APIEntities())
        //        {
        //            var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
        //            if (entity == null)
        //            {
        //                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id =" + id.ToString() + "not found to delete");
        //            }
        //            else
        //            {
        //                entities.Employees.Remove(entity);
        //                entities.SaveChanges();
        //                return Request.CreateResponse(HttpStatusCode.OK);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
    }
}
