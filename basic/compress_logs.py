import collections


def compress_logs(logs):

    que = collections.deque(logs[1:])

    curr_txt = logs[0][0]
    curr_seconds = logs[0][1]
    start_seconds = logs[0][1]
    curr_compress_log = (curr_txt, start_seconds, curr_seconds)

    compressed_logs = []
    while que:
        log = que.popleft()
        txt = log[0]
        seconds = log[1]
        if (curr_txt == txt) and (curr_seconds == seconds - 1):
            curr_compress_log = (curr_txt, start_seconds, seconds)
            curr_seconds = seconds

        else:
            compressed_logs.append(curr_compress_log)
            curr_txt = txt
            curr_seconds = seconds
            start_seconds = seconds
            curr_compress_log = (curr_txt, start_seconds, curr_seconds)

    compressed_logs.append(curr_compress_log)
    return compressed_logs


def compress_logs_v1(logs):

    if not logs:
        return []

    compressed_logs = []
    curr = (logs[0][0], logs[0][1], logs[0][1])

    for txt, sec in logs[1:]:
        if txt == curr[0] and sec - 1 == curr[2]:
            curr = (curr[0], curr[1], sec)

        else:
            compressed_logs.append(curr)
            curr = (txt, sec, sec)

    compressed_logs.append(curr)
    return compressed_logs


if __name__ == "__main__":
    logs = [
        ("login", 1),
        ("login", 2),
        ("login", 3),
        ("logout", 10),
        ("error", 15),
        ("error", 16),
        ("error", 17),
        ("login", 30),
    ]

    print(compress_logs(logs))
    print(compress_logs_v1(logs))
