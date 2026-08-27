// ============================================================================
// 레슨 11. async와 await
// ============================================================================
// Python의 async def / await 와 개념이 거의 같다. 다른 점은 이름이다.
//   Python: async def f() -> ...        C#: async Task<int> F()
//   Python: await asyncio.sleep(1)      C#: await Task.Delay(1000)
//   Python: asyncio.gather(a, b)        C#: await Task.WhenAll(a, b)
//
// 규칙 몇 가지
//   - 값을 돌려주면 Task<T>, 돌려주지 않으면 Task 를 반환 타입으로 쓴다
//   - async 메서드 이름 뒤에 Async를 붙이는 것이 관례다
//   - await 를 만나면 그 자리에서 멈추지 않고 호출한 쪽으로 제어를 돌려준다
//   - .Result 나 .Wait() 로 기다리면 상황에 따라 프로그램이 멈춰 버린다.
//     기다릴 일이 있으면 await 를 쓴다
// ============================================================================

public static class Lesson11
{
    public static async Task RunAsync()
    {
        // --------------------------------------------------------------------
        Check.Section("1. Task<T> 를 await 하기");

        // TODO: LoadPressureAsync("P-1101A") 를 await 해서 결과를 받아라.
        double pressure = 0;

        Check.Close(12.5, pressure, "비동기로 읽어 온 값");

        // --------------------------------------------------------------------
        Check.Section("2. 값을 돌려주지 않는 async 메서드");

        var log = new List<string>();

        // TODO: RecordAsync(log, "시작") 을 await 해라.

        Check.SequenceEqual(new[] { "시작" }, log, "async Task 메서드 호출");

        // --------------------------------------------------------------------
        Check.Section("3. 순차 실행과 동시 실행");
        Check.Note("여러 작업을 각각 await 하면 차례로 기다린다. 먼저 다 띄워 놓고 기다리면 겹쳐서 진행된다.");

        // 순차: 하나씩 기다린다
        var sw = System.Diagnostics.Stopwatch.StartNew();
        double a = await LoadSlowAsync("A", 120);
        double b = await LoadSlowAsync("B", 120);
        sw.Stop();
        long sequentialMs = sw.ElapsedMilliseconds;

        Check.Close(2.0, a + b, "순차 실행 결과");
        Check.Equal(true, sequentialMs >= 200, $"순차 실행은 두 작업 시간을 합한 만큼 걸린다 ({sequentialMs}ms)");

        // TODO: 아래에서 두 작업을 동시에 진행시켜라.
        //       힌트: Task를 먼저 두 개 만들어 두고(아직 await하지 않는다),
        //             Task.WhenAll(t1, t2) 를 await 한 뒤 t1.Result, t2.Result 를 읽는다.
        //             또는 var results = await Task.WhenAll(t1, t2); 로 배열을 받는다.
        sw.Restart();
        double c = 0;
        double d = 0;
        sw.Stop();
        long parallelMs = sw.ElapsedMilliseconds;

        Check.Close(2.0, c + d, "동시 실행 결과");
        Check.Equal(true, parallelMs < 200, $"동시에 진행하면 한 작업 시간 정도로 끝난다 ({parallelMs}ms)");

        // --------------------------------------------------------------------
        Check.Section("4. Task.WhenAll 로 여러 개 모으기");

        string[] tags = { "P-1101A", "V-2201", "E-3301" };

        // TODO: 각 태그에 대해 LoadPressureAsync를 호출하는 Task들을 만들고,
        //       Task.WhenAll 로 한 번에 기다려 결과 배열을 받아라.
        //       힌트: tags.Select(t => LoadPressureAsync(t)) 로 Task들을 만든다.
        double[] all = Array.Empty<double>();

        Check.Equal(3, all.Length, "결과 개수");
        Check.Close(24.0, all.Sum(), "세 값의 합");

        // --------------------------------------------------------------------
        Check.Section("5. 비동기 메서드에서 나는 예외");
        Check.Note("await 하는 순간 예외가 다시 던져진다. try/catch로 잡는 방법은 동기 코드와 같다.");

        string caught = "";
        try
        {
            // TODO: FailAsync() 를 await 해라.
        }
        catch (InvalidOperationException ex)
        {
            caught = ex.Message;
        }

        Check.Equal("비동기 작업이 실패했다", caught, "비동기 예외를 잡았다");

        // --------------------------------------------------------------------
        Check.Section("6. async 메서드 직접 만들기");

        // TODO: 아래 SumWithDelayAsync 를 완성해라. 정의는 이 파일 아래쪽에 있다.
        int sum = await SumWithDelayAsync(new[] { 1, 2, 3, 4 });
        Check.Equal(10, sum, "직접 만든 async 메서드");
    }

    // 데이터베이스나 네트워크에서 읽어 오는 상황을 흉내 낸 것이다.
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

    // TODO: 20밀리초 기다린 뒤 log에 text를 추가해라.
    //       await Task.Delay(20); 을 먼저 쓰고 log.Add(text); 를 이어 쓴다.
    private static async Task RecordAsync(List<string> log, string text)
    {
        await Task.CompletedTask;
    }

    private static async Task FailAsync()
    {
        await Task.Delay(10);
        throw new InvalidOperationException("비동기 작업이 실패했다");
    }

    // TODO: 각 값마다 5밀리초 기다린 뒤 전부 더해서 돌려줘라.
    //       async 메서드 안에서 foreach를 돌면서 await 해도 된다.
    private static async Task<int> SumWithDelayAsync(int[] values)
    {
        await Task.CompletedTask;
        return 0;
    }
}
