--table else else
--insert
local t1 = {}
insert_table = {}
for line in io.lines() do
    table.insert(insert_table,line)
end
print(#insert_table)

for index, value in ipairs(insert_table) do
    print(index,value)
end
