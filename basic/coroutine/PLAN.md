# 코루틴 학습 계획

파일 하나당 주제 하나. 코딩은 직접, Claude는 방향만.
이전 챕터: `basic/generator/` — 제너레이터가 "값을 내보내는" 쪽이었다면, 코루틴은 `yield`를 뒤집어 "값을 받는" 쪽.

범위: 클래식(제너레이터 기반) 코루틴 + async/await로의 진화 개념 연결까지.
asyncio 실전은 별도 챕터로.

## 로드맵

1. **`01_send_basics.py` — 값을 받는 yield**
   - `x = yield` 형태: yield가 문(statement)이 아니라 표현식(expression)
   - `.send(값)`으로 밖에서 안으로 값 주입
   - priming: 첫 `next()` 없이 `send()` 하면 왜 에러인가
   - `next(g)`와 `g.send(None)`이 같다는 것

2. **`02_two_way.py` — 양방향 통신**
   - `y = yield x`: 내보내기와 받기가 동시에 일어날 때의 실행 순서 추적
   - 실습: 값을 보낼 때마다 현재까지의 합/평균을 돌려주는 상태 유지 코루틴

3. **`03_lifecycle.py` — 코루틴 생명주기**
   - `.close()`와 `GeneratorExit`: 정리(cleanup) 코드가 실행되는 시점
   - `.throw()`로 예외 주입 (가볍게)
   - try/finally와의 조합

4. **`04_yield_from_to_async.py` — yield from과 async/await로의 다리**
   - `yield from`의 위임(delegation) 역할
   - "코루틴을 스케줄링하는 루프"를 손으로 흉내 → 이벤트 루프의 원형
   - async/await가 이 패턴을 언어 차원으로 흡수한 과정 (개념만)

5. **`05_capstone.py` — 실전 마무리 (주제 미정)**
   - 후보: push 방식 compress_logs / 실시간 통계 코루틴 / 코루틴 파이프라인
   - 3~4단계쯤 진행 후 결정

## 진행 상황

- [ ] 1단계: 값을 받는 yield
- [ ] 2단계: 양방향 통신
- [ ] 3단계: 코루틴 생명주기
- [ ] 4단계: yield from → async/await
- [ ] 5단계: 캡스톤 (주제 미정)
