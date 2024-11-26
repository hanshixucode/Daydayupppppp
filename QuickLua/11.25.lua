--闭包
--[[local function digitbutton(digit)
    return Button{
        label = tostring(digit),
        action = function ()
            add(digit)
        end
    }
end]]--
local oldSin = math.sin
--math.sin = function (x)
  --  return oldSin(x * (math.pi / 180))
--end

print(oldSin(100))
print(math.sin(100))

--位和字节
