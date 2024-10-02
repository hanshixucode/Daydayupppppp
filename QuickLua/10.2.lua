print(3//2)
print(3%2)
--关系运算符
print(3>2)
print(3>=2)
print(3~=2)
--随机数
print(math.random(6))
--取整
print(math.floor(3.3)) -- 负无穷
print(math.ceil(-3.3)) -- 正无穷
print(math.modf(3.3))  -- 0

x = 3
print(x + 0.0)

--test
for i = -10, 10 do
    print(i, i%3)
end

-- string
a= "one string"
b = string.gsub(a, "one", "another")
print(a)print(b)
print(#a) -- 获取字符串长度
print(a..b)
c = a.."hello"

--强转
print(10 .. 20)

print(tonumber("123"))
a = tostring(123)
print(type(a))