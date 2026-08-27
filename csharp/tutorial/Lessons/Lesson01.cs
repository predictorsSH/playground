// ============================================================================
// 레슨 01. 타입과 변수
// ============================================================================
// Python에서는 x = 3 이라고 쓰면 이름 x가 정수 객체를 가리킬 뿐이고, 나중에
// x = "hello" 라고 다시 대입해도 문제가 없다. C#에서는 변수를 선언할 때
// 타입이 함께 정해지고, 그 뒤로는 같은 타입의 값만 담을 수 있다. 타입이 맞지
// 않으면 실행 전에 컴파일러가 막는다.
//
// 이 레슨에서 익힐 것
//   - 기본 타입: int, long, double, decimal, bool, char, string
//   - var: 타입을 컴파일러가 추론하지만, 타입이 사라지는 것은 아니다
//   - 정수 나눗셈: 7 / 2 는 3이다. Python의 // 와 같다
//   - 문자열 보간 $"..." 와 축자 문자열 @"..."
//   - const: 값을 바꿀 수 없는 이름
//
// 규칙: 아래에서 `TODO` 가 붙은 줄만 고쳐라. Check 호출은 건드리지 않는다.
//       `dotnet run 01` 로 채점한다.
// ============================================================================

public static class Lesson01
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 기본 타입 선언");
        // Python: tag = "P-1101A"
        // C#:     string tag = "P-1101A";  (타입을 앞에 적는다)

        // TODO: 장비 태그를 담는 string 변수를 선언하고 "P-1101A"를 넣어라.
        string equipmentTag = "";

        // TODO: 시퀀스 번호를 담는 int 변수를 선언하고 3을 넣어라.
        int sequence = 0;

        // TODO: 운전 여부를 담는 bool 변수를 선언하고 true를 넣어라.
        bool isRunning = false;

        Check.Equal("P-1101A", equipmentTag, "string 변수 equipmentTag");
        Check.Equal(3, sequence, "int 변수 sequence");
        Check.Equal(true, isRunning, "bool 변수 isRunning");

        // --------------------------------------------------------------------
        Check.Section("2. 정수 나눗셈과 실수 나눗셈");
        Check.Note("int / int 는 소수점을 버린다. Python의 / 가 아니라 // 처럼 동작한다.");

        int total = 7;
        int parts = 2;

        // TODO: total / parts 를 그대로 계산해서 넣어라. 결과가 3.5가 아니라는 점을 확인해라.
        int intDivision = -1;

        // TODO: 3.5가 나오도록 고쳐라. 힌트: 한쪽을 double로 캐스팅한다. (double)total / parts
        double realDivision = 0.0;

        Check.Equal(3, intDivision, "정수 나눗셈 7 / 2");
        Check.Close(3.5, realDivision, "실수 나눗셈 7 / 2");

        // --------------------------------------------------------------------
        Check.Section("3. double과 decimal");
        Check.Note("double은 2진 부동소수라 0.1 + 0.2 != 0.3 이다. 돈이나 계량값은 decimal을 쓴다.");

        double a = 0.1 + 0.2;
        Check.True(a != 0.3, "double 0.1 + 0.2 는 0.3과 다르다");

        // TODO: decimal 리터럴에는 접미사 m을 붙인다. 0.1m + 0.2m 을 계산해 넣어라.
        decimal b = 0m;

        Check.Equal(0.3m, b, "decimal 0.1m + 0.2m 은 정확히 0.3m 이다");

        // --------------------------------------------------------------------
        Check.Section("4. var - 타입 추론");
        Check.Note("var는 '아무 타입'이 아니라 '컴파일러가 알아낸 그 타입'이다. 한 번 정해지면 바뀌지 않는다.");

        var inferred = 12.5;              // 이 변수의 타입은 double이다
        Check.Equal(typeof(double), inferred.GetType(), "var inferred 의 실제 타입");

        // TODO: var로 선언했을 때 타입이 int가 되도록 값을 넣어라. 값은 42로 한다.
        var counted = 42.0;

        Check.Equal(typeof(int), counted.GetType(), "var counted 의 실제 타입은 int여야 한다");
        Check.Equal(42, Convert.ToInt32(counted), "counted 의 값");

        // --------------------------------------------------------------------
        Check.Section("5. 문자열 보간과 축자 문자열");
        Check.Note("$\"...\" 는 Python의 f-string과 같다. @\"...\" 는 백슬래시를 escape로 보지 않는다.");

        // TODO: 문자열 보간으로 "P-1101A #3" 을 만들어라. equipmentTag와 sequence를 사용한다.
        string label = "";

        // TODO: 축자 문자열(@)로 윈도우 경로 C:\plant\data.db3 를 만들어라.
        //       @를 쓰지 않으면 \p, \d 가 escape 문자로 해석되어 컴파일 오류가 난다.
        string path = "";

        Check.Equal("P-1101A #3", label, "문자열 보간 결과");
        Check.Equal(@"C:\plant\data.db3", path, "축자 문자열 경로");

        // --------------------------------------------------------------------
        Check.Section("6. const");
        Check.Note("const는 컴파일 시점에 값이 고정된다. 재대입하면 컴파일 오류가 난다.");

        // TODO: 원주율 근사값을 담는 const double PiApprox 를 3.14159 로 선언해라.
        //       선언 위치는 이 주석 바로 아래다.
        const double PiApprox = 0.0;

        Check.Close(3.14159, PiApprox, "const PiApprox");

        // --------------------------------------------------------------------
        Check.Section("7. 여러 줄 문자열로 쿼리 만들기");
        Check.Note("$@\"...\" 처럼 두 접두사를 함께 쓰면 여러 줄 문자열 안에서 보간도 쓸 수 있다.");

        string viewName = "EquipmentPointView";

        // TODO: 아래 형태가 되도록 $@ 문자열을 만들어라. 줄바꿈과 공백까지 정확히 맞춘다.
        //       SELECT EquipmentTag FROM EquipmentPointView WHERE Seq = 3
        // 힌트: 한 줄짜리로 만들면 된다. 보간 구멍은 두 개다.
        string query = "";

        Check.Equal("SELECT EquipmentTag FROM EquipmentPointView WHERE Seq = 3", query, "보간으로 만든 쿼리");
    }
}
