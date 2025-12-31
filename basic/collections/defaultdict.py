import marimo

__generated_with = "0.18.4"
app = marimo.App(width="medium")


@app.cell
def _():
    import marimo as mo # 마크다운용

    from collections import defaultdict
    return defaultdict, mo


@app.cell(hide_code=True)
def _(mo):
    mo.md(r"""
    기본값을 자동으로 만들어주는 dict <br>
    `if key not in dict` 체크를 없애서 코드를 짧고 안전하게 만들어준다.
    """)
    return


@app.cell
def _():
    d = {}
    d["a"] += 1 # KeyError
    return


@app.cell
def _(defaultdict):
    dd = defaultdict(int)
    dd["a"] +=1
    return


@app.cell(hide_code=True)
def _(mo):
    mo.md(r"""
    ## 그룹핑

    - 일반 dict 였다면 `if key not in groups` 가 필요한 상황에서 유용
    """)
    return


@app.cell
def _(defaultdict):
    groups = defaultdict(list)

    data = [("a",1), ("b", 2), ("a",3)]

    for key, value in data:
        groups[key].append(value)

    print(groups) # {'a':[1,3], 'b':[2]}

    return


@app.cell
def _():
    return


if __name__ == "__main__":
    app.run()
