# 레슨 07. 인터페이스와 상속 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson07
{
    public static void Run()
    {
        Check.Section("1. 인터페이스 구현");

        IArea circle = new Circle(2.0);
        IArea rect = new Rectangle(3.0, 4.0);

        Check.Close(Math.PI * 4.0, circle.Area(), "원의 넓이");
        Check.Close(12.0, rect.Area(), "직사각형의 넓이");

        Check.Section("2. 다형성");

        IArea[] shapes = { new Circle(1.0), new Rectangle(2.0, 5.0), new Circle(3.0) };

        double totalArea = 0.0;
        foreach (var s in shapes)
            totalArea += s.Area();

        Check.Close(Math.PI * 1 + 10.0 + Math.PI * 9, totalArea, "모든 도형의 넓이 합", 1e-9);

        Check.Section("3. 추상 클래스와 상속");

        var pump = new Pump("P-1101A", 55.0);
        var valve = new Valve("V-2201", isOpen: true);

        Check.Equal("P-1101A", pump.Tag, "부모 생성자가 Tag를 채운다");
        Check.Equal("펌프", pump.Kind, "자식이 구현한 Kind");
        Check.Equal("밸브", valve.Kind, "자식이 구현한 Kind");

        Check.Equal("[펌프] P-1101A", pump.Describe(), "부모의 Describe를 그대로 물려받는다");

        Check.Section("4. virtual과 override");

        Check.Equal("[밸브] V-2201 - 열림", valve.Describe(), "Valve가 재정의한 Describe");

        var closed = new Valve("V-2202", isOpen: false);
        Check.Equal("[밸브] V-2202 - 닫힘", closed.Describe(), "닫힌 밸브의 Describe");

        Check.Section("5. 타입 검사와 패턴 매칭");

        Device[] devices = { pump, valve, new Pump("P-1102B", 30.0), closed };

        double totalPower = 0.0;
        foreach (var d in devices)
            if (d is Pump p)
                totalPower += p.PowerKw;

        Check.Close(85.0, totalPower, "펌프 출력의 합");

        int openValves = 0;
        foreach (var d in devices)
            if (d is Valve { IsOpen: true })
                openValves++;

        Check.Equal(1, openValves, "열려 있는 밸브 개수");

        Check.Section("6. 인터페이스를 여러 개 구현하기");

        var monitored = new Pump("P-1103C", 10.0);

        Check.Equal(true, monitored is Device, "Pump는 Device를 상속한다");
        Check.Equal(true, monitored is ITaggable, "Pump는 ITaggable도 구현한다");
        Check.Equal("P-1103C", ((ITaggable)monitored).Tag, "인터페이스로 캐스팅해서 접근");
    }
}

public interface IArea
{
    double Area();
}

public class Circle : IArea
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;

    public double Area() => Math.PI * Radius * Radius;
}

public class Rectangle : IArea
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double Area() => Width * Height;
}

public interface ITaggable
{
    string Tag { get; }
}

public abstract class Device : ITaggable
{
    public string Tag { get; }

    protected Device(string tag) => Tag = tag;

    public abstract string Kind { get; }

    public virtual string Describe() => $"[{Kind}] {Tag}";
}

public class Pump : Device
{
    public double PowerKw { get; }

    public Pump(string tag, double powerKw) : base(tag)
    {
        PowerKw = powerKw;
    }

    public override string Kind => "펌프";
}

public class Valve : Device
{
    public bool IsOpen { get; }

    public Valve(string tag, bool isOpen) : base(tag)
    {
        IsOpen = isOpen;
    }

    public override string Kind => "밸브";

    public override string Describe() => $"{base.Describe()} - {(IsOpen ? "열림" : "닫힘")}";
}
```
