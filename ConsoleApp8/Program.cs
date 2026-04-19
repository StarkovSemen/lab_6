using System;

class Program
{
    static void Main()
    {
        // 1. Тестирование конструкторов и ToString()
        Time t1 = new Time();
        Time t2 = new Time(14, 30);
        Time t3 = new Time(23, 59);
        Time t4 = new Time(150);
        Time t5 = new Time(t2); // Тестирование конструктора копирования

        Console.WriteLine("Конструкторы:");
        Console.WriteLine($"t1 (по умолчанию): {t1}");
        Console.WriteLine($"t2 (14,30): {t2}");
        Console.WriteLine($"t3 (23,59): {t3}");
        Console.WriteLine($"t4 (150 минут): {t4}");
        Console.WriteLine($"t5 (копия t2): {t5}");
        Console.WriteLine();

        // 2. Тестирование метода вычитания
        Console.WriteLine("Вычитание");
        Console.WriteLine($"{t3} - {t2} = {t3.Subtract(t2)}");
        Console.WriteLine($"{t1} - {t2} = {t1.Subtract(t2)}");
        Console.WriteLine($"{t4} - {t1} = {t4.Subtract(t1)}");
        Console.WriteLine();

        // 3. Тестирование перегруженных операторов
        Console.WriteLine("Перегруженные операторы");

        // Унарные ++ и --
        Time t6 = new Time(12, 59);
        Console.WriteLine($"t6 = {t6}");
        Console.WriteLine($"++t6 = {++t6}");
        Console.WriteLine($"--t6 = {--t6}");
        Console.WriteLine($"t6++ : {t6++}");
        Console.WriteLine($"t6 после t6++: {t6}");  // Добавлено для наглядности
        Console.WriteLine();

        Time t7 = new Time(0, 0);
        Time t8 = new Time(10, 15);
        Console.WriteLine($"{t7} -> {(bool)t7}, {t8} -> {(bool)t8}");

        // Бинарные < и >
        Console.WriteLine($"{t2} < {t3}: {t2 < t3}");
        Console.WriteLine($"{t3} > {t2}: {t3 > t2}");
        Console.WriteLine($"{t1} > {t7}: {t1 > t7}");

        // 4. Тестирование с вводом пользователя (проверка корректности)
        Console.WriteLine("\nВвод времени пользователем");
        Time userTime = ReadTimeFromConsole("Введите первое время (ЧЧ:ММ): ");
        Time userTime2 = ReadTimeFromConsole("Введите второе время (ЧЧ:ММ): ");
        Console.WriteLine($"Разность: {userTime} - {userTime2} = {userTime.Subtract(userTime2)}");
        Console.WriteLine($"Сравнение: {userTime} < {userTime2} ? {userTime < userTime2}");
        Console.WriteLine($"Сравнение: {userTime} > {userTime2} ? {userTime > userTime2}");

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static Time ReadTimeFromConsole(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Проверка на пустой ввод
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Ввод не может быть пустым!");
                continue;
            }

            string[] parts = input.Split(':');

            // Проверка формата
            if (parts.Length != 2)
            {
                Console.WriteLine("Ошибка: Неверный формат! Используйте ЧЧ:ММ (например, 14:30)");
                continue;
            }

            // Проверка, что часы и минуты - числа
            if (!byte.TryParse(parts[0], out byte hours))
            {
                Console.WriteLine("Ошибка: Часы должны быть целым числом от 0 до 23!");
                continue;
            }

            if (!byte.TryParse(parts[1], out byte minutes))
            {
                Console.WriteLine("Ошибка: Минуты должны быть целым числом от 0 до 59!");
                continue;
            }

            // Проверка диапазонов ДО вызова конструктора
            if (hours > 23)
            {
                Console.WriteLine("Ошибка: Часы должны быть в диапазоне 0-23!");
                continue;
            }

            if (minutes > 59)
            {
                Console.WriteLine("Ошибка: Минуты должны быть в диапазоне 0-59!");
                continue;
            }

            try
            {
                return new Time(hours, minutes);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Ошибка:");
            }
        }
    }
}