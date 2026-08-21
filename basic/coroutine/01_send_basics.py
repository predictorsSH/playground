def printer():
    print("함수 시작")
    while True:
        print("yield 바로 위")
        s = yield
        print(f"yeild에서 꺠어남: {s}")

if __name__ == "__main__":
    c = printer()

    try :
        # 생성직후의 코루틴은 함수 본문이 한 줄도 실행되지 않은 상태 = yield에 도달하지 않은 상태
        c.send("hello")
    except TypeError as e:
        print(e)

    # next()는 send(None)과 동일
    # 첫 next 호출은 priming(마중물)에 비유된다. 함수를 첫 yield까지는 실행시켜 값을 받을 수 있는 상태로 만들기 때문
    next(c)
    # c.send(None)

    print("#########")
    # send(v) = "중단 중인 yield 표현식을 v로 평가시키며 재개하고, 다음 중단 시점의 yield 우측 값을 반환받는다"
    c.send("hello")

    print("#########")
    c.send("world")

    print("#########")
    next(c)

    print("#########")
    print(c.send("hi"))
    # 1. send() 호출전 코루틴은 s=yield의 yield 지점에서 일시 중단 되어있음. 대입은 아직 실행되지 않음
    # 2. c.send("hi") 호출되면, 코루틴이 중단지점에서 재개됨. 이때 yield 표현식이 send로 전달된 값 "hi"로 평가됨
    # 3. 평가된 값이 s에 대입됨.
    # 4. print(s) 실행
    # 5. s = yield의 yield에 도달. yield 우측에 표현식이 없으므로 None을 호출자에게 반환하며 일시 중단.
    # 6. None이 c.send("hi") 호출의 반환값이 됨. 바깥의 print(...)가 이를 출력.


