using System.Collections;
using System.Text;

/// <summary>
/// 레슨 정답을 검증하는 아주 작은 assert 헬퍼. 외부 테스트 패키지 없이 동작한다.
/// </summary>
public static class Check
{
    private const string Green = "\u001b[32m";
    private const string Red = "\u001b[31m";
    private const string Dim = "\u001b[2m";
    private const string Bold = "\u001b[1m";
    private const string Off = "\u001b[0m";

    private static int _pass;
    private static readonly List<string> _fails = new();

    public static void Reset()
    {
        _pass = 0;
        _fails.Clear();
    }

    public static void Section(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"{Bold}-- {title}{Off}");
    }

    public static void Note(string text)
    {
        Console.WriteLine($"{Dim}   {text}{Off}");
    }

    public static void Equal<T>(T expected, T actual, string label)
    {
        if (EqualityComparer<T>.Default.Equals(expected, actual))
            Pass(label);
        else
            Fail(label, $"기대값 {Fmt(expected)} / 실제값 {Fmt(actual)}");
    }

    public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T>? actual, string label)
    {
        var e = expected.ToList();
        var a = actual?.ToList() ?? new List<T>();
        if (e.Count == a.Count && e.Zip(a).All(p => EqualityComparer<T>.Default.Equals(p.First, p.Second)))
            Pass(label);
        else
            Fail(label, $"기대값 {Fmt(e)} / 실제값 {Fmt(a)}");
    }

    public static void True(bool condition, string label)
    {
        if (condition) Pass(label);
        else Fail(label, "조건이 false다");
    }

    public static void Close(double expected, double actual, string label, double tolerance = 1e-9)
    {
        if (Math.Abs(expected - actual) <= tolerance)
            Pass(label);
        else
            Fail(label, $"기대값 {expected} / 실제값 {actual}");
    }

    public static void Throws<TException>(Action action, string label) where TException : Exception
    {
        try
        {
            action();
            Fail(label, $"{typeof(TException).Name} 예외가 발생하지 않았다");
        }
        catch (TException)
        {
            Pass(label);
        }
        catch (Exception ex)
        {
            Fail(label, $"{typeof(TException).Name}을 기대했는데 {ex.GetType().Name}이 발생했다");
        }
    }

    private static void Pass(string label)
    {
        _pass++;
        Console.WriteLine($"   {Green}PASS{Off} {label}");
    }

    private static void Fail(string label, string detail)
    {
        _fails.Add(label);
        Console.WriteLine($"   {Red}FAIL{Off} {label}");
        Console.WriteLine($"        {Dim}{detail}{Off}");
    }

    /// <summary>레슨 코드가 도중에 예외로 멈췄을 때 러너가 부른다.</summary>
    public static void Crashed(Exception ex)
    {
        _fails.Add($"레슨이 도중에 멈췄다: {ex.GetType().Name}");
        Console.WriteLine();
        Console.WriteLine($"   {Red}멈춤{Off} {ex.GetType().Name}: {ex.Message}");
        Console.WriteLine($"        {Dim}{FirstLessonFrame(ex)}{Off}");
    }

    private static string FirstLessonFrame(Exception ex)
    {
        foreach (var line in (ex.StackTrace ?? "").Split('\n'))
            if (line.Contains("Lesson"))
                return line.Trim();
        return "";
    }

    /// <summary>전부 통과했으면 true를 돌려준다.</summary>
    public static bool Report(string lessonId)
    {
        Console.WriteLine();
        if (_fails.Count == 0)
        {
            Console.WriteLine($"{Green}{Bold}레슨 {lessonId} 통과. {_pass}개 항목을 모두 맞췄다.{Off}");
            return true;
        }

        Console.WriteLine($"{Red}{Bold}레슨 {lessonId}: {_pass}개 통과, {_fails.Count}개 실패{Off}");
        foreach (var f in _fails)
            Console.WriteLine($"{Red}  - {f}{Off}");
        Console.WriteLine($"{Dim}  Lessons/Lesson{lessonId}.cs 의 TODO를 고친 뒤 다시 실행해라. 정답은 answers/Lesson{lessonId}.md 에 있다.{Off}");
        return false;
    }

    private static string Fmt(object? value)
    {
        switch (value)
        {
            case null:
                return "null";
            case string s:
                return $"\"{s}\"";
            case bool b:
                return b ? "true" : "false";
            case IEnumerable seq:
                var sb = new StringBuilder("[");
                bool first = true;
                foreach (var item in seq)
                {
                    if (!first) sb.Append(", ");
                    sb.Append(Fmt(item));
                    first = false;
                }
                return sb.Append(']').ToString();
            default:
                return value.ToString() ?? "null";
        }
    }
}
