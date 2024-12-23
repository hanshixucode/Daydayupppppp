--闭包数据共享upvalue
local function upvalyetest(n)
    local function f1()
        print(n)
    end
    local function f2()
        n = n + 10
        print(n)
    end
    return f1,f2
end

local n1,n2 = upvalyetest(1000)
n1()
n2()
n1()