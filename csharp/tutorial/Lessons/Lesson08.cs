// ============================================================================
// 레슨 08. null 다루기
// ============================================================================
// Python의 None에 해당하는 것이 null이다. 다만 이 프로젝트는 csproj에
// <Nullable>enable</Nullable> 이 켜져 있어서, 컴파일러가 null 가능성을 추적한다.
//
//   string  name   null이 들어오면 안 되는 문자열. null을 대입하면 경고가 뜬다
//   string? name   null이 들어올 수 있는 문자열
//   int     n      값 타입이라 애초에 null이 될 수 없다
//   int?    n      null이 될 수 있는 정수 (Nullable<int>)
//
// 자주 쓰는 연산자
//   ?.   앞이 null이면 전체가 null이 된다 (Python의 조건 분기를 짧게 쓴 것)
//   ??   왼쪽이 null이면 오른쪽을 쓴다 (Python의 x if x is not None else y)
//   ??=  왼쪽이 null일 때만 대입한다
//   !    "여기는 null이 아니다"라고 컴파일러에게 단언한다. 틀리면 실행 중에 터진다
// ============================================================================

public static class Lesson08
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. null 병합 연산자 ??");

        string? maybeName = null;

        // TODO: maybeName이 null이면 "이름없음" 이 되도록 ?? 로 채워라.
        string name = "";

        string? given = "P-1101A";

        // TODO: 같은 방식으로 쓰되, 이번에는 왼쪽에 값이 있으므로 그 값이 그대로 남는다.
        string name2 = "";

        Check.Equal("이름없음", name, "null일 때 기본값을 쓴다");
        Check.Equal("P-1101A", name2, "null이 아니면 원래 값을 쓴다");

        // --------------------------------------------------------------------
        Check.Section("2. null 조건부 연산자 ?.");
        Check.Note("text?.Length 는 text가 null이면 계산을 멈추고 null을 돌려준다.");

        string? text = null;

        // TODO: text의 길이를 ?. 로 안전하게 읽어라. 결과 타입은 int? 다.
        int? lengthOfNull = -1;

        string? filled = "펌프";

        // TODO: 같은 방식으로 filled의 길이를 읽어라.
        int? lengthOfFilled = -1;

        Check.Equal((int?)null, lengthOfNull, "null?.Length 는 null이다");
        Check.Equal((int?)2, lengthOfFilled, "\"펌프\"?.Length 는 2다");

        // TODO: ?. 와 ?? 를 함께 써서, null이면 0이 나오도록 만들어라.
        int safeLength = -1;

        Check.Equal(0, safeLength, "?. 와 ?? 를 이어 쓰기");

        // --------------------------------------------------------------------
        Check.Section("3. int? 다루기");
        Check.Note("int? 는 HasValue 로 확인하고 Value 로 꺼낸다. 없는데 Value를 읽으면 예외가 난다.");

        int? measured = null;

        Check.Equal(false, measured.HasValue, "값이 없다");
        Check.Throws<InvalidOperationException>(() => { _ = measured!.Value; },
            "값이 없는데 Value를 읽으면 예외가 난다");

        // TODO: measured에 값이 없으면 -1을 돌려주도록 채워라.
        //       힌트: GetValueOrDefault(-1) 또는 ?? 를 쓴다.
        int fallback = 0;

        Check.Equal(-1, fallback, "값이 없을 때의 대체값");

        measured = 42;

        // TODO: 이번에는 값이 있으므로 42가 나와야 한다. 위와 같은 방식으로 쓴다.
        int actual = 0;

        Check.Equal(42, actual, "값이 있을 때는 그 값을 쓴다");

        // --------------------------------------------------------------------
        Check.Section("4. ??= 로 한 번만 채우기");

        string? cached = null;

        // TODO: cached가 null일 때만 "계산결과" 를 넣도록 ??= 를 써라.

        // TODO: 한 번 더 같은 방식으로 "다른값" 을 넣어 보아라.
        //       이미 값이 있으므로 덮어써지지 않는다.

        Check.Equal("계산결과", cached, "??= 는 null일 때만 대입한다");

        // --------------------------------------------------------------------
        Check.Section("5. null을 돌려줄 수 있는 메서드");

        Check.Equal("P-1101A", FindTag(new[] { "V-2201", "P-1101A" }, 'P'), "찾은 경우");
        Check.Equal(null, FindTag(new[] { "V-2201" }, 'P'), "못 찾으면 null을 돌려준다");

        // --------------------------------------------------------------------
        Check.Section("6. null 검사와 흐름 분석");
        Check.Note("if로 null을 걸러내면 그 아래에서는 컴파일러가 null이 아님을 안다.");

        string? found = FindTag(new[] { "P-1101A" }, 'P');

        // TODO: found가 null이 아니면 대문자로 바꾸고, null이면 "" 이 되도록 채워라.
        //       if (found is not null) { ... } 형태로 쓴다.
        string upper = "실패";

        Check.Equal("P-1101A", upper, "null 검사 후 안전하게 사용");

        // --------------------------------------------------------------------
        Check.Section("7. 인자 검증");
        Check.Note("null이 들어오면 안 되는 자리는 메서드 앞에서 막는 편이 낫다.");

        Check.Throws<ArgumentNullException>(() => Describe(null!), "null 인자를 막는다");
        Check.Equal("태그: P-1101A", Describe("P-1101A"), "정상 인자");
    }

    // TODO: tags 중 prefix로 시작하는 첫 태그를 돌려줘라. 없으면 null을 돌려준다.
    //       반환 타입이 string? 인 것에 주의해라.
    private static string? FindTag(string[] tags, char prefix)
    {
        return "아직 구현하지 않았다";
    }

    // TODO: tag가 null이면 ArgumentNullException을 던지고,
    //       아니면 "태그: P-1101A" 모양의 문자열을 돌려줘라.
    //       힌트: ArgumentNullException.ThrowIfNull(tag); 한 줄이면 검사가 끝난다.
    private static string Describe(string tag)
    {
        return "";
    }
}
