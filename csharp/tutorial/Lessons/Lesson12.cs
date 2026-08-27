// ============================================================================
// 레슨 12. 제네릭, 델리게이트, 확장 메서드
// ============================================================================
// Python은 타입을 신경 쓰지 않고 아무 리스트나 함수에 넘길 수 있다. C#에서는
// 그런 유연함을 제네릭으로 얻는다. 타입을 나중에 정하되, 정해진 뒤에는 컴파일러가
// 끝까지 확인해 준다.
//
//   T                        나중에 정해질 타입 자리
//   where T : IComparable<T> T가 갖춰야 할 조건(제약)
//   Func<int, string>        int를 받아 string을 돌려주는 함수. Python의 Callable
//   Action<string>           string을 받고 아무것도 돌려주지 않는 함수
//   Predicate<T>             T를 받아 bool을 돌려주는 함수
//
// 확장 메서드는 남이 만든 타입에 내 메서드를 덧붙인 것처럼 보이게 하는 문법이다.
// LINQ의 Where나 Select도 전부 확장 메서드다.
// ============================================================================

public static class Lesson12
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 제네릭 메서드");

        Check.Equal(9, Bigger(9, 3), "int 두 개 중 큰 값");
        Check.Equal("b", Bigger("a", "b"), "string 두 개 중 큰 값");
        Check.Close(2.5, Bigger(1.5, 2.5), "double 두 개 중 큰 값");

        // --------------------------------------------------------------------
        Check.Section("2. 제네릭 클래스");

        var box = new Box<string>("P-1101A");
        Check.Equal("P-1101A", box.Value, "Box에 담긴 값");
        Check.Equal(true, box.HasValue, "값이 들어 있다");

        var empty = new Box<int>();
        Check.Equal(false, empty.HasValue, "비어 있는 Box");
        Check.Equal(0, empty.Value, "int의 기본값은 0이다");

        // --------------------------------------------------------------------
        Check.Section("3. 제네릭 저장소");

        var repo = new Repository<Reading>();
        repo.Add(new Reading("P-1101A", 12.5));
        repo.Add(new Reading("V-2201", 4.0));

        Check.Equal(2, repo.Count, "저장된 개수");
        Check.Equal(new Reading("V-2201", 4.0), repo.Find(r => r.Tag == "V-2201"), "조건으로 찾기");
        Check.Equal(null, repo.Find(r => r.Tag == "없음"), "없으면 null");

        // --------------------------------------------------------------------
        Check.Section("4. Func와 Action");
        Check.Note("함수를 변수에 담아 넘기는 것이다. Python에서 함수를 인자로 넘기는 것과 같다.");

        // TODO: 정수를 받아 두 배로 만들어 돌려주는 Func<int, int> 를 만들어라.
        //       형태: Func<int, int> 이름 = x => x * 2;
        Func<int, int> twice = x => x;

        Check.Equal(10, twice(5), "twice(5)");

        // TODO: 문자열을 받아 log에 추가하는 Action<string> 을 만들어라.
        var log = new List<string>();
        Action<string> record = _ => { };

        record("첫 줄");
        record("둘째 줄");
        Check.SequenceEqual(new[] { "첫 줄", "둘째 줄" }, log, "Action으로 기록하기");

        // --------------------------------------------------------------------
        Check.Section("5. 함수를 인자로 받기");

        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        // TODO: CountWhere를 완성해라. 정의는 이 파일 아래쪽에 있다.
        Check.Equal(3, CountWhere(numbers, n => n % 2 == 0), "짝수 개수");
        Check.Equal(2, CountWhere(numbers, n => n > 4), "4보다 큰 값의 개수");

        // TODO: ApplyAll을 완성해라.
        Check.SequenceEqual(new[] { 2, 4, 6, 8, 10, 12 }, ApplyAll(numbers, n => n * 2), "모두 두 배로");

        // --------------------------------------------------------------------
        Check.Section("6. 확장 메서드");
        Check.Note("this string tag 처럼 첫 매개변수 앞에 this를 붙이면 확장 메서드가 된다.");

        Check.Equal('P', "P-1101A".TagPrefix(), "string에 덧붙인 TagPrefix");
        Check.Equal(1101, "P-1101A".TagNumber(), "string에 덧붙인 TagNumber");

        // 확장 메서드는 컬렉션에도 붙일 수 있다.
        Check.Close(3.5, new[] { 1, 2, 3, 4, 5, 6 }.MeanOrZero(), "평균");
        Check.Close(0.0, Array.Empty<int>().MeanOrZero(), "빈 컬렉션이면 0");

        // --------------------------------------------------------------------
        Check.Section("7. 제약 조건");
        Check.Note("where T : class 를 붙이면 값 타입은 넘길 수 없다. 컴파일러가 미리 막는다.");

        Check.Equal("P-1101A", FirstOrFallback(new[] { "P-1101A", "V-2201" }, "없음"), "첫 항목");
        Check.Equal("없음", FirstOrFallback(Array.Empty<string>(), "없음"), "비어 있으면 대체값");
    }

    // TODO: 두 값 중 큰 쪽을 돌려줘라.
    //       힌트: a.CompareTo(b) 는 a가 크면 양수, 같으면 0, 작으면 음수를 돌려준다.
    private static T Bigger<T>(T a, T b) where T : IComparable<T>
    {
        return a;
    }

    // TODO: predicate를 만족하는 항목의 개수를 세라. LINQ를 써도 되고 foreach를 써도 된다.
    private static int CountWhere(int[] values, Func<int, bool> predicate)
    {
        return 0;
    }

    // TODO: 각 값에 transform을 적용한 결과 배열을 돌려줘라.
    private static int[] ApplyAll(int[] values, Func<int, int> transform)
    {
        return Array.Empty<int>();
    }

    // TODO: items가 비어 있지 않으면 첫 항목을, 비어 있으면 fallback을 돌려줘라.
    private static T FirstOrFallback<T>(T[] items, T fallback) where T : class
    {
        return fallback;
    }
}

// ----------------------------------------------------------------------------
// TODO: Box<T> 를 완성해라.
//   - Value 프로퍼티는 담긴 값을 돌려준다. 비어 있으면 T의 기본값이다
//   - HasValue 는 생성자로 값을 받았으면 true, 기본 생성자로 만들었으면 false다
public class Box<T>
{
    public T Value { get; }
    public bool HasValue { get; }

    public Box()
    {
        // default(T) 는 T가 int면 0, 참조 타입이면 null이다.
        Value = default!;
        HasValue = true;   // <- 고쳐야 한다
    }

    public Box(T value)
    {
        Value = value;
        HasValue = false;  // <- 고쳐야 한다
    }
}

// TODO: Repository<T> 를 완성해라.
public class Repository<T> where T : class
{
    private readonly List<T> _items = new();

    // TODO: item을 _items에 추가해라.
    public void Add(T item)
    {
    }

    // TODO: 저장된 개수를 돌려줘라.
    public int Count => -1;

    // TODO: predicate를 만족하는 첫 항목을 돌려주고, 없으면 null을 돌려줘라.
    //       힌트: _items.FirstOrDefault(predicate)
    public T? Find(Func<T, bool> predicate)
    {
        return null;
    }
}

// 확장 메서드는 static 클래스 안의 static 메서드여야 한다.
public static class TagExtensions
{
    // TODO: 태그의 첫 글자를 돌려줘라.
    public static char TagPrefix(this string tag) => '?';

    // TODO: 태그에서 숫자 부분만 뽑아 int로 돌려줘라. 레슨 02에서 한 것과 같다.
    public static int TagNumber(this string tag) => 0;

    // TODO: 평균을 돌려주되, 비어 있으면 0을 돌려줘라.
    //       힌트: values.Any() 로 비었는지 확인하고, values.Average() 로 평균을 구한다.
    public static double MeanOrZero(this IEnumerable<int> values) => -1.0;
}
