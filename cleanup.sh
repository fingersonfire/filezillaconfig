find . -name "*.config" -exec dos2unix             {} \; > /dev/null 2>&1
find . -name "*.cs"     -exec dos2unix             {} \; > /dev/null 2>&1
find . -name "*.csproj" -exec dos2unix             {} \; > /dev/null 2>&1
find . -name "*.xaml"   -exec dos2unix             {} \; > /dev/null 2>&1

find . -name "*.config" -exec sed -i 's/[ \t]*$//' {} \; > /dev/null 2>&1
find . -name "*.cs"     -exec sed -i 's/[ \t]*$//' {} \; > /dev/null 2>&1
find . -name "*.csproj" -exec sed -i 's/[ \t]*$//' {} \; > /dev/null 2>&1
find . -name "*.xaml"   -exec sed -i 's/[ \t]*$//' {} \; > /dev/null 2>&1

rm *.docstates *.suo *.user > /dev/null 2>&1
hg st
