local function test(num)
    local temp = num
    return function ()
        temp = temp + 1
        return temp
    end
end
print(test(1)())