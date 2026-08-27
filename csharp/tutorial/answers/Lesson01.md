# 레슨 01. 타입과 변수 - 정답

먼저 스스로 채워 보고, 막힐 때만 열어라. 여기 적힌 것이 유일한 정답은 아니다.
`Check` 호출이 통과하기만 하면 다른 방식으로 써도 된다.

```csharp
public static class Lesson01
{
    public static void Run()
    {
        Check.Section("1. 기본 타입 선언");

        string equipmentTag = "P-1101A";
        int sequence = 3;
        bool isRunning = true;

        Check.Equal("P-1101A", equipmentTag, "string 변수 equipmentTag");
        Check.Equal(3, sequence, "int 변수 sequence");
        Check.Equal(true, isRunning, "bool 변수 isRunning");

        Check.Section("2. 정수 나눗셈과 실수 나눗셈");

        int total = 7;
        int parts = 2;

        int intDivision = total / parts;
        double realDivision = (double)total / parts;

        Check.Equal(3, intDivision, "정수 나눗셈 7 / 2");
        Check.Close(3.5, realDivision, "실수 나눗셈 7 / 2");

        Check.Section("3. double과 decimal");

        double a = 0.1 + 0.2;
        Check.True(a != 0.3, "double 0.1 + 0.2 는 0.3과 다르다");

        decimal b = 0.1m + 0.2m;
        Check.Equal(0.3m, b, "decimal 0.1m + 0.2m 은 정확히 0.3m 이다");

        Check.Section("4. var - 타입 추론");

        var inferred = 12.5;
        Check.Equal(typeof(double), inferred.GetType(), "var inferred 의 실제 타입");

        var counted = 42;

        Check.Equal(typeof(int), counted.GetType(), "var counted 의 실제 타입은 int여야 한다");
        Check.Equal(42, Convert.ToInt32(counted), "counted 의 값");

        Check.Section("5. 문자열 보간과 축자 문자열");

        string label = $"{equipmentTag} #{sequence}";
        string path = @"C:\plant\data.db3";

        Check.Equal("P-1101A #3", label, "문자열 보간 결과");
        Check.Equal(@"C:\plant\data.db3", path, "축자 문자열 경로");

        Check.Section("6. const");

        const double PiApprox = 3.14159;

        Check.Close(3.14159, PiApprox, "const PiApprox");

        Check.Section("7. 여러 줄 문자열로 쿼리 만들기");

        string viewName = "EquipmentPointView";

        string query = $@"SELECT EquipmentTag FROM {viewName} WHERE Seq = {sequence}";

        Check.Equal("SELECT EquipmentTag FROM EquipmentPointView WHERE Seq = 3", query, "보간으로 만든 쿼리");
    }
}
```
