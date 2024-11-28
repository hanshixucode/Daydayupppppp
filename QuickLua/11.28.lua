--[[元表
local t = {}
local t1 = {}
setmetatable(t,t1)
print(getmetatable(t) == t1)]]--

Set = {}
local mt = {}
function Set.new(l)
    local set = {}
    setmetatable(set,mt)
    for _, v in ipairs(l) do
        set[v] = true
    end
    return set
end

function Set.union(a,b)
    local res = Set.new{}
    for k in pairs(a) do
        res[k] = true
    end
    for k in pairs(b) do
        res[k] = true
    end
    return res
end

function Set.intersection(a,b)
    local res = Set.new{}
    for k in pairs(a) do
        res[k] = b[k]
    end
    return res
end

function Set.tostring(set)
    local l = {}
    for e in pairs(set) do
        l[#l + 1] = tostring(e)
    end
    return "{" .. table.concat(l, ",") .. "}"
end

local s1 = Set.new{10,20,30,40}
local s2 = Set.new{30,1}
print(getmetatable(s1))
print(getmetatable(s2))

mt.__add = Set.union
mt.__mul = Set.intersection
s3 = s1 + s2
print(Set.tostring(s3))
print(Set.tostring(s3 * s1))
s = Set.new{1,2,3}
s = s + 8
return Set
