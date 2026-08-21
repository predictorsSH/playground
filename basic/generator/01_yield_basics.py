def count_up_to(to:int):
    print("start")
    num = 0
    while num < to:
        num += 1
        yield num
        print("yield 완료")


if __name__ == "__main__":

    g = count_up_to(3)
    print(next(g))
    print(next(g))
    print(next(g))
    # print(next(g))

    print("-------------")
    print(hasattr(g, "__iter__"))
    print(hasattr(g, "__next__"))

    # for문은 내부적으로 next()를 반복하다가, StopIteration을 잡아서 루프 종료 신호로 쓴다.
    for n in count_up_to(3):
        print("########")
        print(n)
        print("########")
