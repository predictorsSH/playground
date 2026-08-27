// ============================================================================
// 레슨 10. 예외 처리와 파일 입출력
// ============================================================================
// try/except 가 C#에서는 try/catch 다. finally는 이름이 같다.
// Python의 with open(...) as f: 에 해당하는 것이 using 이다. 블록을 벗어나면
// 파일이 자동으로 닫힌다.
//
// 이 레슨은 임시 폴더에 파일을 만들었다가 지운다. 프로젝트 폴더는 건드리지 않는다.
// ============================================================================

public static class Lesson10
{
    public static void Run()
    {
        // --------------------------------------------------------------------
        Check.Section("1. try / catch");

        // TODO: 아래에서 0으로 나눠서 DivideByZeroException을 잡아라.
        //       catch 블록에서 message에 예외의 Message를 넣어라.
        string message = "";
        try
        {
            int a = 10;
            int b = 1;      // <- 이 값을 고쳐라
            _ = a / b;
        }
        catch (DivideByZeroException ex)
        {
            // TODO: message에 ex.Message를 넣어라.
        }

        Check.Equal(true, message.Length > 0, "예외를 잡아서 메시지를 얻었다");

        // --------------------------------------------------------------------
        Check.Section("2. 예외 종류별로 잡기");
        Check.Note("catch 블록은 위에서부터 확인한다. 더 구체적인 예외를 위에 둔다.");

        Check.Equal("형식오류", Classify("숫자아님"), "FormatException을 잡는다");
        Check.Equal("널", Classify(null), "ArgumentNullException을 잡는다");
        Check.Equal("정상", Classify("42"), "예외가 없으면 정상이다");

        // --------------------------------------------------------------------
        Check.Section("3. finally");
        Check.Note("finally는 예외가 나든 안 나든 반드시 실행된다.");

        var log = new List<string>();

        try
        {
            log.Add("시작");
            throw new InvalidOperationException("일부러 낸 오류");
        }
        catch (InvalidOperationException)
        {
            log.Add("잡음");
        }
        finally
        {
            // TODO: log에 "정리" 를 추가해라.
        }

        Check.SequenceEqual(new[] { "시작", "잡음", "정리" }, log, "finally까지 실행된 순서");

        // --------------------------------------------------------------------
        Check.Section("4. 예외 던지기");

        Check.Throws<ArgumentOutOfRangeException>(
            () => ValidatePressure(-1),
            "음수 압력은 ArgumentOutOfRangeException을 던진다");
        Check.Equal(12.0, ValidatePressure(12.0), "정상 값은 그대로 돌려준다");

        // --------------------------------------------------------------------
        Check.Section("5. 사용자 정의 예외");

        Check.Throws<InvalidTagException>(
            () => ParseTag("이상한값"),
            "형식이 틀리면 InvalidTagException을 던진다");
        Check.Equal(1101, ParseTag("P-1101A"), "정상 태그는 번호를 돌려준다");

        // --------------------------------------------------------------------
        Check.Section("6. 파일 쓰기와 읽기");

        string dir = Path.Combine(Path.GetTempPath(), "csharp-tutorial");
        Directory.CreateDirectory(dir);
        string file = Path.Combine(dir, "readings.csv");

        try
        {
            string[] lines =
            {
                "P-1101A,12.5",
                "V-2201,4.0",
                "E-3301,bad",
                "P-1102B,30.0",
            };

            // TODO: lines를 file에 한 줄씩 써라. 힌트: File.WriteAllLines(file, lines)

            Check.Equal(true, File.Exists(file), "파일이 만들어졌다");

            // TODO: file의 모든 줄을 읽어라. 힌트: File.ReadAllLines(file)
            string[] loaded = Array.Empty<string>();

            Check.Equal(4, loaded.Length, "읽어 들인 줄 수");
            Check.Equal("P-1101A,12.5", loaded.FirstOrDefault() ?? "", "첫 줄");

            // --------------------------------------------------------------------
            Check.Section("7. 깨진 줄을 건너뛰며 파싱하기");

            // TODO: loaded의 각 줄을 쉼표로 나누고, 두 번째 조각을 double로 파싱해서
            //       파싱에 성공한 것만 parsed에 담아라. 세 번째 줄은 "bad" 라서 건너뛴다.
            //       힌트: double.TryParse 를 쓰면 예외 없이 걸러낼 수 있다.
            var parsed = new List<(string Tag, double Value)>();

            Check.Equal(3, parsed.Count, "파싱에 성공한 줄 수");
            Check.Close(46.5, parsed.Sum(p => p.Value), "성공한 값들의 합");

            // --------------------------------------------------------------------
            Check.Section("8. using 과 StreamWriter");
            Check.Note("using 블록을 벗어나면 writer가 자동으로 닫힌다. Python의 with 와 같다.");

            string file2 = Path.Combine(dir, "report.txt");

            // TODO: 아래 using 블록 안에서 writer로 "리포트" 한 줄을 써라.
            //       힌트: writer.WriteLine("리포트")
            using (var writer = new StreamWriter(file2))
            {
            }

            Check.Equal("리포트", File.Exists(file2) ? File.ReadAllText(file2).Trim() : "", "StreamWriter로 쓴 내용");
        }
        finally
        {
            // 시험이 끝났으니 임시 폴더를 지운다.
            if (Directory.Exists(dir)) Directory.Delete(dir, recursive: true);
        }
    }

    // TODO: input을 int로 파싱하되,
    //       null이면 "널", 숫자 형식이 아니면 "형식오류", 성공하면 "정상"을 돌려줘라.
    //       힌트: int.Parse(null) 은 ArgumentNullException 을,
    //             int.Parse("숫자아님") 은 FormatException 을 던진다.
    private static string Classify(string? input)
    {
        return "";
    }

    // TODO: value가 0보다 작으면 ArgumentOutOfRangeException을 던지고,
    //       아니면 value를 그대로 돌려줘라.
    //       형태: throw new ArgumentOutOfRangeException(nameof(value), "압력은 음수일 수 없다");
    private static double ValidatePressure(double value)
    {
        return value;
    }

    // TODO: "P-1101A" 형태에서 1101을 돌려줘라.
    //       형태가 맞지 않으면 InvalidTagException을 던져라.
    //       레슨 04에서 만든 TrySplitTag와 같은 판단을 하면 된다.
    private static int ParseTag(string tag)
    {
        return 0;
    }
}

// Exception을 상속하면 내 예외를 만들 수 있다.
public class InvalidTagException : Exception
{
    public InvalidTagException(string tag)
        : base($"태그 형식이 올바르지 않다: {tag}")
    {
    }
}
