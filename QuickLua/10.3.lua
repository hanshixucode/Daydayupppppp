--table
a = {}
a["x"] = 10
print(a["x"])
b = a
--a = nil --匿名
print(b["x"])

for i = 1,100 do
    a[i] = i* 2 
end

a.x = 10
a.y = 20

a = {}
a[2.1] = 10
a[2.222] = 10
print(a[2.2])
print(a[2.11])