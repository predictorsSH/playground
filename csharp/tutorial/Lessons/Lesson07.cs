// ============================================================================
// 레슨 07. 인터페이스와 상속
// ============================================================================
// Python은 덕 타이핑을 쓴다. Area 메서드만 있으면 무엇이든 넘길 수 있다.
// C#은 "이 타입은 이 약속을 지킨다"고 미리 선언해야 한다. 그 약속이 인터페이스다.
//
//   interface  구현 내용 없이 약속만 적는다. 여러 개를 동시에 구현할 수 있다
//   abstract   일부는 구현하고 일부는 자식에게 맡기는 클래스. 하나만 상속할 수 있다
//   virtual    부모가 기본 구현을 주고, 자식이 원하면 override로 바꾼다
//
// Python의 super().__init__() 에 해당하는 것이 : base(...) 다.
// ============================================================================

public static class Lesson07
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 인터페이스 구현");

        IArea circle = new Circle(2.0);
        IArea rect = new Rectangle(3.0, 4.0);

        Check.Close(Math.PI * 4.0, circle.Area(), "원의 넓이");
        Check.Close(12.0, rect.Area(), "직사각형의 넓이");

        // --------------------------------------------------------------------
        Check.Section("2. 다형성");
        Check.Note("변수의 타입은 IArea지만, 실제로 불리는 것은 각 타입의 구현이다.");

        IArea[] shapes = { new Circle(1.0), new Rectangle(2.0, 5.0), new Circle(3.0) };

        // TODO: shapes의 넓이를 모두 더해라. foreach로 돈다.
        double totalArea = 0.0;

        Check.Close(Math.PI * 1 + 10.0 + Math.PI * 9, totalArea, "모든 도형의 넓이 합", 1e-9);

        // --------------------------------------------------------------------
        Check.Section("3. 추상 클래스와 상속");

        var pump = new Pump("P-1101A", 55.0);
        var valve = new Valve("V-2201", isOpen: true);

        Check.Equal("P-1101A", pump.Tag, "부모 생성자가 Tag를 채운다");
        Check.Equal("펌프", pump.Kind, "자식이 구현한 Kind");
        Check.Equal("밸브", valve.Kind, "자식이 구현한 Kind");

        // Describe는 부모가 구현했고 자식은 손대지 않았다.
        Check.Equal("[펌프] P-1101A", pump.Describe(), "부모의 Describe를 그대로 물려받는다");

        // --------------------------------------------------------------------
        Check.Section("4. virtual과 override");
        Check.Note("Valve는 Describe를 자기 방식으로 다시 구현한다.");

        Check.Equal("[밸브] V-2201 - 열림", valve.Describe(), "Valve가 재정의한 Describe");

        var closed = new Valve("V-2202", isOpen: false);
        Check.Equal("[밸브] V-2202 - 닫힘", closed.Describe(), "닫힌 밸브의 Describe");

        // --------------------------------------------------------------------
        Check.Section("5. 타입 검사와 패턴 매칭");
        Check.Note("is 는 타입을 확인하면서 동시에 그 타입의 변수로 받아 준다.");

        Device[] devices = { pump, valve, new Pump("P-1102B", 30.0), closed };

        // TODO: devices 중 Pump인 것들의 PowerKw 합을 구해라. 정답은 85.0 이다.
        //       힌트: foreach 안에서 if (d is Pump p) { ... p.PowerKw ... }
        double totalPower = 0.0;

        Check.Close(85.0, totalPower, "펌프 출력의 합");

        // TODO: 열려 있는 밸브의 개수를 세라. 정답은 1이다.
        //       힌트: d is Valve { IsOpen: true } 라고 쓰면 타입과 프로퍼티를 한 번에 검사한다.
        int openValves = 0;

        Check.Equal(1, openValves, "열려 있는 밸브 개수");

        // --------------------------------------------------------------------
        Check.Section("6. 인터페이스를 여러 개 구현하기");

        var monitored = new Pump("P-1103C", 10.0);

        Check.Equal(true, monitored is Device, "Pump는 Device를 상속한다");
        Check.Equal(true, monitored is ITaggable, "Pump는 ITaggable도 구현한다");
        Check.Equal("P-1103C", ((ITaggable)monitored).Tag, "인터페이스로 캐스팅해서 접근");
    }
}

// ----------------------------------------------------------------------------
public interface IArea
{
    double Area();
}

public class Circle : IArea
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;

    // TODO: 원의 넓이를 돌려줘라. Math.PI 를 쓴다.
    public double Area() => 0.0;
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

    // TODO: 직사각형의 넓이를 돌려줘라.
    public double Area() => 0.0;
}

// ----------------------------------------------------------------------------
public interface ITaggable
{
    string Tag { get; }
}

// abstract 클래스는 그 자체로는 new 할 수 없다. 상속해서 쓴다.
public abstract class Device : ITaggable
{
    public string Tag { get; }

    protected Device(string tag) => Tag = tag;

    // abstract 멤버는 본문이 없다. 자식이 반드시 구현해야 한다.
    public abstract string Kind { get; }

    // virtual 멤버는 기본 구현이 있고, 자식이 원하면 바꿀 수 있다.
    // TODO: "[펌프] P-1101A" 모양의 문자열을 돌려주도록 채워라.
    public virtual string Describe() => "";
}

public class Pump : Device
{
    public double PowerKw { get; }

    // : base(tag) 가 부모 생성자를 부른다. Python의 super().__init__(tag) 와 같다.
    public Pump(string tag, double powerKw) : base(tag)
    {
        PowerKw = powerKw;
    }

    // TODO: "펌프" 를 돌려주도록 채워라.
    public override string Kind => "";
}

public class Valve : Device
{
    public bool IsOpen { get; }

    public Valve(string tag, bool isOpen) : base(tag)
    {
        IsOpen = isOpen;
    }

    // TODO: "밸브" 를 돌려주도록 채워라.
    public override string Kind => "";

    // TODO: "[밸브] V-2201 - 열림" 모양으로 재정의해라. 닫혀 있으면 "닫힘" 이다.
    //       힌트: base.Describe() 로 부모의 결과를 먼저 얻은 뒤 뒤에 덧붙이면 짧다.
    public override string Describe() => "";
}
