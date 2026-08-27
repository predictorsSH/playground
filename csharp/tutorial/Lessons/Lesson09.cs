// ============================================================================
// 레슨 09. LINQ
// ============================================================================
// LINQ는 컬렉션을 다루는 표준 메서드 묶음이다. Python의 리스트 컴프리헨션,
// filter/map, itertools.groupby 가 하던 일을 메서드 체인으로 이어서 쓴다.
//
//   Python                                C#
//   [x*2 for x in xs]                     xs.Select(x => x * 2)
//   [x for x in xs if x > 3]              xs.Where(x => x > 3)
//   sorted(xs, key=f)                     xs.OrderBy(f)
//   sum(xs), max(xs), len(xs)             xs.Sum(), xs.Max(), xs.Count()
//   next((x for x in xs if p(x)), None)   xs.FirstOrDefault(p)
//   any(...), all(...)                    xs.Any(...), xs.All(...)
//
// 중요한 성질이 하나 있다. Where나 Select는 그 자리에서 계산하지 않고, 결과를
// 실제로 훑을 때 계산한다(지연 실행). ToList()나 ToArray()를 부르면 그 시점에
// 계산이 끝나고 결과가 고정된다.
//
// 람다 x => x * 2 는 Python의 lambda x: x * 2 와 같다.
// ============================================================================

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
        // --------------------------------------------------------------------
        Check.Section("1. Where와 Select");

        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        // TODO: 짝수만 골라내라. 결과는 List<int> 로 만든다. 힌트: .Where(...).ToList()
        List<int> evens = new();

        // TODO: 각 값을 제곱해라. 결과는 1, 4, 9, 16, 25, 36 이다.
        List<int> squares = new();

        // TODO: 짝수만 골라 제곱해라. Where와 Select를 이어 붙인다. 결과는 4, 16, 36 이다.
        List<int> evenSquares = new();

        Check.SequenceEqual(new[] { 2, 4, 6 }, evens, "Where로 짝수 걸러내기");
        Check.SequenceEqual(new[] { 1, 4, 9, 16, 25, 36 }, squares, "Select로 제곱하기");
        Check.SequenceEqual(new[] { 4, 16, 36 }, evenSquares, "Where와 Select 잇기");

        // --------------------------------------------------------------------
        Check.Section("2. 집계");

        // TODO: Readings의 Value 합을 구해라. 힌트: Readings.Sum(r => r.Value)
        double sum = 0;

        // TODO: 평균을 구해라.
        double average = 0;

        // TODO: 가장 큰 Value를 구해라.
        double max = 0;

        // TODO: Value가 10 이상인 항목의 개수를 세라.
        int highCount = 0;

        Check.Close(80.0, sum, "Value의 합");
        Check.Close(80.0 / 6, average, "Value의 평균");
        Check.Close(30.0, max, "가장 큰 Value");
        Check.Equal(3, highCount, "10 이상인 항목 개수");

        // --------------------------------------------------------------------
        Check.Section("3. 정렬");

        // TODO: Value가 큰 순서대로 정렬한 뒤 Tag만 뽑아 배열로 만들어라.
        //       힌트: OrderByDescending(...).Select(...).ToArray()
        string[] byValueDesc = Array.Empty<string>();

        Check.SequenceEqual(
            new[] { "P-1102B", "P-1103C", "P-1101A", "E-3301", "V-2201", "V-2202" },
            byValueDesc,
            "Value 내림차순 정렬");

        // TODO: Value 오름차순으로 정렬하되, Value가 같으면 Tag 오름차순으로 정렬해라.
        //       힌트: OrderBy(...).ThenBy(...)
        string[] byValueThenTag = Array.Empty<string>();

        Check.SequenceEqual(
            new[] { "V-2201", "V-2202", "E-3301", "P-1101A", "P-1103C", "P-1102B" },
            byValueThenTag,
            "OrderBy 뒤에 ThenBy");

        // --------------------------------------------------------------------
        Check.Section("4. 첫 항목 찾기");
        Check.Note("First는 없으면 예외를 던지고, FirstOrDefault는 기본값(참조 타입이면 null)을 돌려준다.");

        // TODO: Value가 20을 넘는 첫 항목의 Tag를 구해라.
        string firstHigh = "";

        // TODO: Value가 1000을 넘는 항목을 FirstOrDefault로 찾아라. 없으므로 null이 나온다.
        Reading? none = Readings[0];

        Check.Equal("P-1102B", firstHigh, "20을 넘는 첫 항목");
        Check.Equal(null, none, "조건에 맞는 항목이 없으면 null");
        Check.Throws<InvalidOperationException>(
            () => Readings.First(r => r.Value > 1000),
            "First는 없으면 예외를 던진다");

        // --------------------------------------------------------------------
        Check.Section("5. Any와 All");

        // TODO: Value가 0 미만인 항목이 하나라도 있는지 확인해라.
        bool anyNegative = true;

        // TODO: 모든 Value가 0보다 큰지 확인해라.
        bool allPositive = false;

        Check.Equal(false, anyNegative, "음수는 없다");
        Check.Equal(true, allPositive, "모두 양수다");

        // --------------------------------------------------------------------
        Check.Section("6. GroupBy");
        Check.Note("그룹의 키는 g.Key로, 그룹에 든 항목들은 g 자체를 훑어서 얻는다.");

        // TODO: Tag의 첫 글자로 묶고, 각 그룹의 개수를 Dictionary<char, int> 로 만들어라.
        //       힌트: Readings.GroupBy(r => r.Tag[0]).ToDictionary(g => g.Key, g => g.Count())
        Dictionary<char, int> countByPrefix = new();

        Check.Equal(3, countByPrefix.GetValueOrDefault('P'), "P 그룹의 개수");
        Check.Equal(2, countByPrefix.GetValueOrDefault('V'), "V 그룹의 개수");
        Check.Equal(1, countByPrefix.GetValueOrDefault('E'), "E 그룹의 개수");

        // TODO: 같은 방식으로 묶되, 값은 그룹의 Value 합으로 만들어라.
        Dictionary<char, double> sumByPrefix = new();

        Check.Close(64.5, sumByPrefix.GetValueOrDefault('P'), "P 그룹의 Value 합");
        Check.Close(8.0, sumByPrefix.GetValueOrDefault('V'), "V 그룹의 Value 합");

        // --------------------------------------------------------------------
        Check.Section("7. Distinct, Take, Skip");

        int[] withDuplicates = { 3, 1, 3, 2, 1, 3, 5 };

        // TODO: 중복을 없애고 오름차순으로 정렬해라.
        int[] distinctSorted = Array.Empty<int>();

        // TODO: 정렬 결과에서 앞의 두 개만 가져와라.
        int[] firstTwo = Array.Empty<int>();

        // TODO: 정렬 결과에서 앞의 두 개를 건너뛴 나머지를 가져와라.
        int[] rest = Array.Empty<int>();

        Check.SequenceEqual(new[] { 1, 2, 3, 5 }, distinctSorted, "Distinct 후 정렬");
        Check.SequenceEqual(new[] { 1, 2 }, firstTwo, "Take(2)");
        Check.SequenceEqual(new[] { 3, 5 }, rest, "Skip(2)");

        // --------------------------------------------------------------------
        Check.Section("8. 지연 실행");
        Check.Note("Where는 정의하는 시점이 아니라 훑는 시점에 계산된다.");

        var source = new List<int> { 1, 2, 3 };
        var query = source.Where(x => x > 1);        // 아직 계산하지 않았다
        var snapshot = source.Where(x => x > 1).ToList();   // 여기서 계산이 끝났다

        source.Add(10);

        // TODO: query를 지금 세면 몇 개인가? 나중에 추가한 10도 포함된다.
        int queryCount = -1;

        // TODO: snapshot은 이미 고정되었다. 몇 개인가?
        int snapshotCount = -1;

        Check.Equal(3, queryCount, "지연 실행되는 query는 나중에 추가된 값도 본다");
        Check.Equal(2, snapshotCount, "ToList로 고정한 snapshot은 그대로다");
    }
}
