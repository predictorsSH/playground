// C# 튜토리얼 러너.
//   dotnet run          -> 레슨 목록
//   dotnet run 03       -> 3번 레슨 채점
//   dotnet run all      -> 전체 채점

var lessons = new (string Id, string Title, Func<Task> Run)[]
{
    ("01", "타입과 변수", Sync(Lesson01.Run)),
    ("02", "형변환과 파싱", Sync(Lesson02.Run)),
    ("03", "조건문, 반복문, 패턴 매칭", Sync(Lesson03.Run)),
    ("04", "메서드", Sync(Lesson04.Run)),
    ("05", "배열과 컬렉션", Sync(Lesson05.Run)),
    ("06", "클래스, 레코드, 값 타입과 참조 타입", Sync(Lesson06.Run)),
    ("07", "인터페이스와 상속", Sync(Lesson07.Run)),
    ("08", "null 다루기", Sync(Lesson08.Run)),
    ("09", "LINQ", Sync(Lesson09.Run)),
    ("10", "예외 처리와 파일 입출력", Sync(Lesson10.Run)),
    ("11", "async와 await", Lesson11.RunAsync),
    ("12", "제네릭, 델리게이트, 확장 메서드", Sync(Lesson12.Run)),
};

string arg = args.Length > 0 ? args[0].Trim() : "";

if (arg is "" or "list" or "--help" or "-h")
{
    Console.WriteLine("C# 튜토리얼 - 빈칸을 채우면서 배우는 방식이다.");
    Console.WriteLine();
    foreach (var (id, title, _) in lessons)
        Console.WriteLine($"  {id}  {title}");
    Console.WriteLine();
    Console.WriteLine("  dotnet run 01     한 레슨만 채점한다");
    Console.WriteLine("  dotnet run all    전체를 채점한다");
    return;
}

if (arg is "all")
{
    var failed = new List<string>();
    foreach (var (id, title, run) in lessons)
    {
        Console.WriteLine();
        Console.WriteLine($"===== 레슨 {id}. {title} =====");
        Check.Reset();
        try { await run(); }
        catch (Exception ex) { Check.Crashed(ex); }
        if (!Check.Report(id)) failed.Add(id);
    }

    Console.WriteLine();
    Console.WriteLine(failed.Count == 0
        ? "전체 레슨을 통과했다."
        : $"아직 남은 레슨: {string.Join(", ", failed)}");
    Environment.ExitCode = failed.Count == 0 ? 0 : 1;
    return;
}

var lesson = lessons.FirstOrDefault(l => l.Id == arg.PadLeft(2, '0'));
if (lesson.Run is null)
{
    Console.WriteLine($"레슨 {arg}은 없다. `dotnet run` 으로 목록을 확인해라.");
    Environment.ExitCode = 1;
    return;
}

Console.WriteLine($"===== 레슨 {lesson.Id}. {lesson.Title} =====");
Check.Reset();
try { await lesson.Run(); }
catch (Exception ex) { Check.Crashed(ex); }
Environment.ExitCode = Check.Report(lesson.Id) ? 0 : 1;

// 동기 메서드를 러너가 요구하는 Func<Task> 모양으로 감싼다.
static Func<Task> Sync(Action action) => () =>
{
    action();
    return Task.CompletedTask;
};
