# 레슨 08. null 다루기 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson08
{
    public static void Run()
    {
        Check.Section("1. null 병합 연산자 ??");

        string? maybeName = null;
        string name = maybeName ?? "이름없음";

        string? given = "P-1101A";
        string name2 = given ?? "이름없음";

        Check.Equal("이름없음", name, "null일 때 기본값을 쓴다");
        Check.Equal("P-1101A", name2, "null이 아니면 원래 값을 쓴다");

        Check.Section("2. null 조건부 연산자 ?.");

        string? text = null;
        int? lengthOfNull = text?.Length;

        string? filled = "펌프";
        int? lengthOfFilled = filled?.Length;

        Check.Equal((int?)null, lengthOfNull, "null?.Length 는 null이다");
        Check.Equal((int?)2, lengthOfFilled, "\"펌프\"?.Length 는 2다");

        int safeLength = text?.Length ?? 0;

        Check.Equal(0, safeLength, "?. 와 ?? 를 이어 쓰기");

        Check.Section("3. int? 다루기");

        int? measured = null;

        Check.Equal(false, measured.HasValue, "값이 없다");
        Check.Throws<InvalidOperationException>(() => { _ = measured!.Value; },
            "값이 없는데 Value를 읽으면 예외가 난다");

        int fallback = measured ?? -1;

        Check.Equal(-1, fallback, "값이 없을 때의 대체값");

        measured = 42;

        int actual = measured ?? -1;

        Check.Equal(42, actual, "값이 있을 때는 그 값을 쓴다");

        Check.Section("4. ??= 로 한 번만 채우기");

        string? cached = null;

        cached ??= "계산결과";
        cached ??= "다른값";

        Check.Equal("계산결과", cached, "??= 는 null일 때만 대입한다");

        Check.Section("5. null을 돌려줄 수 있는 메서드");

        Check.Equal("P-1101A", FindTag(new[] { "V-2201", "P-1101A" }, 'P'), "찾은 경우");
        Check.Equal(null, FindTag(new[] { "V-2201" }, 'P'), "못 찾으면 null을 돌려준다");

        Check.Section("6. null 검사와 흐름 분석");

        string? found = FindTag(new[] { "P-1101A" }, 'P');

        string upper = "실패";
        if (found is not null)
        {
            upper = found.ToUpper();
        }
        else
        {
            upper = "";
        }

        Check.Equal("P-1101A", upper, "null 검사 후 안전하게 사용");

        Check.Section("7. 인자 검증");

        Check.Throws<ArgumentNullException>(() => Describe(null!), "null 인자를 막는다");
        Check.Equal("태그: P-1101A", Describe("P-1101A"), "정상 인자");
    }

    private static string? FindTag(string[] tags, char prefix)
    {
        foreach (var tag in tags)
            if (tag.Length > 0 && tag[0] == prefix)
                return tag;
        return null;
    }

    private static string Describe(string tag)
    {
        ArgumentNullException.ThrowIfNull(tag);
        return $"태그: {tag}";
    }
}
```
