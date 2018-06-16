using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using LowCarb;

namespace FileBaker
{
    public class ChipSourceReader
    {
        public static IChip ReadSource(string path, string chipName)
        {
            Assembly assembly = CompileSource(path);
            Type chipType = GetChipType(assembly, chipName);
            return InstantiateChip(chipType);
        }

        private static Assembly CompileSource(string path)
        {
            CSharpCodeProvider cSharpProvider = new CSharpCodeProvider();
            CompilerParameters compilerParams = new CompilerParameters();

            compilerParams.ReferencedAssemblies.Add("LowCarb.dll");
            compilerParams.GenerateInMemory = true;
            compilerParams.GenerateExecutable = false;

            CompilerResults compilerResults = cSharpProvider.CompileAssemblyFromFile(compilerParams, path);

            if (compilerResults.Errors.HasErrors)
            {
                StringBuilder compileErrors = new StringBuilder();
                compileErrors.AppendLine(String.Format("File '{0}' has errors.", path));
                foreach (CompilerError error in compilerResults.Errors)
                {
                    compileErrors.AppendLine(String.Format(
                        "{0} {1}: {2} ({3}, {4})",
                        error.IsWarning ? "warning" : "error",
                        error.ErrorNumber,
                        error.ErrorText,
                        error.Line,
                        error.Column));
                }
                throw new ApplicationException(compileErrors.ToString());
            }

            return compilerResults.CompiledAssembly;
        }

        private static Type GetChipType(Assembly assembly, string chipName)
        {
            Type chipType = null;
            try
            {
                chipType = assembly.GetType(chipName, true);
            }
            catch (TypeLoadException e)
            {
                throw new TypeLoadException(String.Format("Type '{0}' not found in compiled source.", chipName), e);
            }

            return chipType;
        }

        private static IChip InstantiateChip(Type chipType)
        {
            List<Type> chipInterfaces = new List<Type>(chipType.GetInterfaces());
            if (!chipInterfaces.Contains(typeof(IChip)))
            {
                throw new InvalidCastException(String.Format("Type '{0}' does not implement 'IChip' interface.", chipType.FullName));
            }

            ConstructorInfo chipConstructor = chipType.GetConstructor(new Type[] { });
            if (chipConstructor == null)
            {
                throw new MissingMethodException(String.Format("Type '{0}' does not implement 'new()'.", chipType.FullName));
            }

            Object chip = chipConstructor.Invoke(new Object[] { });
            return (IChip)chip;
        }
    }
}
