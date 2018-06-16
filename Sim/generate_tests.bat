set SOLUTION_DIR=%1
set BAKER_DIR=%2
set SOURCE_DIR=%SOLUTION_DIR%LowCarb\BuiltIns\
set CONTRACTS_DIR=%SOLUTION_DIR%LowCarbTests\BuiltIns\Contracts\
set UNITTEST_DIR=%SOLUTION_DIR%LowCarbTests\BuiltIns\
set PROJECT=%SOLUTION_DIR%LowCarbTests\LowCarbTests.csproj

pushd %CD%
chdir %BAKER_DIR%

for %%F in (%SOURCE_DIR%*.cs) do bake contracts -s=%%F -c=LowCarb.BuiltIns.%%~nF %CONTRACTS_DIR%%%~nFContractsFile.txt
for %%F in (%CONTRACTS_DIR%*.txt) do bake fillproject -t=contract -F=BuiltIns\Contracts\%%~nxF %PROJECT%

for %%F in (%SOURCE_DIR%*.cs) do bake unittest -d=BuiltIns\\Contracts\\ -n=LowCarb.BuiltIns -c=%%~nF %UNITTEST_DIR%%%~nFTests.cs
for %%F in (%UNITTEST_DIR%*.cs) do bake fillproject -t=unittest -F=BuiltIns\%%~nxF %PROJECT%

popd