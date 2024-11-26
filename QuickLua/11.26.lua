--数据结构
--array
local a = {}
for i = 1,100 do
    a[i] = 0
end
print(#a)
--[[矩阵
local mt = {}
for i = 1, N do
    local row = {}
    mt[i] = row
    for j = 1, M do
        row[j] = 0
    end
end
 
--链表
local list = nil
list = {next = list, value = v}
local l = list
while l do
    l = l.next
end
]]--
--序列化
--local f = load("i = i + 1")
--local i = 0
--load("i = i + 1")
--print(i)
--loadfile("9.5.lua")