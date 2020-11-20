using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateApi.model
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string CategoryLogo { get; set; }

    }

    public class CategoryVM
    {
        public List<Category> List { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Picture { get; set; }
        public string Photo { get; set; }
        public string Requirement { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
        public string ParticipationIcon { get; set; }
        //Is the number of seats limited?
        public bool ISsetlimited { get; set; }
        //Number of seats
        public int? Setlimited { get; set; }
        //Do you need approval?
        public bool IsNeedApproval { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }

    public class UserInEvent
    {
        public int Id { get; set; }
        public Event Events { get; set; }
        public int EventId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
        // changed to here 
        public string EventBarcode { get; set; }
        // add this if need Event approval?
        public bool? IsApproved { get; set; }
    }

    // You can use a default user for testing
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserInEvent> UserInCourses { get; set; }
    }

    // Created on 16-11-2020

    //Department  // Modified 18-11-2020
    public class Department
    {
        public int Id { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string Logo { get; set; }
    }

    // Suggestion
    public class Suggestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }

    // Complaint
    public class Complaint
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }

    //MY Note
    public class Note
    {
        public int Id { get; set; }
        public String Details { get; set; }
    }

    //request Support
    public class request_support
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string supportType { get; set; }
    }

}
