import marimo

__generated_with = "0.18.4"
app = marimo.App(width="medium")


@app.cell
def _(get_word, source):
    words = []
    while True:
        word = get_word(src=source)
        if word is None:
            break
        words.append(word)
    return


@app.function
def word_number(word):
    magic = 0
    for letter in word:
        magic += 1 + ord(letter) - ord('a')

    return magic


@app.cell
def _():
    word_number('a')
    return


@app.cell
def _(get_word, source2):
    def word_numbers(src):
        while (word := get_word(src)) is not None:
            yield word_number(word)


    magic_nums = list(word_numbers(source2))
    return


if __name__ == "__main__":
    app.run()
