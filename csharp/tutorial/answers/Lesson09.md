# 레슨 09. LINQ - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson09
{
    private static readonly Reading[] Readings =
    {
        new("P-1101A", 12.5),
        new("V-2201", 4.0),
        new("P-1102B", 30.0),
        new("E-3301", 7.5),
        new("P-1103C", 22.0),
        new("V-2202", 4.0),
    };

    public static void Run()
    {
        Check.Section("1. Where와 Select");

        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        List<int> evens = numbers.Where(n => n % 2 == 0).ToList();
        List<int> squares = numbers.Select(n => n * n).ToList();
        List<int> evenSquares = numbers.Where(n => n % 2 == 0).Select(n => n * n).ToList();

        Check.SequenceEqual(new[] { 2, 4, 6 }, evens, "Where로 짝수 걸러내기");
        Check.SequenceEqual(new[] { 1, 4, 9, 16, 25, 36 }, squares, "Select로 제곱하기");
        Check.SequenceEqual(new[] { 4, 16, 36 }, evenSquares, "Where와 Select 잇기");

        Check.Section("2. 집계");

        double sum = Readings.Sum(r => r.Value);
        double average = Readings.Average(r => r.Value);
        double max = Readings.Max(r => r.Value);
        int highCount = Readings.Count(r => r.Value >= 10);

        Check.Close(80.0, sum, "Value의 합");
        Check.Close(80.0 / 6, average, "Value의 평균");
        Check.Close(30.0, max, "가장 큰 Value");
        Check.Equal(3, highCount, "10 이상인 항목 개수");

        Check.Section("3. 정렬");

        string[] byValueDesc = Readings.OrderByDescending(r => r.Value).Select(r => r.Tag).ToArray();

        Check.SequenceEqual(
            new[] { "P-1102B", "P-1103C", "P-1101A", "E-3301", "V-2201", "V-2202" },
            byValueDesc,
            "Value 내림차순 정렬");

        string[] byValueThenTag = Readings
            .OrderBy(r => r.Value)
            .ThenBy(r => r.Tag)
            .Select(r => r.Tag)
            .ToArray();

        Check.SequenceEqual(
            new[] { "V-2201", "V-2202", "E-3301", "P-1101A", "P-1103C", "P-1102B" },
            byValueThenTag,
            "OrderBy 뒤에 ThenBy");

        Check.Section("4. 첫 항목 찾기");

        string firstHigh = Readings.First(r => r.Value > 20).Tag;
        Reading? none = Readings.FirstOrDefault(r => r.Value > 1000);

        Check.Equal("P-1102B", firstHigh, "20을 넘는 첫 항목");
        Check.Equal(null, none, "조건에 맞는 항목이 없으면 null");
        Check.Throws<InvalidOperationException>(
            () => Readings.First(r => r.Value > 1000),
            "First는 없으면 예외를 던진다");

        Check.Section("5. Any와 All");

        bool anyNegative = Readings.Any(r => r.Value < 0);
        bool allPositive = Readings.All(r => r.Value > 0);

        Check.Equal(false, anyNegative, "음수는 없다");
        Check.Equal(true, allPositive, "모두 양수다");

        Check.Section("6. GroupBy");

        Dictionary<char, int> countByPrefix = Readings
            .GroupBy(r => r.Tag[0])
            .ToDictionary(g => g.Key, g => g.Count());

        Check.Equal(3, countByPrefix.GetValueOrDefault('P'), "P 그룹의 개수");
        Check.Equal(2, countByPrefix.GetValueOrDefault('V'), "V 그룹의 개수");
        Check.Equal(1, countByPrefix.GetValueOrDefault('E'), "E 그룹의 개수");

        Dictionary<char, double> sumByPrefix = Readings
            .GroupBy(r => r.Tag[0])
            .ToDictionary(g => g.Key, g => g.Sum(r => r.Value));

        Check.Close(64.5, sumByPrefix.GetValueOrDefault('P'), "P 그룹의 Value 합");
        Check.Close(8.0, sumByPrefix.GetValueOrDefault('V'), "V 그룹의 Value 합");

        Check.Section("7. Distinct, Take, Skip");

        int[] withDuplicates = { 3, 1, 3, 2, 1, 3, 5 };

        int[] distinctSorted = withDuplicates.Distinct().OrderBy(n => n).ToArray();
        int[] firstTwo = distinctSorted.Take(2).ToArray();
        int[] rest = distinctSorted.Skip(2).ToArray();

        Check.SequenceEqual(new[] { 1, 2, 3, 5 }, distinctSorted, "Distinct 후 정렬");
        Check.SequenceEqual(new[] { 1, 2 }, firstTwo, "Take(2)");
        Check.SequenceEqual(new[] { 3, 5 }, rest, "Skip(2)");

        Check.Section("8. 지연 실행");

        var source = new List<int> { 1, 2, 3 };
        var query = source.Where(x => x > 1);
        var snapshot = source.Where(x => x > 1).ToList();

        source.Add(10);

        int queryCount = query.Count();
        int snapshotCount = snapshot.Count;

        Check.Equal(3, queryCount, "지연 실행되는 query는 나중에 추가된 값도 본다");
        Check.Equal(2, snapshotCount, "ToList로 고정한 snapshot은 그대로다");
    }
}
```
