using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interface;
using Infrastructure.Service;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentGroupService, StudentGroupService>();
builder.Services.AddScoped<IGroupService, GroupService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(n => n.SwaggerEndpoint("/swagger/v1/swagger.json", "My App"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();

// create table students
// (
// 	id serial primary key,
// 	fullName varchar(150),
// 	email varchar(150),
// 	phone varchar(20),
// 	enrollmentDate date
// );

// INSERT INTO Students (FullName, Email, Phone, EnrollmentDate)
// VALUES 
// ('Александр Петров', 'petrov@example.com', '7778889990', '2024-02-15'),
// ('Екатерина Соколова', 'sokolova@example.com', '1112223334', '2024-03-10'),
// ('Дмитрий Иванов', 'ivanov_d@example.com', '9991112223', '2024-01-20'),
// ('Ольга Смирнова', 'smirnova_o@example.com', '5556667778', '2024-02-28'),
// ('Сергей Федоров', 'fedorov_s@example.com', '3332221110', '2024-04-05'),
// ('Марина Орлова', 'orlova_m@example.com', '7775553332', '2024-03-15'),
// ('Артем Кузнецов', 'kuznetsov_a@example.com', '6669998887', '2024-05-01'),
// ('Наталья Лебедева', 'lebedeva_n@example.com', '4443332226', '2024-06-10'),
// ('Игорь Васильев', 'vasiliev_i@example.com', '1114445557', '2024-07-20'),
// ('Виктория Морозова', 'morozova_v@example.com', '2225558884', '2024-08-05');

// select * from students

// create table courses
// (
// 	id serial primary key,
// 	title varchar(200),
// 	description text,
// 	durationWeeks int
// );

// INSERT INTO Courses (Title, Description, DurationWeeks)
// VALUES 
// ('Программирование на C#', 'Курс по основам C# и .NET', 12),
// ('Web-разработка', 'Frontend и Backend разработка', 16),
// ('Data Science', 'Основы анализа данных и машинного обучения', 14),
// ('UI/UX Дизайн', 'Проектирование пользовательского интерфейса', 10),
// ('DevOps', 'Автоматизация и CI/CD процессы', 8),
// ('Мобильная разработка', 'Разработка приложений для iOS и Android', 12),
// ('Кибербезопасность', 'Основы защиты информации', 10),
// ('Game Development', 'Создание игр на Unity и Unreal Engine', 20),
// ('Базы данных', 'Проектирование и оптимизация SQL и NoSQL', 12),
// ('Cloud Computing', 'Работа с AWS, Azure и Google Cloud', 16);

// select * from courses

// create table mentors
// (
// 	id serial primary key,
// 	fullName varchar(150),
// 	email varchar(150),
// 	phone varchar(20),
// 	specialization int
// );

// INSERT INTO Mentors (FullName, Email, Phone, Specialization)
// VALUES 
// ('Иван Иванов', 'ivanov@example.com', '1234567890', null),
// ('Мария Смирнова', 'smirnova@example.com', '0987654321',null ),
// ('Александр Кузнецов', 'kuznetsov@example.com', '7778889990', null),
// ('Светлана Орлова', 'orlova@example.com', '5551234567', null),
// ('Дмитрий Федоров', 'fedorov@example.com', '9998887776', null),
// ('Ольга Николаева', 'nikolaeva@example.com', '4445556667', null),
// ('Анастасия Лебедева', 'lebedeva@example.com', '3334445556', null),
// ('Виктор Петров', 'petrov@example.com', '1112223334', null),
// ('Екатерина Морозова', 'morozova@example.com', '8887776665', null),
// ('Сергей Васильев', 'vasiliev@example.com', '2223334445',null);

// select * from mentors

// select * from mentors

// create table groups
// (
// 	id serial primary key,
// 	groupName varchar(100),
// 	courseId int references courses(id),
// 	mentorId int references mentors(id),
// 	startDate date,
// 	endDate date
// );

// INSERT INTO Groups (GroupName, CourseId, MentorId, StartDate, EndDate)
// VALUES 
// ('C# Базовый', 1, 1, '2024-03-01', '2024-06-01'),
// ('Web-разработка 2024', 2, 2, '2024-04-01', '2024-08-01'),
// ('Data Science Pro', 3, 3, '2024-05-01', '2024-09-01'),
// ('UI/UX Дизайн 2024', 4, 4, '2024-06-01', '2024-08-10'),
// ('DevOps 2024', 5, 5, '2024-07-01', '2024-09-30'),
// ('Мобильная разработка iOS', 6, 6, '2024-05-15', '2024-09-15'),
// ('Cyber Security 101', 7, 7, '2024-06-20', '2024-09-20'),
// ('Game Dev Unity', 8, 8, '2024-07-05', '2024-12-05'),
// ('SQL & NoSQL', 9, 9, '2024-08-01', '2024-10-01'),
// ('Cloud Tech Experts', 10, 10, '2024-09-01', '2024-12-01');

// select * from groups

// create table studentGroups
// (
// 	id serial primary key,
// 	studentId int references students(id),
// 	groupId int references groups(id),
// 	status int
// );

// INSERT INTO StudentGroups (StudentId, GroupId, Status)
// VALUES 
// (1, 1, null),
// (2, 1, null),
// (3, 2, null),
// (4, 2, null),
// (5, 3, null),
// (6, 4, null),
// (7, 5, null),
// (8, 5, null),
// (9, 6, null),
// (10, 7,null),
// (1, 8, null),
// (3, 9, null),
// (4, 10, null),
// (6, 7, null),
// (8, 3, null);











