--end functions
--in out
--[[local t = io.read("a")
print(t)
io.write(t)

while true do
    local n1 ,n2, n3 = io.read("n","n","n")
    if not n1 then break end
    print(math.max(n1.n2.n3))
end
]]--

--[[controls
if a == 0 then a = 1 end
if a ==1 then
elseif a == 2 then    
end 
-- not support switch 
while true do
    
end
]]--

--[[闭包
local a = {p = print}
a.p("hello world")
local print = math.sin
a.p(print(1))]]--

local fact = function (n)
    if n == 0 then return 1
    else return n*fact(n-1)
    end
end
local fact1
fact1 = function (n)
    if n == 0 then return 1
    else return n*fact1(n-1)
    end
end
print(fact1(3))