// ============================================================================
// 레슨 05. 배열과 컬렉션
// ============================================================================
// Python의 list 하나가 담당하던 역할을 C#에서는 여러 타입이 나눠 맡는다.
//   int[]              고정 길이 배열. 크기를 바꿀 수 없다
//   List<int>          늘었다 줄었다 하는 목록. Python의 list에 가장 가깝다
//   Dictionary<K, V>   Python의 dict
//   HashSet<T>         Python의 set
//
// <int> 처럼 꺾쇠 안에 적는 것이 제네릭 인자다. "정수만 담는 목록"이라는 뜻이고,
// 다른 타입을 넣으면 컴파일러가 막는다. 제네릭은 레슨 12에서 더 다룬다.
// ============================================================================

public static class Lesson05
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 배열");
        Check.Note("배열은 만들 때 길이가 정해지고, 그 뒤로는 바뀌지 않는다.");

        // TODO: 문자열 배열을 만들고 "P-1101A", "V-2201", "E-3301" 을 담아라.
        //       형태: string[] 이름 = { "a", "b" };
        string[] tags = Array.Empty<string>();

        // TODO: 길이가 5인 int 배열을 만들어라. 값은 넣지 않는다.
        //       형태: new int[5]. 각 칸은 자동으로 0으로 채워진다.
        int[] buffer = Array.Empty<int>();

        Check.Equal(3, tags.Length, "tags의 길이");
        Check.Equal("V-2201", tags.ElementAtOrDefault(1) ?? "", "인덱스는 0부터 시작한다");
        Check.Equal(5, buffer.Length, "buffer의 길이");
        Check.Equal(0, buffer.ElementAtOrDefault(0), "int 배열의 기본값은 0이다");
        Check.Throws<IndexOutOfRangeException>(() => { _ = tags[99]; },
            "범위를 벗어난 인덱스는 예외를 던진다");

        // --------------------------------------------------------------------
        Check.Section("2. List<T>");
        Check.Note("Python의 append, insert, remove 에 해당하는 것이 Add, Insert, Remove 다.");

        var readings = new List<int> { 10, 20, 30 };

        // TODO: readings 뒤에 40을 덧붙여라.

        // TODO: readings 맨 앞(인덱스 0)에 5를 끼워 넣어라.

        // TODO: readings에서 값 20을 지워라. 인덱스가 아니라 값으로 지운다.

        Check.SequenceEqual(new[] { 5, 10, 30, 40 }, readings, "Add, Insert, Remove 결과");
        Check.Equal(4, readings.Count, "List는 Length가 아니라 Count를 쓴다");
        Check.Equal(true, readings.Contains(30), "Contains");
        Check.Equal(2, readings.IndexOf(30), "IndexOf");

        // --------------------------------------------------------------------
        Check.Section("3. Dictionary<K, V>");

        var pressures = new Dictionary<string, int>
        {
            ["P-1101A"] = 12,
            ["V-2201"] = 4,
        };

        // TODO: "E-3301" 에 7을 넣어라. 없는 키에 대입하면 새로 추가된다.

        // TODO: "P-1101A" 의 값을 15로 바꿔라.

        Check.Equal(3, pressures.Count, "항목 개수");
        Check.Equal(15, pressures["P-1101A"], "값 갱신");
        Check.Equal(7, pressures.GetValueOrDefault("E-3301"), "항목 추가");
        Check.Throws<KeyNotFoundException>(() => { _ = pressures["없는태그"]; },
            "없는 키를 읽으면 예외가 난다");

        // TODO: TryGetValue로 "V-2201" 의 값을 안전하게 읽어라.
        //       형태: pressures.TryGetValue("V-2201", out found)
        int found = -1;
        bool hasValve = false;

        Check.Equal(true, hasValve, "TryGetValue는 키가 있으면 true");
        Check.Equal(4, found, "TryGetValue가 채워준 값");

        // --------------------------------------------------------------------
        Check.Section("4. Dictionary 순회");
        Check.Note("foreach로 돌면 KeyValuePair가 나온다. var (k, v) 로 풀어 쓸 수 있다.");

        // TODO: pressures의 모든 값을 더해라. 정답은 15 + 4 + 7 = 26 이다.
        //       형태: foreach (var (tag, value) in pressures) { ... }
        int totalPressure = 0;

        Check.Equal(26, totalPressure, "모든 압력의 합");

        // --------------------------------------------------------------------
        Check.Section("5. 개수 세기 - Dictionary 활용");

        string[] samples = { "P-1101A", "V-2201", "P-1102B", "E-3301", "P-1103C", "V-2202" };

        // TODO: 접두사 문자(첫 글자)별로 개수를 세어 counts에 담아라.
        //       결과는 P->3, V->2, E->1 이다.
        //       힌트: 키가 이미 있으면 1 더하고, 없으면 1로 시작한다.
        //             counts.ContainsKey(c) 로 확인하거나
        //             counts.TryGetValue(c, out int n) 를 쓴다.
        var counts = new Dictionary<char, int>();

        Check.Equal(3, counts.GetValueOrDefault('P'), "P로 시작하는 개수");
        Check.Equal(2, counts.GetValueOrDefault('V'), "V로 시작하는 개수");
        Check.Equal(1, counts.GetValueOrDefault('E'), "E로 시작하는 개수");

        // --------------------------------------------------------------------
        Check.Section("6. HashSet<T>");
        Check.Note("중복을 허용하지 않고, 들어 있는지 확인하는 속도가 빠르다.");

        int[] withDuplicates = { 3, 1, 3, 2, 1, 3 };

        // TODO: withDuplicates의 중복을 없앤 집합을 만들어라.
        //       형태: new HashSet<int>(배열)
        var unique = new HashSet<int>();

        Check.Equal(3, unique.Count, "중복 제거 후 개수");
        Check.Equal(true, unique.Contains(2), "2가 들어 있다");

        // TODO: unique에 3을 한 번 더 넣어 보고, Add가 돌려주는 값을 담아라.
        //       이미 있는 값을 넣으면 false가 나온다.
        bool addedAgain = true;

        Check.Equal(false, addedAgain, "이미 있는 값을 Add하면 false");
        Check.Equal(3, unique.Count, "개수는 그대로다");
    }
}
