class Time
{
    private byte _hours;
    private byte _minutes;

    public byte Hours
    {
        get => _hours;
        set
        {
            if (value > 23)
                throw new ArgumentOutOfRangeException(nameof(value), "Часы должны быть в диапазоне 0–23.");
            _hours = value;
        }
    }

    public byte Minutes
    {
        get => _minutes;
        set
        {
            if (value > 59)
                throw new ArgumentOutOfRangeException(nameof(value), "Минуты должны быть в диапазоне 0–59.");
            _minutes = value;
        }
    }

    // Конструктор по умолчанию
    public Time()
    {
        Hours = 0;
        Minutes = 0;
    }

    // Конструктор с параметрами
    public Time(byte hours, byte minutes)
    {
        Hours = hours;
        Minutes = minutes;
    }

    // Конструктор из минут
    public Time(int totalMinutes)
    {
        int normalized = totalMinutes % (24 * 60);
        if (normalized < 0)
            normalized += 24 * 60;
        Hours = (byte)(normalized / 60);
        Minutes = (byte)(normalized % 60);
    }

    // КОНСТРУКТОР КОПИРОВАНИЯ
    public Time(Time other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        Hours = other.Hours;
        Minutes = other.Minutes;
    }

    public override string ToString() => $"{Hours:D2}:{Minutes:D2}";

    // Метод вычитания
    public Time Subtract(Time other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        int thisTotal = Hours * 60 + Minutes;
        int otherTotal = other.Hours * 60 + other.Minutes;
        int diff = thisTotal - otherTotal;
        return new Time(diff);
    }

    // Унарный оператор ++
    public static Time operator ++(Time t)
    {
        if (t == null)
            throw new ArgumentNullException(nameof(t));
        int total = t.Hours * 60 + t.Minutes + 1;
        return new Time(total);
    }

    // Унарный оператор --
    public static Time operator --(Time t)
    {
        if (t == null)
            throw new ArgumentNullException(nameof(t));
        int total = t.Hours * 60 + t.Minutes - 1;
        return new Time(total);
    }

    // Неявное приведение к int
    public static implicit operator int(Time t) => t?.Hours * 60 + t.Minutes ?? 0;

    // Явное приведение к bool
    public static explicit operator bool(Time t) => t != null && !(t.Hours == 0 && t.Minutes == 0);

    // Операторы сравнения
    public static bool operator <(Time t1, Time t2) => (int)t1 < (int)t2;
    public static bool operator >(Time t1, Time t2) => (int)t1 > (int)t2;

    // Оператор вычитания
    public static Time operator -(Time t1, Time t2) => t1.Subtract(t2);

    public override bool Equals(object obj)
    {
        if (obj is Time other)
            return this == other;
        return false;
    }

    public override int GetHashCode() => Hours * 60 + Minutes;
}