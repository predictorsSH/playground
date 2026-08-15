from itertools import islice

class CountUp:
    def __init__(self, start=0, end=None):
        self.current = start
        self.end = end

    def __next__(self):
        current = self.current

        if self.end is not None and current >= self.end:
            raise StopIteration()

        self.current += 1
        return current

    def __iter__(self):
        return self


if __name__ == "__main__":
    cnt_up = CountUp(start=0, end=15)

    print(next(cnt_up))
    print(next(cnt_up))

    # 이어서 짝수 8개만 사용. but 마지막 숫자 16은 end=15 보다 커서 반환 안함.
    sliced = islice(filter(lambda v: v %2 ==0, cnt_up), 8)
    for i in sliced:
        print(i)

    # 15 에서는 StopIteration
    print(next(cnt_up))