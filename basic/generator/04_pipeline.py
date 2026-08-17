def parse(lines):
    """로그 문자열을 (이벤트명, 초) 튜플로 변환한다.

    "이벤트명 초" 형식이 아닌 줄(빈 줄, 토큰 개수 불일치, 초가 숫자가 아님)은
    에러 없이 건너뛴다.

    예: ["login 1", "", "login abc", "error 15"]
        -> ("login", 1), ("error", 15)
    """
    for line in lines:

        try:
            name, sec = line.split()
            sec = int(sec)
        except ValueError:
            continue

        print(f"  [parse]   {(name, sec)}")
        yield name, sec


def exclude(events, name):
    """이벤트명이 name인 항목을 제외하고 나머지를 그대로 통과시킨다.

    예: [("login", 1), ("ping", 2), ("error", 3)], name="ping"
        -> ("login", 1), ("error", 3)
    """
    for event in events:
        if event[0] != name:
            print(f"  [exclude] {event}")
            yield event

def take(items, n):
    """앞에서 최대 n개만 통과시킨다. itertools.islice의 직접 구현판.

    n개를 채우면 원본에서 한 칸도 더 당기지 않고 멈춘다.
    원본이 n개보다 먼저 끝나면 그만큼만 내보내고 정상 종료한다.

    예: [1, 2, 3], n=2  -> 1, 2
        [1, 2, 3], n=10 -> 1, 2, 3
    """
    if n <= 0:
        return

    cnt = 0
    for item in items:
        cnt += 1
        print(f"  [take]    {item}")
        yield item

        if cnt == n:
            return


if __name__ == "__main__":

    # 아래 lines는 리스트다. 이 줄에서 이미 10줄 전부가 메모리에 올라간다.
    # 즉 이 데모에 메모리 이득은 없다. (뒤쪽 5줄을 파싱하지 않는 이득만 있다)
    #
    # 메모리 이득을 보려면 소스를 파일 객체로 바꾼다:
    #     with open("app.log") as f:
    #         pipeline = take(exclude(parse(f), "ping"), 3)
    #
    # 파일 객체는 한 줄씩 내놓으므로 10GB 파일도 한 줄 분량 메모리로 끝난다.
    # f.readlines()는 전부 리스트로 읽으니 f를 그대로 넘겨야 한다.
    lines = [
        "login 1",
        "ping 2",
        "login 3",
        "",
        "error 15",
        "ping 16",
        "error abc",
        "logout 20",
        "ping 21",
        "login 30",
    ]


    pipeline = take(exclude(parse(lines), "ping"), 3)
    print("연결 완료 — 여기 위로는 아무 출력도 없어야 정상")

    for event in pipeline:
        print(f"결과: {event}")