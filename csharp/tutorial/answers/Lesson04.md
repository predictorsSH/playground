# 레슨 04. 메서드 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson04
{
    public static void Run()
    {
        Check.Section("1. 값을 돌려주는 메서드");

        Check.Equal(25, Square(5), "Square(5)");
        Check.Equal(0, Square(0), "Square(0)");

        Check.Section("2. 식 본문 메서드");

        Check.Equal("P-1101A", Normalize("  p-1101a  "), "Normalize - 공백 제거 후 대문자로");

        Check.Section("3. 오버로딩");

        Check.Equal(7, Add(3, 4), "Add(int, int)");
        Check.Close(7.5, Add(3.0, 4.5), "Add(double, double)");

        Check.Section("4. 선택적 매개변수와 명명 인자");

        Check.Equal("P-1101", MakeTag(1101), "접두사 기본값 P");
        Check.Equal("V-1101", MakeTag(1101, "V"), "접두사를 직접 넘긴 경우");
        Check.Equal("E-2201", MakeTag(prefix: "E", number: 2201), "명명 인자는 순서를 바꿔도 된다");

        Check.Section("5. out 으로 값 두 개 돌려받기");

        bool ok = TrySplitTag("P-1101A", out string prefix, out int number);
        Check.Equal(true, ok, "TrySplitTag가 성공을 돌려준다");
        Check.Equal("P", prefix, "분리된 접두사");
        Check.Equal(1101, number, "분리된 번호");

        bool bad = TrySplitTag("이상한값", out string prefix2, out int number2);
        Check.Equal(false, bad, "형식이 맞지 않으면 false");
        Check.Equal("", prefix2, "실패 시 접두사는 빈 문자열");
        Check.Equal(0, number2, "실패 시 번호는 0");

        Check.Section("6. 튜플로 값 두 개 돌려주기");

        var stats = MinMax(new[] { 5, 2, 9, 4 });
        Check.Equal(2, stats.Min, "MinMax의 Min");
        Check.Equal(9, stats.Max, "MinMax의 Max");

        Check.Section("7. params");

        Check.Equal(0, SumAll(), "인자 없이 호출");
        Check.Equal(6, SumAll(1, 2, 3), "세 개를 나열해서 호출");
        Check.Equal(10, SumAll(1, 2, 3, 4), "네 개를 나열해서 호출");
    }

    private static int Square(int n)
    {
        return n * n;
    }

    private static string Normalize(string text) => text.Trim().ToUpper();

    private static int Add(int a, int b) => a + b;

    private static double Add(double a, double b) => a + b;

    private static string MakeTag(int number, string prefix = "P")
    {
        return $"{prefix}-{number}";
    }

    private static bool TrySplitTag(string tag, out string prefix, out int number)
    {
        prefix = "";
        number = 0;

        var parts = tag.Split('-');
        if (parts.Length != 2) return false;
        if (parts[0].Length == 0) return false;

        string digits = new string(parts[1].Where(char.IsDigit).ToArray());
        if (!int.TryParse(digits, out number))
        {
            number = 0;
            return false;
        }

        prefix = parts[0];
        return true;
    }

    private static (int Min, int Max) MinMax(int[] values)
    {
        return (values.Min(), values.Max());
    }

    private static int SumAll(params int[] values)
    {
        int sum = 0;
        foreach (var v in values) sum += v;
        return sum;
    }
}
```
