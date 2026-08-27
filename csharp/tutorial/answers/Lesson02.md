# 레슨 02. 형변환과 파싱 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson02
{
    public static void Run()
    {
        Check.Section("1. 캐스팅");

        int steps = 7;
        double stepsAsDouble = steps;

        double measured = 3.9;
        int truncated = (int)measured;
        int rounded = (int)Math.Round(measured);

        Check.Close(7.0, stepsAsDouble, "int -> double 암시적 변환");
        Check.Equal(3, truncated, "(int)3.9 는 버림이다");
        Check.Equal(4, rounded, "Math.Round(3.9) 후 int 변환");

        Check.Section("2. Parse");

        string raw = "1101";
        int parsed = int.Parse(raw);

        Check.Equal(1101, parsed, "int.Parse(\"1101\")");

        Check.Throws<FormatException>(() =>
        {
            int.Parse("P-1101");
        }, "숫자가 아닌 문자열 Parse는 FormatException을 던진다");

        Check.Section("3. TryParse와 out");

        bool okGood = false;
        int good = -1;
        bool okBad = true;
        int bad = -1;

        okGood = int.TryParse("42", out good);
        okBad = int.TryParse("사십이", out bad);

        Check.Equal(true, okGood, "TryParse(\"42\") 는 true를 돌려준다");
        Check.Equal(42, good, "TryParse가 채워준 값");
        Check.Equal(false, okBad, "TryParse(\"사십이\") 는 false를 돌려준다");
        Check.Equal(0, bad, "실패하면 out 매개변수에는 기본값 0이 들어간다");

        Check.Section("4. 태그 문자열에서 숫자 뽑기");

        string tag = "P-1101A";
        int tagNumber = int.Parse(new string(tag.Where(char.IsDigit).ToArray()));

        Check.Equal(1101, tagNumber, "P-1101A 에서 뽑은 숫자");

        Check.Section("5. 숫자를 문자열로 서식화");

        double pressure = 3.14159;
        string twoDecimals = pressure.ToString("F2");

        int big = 1234567;
        string grouped = big.ToString("N0");

        Check.Equal("3.14", twoDecimals, "F2 서식");
        Check.Equal("1,234,567", grouped, "N0 서식");
    }
}
```
