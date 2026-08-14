# 제너레이터 학습 계획

파일 하나당 주제 하나. 코딩은 직접, Claude는 방향만.

## 로드맵

1. **`01_yield_basics.py` — yield 기초**
   제너레이터 함수가 일반 함수와 뭐가 다른지.
   호출 시 실행되지 않고 제너레이터 객체가 반환됨, `next()`로 한 스텝씩 진행,
   yield에서 멈추고 지역 변수는 유지, 소진되면 `StopIteration`.

2. **`02_lazy_evaluation.py` — 지연 평가(lazy)**
   리스트와의 메모리 차이 (`sys.getsizeof` 비교), 무한 시퀀스 만들기.

3. **`03_generator_expression.py` — 제너레이터 표현식**
   `[x for ...]` vs `(x for ...)`. 언제 뭘 쓰는지, 한 번만 소비 가능하다는 특성.

4. **`04_pipeline.py` — 파이프라인**
   제너레이터를 체이닝해서 스트림 처리. 각 단계가 lazy하게 연결되는 구조.

5. **`05_compress_logs_stream.py` — 마무리 실전**
   `compress_logs`를 제너레이터 버전으로.
   로그가 무한히 들어와도 동작하게 (전체 리스트를 받지 않고 스트림으로 처리).

## 진행 상황

- [x] 1단계: yield 기초 — "시작!"은 첫 `next()` 때 찍히고, 이후엔 yield 사이만 실행, 소진 시 `StopIteration`
- [ ] 2단계: 지연 평가
- [ ] 3단계: 제너레이터 표현식
- [ ] 4단계: 파이프라인
- [ ] 5단계: compress_logs 스트림 버전
