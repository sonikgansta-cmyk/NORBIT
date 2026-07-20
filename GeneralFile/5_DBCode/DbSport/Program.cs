using DbSport.Models;
using DbSport.Repositories.Ado;
using DbSport.Repositories.Ef;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DbSport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1 - ADO.NET");
                Console.WriteLine("2 - Entity Framework");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");
                string tech = Console.ReadLine();

                if (tech == "0") break;
                if (tech != "1" && tech != "2")
                {
                    Pause("Неверный выбор.");
                    continue;
                }

                TableMenu(tech);
            }
        }

            static void TableMenu(string tech)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"=== ТАБЛИЦЫ ({(tech == "1" ? "ADO.NET" : "Entity Framework")}) ===");
                    Console.WriteLine("1 - Users");
                    Console.WriteLine("2 - Programs");
                    Console.WriteLine("3 - Exercises");
                    Console.WriteLine("4 - Activities");
                    Console.WriteLine("5 - MuscleGroups");
                    Console.WriteLine("6 - ExerciseMuscleGroups (связь N:N)");
                    Console.WriteLine("0 - Назад");
                    Console.Write("Выбор: ");
                    string table = Console.ReadLine();

                    if (table == "0") return;

                    switch (table)
                    {
                        case "1": OperationMenu(tech, "Users"); break;
                        case "2": OperationMenu(tech, "Programs"); break;
                        case "3": OperationMenu(tech, "Exercises"); break;
                        case "4": OperationMenu(tech, "Activities"); break;
                        case "5": OperationMenu(tech, "MuscleGroups"); break;
                        case "6": OperationMenu(tech, "ExerciseMuscleGroups"); break;
                        default: Pause("Неверный выбор."); break;
                    }
                }
            }

            static void OperationMenu(string tech, string table)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"=== {table} ({(tech == "1" ? "ADO.NET" : "Entity Framework")}) ===");
                    Console.WriteLine("1 - Создать (Create)");
                    Console.WriteLine("2 - Показать все (Read All)");
                    Console.WriteLine("3 - Найти по Id (Read By Id)");
                    Console.WriteLine("4 - Изменить (Update)");
                    Console.WriteLine("5 - Удалить (Delete)");
                    Console.WriteLine("0 - Назад");
                    Console.Write("Выбор: ");
                    string op = Console.ReadLine();

                    if (op == "0") return;
                    if (op != "1" && op != "2" && op != "3" && op != "4" && op != "5")
                    {
                        Pause("Неверный выбор.");
                        continue;
                    }

                    try
                    {
                        Execute(tech, table, op);
                    }
                    catch (Exception ex)
                    {
                        Pause("Ошибка: " + ex.Message);
                        continue;
                    }

                    Pause("Готово.");
                }
            }

            static void Execute(string tech, string table, string op)
            {
                bool isAdo = tech == "1";

                switch (table)
                {
                    case "Users": HandleUsers(isAdo, op); break;
                    case "Programs": HandlePrograms(isAdo, op); break;
                    case "Exercises": HandleExercises(isAdo, op); break;
                    case "Activities": HandleActivities(isAdo, op); break;
                    case "MuscleGroups": HandleMuscleGroups(isAdo, op); break;
                    case "ExerciseMuscleGroups": HandleLinks(isAdo, op); break;
                }
            }

            // ================= USERS =================
            static void HandleUsers(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new UserAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new User
                            {
                                Id = Guid.NewGuid(),
                                Name = ReadString("Имя"),
                                Email = ReadString("Email"),
                                RegisteredAt = DateTime.Now,
                                IsActive = ReadBool("Активен?")
                            });
                            break;
                        case "2":
                            foreach (var u in repo.GetAll())
                                Console.WriteLine($"{u.Id} | {u.Name} | {u.Email} | {u.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name} | {found.Email}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое имя");
                            existing.Email = ReadString("Новый email");
                            existing.IsActive = ReadBool("Активен?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
                else
                {
                    var repo = new UserEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Users
                            {
                                Id = Guid.NewGuid(),
                                Name = ReadString("Имя"),
                                Email = ReadString("Email"),
                                RegisteredAt = DateTime.Now,
                                IsActive = ReadBool("Активен?")
                            });
                            break;
                        case "2":
                            foreach (var u in repo.GetAll())
                                Console.WriteLine($"{u.Id} | {u.Name} | {u.Email} | {u.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name} | {found.Email}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое имя");
                            existing.Email = ReadString("Новый email");
                            existing.IsActive = ReadBool("Активен?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
            }

            // ================= PROGRAMS =================
            static void HandlePrograms(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new TrainingProgramAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new TrainingProgram
                            {
                                Id = Guid.NewGuid(),
                                UserId = ReadGuid("UserId"),
                                Name = ReadString("Название"),
                                Type = ReadString("Тип"),
                                IsActive = ReadBool("Активна?"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var p in repo.GetAll())
                                Console.WriteLine($"{p.Id} | {p.Name} | {p.Type} | {p.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name} | {found.Type}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            existing.Type = ReadString("Новый тип");
                            existing.IsActive = ReadBool("Активна?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
                else
                {
                    var repo = new TrainingProgramEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Programs
                            {
                                Id = Guid.NewGuid(),
                                UserId = ReadGuid("UserId"),
                                Name = ReadString("Название"),
                                Type = ReadString("Тип"),
                                IsActive = ReadBool("Активна?"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var p in repo.GetAll())
                                Console.WriteLine($"{p.Id} | {p.Name} | {p.Type} | {p.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name} | {found.Type}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            existing.Type = ReadString("Новый тип");
                            existing.IsActive = ReadBool("Активна?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
            }

            // ================= EXERCISES =================
            static void HandleExercises(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new ExerciseAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Exercise
                            {
                                Id = Guid.NewGuid(),
                                ProgramId = ReadGuid("ProgramId"),
                                Name = ReadString("Название"),
                                IsActive = ReadBool("Активно?"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var e in repo.GetAll())
                                Console.WriteLine($"{e.Id} | {e.Name} | {e.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            existing.IsActive = ReadBool("Активно?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
                else
                {
                    var repo = new ExerciseEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Exercises
                            {
                                Id = Guid.NewGuid(),
                                ProgramId = ReadGuid("ProgramId"),
                                Name = ReadString("Название"),
                                IsActive = ReadBool("Активно?"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var e in repo.GetAll())
                                Console.WriteLine($"{e.Id} | {e.Name} | {e.IsActive}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            existing.IsActive = ReadBool("Активно?");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
            }

            // ================= ACTIVITIES =================
            static void HandleActivities(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new ActivityAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Activity
                            {
                                Id = Guid.NewGuid(),
                                ExerciseId = ReadGuid("ExerciseId"),
                                ActivityDate = ReadDate("Дата активности (гггг-мм-дд)"),
                                DurationMinutes = ReadInt("Минут"),
                                Notes = ReadString("Заметка"),
                                CaloriesBurned = ReadNullableDecimal("Калории (можно пусто)"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var a in repo.GetAll())
                                Console.WriteLine($"{a.Id} | {a.ActivityDate:d} | {a.DurationMinutes} мин | {a.Notes}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.ActivityDate:d} | {found.DurationMinutes} мин");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.DurationMinutes = ReadInt("Новое кол-во минут");
                            existing.Notes = ReadString("Новая заметка");
                            existing.CaloriesBurned = ReadNullableDecimal("Калории (можно пусто)");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
                else
                {
                    var repo = new ActivityEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new Activities
                            {
                                Id = Guid.NewGuid(),
                                ExerciseId = ReadGuid("ExerciseId"),
                                ActivityDate = ReadDate("Дата активности (гггг-мм-дд)"),
                                DurationMinutes = ReadInt("Минут"),
                                Notes = ReadString("Заметка"),
                                CaloriesBurned = ReadNullableDecimal("Калории (можно пусто)"),
                                CreatedAt = DateTime.Now
                            });
                            break;
                        case "2":
                            foreach (var a in repo.GetAll())
                                Console.WriteLine($"{a.Id} | {a.ActivityDate:d} | {a.DurationMinutes} мин | {a.Notes}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.ActivityDate:d} | {found.DurationMinutes} мин");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.DurationMinutes = ReadInt("Новое кол-во минут");
                            existing.Notes = ReadString("Новая заметка");
                            existing.CaloriesBurned = ReadNullableDecimal("Калории (можно пусто)");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
            }

            // ================= MUSCLE GROUPS =================
            static void HandleMuscleGroups(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new MuscleGroupAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new MuscleGroup { Id = Guid.NewGuid(), Name = ReadString("Название") });
                            break;
                        case "2":
                            foreach (var m in repo.GetAll())
                                Console.WriteLine($"{m.Id} | {m.Name}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
                else
                {
                    var repo = new MuscleGroupEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new MuscleGroups { Id = Guid.NewGuid(), Name = ReadString("Название") });
                            break;
                        case "2":
                            foreach (var m in repo.GetAll())
                                Console.WriteLine($"{m.Id} | {m.Name}");
                            break;
                        case "3":
                            var found = repo.GetById(ReadGuid("Id"));
                            Console.WriteLine(found == null ? "Не найдено." : $"{found.Id} | {found.Name}");
                            break;
                        case "4":
                            var id = ReadGuid("Id для изменения");
                            var existing = repo.GetById(id);
                            if (existing == null) { Console.WriteLine("Не найдено."); return; }
                            existing.Name = ReadString("Новое название");
                            repo.Update(existing);
                            break;
                        case "5":
                            repo.Delete(ReadGuid("Id для удаления"));
                            break;
                    }
                }
            }

            // ================= EXERCISE MUSCLE GROUPS (N:N) =================
            static void HandleLinks(bool isAdo, string op)
            {
                if (isAdo)
                {
                    var repo = new ExerciseMuscleGroupAdoRepository();
                    switch (op)
                    {
                        case "1":
                            repo.Create(new ExerciseMuscleGroup
                            {
                                ExerciseId = ReadGuid("ExerciseId"),
                                MuscleGroupId = ReadGuid("MuscleGroupId")
                            });
                            break;
                        case "2":
                            foreach (var l in repo.GetAll())
                                Console.WriteLine($"ExerciseId: {l.ExerciseId} | MuscleGroupId: {l.MuscleGroupId}");
                            break;
                        case "3":
                            Console.WriteLine("Для этой таблицы нет поиска по одному Id — используйте 'Показать все'.");
                            break;
                        case "4":
                            Console.WriteLine("Обновление не применимо — есть только связь или её отсутствие.");
                            break;
                        case "5":
                            repo.Delete(ReadGuid("ExerciseId"), ReadGuid("MuscleGroupId"));
                            break;
                    }
                }
                else
                {
                    var repo = new ExerciseMuscleGroupEfRepository();
                    switch (op)
                    {
                        case "1":
                            repo.AddLink(ReadGuid("ExerciseId"), ReadGuid("MuscleGroupId"));
                            break;
                        case "2":
                            repo.PrintAllLinks();
                            break;
                        case "3":
                            Console.WriteLine("Для этой таблицы нет поиска по одному Id — используйте 'Показать все'.");
                            break;
                        case "4":
                            Console.WriteLine("Обновление не применимо — есть только связь или её отсутствие.");
                            break;
                        case "5":
                            repo.RemoveLink(ReadGuid("ExerciseId"), ReadGuid("MuscleGroupId"));
                            break;
                    }
                }
            }

            // ================= ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ВВОДА =================
            static string ReadString(string prompt)
            {
                Console.Write(prompt + ": ");
                return Console.ReadLine();
            }

            static bool ReadBool(string prompt)
            {
                Console.Write(prompt + " (y/n): ");
                string input = Console.ReadLine();
                return input != null && input.Trim().ToLower() == "y";
            }

            static Guid ReadGuid(string prompt)
            {
                Console.Write(prompt + ": ");
                return Guid.Parse(Console.ReadLine());
            }

            static int ReadInt(string prompt)
            {
                Console.Write(prompt + ": ");
                return int.Parse(Console.ReadLine());
            }

            static DateTime ReadDate(string prompt)
            {
                Console.Write(prompt + ": ");
                return DateTime.Parse(Console.ReadLine());
            }

            static decimal? ReadNullableDecimal(string prompt)
            {
                Console.Write(prompt + ": ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return null;
                return decimal.Parse(input);
            }

            static void Pause(string message)
            {
                Console.WriteLine(message);
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
    }
}
