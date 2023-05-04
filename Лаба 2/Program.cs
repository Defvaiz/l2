using System;
public class Person
{
    private string firstName;
    private string lastName;
    private DateTime birthDate;

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
    }
    public Person() : this("Maxim", "Pankov", DateTime.MinValue)
    {

    }
    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public DateTime BirthDate
    {
        get { return birthDate; }
        set { birthDate = value; }
    }
    public int BirhYear
    {
        get { return birthDate.Year; }
        set { birthDate = birthDate.AddYears(value - birthDate.Year); }
    }
    public string ToFullString()
    {
        return $"{firstName} {lastName}, {birthDate.ToString("dd.MM.yyyy")}";
    }
    public string ToShortString()
    {
        return $"{firstName} {lastName}";
    }
}
public enum Education
{
    Specialist,
    Bachelor,
    SecondEducation
}
public class Exam
{
    public string Subject { get; set; }
    public int Grade { get; set; }
    public DateTime ExamDate { get; set; }
    
    public Exam(string subject, int grade, DateTime examDate)
    {
        Subject = subject;
        Grade = grade;
        ExamDate = examDate;
    }
    public Exam()
    {
        Subject = "Unknown";
        Grade = 0;
        ExamDate = DateTime.Now;
    }
    public string ToFullString()
    {
        return $"Subject: {Subject}, Grade: {Grade}, Exam Date: {ExamDate}";
    }
}
class Student
{
    private Person person;
    private Education education;
    private int groupNumber;
    private Exam[] exams;

    public Student(Person person, Education education, int groupNumber)
    {
        this.person = person;
        this.education = education;
        this.groupNumber = groupNumber;
        exams = new Exam[0];
    }
    public Student()
    {
        person = new Person();
        education = Education.Bachelor;
        groupNumber = 0;
        exams = new Exam[0];

    }
    public Person Person
    {
        get { return person; }
        set { person = value; }
    }
    public Education Education
    {
        get { return education; }
        set { education = value; }
    }
    public int GroupNumber
    {
        get { return groupNumber; }
        set { groupNumber = value; }
    }
    public Exam[] Exams
    {
        get { return exams; }
        set { exams = value; }
    }
    public double AverageScore
    {
        get
        {
            if (exams.Length == 0)
            {
                return 0;
            }
            double sum = 0;
            foreach(Exam exam in exams)
            {
                sum += exam.Grade;
            }
            return sum / exams.Length;
        }
    }
    public void AddExams(params Exam[] newExams)
    {
        int length = exams.Length;
        Array.Resize(ref exams, length + newExams.Length);
        for (int i = 0; i < newExams.Length; i++)
        {
            exams[length + i] = newExams[i];
        }
    }
    public string ToFullString()
    {
        string examsInfo = " ";
        foreach (var item in exams)
            examsInfo += $"{item.Subject} {item.Grade}\n";
        return $"Студент: {person.FirstName} {person.LastName} {person.BirthDate}, Форма обучения: {education}, Группа: {groupNumber}, Результаты экзаменов:\n{examsInfo}";
    }
    public string ToShortString()
    {
        return $"Студент: {person.FirstName} {person.LastName} {person.BirthDate}, Форма обучения: {education}, Группа: {groupNumber}, Средний балл: {AverageScore}";
    }
}

class Program
{
    static void Main()
    {
        // Создаем студента
        Student student = new Student(new Person("Иван", "Иванов", new DateTime(2003, 5, 1)), Education.Bachelor, 11);

        // Устанавливаем список экзаменов
        student.AddExams(
            new Exam("Математика", 4, new DateTime(2023, 1, 15)),
            new Exam("Физика", 5, new DateTime(2023, 1, 20)),
            new Exam("История", 3, new DateTime(2023, 1, 25))
        );

        // Выводим данные о студенте
        Console.WriteLine("Краткая информация о студенте:");
        Console.WriteLine(student.ToShortString());
        Console.WriteLine();

        Console.WriteLine("Полная информация о студенте:");
        Console.WriteLine(student.ToFullString());
        Console.WriteLine();

        // Добавляем еще один экзамен и выводим обновленную информацию
        student.AddExams(new Exam("Информатика", 4, new DateTime(2023, 2, 1)));
        Console.WriteLine("Полная информация о студенте после добавления экзамена:");
        Console.WriteLine(student.ToFullString());

        
        Console.ReadKey();
    }
}

