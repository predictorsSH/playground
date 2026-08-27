# 레슨 05. 배열과 컬렉션 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson05
{
    public static void Run()
    {
        Check.Section("1. 배열");

        string[] tags = { "P-1101A", "V-2201", "E-3301" };
        int[] buffer = new int[5];

        Check.Equal(3, tags.Length, "tags의 길이");
        Check.Equal("V-2201", tags.ElementAtOrDefault(1) ?? "", "인덱스는 0부터 시작한다");
        Check.Equal(5, buffer.Length, "buffer의 길이");
        Check.Equal(0, buffer.ElementAtOrDefault(0), "int 배열의 기본값은 0이다");
        Check.Throws<IndexOutOfRangeException>(() => { _ = tags[99]; },
            "범위를 벗어난 인덱스는 예외를 던진다");

        Check.Section("2. List<T>");

        var readings = new List<int> { 10, 20, 30 };

        readings.Add(40);
        readings.Insert(0, 5);
        readings.Remove(20);

        Check.SequenceEqual(new[] { 5, 10, 30, 40 }, readings, "Add, Insert, Remove 결과");
        Check.Equal(4, readings.Count, "List는 Length가 아니라 Count를 쓴다");
        Check.Equal(true, readings.Contains(30), "Contains");
        Check.Equal(2, readings.IndexOf(30), "IndexOf");

        Check.Section("3. Dictionary<K, V>");

        var pressures = new Dictionary<string, int>
        {
            ["P-1101A"] = 12,
            ["V-2201"] = 4,
        };

        pressures["E-3301"] = 7;
        pressures["P-1101A"] = 15;

        Check.Equal(3, pressures.Count, "항목 개수");
        Check.Equal(15, pressures["P-1101A"], "값 갱신");
        Check.Equal(7, pressures.GetValueOrDefault("E-3301"), "항목 추가");
        Check.Throws<KeyNotFoundException>(() => { _ = pressures["없는태그"]; },
            "없는 키를 읽으면 예외가 난다");

        int found = -1;
        bool hasValve = pressures.TryGetValue("V-2201", out found);

        Check.Equal(true, hasValve, "TryGetValue는 키가 있으면 true");
        Check.Equal(4, found, "TryGetValue가 채워준 값");

        Check.Section("4. Dictionary 순회");

        int totalPressure = 0;
        foreach (var (tag, value) in pressures)
            totalPressure += value;

        Check.Equal(26, totalPressure, "모든 압력의 합");

        Check.Section("5. 개수 세기 - Dictionary 활용");

        string[] samples = { "P-1101A", "V-2201", "P-1102B", "E-3301", "P-1103C", "V-2202" };

        var counts = new Dictionary<char, int>();
        foreach (var s in samples)
        {
            char c = s[0];
            counts.TryGetValue(c, out int n);
            counts[c] = n + 1;
        }

        Check.Equal(3, counts.GetValueOrDefault('P'), "P로 시작하는 개수");
        Check.Equal(2, counts.GetValueOrDefault('V'), "V로 시작하는 개수");
        Check.Equal(1, counts.GetValueOrDefault('E'), "E로 시작하는 개수");

        Check.Section("6. HashSet<T>");

        int[] withDuplicates = { 3, 1, 3, 2, 1, 3 };

        var unique = new HashSet<int>(withDuplicates);

        Check.Equal(3, unique.Count, "중복 제거 후 개수");
        Check.Equal(true, unique.Contains(2), "2가 들어 있다");

        bool addedAgain = unique.Add(3);

        Check.Equal(false, addedAgain, "이미 있는 값을 Add하면 false");
        Check.Equal(3, unique.Count, "개수는 그대로다");
    }
}
```
