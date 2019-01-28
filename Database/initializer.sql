USE [master];
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'School')
	DROP DATABASE School;
GO

-- Create the School database.
CREATE DATABASE School;
GO

-- Specify a simple recovery model 
-- to keep the log growth to a minimum.
ALTER DATABASE School 
	SET RECOVERY SIMPLE;
GO

USE School;
GO

-- Create the Departments table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[Department]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Departments](
	[DepartmentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Budget] [money] NOT NULL,
	[CreatedDate] [datetime] NOT NULL
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

-- Create the Students table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[Students]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Students](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,	
	[EnrollmentDate] [datetime] NULL,
 CONSTRAINT [PK_School.Students] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

-- Create the Instructors table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[Instructors]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Instructors](
	[InstructorID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,	
	[HireDate] [datetime] NULL,
	[Terminated] [bit] NOT NULL,
 CONSTRAINT [PK_School.Instructors] PRIMARY KEY CLUSTERED 
(
	[InstructorID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

-- Create the CourseInstructors table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[CourseInstructors]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CourseInstructors](
	[CourseID] [int] NOT NULL,
	[InstructorID] [int] NOT NULL,
 CONSTRAINT [PK_CourseInstructor] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC,
	[InstructorID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

-- Create the Courses table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[Courses]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] NOT NULL IDENTITY(5001, 1),
	[Name] [nvarchar](100) NOT NULL,
	[Credits] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
 CONSTRAINT [PK_School.Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

--Create the StudentCourses table.
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[dbo].[StudentCourses]') 
		AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StudentCourses](
	[StudentID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Grade] decimal(18,2) NOT NULL,
	[EnrolledYear] [int] NOT NULL,
	[EnrolledSemester] NVARCHAR(10) NOT NULL,
	[Completed] [bit] NOT NULL,	
	[Dropped] [bit] NOT NULL,
	[DroppedTime] [DateTime] NULL,
 CONSTRAINT [PK_School.StudentCourses] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC,
	[CourseID] ASC,
	[EnrolledYear] ASC,
	[EnrolledSemester] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'[dbo].[FK_Students_StudentCourses]')
   AND parent_object_id = OBJECT_ID(N'[dbo].[StudentCourses]'))
ALTER TABLE [dbo].[StudentCourses]  WITH CHECK ADD  
   CONSTRAINT [FK_Students_StudentCourses] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([StudentID])
GO
ALTER TABLE [dbo].[StudentCourses] CHECK 
   CONSTRAINT [FK_Students_StudentCourses]
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'[dbo].[FK_Courses_StudentCourses]')
   AND parent_object_id = OBJECT_ID(N'[dbo].[StudentCourses]'))
ALTER TABLE [dbo].[StudentCourses]  WITH CHECK ADD  
   CONSTRAINT [FK_Courses_StudentCourses] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[StudentCourses] CHECK 
   CONSTRAINT [FK_Courses_StudentCourses]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'[dbo].[FK_Courses_Departments]')
   AND parent_object_id = OBJECT_ID(N'[dbo].[Courses]'))
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  
   CONSTRAINT [FK_Courses_Departments] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([DepartmentID])
GO
ALTER TABLE [dbo].[Courses] CHECK 
   CONSTRAINT [FK_Courses_Departments]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'[dbo].[FK_Courses_CourseInstructors]')
   AND parent_object_id = OBJECT_ID(N'[dbo].[CourseInstructors]'))
ALTER TABLE [dbo].[CourseInstructors]  WITH CHECK ADD  
   CONSTRAINT [FK_Courses_CourseInstructors] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[CourseInstructors] CHECK 
   CONSTRAINT [FK_Courses_CourseInstructors]
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'[dbo].[FK_Instructors_CourseInstructors]')
   AND parent_object_id = OBJECT_ID(N'[dbo].[CourseInstructors]'))
ALTER TABLE [dbo].[CourseInstructors]  WITH CHECK ADD  
   CONSTRAINT [FK_Instructors_CourseInstructors] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[Instructors] ([InstructorID])
GO
ALTER TABLE [dbo].[CourseInstructors] CHECK 
   CONSTRAINT [FK_Instructors_CourseInstructors]
GO



-- Insert data into the Person table.
USE School
GO

--TODO: generate test data.

DELETE FROM dbo.StudentCourses;
DELETE FROM dbo.CourseInstructors;
DELETE FROM dbo.Courses;
DELETE FROM dbo.Students;
DELETE FROM dbo.Instructors;
DELETE FROM dbo.Departments;


INSERT INTO [dbo].[Departments] ([DepartmentID] ,[Name] ,[Budget] ,[CreatedDate]) VALUES (1001, 'Business', 1500000, '1950-01-01');
INSERT INTO [dbo].[Departments] ([DepartmentID] ,[Name] ,[Budget] ,[CreatedDate]) VALUES (1002, 'Engineering', 3000000, '1972-01-01');
INSERT INTO [dbo].[Departments] ([DepartmentID] ,[Name] ,[Budget] ,[CreatedDate]) VALUES (1003, 'Human Sciences', 4200000, '1950-01-01');
INSERT INTO [dbo].[Departments] ([DepartmentID] ,[Name] ,[Budget] ,[CreatedDate]) VALUES (1004, 'Design', 150000, '1988-01-01');
INSERT INTO [dbo].[Departments] ([DepartmentID] ,[Name] ,[Budget] ,[CreatedDate]) VALUES (1005, 'Medicine', 5000000, '1949-01-01');

INSERT INTO [dbo].[Instructors] ([FirstName],[LastName] ,[HireDate] ,[Terminated]) VALUES ('James', 'Daunting', '2004-08-14', 0);
INSERT INTO [dbo].[Instructors] ([FirstName],[LastName] ,[HireDate] ,[Terminated]) VALUES ('Amanda', 'Charbonneau', '1963-06-27', 1);
INSERT INTO [dbo].[Instructors] ([FirstName],[LastName] ,[HireDate] ,[Terminated]) VALUES ('Lewis', 'Bell', '1996-12-28', 0);
INSERT INTO [dbo].[Instructors] ([FirstName],[LastName] ,[HireDate] ,[Terminated]) VALUES ('Barbara', 'Rasmussen', '2019-01-02', 0);
INSERT INTO [dbo].[Instructors] ([FirstName],[LastName] ,[HireDate] ,[Terminated]) VALUES ('Laurel', 'Harris', '2015-06-27', 0);

INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Jane', 'Rapp', '2018-01-06');
INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Donald', 'Reed', '2018-01-06');
INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Anton', 'Walsh', '2018-01-06');
INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Angel', 'Breazeale', '2018-01-06');
INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Raymundo', 'Criner', '2017-01-06');
INSERT INTO [dbo].[Students] ([FirstName] ,[LastName] ,[EnrollmentDate]) VALUES ('Roseann', 'Scott', '2017-01-06');

INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Intro to Math', 3, 1002);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Intro to Chem', 3, 1005);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Intro to Bio', 3, 1005);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Advanced Engineering', 3, 1002);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Historical Singers', 2, 1004);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Intro to Planking', 1, 1002);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Advanced Diagnosises', 5, 1005);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Macroeconomics', 3, 1001);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Accounting 302', 3, 1001);
INSERT INTO [dbo].[Courses] ([Name] ,[Credits] ,[DepartmentID]) VALUES ('Advanced Sciences', 4, 1003);

INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5001, 1);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5006, 1);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5002, 2);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5003, 2);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5007, 2);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5010, 3);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5007, 4);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5005, 4);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5008, 5);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5009, 5);
INSERT INTO [dbo].[CourseInstructors] ([CourseID] ,[InstructorID]) VALUES (5005, 5);

INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (1 ,5001 ,3.5 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (1 ,5002 ,2.9 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (1 ,5003 ,3.2 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (1 ,5006 ,4.0 ,2019 , 'Winter',0, 0, NULL);

INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (2 ,5010 ,3.5 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (2 ,5008 ,2.9 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (2 ,5009 ,3.2 ,2018 , 'Fall',1, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (2 ,5002 ,3.95 ,2018 , 'Fall',1, 0, NULL);

INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (3 ,5001 ,3.5 ,2019 , 'Fall',0, 1, '2018-08-24');
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (3 ,5002 ,2.9 ,2019 , 'Fall',0, 1, '2018-08-24');
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (3 ,5001 ,3.2 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (3 ,5002 ,4.0 ,2019 , 'Winter',0, 0, NULL);

INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (4 ,5001 ,2.0 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (4 ,5002 ,2.1 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (4 ,5003 ,2.5 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (4 ,5005 ,4.0 ,2019 , 'Winter',0, 0, NULL);

INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (5 ,5010 ,3.5 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (5 ,5007 ,2.9 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (5 ,5004 ,3.2 ,2019 , 'Winter',0, 0, NULL);
INSERT INTO [dbo].[StudentCourses] ([StudentID] ,[CourseID] ,[Grade] ,[EnrolledYear] ,[EnrolledSemester] ,[Completed] ,[Dropped] ,[DroppedTime]) VALUES (5 ,5001 ,4.0 ,2018 , 'Fall',1, 0, NULL);

GO