--_index元方法
local pro = {x = 0,y = 0, w = 0,h = 0}
local pro1 = {x = 3,y = 4, w = 5,s = 0}

local mt = {}
function New(o)
    setmetatable(o , mt)
    return 0
end
--mt.__index = pro
mt.__newindex = pro1
--mt.__index = function (_, key)
--    return pro[key]
--end
local t = {x = 1, y = 2}
New(t)
t.z = 3
print(t.z)
print(pro1.z)