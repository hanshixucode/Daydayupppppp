--module
local m = require "math"
print(m.sin(3.14))
local h = require "hello"
print(h.fact(3))

local module = {}
module.content = "ss"

function module.func1()
    print("ssssssss")
end
return module