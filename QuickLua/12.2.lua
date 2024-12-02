---@diagnostic disable: undefined-global
--面向对象
--封装
local object = {}
object.id = 1

function object:test()
    print(self.id)
end
function object:new()
    print(self.id)
    local obj = {}
    setmetatable(obj,self)
    self.__index = self
    return obj
end
print(object:new())
local newobj = object:new()
print(newobj.id)

--newobj:test()
newobj.id = 2
--newobj:test()
--object:test()

--继承
function object:subclass(classname)
    _G[classname] = {}
    local t = _G[classname]
    self.__index = self
    self.base = self
    setmetatable(t,self)
end

object:subclass("newclass")
local nc = newclass:new()
print(nc.id + 3)
nc.id = 3
nc:test()

--多态
object:subclass("gameobject")
gameobject.posx = 0
gameobject.posy = 0
function gameobject:move()
    self.posx = self.posx + 11
    self.posy = self.posy + 11
    print(self.posx)
    print(self.posy)
end

gameobject:subclass("player")
function player:move()
    self.base.move(self)
    print(self.posx+100)
end
local p1 = player:new()
p1:move()
p1:move()

--垃圾回收
print(collectgarbage("count"))