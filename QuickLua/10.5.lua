--table else
--[[表构造器
days = {"1","2"}
print(days[1])
a = {x = 1, y = 2} -- => a = {} a.x = 1 a.y = 2
days[3] = "asd"
l = "ss"
a[l] = "asd"
print(a["l"])
print(a[l])
w = {x = 0,y = 0, label = "console"}
w[1] = "ano"
w.x = nil

testtable = {
    x = 5,
    "1",
    {x = 1, y = 2},
    {x = 2, y = 3}
}
print(testtable[2].y)

opnames = {
    ["ss"] = "ss",
    ["aa"] = "aa"
} -- => ss = "ss" , aa = "aa"
print(opnames.ss)
]]--

-- 数组 array
testarray = {}
--[[for i = 1, 10 do
    testarray[i] = io.read()
end
for i = 1, #testarray do
    print(testarray[i])
end]]--
testarray[1] = 1
testarray[2] = 2
testarray[3] = 3
testarray[100] = 100
print(#testarray)
testarray[2] = nil
print(#testarray)

--遍历
t = {
    ["ss"] = "ss",
    ["aa"] = "aa",
    ta = {x = 1,y = 3}
}
for key, value in pairs(t) do
    print(key,value)
end
for index, value in ipairs(testarray) do
    print(index,value)
end
bool = t and t.ta and t.ta.x
print(bool)

