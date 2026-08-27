# 레슨 10. 예외 처리와 파일 입출력 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson10
{
    public static void Run()
    {
        Check.Section("1. try / catch");

        string message = "";
        try
        {
            int a = 10;
            int b = 0;
            _ = a / b;
        }
        catch (DivideByZeroException ex)
        {
            message = ex.Message;
        }

        Check.Equal(true, message.Length > 0, "예외를 잡아서 메시지를 얻었다");

        Check.Section("2. 예외 종류별로 잡기");

        Check.Equal("형식오류", Classify("숫자아님"), "FormatException을 잡는다");
        Check.Equal("널", Classify(null), "ArgumentNullException을 잡는다");
        Check.Equal("정상", Classify("42"), "예외가 없으면 정상이다");

        Check.Section("3. finally");

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
            log.Add("정리");
        }

        Check.SequenceEqual(new[] { "시작", "잡음", "정리" }, log, "finally까지 실행된 순서");

        Check.Section("4. 예외 던지기");

        Check.Throws<ArgumentOutOfRangeException>(
            () => ValidatePressure(-1),
            "음수 압력은 ArgumentOutOfRangeException을 던진다");
        Check.Equal(12.0, ValidatePressure(12.0), "정상 값은 그대로 돌려준다");

        Check.Section("5. 사용자 정의 예외");

        Check.Throws<InvalidTagException>(
            () => ParseTag("이상한값"),
            "형식이 틀리면 InvalidTagException을 던진다");
        Check.Equal(1101, ParseTag("P-1101A"), "정상 태그는 번호를 돌려준다");

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

            File.WriteAllLines(file, lines);

            Check.Equal(true, File.Exists(file), "파일이 만들어졌다");

            string[] loaded = File.ReadAllLines(file);

            Check.Equal(4, loaded.Length, "읽어 들인 줄 수");
            Check.Equal("P-1101A,12.5", loaded.FirstOrDefault() ?? "", "첫 줄");

            Check.Section("7. 깨진 줄을 건너뛰며 파싱하기");

            var parsed = new List<(string Tag, double Value)>();
            foreach (var line in loaded)
            {
                var parts = line.Split(',');
                if (parts.Length != 2) continue;
                if (!double.TryParse(parts[1], out double v)) continue;
                parsed.Add((parts[0], v));
            }

            Check.Equal(3, parsed.Count, "파싱에 성공한 줄 수");
            Check.Close(46.5, parsed.Sum(p => p.Value), "성공한 값들의 합");

            Check.Section("8. using 과 StreamWriter");

            string file2 = Path.Combine(dir, "report.txt");

            using (var writer = new StreamWriter(file2))
            {
                writer.WriteLine("리포트");
            }

            Check.Equal("리포트", File.Exists(file2) ? File.ReadAllText(file2).Trim() : "", "StreamWriter로 쓴 내용");
        }
        finally
        {
            if (Directory.Exists(dir)) Directory.Delete(dir, recursive: true);
        }
    }

    private static string Classify(string? input)
    {
        try
        {
            _ = int.Parse(input!);
            return "정상";
        }
        catch (ArgumentNullException)
        {
            return "널";
        }
        catch (FormatException)
        {
            return "형식오류";
        }
    }

    private static double ValidatePressure(double value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "압력은 음수일 수 없다");
        return value;
    }

    private static int ParseTag(string tag)
    {
        var parts = tag.Split('-');
        if (parts.Length != 2)
            throw new InvalidTagException(tag);

        string digits = new string(parts[1].Where(char.IsDigit).ToArray());
        if (!int.TryParse(digits, out int number))
            throw new InvalidTagException(tag);

        return number;
    }
}

public class InvalidTagException : Exception
{
    public InvalidTagException(string tag)
        : base($"태그 형식이 올바르지 않다: {tag}")
    {
    }
}
```
