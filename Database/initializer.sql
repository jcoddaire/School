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
	[CourseID] [int] NOT NULL,
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
	[Grade] decimal(2,2) NOT NULL,
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


GO