﻿namespace Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? Birthday { get; set; }

        public List<StudentCourses> Courses { get; set; } = new List<StudentCourses>();

        public List<Homework> Homeworks { get; set; } = new List<Homework>();
    }
}
