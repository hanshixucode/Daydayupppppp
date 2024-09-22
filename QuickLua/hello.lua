function fact (n)
    if n==0 then
        return 1
        else
            return n * fact(n-1)
    end
end

print("enter number")
a = io.read("*n")
if a < 0 then
    a = -a
end 
print(fact(a))