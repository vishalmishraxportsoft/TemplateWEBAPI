using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateApi.Context;
using TemplateApi.model;
using static TemplateApi.CommonHelper.CommonHelper;

namespace TemplateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly CustomDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DepartmentsController(CustomDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<Json>> GetDepartments()
        {
            var departmentList =  await _context.Departments.ToListAsync();
            if(departmentList != null && departmentList.Count() > 0)
            {
                return new Json() { status = "200" , message = "Data Found", data = departmentList };
            }
            else
            {
                return new Json() { status = "200", message = "No Record Found", data = null };
            }
        }


        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Json>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return new Json() { status = "400", message = "No Record Found", data = null };
            }

            return new Json() {status = "200", message = "Record Found", data = department } ;
        }


        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Json>> PostDepartment(Department department)
        {
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrWhiteSpace(department.Logo))
                {
                    String FileContent = department.Logo.ToLower();
                    if (FileContent.IndexOf("base64") != -1)
                    {
                        String filename = Guid.NewGuid().ToString();
                        String extension = ".jpg";
                        extension = (FileContent.IndexOf("png") != -1) ? ".png" : (FileContent.IndexOf("jpg") != -1) ? ".jpg" : (FileContent.IndexOf("tiff") != -1) ? ".tiff" : (FileContent.IndexOf("jpeg") != -1) ? ".jpeg" : ".jpg";
                        string completepath = "Content/uploads/departmentLogo/" + filename + extension;
                        string filePath = Path.Combine(webHostEnvironment.ContentRootPath, completepath);
                        department.Logo = department.Logo.Substring(department.Logo.IndexOf(",") + 1);
                        try
                        {
                            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(department.Logo));
                        }
                        catch { }
                        department.Logo = '/' + completepath;
                    }
                }

                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
            }
            return new Json() { status = "400", message = "Check Your Parameters", data = ModelState};

        }


        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }


        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
