# 2027.08.15
# generator는 값을 미리 다 만들어두지 않고, 요청받을 때마다 멈췄던 자리에서 이어서
# 하나씩 만들어 내놓는 이터레이터다. 되감기가 없어서 한 번 지나간 값은 다시 못 본다.


import sys
from itertools import islice

def count_up():
    cnt = 0
    while True:
        cnt += 1
        yield cnt

if __name__ == "__main__":
    n = 1_000

    cph = [i for i in range(n)]

    # ()로 감싸면  generator expression
    gen = (i for i in range(n))

    # generator 사용의 주 목적은 메모리 사용량 감소다.
    print(f"list: {sys.getsizeof(cph)}") # list: 8856
    print(f"gen: {sys.getsizeof(gen)}") # gen: 200


    # islice가 리턴하는 것도 리스트가 아니라, 이터레이터
    sliced = islice(count_up(), 10)
    for i in sliced:
        print(i)


    # generator는 소진
    n = 5
    cph = [i for i in range(n)]
    gen = (i for i in range(n))

    print("cph:")
    print(list(cph))
    print(list(cph))

    print("gen:")
    print(list(gen))
    print(list(gen))