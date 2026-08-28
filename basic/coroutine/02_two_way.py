"""
핵심 질문

1. y = yield x에 도달하면 x가 먼저 나가는가, y 대입이 먼저 일어나는가? (한 번의 send 안에서 벌어지는 일이 아니라는 게 요점)
   -> x가 먼저 평가되어 반환되고 거기서 중단한다. y 대입은 다음 send(v)가 올 때 v를 받아 실행된다.
      두 방향이 한 줄에 있지만 시점이 다르다.

2. g.send(v)의 반환값은 어느 시점의 x인가 — 재개 직전에 멈춰 있던 그 yield의 x인가, 재개 후 새로 도달한 yield의 x인가?
   -> 재개 후 새로 도달한 yield의 x다. yield 우측은 도달할 때마다 다시 평가된다.
      send(None)처럼 직전과 같은 값이 나오는 경우도 예외가 아니다. 옛날 yield로 돌아간 게 아니라,
      재도달한 yield 우측을 정상적으로 재평가했는데 그 사이 코드가 answer를 갱신하지 않았을 뿐이다.
      (여기서는 if value is not None 가드에 걸려 갱신을 건너뛴다)
      즉 반환값을 정하는 건 언제나 "재도달한 yield 우측의 재평가 결과"이고,
      그 값이 달라지느냐는 재개와 재도달 사이의 코드가 무엇을 했느냐에 달려 있다.

3. priming next(g)가 돌려주는 값은 무엇인가? 1단계에선 None이라 안 보였지만 이제 보인다.
   -> next(g)는 send(None)과 같고, 함수 본문을 첫 yield까지 실행시킨다.
      그래서 첫 yield 우측 값, 즉 초기값 0이 나온다.
"""


def averager():
    total =0
    cnt = 0
    answer = 0
    while True:
        value = yield answer

        if value is not None:
            total += value
            cnt +=1
            answer = total/cnt





if __name__ == "__main__" :
    a = averager()
    print(next(a)) # 0
    print(a.send(10)) # 10
    print(a.send(20)) # 15

    # next(a) = priming
    # 1. 함수 본문 시작. total=0, cnt=0, answer=0
    # 2. while 진입, yield answer 에 도달. 우측 answer가 0으로 평가됨
    # 3. 0을 반환하며 중단. value 대입은 아직 실행되지 않음  -> 출력 0

    # a.send(10)
    # 1. 중단 중이던 yield 표현식이 10으로 평가되며 재개
    # 2. value에 10 대입
    # 3. total=10, cnt=1, answer=10.0
    # 4. 루프 위로 돌아가 yield answer 재도달. 우측이 "다시" 평가되어 10.0
    # 5. 10.0을 반환하며 중단  -> 출력 10.0

    # a.send(20)
    # 1. yield 표현식이 20으로 평가되며 재개
    # 2. value에 20 대입          <- 들어오는 값 20
    # 3. total=30, cnt=2, answer=15.0
    # 4. yield answer 재도달, 우측이 다시 평가되어 15.0
    # 5. 15.0을 반환하며 중단     -> 출력 15.0. 나가는 값은 20이 아니라 15.0
    #    들어오는 값(value)과 나가는 값(answer)은 서로 다른 변수다.
    #    send(10) 때 둘 다 10이었던 건 첫 값이라 평균이 입력값과 같았을 뿐.

