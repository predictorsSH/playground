// ============================================================================
// 레슨 04. 메서드
// ============================================================================
// Python의 def 에 해당하는 것이 C#의 메서드다. 다른 점은 반환 타입과 매개변수
// 타입을 반드시 적는다는 것, 그리고 이름이 같아도 매개변수 모양이 다르면 여러 개를
// 만들 수 있다는 것이다(오버로딩).
//
// 이 레슨에서 익힐 것
//   - 반환 타입, void
//   - 식 본문 메서드 =>
//   - 오버로딩
//   - 선택적 매개변수와 명명 인자
//   - out 매개변수로 값을 여러 개 돌려주기
//   - params 로 개수가 정해지지 않은 인자 받기
//
// 메서드는 Run() 아래쪽에 클래스 멤버로 정의해 두었다. 그쪽의 TODO를 채워라.
// ============================================================================

public static class Lesson04
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 값을 돌려주는 메서드");

        Check.Equal(25, Square(5), "Square(5)");
        Check.Equal(0, Square(0), "Square(0)");

        // --------------------------------------------------------------------
        Check.Section("2. 식 본문 메서드");
        Check.Note("한 줄짜리 메서드는 { return ...; } 대신 => 로 짧게 쓸 수 있다.");

        Check.Equal("P-1101A", Normalize("  p-1101a  "), "Normalize - 공백 제거 후 대문자로");

        // --------------------------------------------------------------------
        Check.Section("3. 오버로딩");
        Check.Note("이름이 같아도 매개변수의 타입이나 개수가 다르면 별개의 메서드다.");

        Check.Equal(7, Add(3, 4), "Add(int, int)");
        Check.Close(7.5, Add(3.0, 4.5), "Add(double, double)");

        // --------------------------------------------------------------------
        Check.Section("4. 선택적 매개변수와 명명 인자");

        Check.Equal("P-1101", MakeTag(1101), "접두사 기본값 P");
        Check.Equal("V-1101", MakeTag(1101, "V"), "접두사를 직접 넘긴 경우");
        Check.Equal("E-2201", MakeTag(prefix: "E", number: 2201), "명명 인자는 순서를 바꿔도 된다");

        // --------------------------------------------------------------------
        Check.Section("5. out 으로 값 두 개 돌려받기");
        Check.Note("Python은 튜플로 여러 값을 돌려주지만, C#에서는 out 매개변수나 튜플을 쓴다.");

        bool ok = TrySplitTag("P-1101A", out string prefix, out int number);
        Check.Equal(true, ok, "TrySplitTag가 성공을 돌려준다");
        Check.Equal("P", prefix, "분리된 접두사");
        Check.Equal(1101, number, "분리된 번호");

        bool bad = TrySplitTag("이상한값", out string prefix2, out int number2);
        Check.Equal(false, bad, "형식이 맞지 않으면 false");
        Check.Equal("", prefix2, "실패 시 접두사는 빈 문자열");
        Check.Equal(0, number2, "실패 시 번호는 0");

        // --------------------------------------------------------------------
        Check.Section("6. 튜플로 값 두 개 돌려주기");
        Check.Note("C#의 튜플은 이름을 붙일 수 있어서 Python 튜플보다 읽기 쉽다.");

        var stats = MinMax(new[] { 5, 2, 9, 4 });
        Check.Equal(2, stats.Min, "MinMax의 Min");
        Check.Equal(9, stats.Max, "MinMax의 Max");

        // --------------------------------------------------------------------
        Check.Section("7. params");
        Check.Note("params를 붙이면 인자를 몇 개든 나열해서 넘길 수 있다. Python의 *args 와 비슷하다.");

        Check.Equal(0, SumAll(), "인자 없이 호출");
        Check.Equal(6, SumAll(1, 2, 3), "세 개를 나열해서 호출");
        Check.Equal(10, SumAll(1, 2, 3, 4), "네 개를 나열해서 호출");
    }

    // ------------------------------------------------------------------------
    // TODO: n의 제곱을 돌려주도록 채워라.
    private static int Square(int n)
    {
        return 0;
    }

    // TODO: 앞뒤 공백을 없애고 대문자로 바꿔서 돌려주도록 채워라.
    //       힌트: text.Trim() 과 .ToUpper() 를 이어 붙인다.
    //       식 본문 형태 그대로 두고 => 오른쪽만 고치면 된다.
    private static string Normalize(string text) => "";

    // TODO: 두 정수를 더해서 돌려줘라.
    private static int Add(int a, int b) => 0;

    // TODO: 두 실수를 더해서 돌려줘라. 위와 이름이 같지만 매개변수 타입이 달라 별개의 메서드다.
    private static double Add(double a, double b) => 0.0;

    // TODO: prefix와 number를 "P-1101" 같은 모양으로 합쳐라.
    //       prefix에는 기본값 "P" 를 주어라. 기본값이 있는 매개변수는 뒤쪽에 와야 한다.
    private static string MakeTag(int number, string prefix = "?")
    {
        return "";
    }

    // TODO: "P-1101A" 처럼 <문자><하이픈><숫자들><선택적 문자> 형태인 태그를 나눠라.
    //       성공하면 prefix에 "P", number에 1101을 넣고 true를 돌려준다.
    //       형태가 맞지 않으면 prefix에 "", number에 0을 넣고 false를 돌려준다.
    //       힌트: tag.Split('-') 로 나눈 뒤, 조각이 2개인지 확인한다.
    //             숫자 부분은 뒤에 문자가 붙어 있을 수 있으므로 레슨 02에서 한 것처럼
    //             숫자만 골라내고 int.TryParse로 확인한다.
    private static bool TrySplitTag(string tag, out string prefix, out int number)
    {
        // out 매개변수는 메서드가 끝나기 전에 반드시 값이 채워져야 한다.
        prefix = "";
        number = 0;
        return false;
    }

    // TODO: 배열에서 최솟값과 최댓값을 찾아 이름 붙은 튜플로 돌려줘라.
    //       반복문으로 직접 구해도 되고, values.Min() 과 values.Max() 를 써도 된다.
    private static (int Min, int Max) MinMax(int[] values)
    {
        return (0, 0);
    }

    // TODO: 넘어온 모든 값을 더해서 돌려줘라. 인자가 없으면 0이다.
    private static int SumAll(params int[] values)
    {
        return -1;
    }
}
