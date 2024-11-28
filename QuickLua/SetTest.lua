--require "11.28"
local set = Set{}
local s1 = set.new{10,20,30,40}
local s2 = set.new{30,1}
print(getmetatable(s1))
print(getmetatable(s2))
