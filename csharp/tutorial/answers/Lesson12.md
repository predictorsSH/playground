# 레슨 12. 제네릭, 델리게이트, 확장 메서드 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson12
{
    public static void Run()
    {
        Check.Section("1. 제네릭 메서드");

        Check.Equal(9, Bigger(9, 3), "int 두 개 중 큰 값");
        Check.Equal("b", Bigger("a", "b"), "string 두 개 중 큰 값");
        Check.Close(2.5, Bigger(1.5, 2.5), "double 두 개 중 큰 값");

        Check.Section("2. 제네릭 클래스");

        var box = new Box<string>("P-1101A");
        Check.Equal("P-1101A", box.Value, "Box에 담긴 값");
        Check.Equal(true, box.HasValue, "값이 들어 있다");

        var empty = new Box<int>();
        Check.Equal(false, empty.HasValue, "비어 있는 Box");
        Check.Equal(0, empty.Value, "int의 기본값은 0이다");

        Check.Section("3. 제네릭 저장소");

        var repo = new Repository<Reading>();
        repo.Add(new Reading("P-1101A", 12.5));
        repo.Add(new Reading("V-2201", 4.0));

        Check.Equal(2, repo.Count, "저장된 개수");
        Check.Equal(new Reading("V-2201", 4.0), repo.Find(r => r.Tag == "V-2201"), "조건으로 찾기");
        Check.Equal(null, repo.Find(r => r.Tag == "없음"), "없으면 null");

        Check.Section("4. Func와 Action");

        Func<int, int> twice = x => x * 2;

        Check.Equal(10, twice(5), "twice(5)");

        var log = new List<string>();
        Action<string> record = s => log.Add(s);

        record("첫 줄");
        record("둘째 줄");
        Check.SequenceEqual(new[] { "첫 줄", "둘째 줄" }, log, "Action으로 기록하기");

        Check.Section("5. 함수를 인자로 받기");

        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        Check.Equal(3, CountWhere(numbers, n => n % 2 == 0), "짝수 개수");
        Check.Equal(2, CountWhere(numbers, n => n > 4), "4보다 큰 값의 개수");

        Check.SequenceEqual(new[] { 2, 4, 6, 8, 10, 12 }, ApplyAll(numbers, n => n * 2), "모두 두 배로");

        Check.Section("6. 확장 메서드");

        Check.Equal('P', "P-1101A".TagPrefix(), "string에 덧붙인 TagPrefix");
        Check.Equal(1101, "P-1101A".TagNumber(), "string에 덧붙인 TagNumber");

        Check.Close(3.5, new[] { 1, 2, 3, 4, 5, 6 }.MeanOrZero(), "평균");
        Check.Close(0.0, Array.Empty<int>().MeanOrZero(), "빈 컬렉션이면 0");

        Check.Section("7. 제약 조건");

        Check.Equal("P-1101A", FirstOrFallback(new[] { "P-1101A", "V-2201" }, "없음"), "첫 항목");
        Check.Equal("없음", FirstOrFallback(Array.Empty<string>(), "없음"), "비어 있으면 대체값");
    }

    private static T Bigger<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) >= 0 ? a : b;
    }

    private static int CountWhere(int[] values, Func<int, bool> predicate)
    {
        return values.Count(predicate);
    }

    private static int[] ApplyAll(int[] values, Func<int, int> transform)
    {
        return values.Select(transform).ToArray();
    }

    private static T FirstOrFallback<T>(T[] items, T fallback) where T : class
    {
        return items.Length > 0 ? items[0] : fallback;
    }
}

public class Box<T>
{
    public T Value { get; }
    public bool HasValue { get; }

    public Box()
    {
        Value = default!;
        HasValue = false;
    }

    public Box(T value)
    {
        Value = value;
        HasValue = true;
    }
}

public class Repository<T> where T : class
{
    private readonly List<T> _items = new();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public int Count => _items.Count;

    public T? Find(Func<T, bool> predicate)
    {
        return _items.FirstOrDefault(predicate);
    }
}

public static class TagExtensions
{
    public static char TagPrefix(this string tag) => tag[0];

    public static int TagNumber(this string tag)
        => int.Parse(new string(tag.Where(char.IsDigit).ToArray()));

    public static double MeanOrZero(this IEnumerable<int> values)
        => values.Any() ? values.Average() : 0.0;
}
```
