// ============================================================================
// 레슨 03. 조건문, 반복문, 패턴 매칭
// ============================================================================
// Python은 들여쓰기로 블록을 나누지만 C#은 중괄호 { } 로 나눈다. 조건에는
// 반드시 bool이 와야 한다. Python처럼 빈 문자열이나 0을 거짓으로 취급하지 않는다.
//
// 이 레슨에서 익힐 것
//   - if / else if / else, 삼항 연산자 ?:
//   - for, while, foreach
//   - switch 식(expression): C# 8부터 들어온 문법으로, 값을 돌려주는 switch다
//   - 패턴 매칭: is, 관계 패턴(>=), or/and
// ============================================================================

public static class Lesson03
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. if와 삼항 연산자");

        int pressure = 12;

        // TODO: pressure가 10 이상이면 "HIGH", 아니면 "NORMAL" 이 되도록 if/else로 채워라.
        string levelByIf;
        if (pressure < 0)   // <- 이 조건도 고쳐야 한다
        {
            levelByIf = "";
        }
        else
        {
            levelByIf = "";
        }

        // TODO: 같은 판정을 삼항 연산자 한 줄로 써라. 형태는 조건 ? 참일때 : 거짓일때 다.
        string levelByTernary = "";

        Check.Equal("HIGH", levelByIf, "if/else 판정");
        Check.Equal("HIGH", levelByTernary, "삼항 연산자 판정");

        // --------------------------------------------------------------------
        Check.Section("2. for 반복문");
        Check.Note("for (초기화; 조건; 증감) 세 부분으로 이루어진다. Python의 range와 대응한다.");

        // TODO: 1부터 10까지 더한 값을 sum에 넣어라. for문을 사용한다. 정답은 55다.
        int sum = 0;

        // TODO: 1부터 10까지 중 짝수만 더해라. 정답은 30이다. 힌트: i % 2 == 0
        int evenSum = 0;

        Check.Equal(55, sum, "1부터 10까지의 합");
        Check.Equal(30, evenSum, "1부터 10까지 짝수의 합");

        // --------------------------------------------------------------------
        Check.Section("3. foreach");
        Check.Note("Python의 for x in items 와 같다. 인덱스가 필요 없을 때 쓴다.");

        string[] tags = { "P-1101A", "V-2201", "P-1102B", "E-3301" };

        // TODO: tags 중 "P-" 로 시작하는 것의 개수를 세라. 힌트: tag.StartsWith("P-")
        int pumpCount = 0;

        // TODO: 가장 긴 태그 문자열을 찾아라. 힌트: 길이는 tag.Length 다.
        string longest = "";

        Check.Equal(2, pumpCount, "P- 로 시작하는 태그 개수");
        Check.Equal("P-1101A", longest, "가장 긴 태그");

        // --------------------------------------------------------------------
        Check.Section("4. while");

        int value = 1024;

        // TODO: value를 2로 계속 나누어 1이 될 때까지 몇 번 나눠야 하는지 세라.
        //       while문을 쓴다. 1024는 2를 열 번 곱한 값이므로 정답은 10이다.
        int halvings = 0;

        Check.Equal(10, halvings, "1024를 1이 될 때까지 반으로 나눈 횟수");

        // --------------------------------------------------------------------
        Check.Section("5. switch 식");
        Check.Note("switch 식은 값을 돌려준다. => 로 결과를 적고, _ 는 나머지 전부를 뜻한다.");

        // 아래는 완성된 예시다. 형태를 눈으로 익혀라.
        static string KindOfExample(char prefix) => prefix switch
        {
            'P' => "펌프",
            'V' => "밸브",
            _ => "기타",
        };
        Check.Equal("펌프", KindOfExample('P'), "예시 switch 식");

        // TODO: 아래 KindOf를 완성해라.
        //       'P' -> "펌프", 'V' -> "밸브", 'E' -> "열교환기", 'T' -> "탱크",
        //       그 밖의 문자 -> "미분류"
        static string KindOf(char prefix) => prefix switch
        {
            _ => "",
        };

        Check.Equal("펌프", KindOf('P'), "KindOf('P')");
        Check.Equal("밸브", KindOf('V'), "KindOf('V')");
        Check.Equal("열교환기", KindOf('E'), "KindOf('E')");
        Check.Equal("탱크", KindOf('T'), "KindOf('T')");
        Check.Equal("미분류", KindOf('Z'), "KindOf('Z')");

        // --------------------------------------------------------------------
        Check.Section("6. 관계 패턴");
        Check.Note("switch 식 안에서 >= 100 처럼 범위를 직접 적을 수 있다. and, or 도 쓸 수 있다.");

        // TODO: 압력 등급을 돌려주도록 완성해라.
        //       100 이상            -> "위험"
        //       50 이상 100 미만     -> "주의"
        //       0 이상 50 미만       -> "정상"
        //       음수                -> "센서오류"
        // 힌트: >= 50 and < 100 처럼 쓴다. 위에서부터 먼저 맞는 가지가 선택되므로 순서가 중요하다.
        static string Grade(int p) => p switch
        {
            _ => "",
        };

        Check.Equal("위험", Grade(120), "Grade(120)");
        Check.Equal("주의", Grade(70), "Grade(70)");
        Check.Equal("정상", Grade(10), "Grade(10)");
        Check.Equal("센서오류", Grade(-5), "Grade(-5)");

        // --------------------------------------------------------------------
        Check.Section("7. continue와 break");

        int[] readings = { 3, -1, 8, 0, 12, -4, 20 };

        // TODO: readings를 앞에서부터 훑되, 음수는 건너뛰고(continue), 값이 12 이상인
        //       것을 만나면 그 값을 포함해서 더한 뒤 멈춰라(break).
        //       3 + 8 + 0 + 12 = 23 이 정답이다.
        int accumulated = 0;

        Check.Equal(23, accumulated, "continue와 break를 쓴 누적합");
    }
}
