using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TekoTestApp.Models;

namespace TekoTestApp.Services
{
    public class DataGeneratorService
    {
        public List<Employee> GenerateRandomEmployees(int count)
        {
            List<Employee> employees = new List<Employee>();
            Random rand = new Random();

            string[] genders = { "Мужчина", "Женщина" };
            string[] positions = { "Менеджер", "Разработчик", "Дизайнер", "HR-менеджер", "Бухгалтер", "Продавец", "Маркетинг", "Техническая поддержка", "Аналитик", "Стажировщик" };

            // Примеры популярных имен и фамилий
            string[] maleNames = { "Иван", "Александр", "Сергей", "Дмитрий", "Михаил" };
            string[] femaleNames = { "Елена", "Ольга", "Татьяна", "Наталья", "Ирина" };
            string[] lastNames = { "Иванов", "Петров", "Смирнов", "Сергеев", "Козлов" };

            for (int i = 1; i <= count; i++)
            {
                string randomGender = genders[rand.Next(genders.Length)];
                string randomPosition = positions[rand.Next(positions.Length)];
                string randomName = randomGender == "Мужчина" ? maleNames[rand.Next(maleNames.Length)] : femaleNames[rand.Next(femaleNames.Length)];
                string randomLastName = lastNames[rand.Next(lastNames.Length)];

                // Если пол женский и фамилия не оканчивается на "а", добавляем "а"
                if (randomGender == "Женщина" && !randomLastName.EndsWith("а"))
                {
                    randomLastName += "а";
                }

                Employee employee = new Employee
                {
                    Id = i,
                    FullName = $"{randomName} {randomLastName}",
                    Gender = randomGender,
                    Position = randomPosition,
                    Age = rand.Next(20, 60)
                };

                employees.Add(employee);
            }
  
            return employees;
        }

        public List<Vacation> GenerateRandomVacations(List<Employee> employees)
        {
            List<Vacation> vacations = new List<Vacation>();
            Random rand = new Random();

            // Создайте список всех доступных дней в году для отпусков
            List<DateTime> availableVacationDays = Enumerable.Range(1, 365)
                .Select(day => new DateTime(DateTime.Now.Year, 1, 1).AddDays(day - 1))
                .ToList();

            // Перемешайте список для случайного выбора дней
            availableVacationDays = availableVacationDays.OrderBy(d => rand.Next()).ToList();

            foreach (var employee in employees)
            {
                // Выберите дни для отпуска и удаляйте их из доступных
                DateTime startDate1 = availableVacationDays.First();
                availableVacationDays.Remove(startDate1);

                DateTime startDate2 = availableVacationDays.First();
                availableVacationDays.Remove(startDate2);

                DateTime startDate3 = availableVacationDays.First();
                availableVacationDays.Remove(startDate3);

                Vacation vacation1 = new Vacation
                {
                    Id = vacations.Count + 1,
                    StartDate = startDate1,
                    EndDate = startDate1.AddDays(13),
                    Employee = employee
                };
                vacations.Add(vacation1);

                Vacation vacation2 = new Vacation
                {
                    Id = vacations.Count + 1,
                    StartDate = startDate2,
                    EndDate = startDate2.AddDays(6),
                    Employee = employee
                };
                vacations.Add(vacation2);

                Vacation vacation3 = new Vacation
                {
                    Id = vacations.Count + 1,
                    StartDate = startDate3,
                    EndDate = startDate3.AddDays(6),
                    Employee = employee
                };
                vacations.Add(vacation3);
            }

            return vacations;
        }

    }
}