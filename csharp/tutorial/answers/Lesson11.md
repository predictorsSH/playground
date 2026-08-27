# 레슨 11. async와 await - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson11
{
    public static async Task RunAsync()
    {
        Check.Section("1. Task<T> 를 await 하기");

        double pressure = await LoadPressureAsync("P-1101A");

        Check.Close(12.5, pressure, "비동기로 읽어 온 값");

        Check.Section("2. 값을 돌려주지 않는 async 메서드");

        var log = new List<string>();

        await RecordAsync(log, "시작");

        Check.SequenceEqual(new[] { "시작" }, log, "async Task 메서드 호출");

        Check.Section("3. 순차 실행과 동시 실행");

        var sw = System.Diagnostics.Stopwatch.StartNew();
        double a = await LoadSlowAsync("A", 120);
        double b = await LoadSlowAsync("B", 120);
        sw.Stop();
        long sequentialMs = sw.ElapsedMilliseconds;

        Check.Close(2.0, a + b, "순차 실행 결과");
        Check.Equal(true, sequentialMs >= 200, $"순차 실행은 두 작업 시간을 합한 만큼 걸린다 ({sequentialMs}ms)");

        sw.Restart();
        Task<double> t1 = LoadSlowAsync("C", 120);
        Task<double> t2 = LoadSlowAsync("D", 120);
        double[] both = await Task.WhenAll(t1, t2);
        double c = both[0];
        double d = both[1];
        sw.Stop();
        long parallelMs = sw.ElapsedMilliseconds;

        Check.Close(2.0, c + d, "동시 실행 결과");
        Check.Equal(true, parallelMs < 200, $"동시에 진행하면 한 작업 시간 정도로 끝난다 ({parallelMs}ms)");

        Check.Section("4. Task.WhenAll 로 여러 개 모으기");

        string[] tags = { "P-1101A", "V-2201", "E-3301" };

        double[] all = await Task.WhenAll(tags.Select(t => LoadPressureAsync(t)));

        Check.Equal(3, all.Length, "결과 개수");
        Check.Close(24.0, all.Sum(), "세 값의 합");

        Check.Section("5. 비동기 메서드에서 나는 예외");

        string caught = "";
        try
        {
            await FailAsync();
        }
        catch (InvalidOperationException ex)
        {
            caught = ex.Message;
        }

        Check.Equal("비동기 작업이 실패했다", caught, "비동기 예외를 잡았다");

        Check.Section("6. async 메서드 직접 만들기");

        int sum = await SumWithDelayAsync(new[] { 1, 2, 3, 4 });
        Check.Equal(10, sum, "직접 만든 async 메서드");
    }

    private static async Task<double> LoadPressureAsync(string tag)
    {
        await Task.Delay(20);
        return tag switch
        {
            "P-1101A" => 12.5,
            "V-2201" => 4.0,
            "E-3301" => 7.5,
            _ => 0.0,
        };
    }

    private static async Task<double> LoadSlowAsync(string name, int ms)
    {
        await Task.Delay(ms);
        return 1.0;
    }

    private static async Task RecordAsync(List<string> log, string text)
    {
        await Task.Delay(20);
        log.Add(text);
    }

    private static async Task FailAsync()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("비동기 작업이 실패했다");
    }

    private static async Task<int> SumWithDelayAsync(int[] values)
    {
        int sum = 0;
        foreach (var v in values)
        {
            await Task.Delay(5);
            sum += v;
        }
        return sum;
    }
}
```
