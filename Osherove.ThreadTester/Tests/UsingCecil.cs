using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;

namespace Osherove.ThreadTester.Tests
{
    
    [TestFixture]
    public class UsingCecil
    {
        [Test]
        public void methodDef()
        {

            AssemblyDefinition assembly = AssemblyFactory.GetAssembly(Assembly.GetExecutingAssembly().Location);
            foreach (TypeDefinition definition in assembly.MainModule.Types)
            {
                if(definition.Name==typeof(Person).Name)
                {
                    foreach (MethodDefinition method in definition.Methods)
                    {
                        if (method.Name=="SayHello")
                        {
                            changeMethod(method);
                            return;
                        }
                    }
                }
            }
        }

        private void changeMethod(MethodDefinition method)
        {
            ModuleDefinition module = method.DeclaringType.Module;
            MethodInfo writeline = typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) });
            MethodReference writeLineRef = module.Import(writeline);
            
            CilWorker cil = method.Body.CilWorker;
            Instruction op1 = cil.Create(OpCodes.Ldstr, "Say hello was called!");
            Instruction op2 = cil.Create(OpCodes.Call, writeLineRef);
            cil.InsertBefore(method.Body.Instructions[0],op1);
            cil.InsertAfter(op1,op2);

            module.Import(method.DeclaringType);
            AssemblyFactory.SaveAssembly(module.Assembly,@"c:\temp\cilTemp\tempasm.dll");
        }
    }

    class Person
    {
        public string SayHello()
        {
            return string.Empty;
        }
    }

}
