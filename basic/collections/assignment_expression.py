import re

pattern = r"item-(\d+)"

# 1. 일반 for + if
def collect_number(words):
    collection = []
    for word in words:
        matched = re.match(pattern,word)
        if matched:
            collection.append(word.split("-")[1])
            
    return collection


# 2. list comprehension

def collect_number_comp(words):

    matched_lst = filter(None, (re.match(pattern, word) for word in words))

    return [m.group(1) for m in matched_lst]
            
        
            
# 3. assignment expression

def collect_number_assign(words):
    return [m.group(1) for word in words if (m := re.match(pattern, word))]



# 4. assignment expression examples

# # 이전 방식
# line = input("입력: ")
# while line != "quit":
#     print(f"받음: {line}")
#     line = input("입력: ")
    
# # walrus
# while (line := input("입력:")) != "quit":
#     print(f"받음: {line}")

result = []
nums = [1, 2, 3, 4, 5]

for x in nums:
    y = x*x
    if y > 5:
        result.append(y)
print(result)

print([x*x for x in nums if x*x >5])

print([y for x in nums if (y:=x*x)>5])


data = {"name": "kim", "age": 30}

# 옛날 방식: 먼저 변수에 담고, 그 다음 줄에서 검사
name = data.get("name")
if name:
    print(f"이름: {name}")
    
if name:=data.get("name"):
    print(name)
    
    
    
# if __name__ == "__main__" :
    # import timeit
    
    # words = ["item-123", "item-456", "item-789", "bar", "foo"]
    # big = words * 10_000
    # number = 100
    
    # for name, func in [
    #     ("for + if", collect_number),
    #     ("comprehension", collect_number_comp),
    #     ("walrus", collect_number_assign),
    # ]:
    #     elapsed = timeit.timeit(lambda f=func: f(big), number=number)
    #     print(f"{name:14}: {elapsed / number * 1000:.3f} ms / call")
        
