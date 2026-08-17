from itertools import islice


def compress(events):

    curr = ("", 0, 0)
    for event in events:
        txt, sec = event

        if curr[0] == "":
            curr = (txt, sec, sec)

        elif (txt == curr[0]) and (sec == curr[2]+1):
            curr = (curr[0], curr[1], sec)

        else:
            print("   [compress]   ", curr)
            yield curr
            curr = (txt, sec, sec)

    if curr[0] != "":
        yield curr

def endless():
    n = 0
    while True:
        print("   [endless]   ","login", n)
        yield ("login", n)
        print("   [endless]   ", "logout", n)
        yield ("logout", n)
        n += 1



if __name__ == "__main__":

    # # 기본 동작
    # events = [
    #     ("login", 1),
    #     ("login", 2),
    #     ("login", 3),
    #     ("logout", 10),
    #     ("error", 15),
    #     ("error", 16),
    # ]
    # print("A 결과:", list(compress(events)))
    # print("A 기대:", [("login", 1, 3), ("logout", 10, 10), ("error", 15, 16)])

    # # 아무것도 안나와야 정상
    # print(list(compress(iter([]))))

    # 무한 로그
    print(list(islice(compress(endless()), 6)))