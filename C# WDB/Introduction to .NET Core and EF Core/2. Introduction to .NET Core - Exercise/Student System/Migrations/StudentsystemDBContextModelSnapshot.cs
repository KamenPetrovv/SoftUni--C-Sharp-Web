﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace StudentSystem.Migrations
{
    [DbContext(typeof(StudentsystemDBContext))]
    partial class StudentsystemDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Data.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("ContentType");

                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.Property<DateTime>("SubmissionDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("Data.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<DateTime>("RegistrationDate");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Data.StudentCourses", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("CourseId");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("Data.Homework", b =>
                {
                    b.HasOne("Data.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Student", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.Resource", b =>
                {
                    b.HasOne("Data.Course", "Course")
                        .WithMany("Resources")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Data.StudentCourses", b =>
                {
                    b.HasOne("Data.Student", "Student")
                        .WithMany("Courses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Data.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}