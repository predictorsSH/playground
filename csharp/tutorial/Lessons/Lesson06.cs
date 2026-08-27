// ============================================================================
// 레슨 06. 클래스, 레코드, 값 타입과 참조 타입
// ============================================================================
// Python의 class와 비슷하지만 두 가지가 크게 다르다.
//   1) 필드에 직접 접근하는 대신 프로퍼티(get/set)를 쓴다
//   2) class 말고 struct 와 record 라는 선택지가 있다
//
// 값 타입(struct, int, double, bool)은 대입할 때 내용이 복사된다.
// 참조 타입(class, string, 배열, List)은 대입할 때 같은 객체를 함께 가리킨다.
// Python은 모든 것이 참조 타입처럼 동작하므로 이 구분이 낯설 수 있다.
// ============================================================================

public static class Lesson06
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 클래스와 프로퍼티");

        var pump = new Equipment("P-1101A", "펌프");

        Check.Equal("P-1101A", pump.Tag, "생성자가 Tag를 채운다");
        Check.Equal("펌프", pump.Kind, "생성자가 Kind를 채운다");
        Check.Equal(0.0, pump.Pressure, "Pressure의 초기값은 0이다");

        pump.Pressure = 12.5;
        Check.Close(12.5, pump.Pressure, "Pressure는 나중에 바꿀 수 있다");

        // --------------------------------------------------------------------
        Check.Section("2. 메서드와 ToString 재정의");
        Check.Note("Python의 __str__ 에 해당하는 것이 ToString이다. override 키워드가 필요하다.");

        Check.Equal("P-1101A(펌프)", pump.ToString(), "ToString 재정의");
        Check.Equal(true, pump.IsHighPressure(10.0), "12.5는 기준 10.0을 넘는다");
        Check.Equal(false, pump.IsHighPressure(20.0), "12.5는 기준 20.0을 넘지 않는다");

        // --------------------------------------------------------------------
        Check.Section("3. 참조 타입의 대입");
        Check.Note("class 객체를 대입하면 같은 객체를 가리키게 된다. 복사본이 생기지 않는다.");

        var same = pump;
        same.Pressure = 99.0;

        Check.Close(99.0, pump.Pressure, "same을 고치면 pump도 바뀐다 - 같은 객체이기 때문이다");
        Check.Equal(true, ReferenceEquals(pump, same), "둘은 같은 객체를 가리킨다");

        // --------------------------------------------------------------------
        Check.Section("4. 값 타입의 대입");

        var p1 = new Point(1.0, 2.0);
        var p2 = p1;                    // struct라서 내용이 복사된다

        Check.Close(1.0, p2.X, "복사된 값");
        Check.Equal(true, p1.Equals(p2), "struct는 내용이 같으면 같다고 본다");

        // TODO: Point의 Distance 메서드를 완성해라. 정의는 이 파일 아래쪽에 있다.
        Check.Close(5.0, new Point(0, 0).Distance(new Point(3, 4)), "원점에서 (3,4)까지의 거리");

        // --------------------------------------------------------------------
        Check.Section("5. record");
        Check.Note("record는 '값처럼 비교되는 클래스'다. 내용이 같으면 == 가 true다.");

        var r1 = new Reading("P-1101A", 12.5);
        var r2 = new Reading("P-1101A", 12.5);
        var c1 = new Equipment("P-1101A", "펌프");
        var c2 = new Equipment("P-1101A", "펌프");

        Check.Equal(true, r1 == r2, "record는 내용이 같으면 == 가 true다");
        Check.Equal(false, c1 == c2, "일반 class는 내용이 같아도 == 가 false다");

        // TODO: with 식으로 r1에서 Value만 20.0으로 바꾼 새 record를 만들어라.
        //       형태: r1 with { Value = 20.0 }
        //       원본 r1은 그대로 남는다.
        Reading r3 = r1;

        Check.Close(20.0, r3.Value, "with 식으로 바꾼 값");
        Check.Equal("P-1101A", r3.Tag, "바꾸지 않은 값은 그대로다");
        Check.Close(12.5, r1.Value, "원본은 변하지 않는다");

        // --------------------------------------------------------------------
        Check.Section("6. 정적 멤버");
        Check.Note("static은 인스턴스가 아니라 클래스 자체에 붙는다. Python의 클래스 변수와 비슷하다.");

        Equipment.ResetCount();
        _ = new Equipment("A-1", "기타");
        _ = new Equipment("A-2", "기타");

        Check.Equal(2, Equipment.CreatedCount, "생성자에서 세는 정적 카운터");
    }
}

// ----------------------------------------------------------------------------
// 클래스 정의
// ----------------------------------------------------------------------------
public class Equipment
{
    // 자동 구현 프로퍼티다. { get; } 만 있으면 생성자 안에서만 값을 넣을 수 있다.
    public string Tag { get; }
    public string Kind { get; }

    // TODO: 아래 프로퍼티는 set은 제대로 저장하는데 get이 늘 0을 돌려주도록 잘못 짜여 있다.
    //       get이 _pressure를 돌려주도록 고쳐라.
    //       참고: 이렇게 직접 쓴 형태 대신 public double Pressure { get; set; } 라고만 적으면
    //             컴파일러가 숨은 저장 공간까지 알아서 만들어 준다. 그쪽이 보통의 작성 방식이다.
    private double _pressure;
    public double Pressure
    {
        get => 0.0;
        set => _pressure = value;
    }

    // TODO: 지금까지 만들어진 개수를 세는 정적 프로퍼티다. 생성자에서 1씩 늘려라.
    public static int CreatedCount { get; private set; }

    public Equipment(string tag, string kind)
    {
        Tag = tag;
        Kind = kind;
        // TODO: 여기에서 CreatedCount 를 1 늘려라.
    }

    public static void ResetCount() => CreatedCount = 0;

    // TODO: "P-1101A(펌프)" 모양의 문자열을 돌려주도록 채워라.
    public override string ToString() => "";

    // TODO: Pressure가 threshold보다 크면 true를 돌려줘라.
    public bool IsHighPressure(double threshold) => false;
}

// struct는 값 타입이다. 작고 불변인 데이터에 어울린다.
public struct Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    // TODO: 두 점 사이의 거리를 돌려줘라.
    //       힌트: Math.Sqrt 와 제곱을 쓴다. dx = other.X - X 로 시작하면 편하다.
    public double Distance(Point other) => 0.0;
}

// record는 이 한 줄로 프로퍼티, 생성자, 동등 비교, ToString이 모두 만들어진다.
public record Reading(string Tag, double Value);
