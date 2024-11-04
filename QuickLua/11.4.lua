--table else else
local t_114 = {10,20,30}
table.insert(t_114,1,111)
--{111,10,20,30}


table.move(t_114,1,#t_114,2)
--print(#t_114)
table.move(t_114,2,#t_114,1)
--print(#t_114)
--[[for index, value in ipairs(t_114) do
    print(inde, value) -- nil
end
for key, value in pairs(t_114) do
    print(t_114[key], value)
end]]--

--function
function add(a)
    local sum = 0
    for i = 1, #a do
        sum = sum + a[i]
    end
    return sum
end
--print(add(t_114))
--function(a,b) -> fun(3,4,5) - remove 5

--variadic
function adddic (...)
    local s = 0
    for _, value in ipairs{...} do
        s = s + value
    end
    return s
end
--print(adddic(1,2,3,4,5,5,5,5,5))

function foo1 (...)
    print("calling",...)
end
--select
print(select(2,"1","2","3"))

local function addaselect(...)
    local sum1 = 0
    for i = 1, select("#", ...) do
        sum1 = sum1 + select(i,...)
    end
    return sum1
end

--table.unpack
print(table.unpack(t_114)) --111     10      20      30      30

