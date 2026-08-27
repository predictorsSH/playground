# 레슨 03. 조건문, 반복문, 패턴 매칭 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson03
{
    public static void Run()
    {
        Check.Section("1. if와 삼항 연산자");

        int pressure = 12;

        string levelByIf;
        if (pressure >= 10)
        {
            levelByIf = "HIGH";
        }
        else
        {
            levelByIf = "NORMAL";
        }

        string levelByTernary = pressure >= 10 ? "HIGH" : "NORMAL";

        Check.Equal("HIGH", levelByIf, "if/else 판정");
        Check.Equal("HIGH", levelByTernary, "삼항 연산자 판정");

        Check.Section("2. for 반복문");

        int sum = 0;
        for (int i = 1; i <= 10; i++)
            sum += i;

        int evenSum = 0;
        for (int i = 1; i <= 10; i++)
            if (i % 2 == 0)
                evenSum += i;

        Check.Equal(55, sum, "1부터 10까지의 합");
        Check.Equal(30, evenSum, "1부터 10까지 짝수의 합");

        Check.Section("3. foreach");

        string[] tags = { "P-1101A", "V-2201", "P-1102B", "E-3301" };

        int pumpCount = 0;
        foreach (var tag in tags)
            if (tag.StartsWith("P-"))
                pumpCount++;

        string longest = "";
        foreach (var tag in tags)
            if (tag.Length > longest.Length)
                longest = tag;

        Check.Equal(2, pumpCount, "P- 로 시작하는 태그 개수");
        Check.Equal("P-1101A", longest, "가장 긴 태그");

        Check.Section("4. while");

        int value = 1024;
        int halvings = 0;
        while (value > 1)
        {
            value /= 2;
            halvings++;
        }

        Check.Equal(10, halvings, "1024를 1이 될 때까지 반으로 나눈 횟수");

        Check.Section("5. switch 식");

        static string KindOfExample(char prefix) => prefix switch
        {
            'P' => "펌프",
            'V' => "밸브",
            _ => "기타",
        };
        Check.Equal("펌프", KindOfExample('P'), "예시 switch 식");

        static string KindOf(char prefix) => prefix switch
        {
            'P' => "펌프",
            'V' => "밸브",
            'E' => "열교환기",
            'T' => "탱크",
            _ => "미분류",
        };

        Check.Equal("펌프", KindOf('P'), "KindOf('P')");
        Check.Equal("밸브", KindOf('V'), "KindOf('V')");
        Check.Equal("열교환기", KindOf('E'), "KindOf('E')");
        Check.Equal("탱크", KindOf('T'), "KindOf('T')");
        Check.Equal("미분류", KindOf('Z'), "KindOf('Z')");

        Check.Section("6. 관계 패턴");

        static string Grade(int p) => p switch
        {
            < 0 => "센서오류",
            >= 100 => "위험",
            >= 50 and < 100 => "주의",
            _ => "정상",
        };

        Check.Equal("위험", Grade(120), "Grade(120)");
        Check.Equal("주의", Grade(70), "Grade(70)");
        Check.Equal("정상", Grade(10), "Grade(10)");
        Check.Equal("센서오류", Grade(-5), "Grade(-5)");

        Check.Section("7. continue와 break");

        int[] readings = { 3, -1, 8, 0, 12, -4, 20 };

        int accumulated = 0;
        foreach (var r in readings)
        {
            if (r < 0) continue;
            accumulated += r;
            if (r >= 12) break;
        }

        Check.Equal(23, accumulated, "continue와 break를 쓴 누적합");
    }
}
```
