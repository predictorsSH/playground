// ============================================================================
// 레슨 02. 형변환과 파싱
// ============================================================================
// Python에서는 int("12") 하나로 끝나고, 실패하면 ValueError가 난다. C#에는
// 세 가지 길이 있다.
//   - 캐스팅 (int)3.9   : 숫자 타입끼리 변환한다. 소수점은 버린다
//   - int.Parse("12")   : 문자열을 숫자로 바꾼다. 실패하면 예외가 난다
//   - int.TryParse(...) : 실패해도 예외 없이 false를 돌려준다. 실무에서 가장 많이 쓴다
//
// 이 레슨에서 익힐 것
//   - 암시적 변환과 명시적 캐스팅
//   - Parse, TryParse, out 매개변수
//   - ToString("F2") 같은 서식 지정
// ============================================================================

public static class Lesson02
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. 캐스팅");
        Check.Note("int -> double 은 손실이 없어서 자동으로 된다. double -> int 는 손실이 있어서 명시해야 한다.");

        int steps = 7;

        // TODO: steps를 double에 그대로 넣어라. 캐스팅 없이 대입만 하면 된다.
        double stepsAsDouble = 0;

        double measured = 3.9;

        // TODO: measured를 int로 캐스팅해라. 반올림이 아니라 버림이라는 점을 확인해라.
        int truncated = 0;

        // TODO: measured를 반올림해서 int로 만들어라. 힌트: Math.Round는 double을 돌려주므로 캐스팅이 필요하다.
        int rounded = 0;

        Check.Close(7.0, stepsAsDouble, "int -> double 암시적 변환");
        Check.Equal(3, truncated, "(int)3.9 는 버림이다");
        Check.Equal(4, rounded, "Math.Round(3.9) 후 int 변환");

        // --------------------------------------------------------------------
        Check.Section("2. Parse");
        Check.Note("Parse는 실패하면 FormatException을 던진다.");

        string raw = "1101";

        // TODO: raw를 int로 파싱해라.
        int parsed = 0;

        Check.Equal(1101, parsed, "int.Parse(\"1101\")");

        // TODO: 아래 람다 안에서 "P-1101" 을 int.Parse로 파싱해라.
        //       Check.Throws가 FormatException이 실제로 나는지 확인한다.
        Check.Throws<FormatException>(() =>
        {
            // 여기에 int.Parse("P-1101"); 형태의 코드를 넣어라.
        }, "숫자가 아닌 문자열 Parse는 FormatException을 던진다");

        // --------------------------------------------------------------------
        Check.Section("3. TryParse와 out");
        Check.Note("out은 '이 매개변수로 값을 되돌려 받는다'는 표시다. 호출할 때도 out을 적는다.");

        bool okGood = false;
        int good = -1;
        bool okBad = true;
        int bad = -1;

        // TODO: okGood = int.TryParse("42", out good); 형태로 호출해라.
        //       (변수를 미리 선언해 두었으므로 out 뒤에 타입을 다시 적지 않는다.
        //        새로 선언하며 받고 싶을 때는 out int good 처럼 쓴다.)

        // TODO: okBad = int.TryParse("사십이", out bad); 형태로 호출해라.
        //       실패해도 예외는 나지 않고, bad에는 0이 들어간다.

        Check.Equal(true, okGood, "TryParse(\"42\") 는 true를 돌려준다");
        Check.Equal(42, good, "TryParse가 채워준 값");
        Check.Equal(false, okBad, "TryParse(\"사십이\") 는 false를 돌려준다");
        Check.Equal(0, bad, "실패하면 out 매개변수에는 기본값 0이 들어간다");

        // --------------------------------------------------------------------
        Check.Section("4. 태그 문자열에서 숫자 뽑기");
        Check.Note("실무에서 자주 하는 작업이다. \"P-1101A\" 에서 1101을 얻는다.");

        string tag = "P-1101A";

        // TODO: tag에서 숫자 부분만 골라 문자열로 만든 뒤 int로 파싱해라.
        //       힌트 1: tag.Where(char.IsDigit) 는 char들의 나열을 돌려준다.
        //       힌트 2: new string(...ToArray()) 로 char 나열을 문자열로 되돌린다.
        //       힌트 3: LINQ는 레슨 09에서 제대로 다룬다. 지금은 힌트대로 써도 좋고,
        //               foreach로 직접 골라 담아도 좋다.
        int tagNumber = 0;

        Check.Equal(1101, tagNumber, "P-1101A 에서 뽑은 숫자");

        // --------------------------------------------------------------------
        Check.Section("5. 숫자를 문자열로 서식화");
        Check.Note("Python의 f\"{x:.2f}\" 에 해당하는 것이 C#의 ToString(\"F2\") 또는 $\"{x:F2}\" 다.");

        double pressure = 3.14159;

        // TODO: 소수점 둘째 자리까지 나오도록 만들어라. 결과는 "3.14" 다.
        string twoDecimals = "";

        int big = 1234567;

        // TODO: 천 단위 구분 기호가 들어가도록 만들어라. 결과는 "1,234,567" 이다.
        //       힌트: 서식 문자열 "N0" 을 쓴다.
        string grouped = "";

        Check.Equal("3.14", twoDecimals, "F2 서식");
        Check.Equal("1,234,567", grouped, "N0 서식");
    }
}
