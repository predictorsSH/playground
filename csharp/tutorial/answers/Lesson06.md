# 레슨 06. 클래스, 레코드, 값 타입과 참조 타입 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson06
{
    public static void Run()
    {
        Check.Section("1. 클래스와 프로퍼티");

        var pump = new Equipment("P-1101A", "펌프");

        Check.Equal("P-1101A", pump.Tag, "생성자가 Tag를 채운다");
        Check.Equal("펌프", pump.Kind, "생성자가 Kind를 채운다");
        Check.Equal(0.0, pump.Pressure, "Pressure의 초기값은 0이다");

        pump.Pressure = 12.5;
        Check.Close(12.5, pump.Pressure, "Pressure는 나중에 바꿀 수 있다");

        Check.Section("2. 메서드와 ToString 재정의");

        Check.Equal("P-1101A(펌프)", pump.ToString(), "ToString 재정의");
        Check.Equal(true, pump.IsHighPressure(10.0), "12.5는 기준 10.0을 넘는다");
        Check.Equal(false, pump.IsHighPressure(20.0), "12.5는 기준 20.0을 넘지 않는다");

        Check.Section("3. 참조 타입의 대입");

        var same = pump;
        same.Pressure = 99.0;

        Check.Close(99.0, pump.Pressure, "same을 고치면 pump도 바뀐다 - 같은 객체이기 때문이다");
        Check.Equal(true, ReferenceEquals(pump, same), "둘은 같은 객체를 가리킨다");

        Check.Section("4. 값 타입의 대입");

        var p1 = new Point(1.0, 2.0);
        var p2 = p1;

        Check.Close(1.0, p2.X, "복사된 값");
        Check.Equal(true, p1.Equals(p2), "struct는 내용이 같으면 같다고 본다");

        Check.Close(5.0, new Point(0, 0).Distance(new Point(3, 4)), "원점에서 (3,4)까지의 거리");

        Check.Section("5. record");

        var r1 = new Reading("P-1101A", 12.5);
        var r2 = new Reading("P-1101A", 12.5);
        var c1 = new Equipment("P-1101A", "펌프");
        var c2 = new Equipment("P-1101A", "펌프");

        Check.Equal(true, r1 == r2, "record는 내용이 같으면 == 가 true다");
        Check.Equal(false, c1 == c2, "일반 class는 내용이 같아도 == 가 false다");

        Reading r3 = r1 with { Value = 20.0 };

        Check.Close(20.0, r3.Value, "with 식으로 바꾼 값");
        Check.Equal("P-1101A", r3.Tag, "바꾸지 않은 값은 그대로다");
        Check.Close(12.5, r1.Value, "원본은 변하지 않는다");

        Check.Section("6. 정적 멤버");

        Equipment.ResetCount();
        _ = new Equipment("A-1", "기타");
        _ = new Equipment("A-2", "기타");

        Check.Equal(2, Equipment.CreatedCount, "생성자에서 세는 정적 카운터");
    }
}

public class Equipment
{
    public string Tag { get; }
    public string Kind { get; }

    private double _pressure;
    public double Pressure
    {
        get => _pressure;
        set => _pressure = value;
    }

    public static int CreatedCount { get; private set; }

    public Equipment(string tag, string kind)
    {
        Tag = tag;
        Kind = kind;
        CreatedCount++;
    }

    public static void ResetCount() => CreatedCount = 0;

    public override string ToString() => $"{Tag}({Kind})";

    public bool IsHighPressure(double threshold) => Pressure > threshold;
}

public struct Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Distance(Point other)
    {
        double dx = other.X - X;
        double dy = other.Y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

public record Reading(string Tag, double Value);
```
