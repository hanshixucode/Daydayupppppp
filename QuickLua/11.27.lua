--[[module
local m = require "math"
print(m.sin(3.14))
local h = require "hello"
print(h.fact(3))

--[[
local module = {}
module.content = "ss"

function module.func1()
    print("ssssssss")
end
return module
]]--
--迭代器 闭包
local function values(t)
    local i = 0
    return function ()
        i = i + 1;
        return t[i]
    end
end
local t = {10,20,30}
for element in values(t) do
    print(element)
end