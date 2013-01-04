//EventsVerifier for NUnit by Roy Osherove.
//Team Agile Consulting, www.TeamAgile.com
//blog: www.iserializable.com
//for questions and comments: eventsTesting@TeamAgile.com

using System;
using System.Collections;
using System.Reflection;
using TeamAgile.NUnitExtensions.EventsTesting.CommonEventHandler;
using NUnit.Framework;

namespace AOUT.CH5.Logan.Tests
{
    class EventsVerifier : IDisposable
    {
        private ArrayList expectations = new ArrayList();

        public void Expect(object target, string eventName, params object[] args)
        {
            EventExpectation expectation = new EventExpectation();
            expectation.ExpectEvent(target, eventName, args);
            expectations.Add(expectation);
        }

        public void Verify()
        {
            foreach (EventExpectation expectation in expectations)
            {
                expectation.Verify();
            }


        }

        public void ExpectNoEvent(object target, string eventName)
        {
            EventExpectation expectation = new EventExpectation();
            expectation.ExpectNoFire(target, eventName);
            expectations.Add(expectation);
        }
        #region IDisposable Members

        public void Dispose()
        {
            Verify();
        }

        #endregion
    }

    public class EventExpectation : IDisposable
    {
        private bool callbackCalled = false;
        private string expectedEventName = String.Empty;
        private string expectedTargetName = String.Empty;
        private object[] expectedArgs = new object[] { };
        private object[] actualArgs = new object[] { };
        private bool expectNoFire = false;

        public void Verify()
        {
            if (expectNoFire)
            {
                expectNoFire = false;
                return;
            }

            if (!callbackCalled)
            {
                string message = "Event '{0}' was expected from class '{1}'";
                Assert.Fail(message, expectedEventName, expectedTargetName);
            }

        }

        public void ExpectNoFire(object target, string eventName)
        {
            expectNoFire = true;
            subscribeToEvent(target, eventName);
        }

        public void ExpectEvent(object target, string eventName, params object[] args)
        {
            expectNoFire = false;
            expectedArgs = args;
            subscribeToEvent(target, eventName);

        }

        private void subscribeToEvent(object target, string eventName)
        {
            Type targetType = target.GetType();

            expectedTargetName = targetType.Name;
            expectedEventName = eventName;
            EventInfo eventInfo = targetType.GetEvent(eventName);

            registerLocallyForEvent(eventInfo, target);
        }


        public void MyEventCallback(Type eventType, object[] args)
        {
            callbackCalled = true;
            actualArgs = args;

            if (expectNoFire)
            {
                expectNoFire = false;
                Assert.Fail("Unexpected event was fired:{0}.{1}"
                            , expectedTargetName, expectedEventName);
            }


            for (int i = 0; i < expectedArgs.Length; i++)
            {
                Assert.AreEqual(expectedArgs[i], actualArgs[i],
                                "Wrong argument[{0}] value passed by event",
                                i.ToString());
            }

        }


        #region registerForEvent

        private MethodInfo registerLocallyForEvent(EventInfo eventInfo, object target)
        {
            EventHandlerFactory factory = new EventHandlerFactory("testing");

            object handler = factory.GetEventHandler(eventInfo);

            // Create a delegate, which points to the custom event handler
            Delegate customEventDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, handler, "CustomEventHandler");
            // Link event handler to event
            eventInfo.AddEventHandler(target, customEventDelegate);

            // Map our own event handler to the common event
            EventInfo commonEventInfo = handler.GetType().GetEvent("CommonEvent");
            Delegate commonDelegate = Delegate.CreateDelegate(commonEventInfo.EventHandlerType, this, "MyEventCallback");
            commonEventInfo.AddEventHandler(handler, commonDelegate);

            return null;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Verify();
        }

        #endregion
    }
}

namespace TeamAgile.NUnitExtensions.EventsTesting
{

    #region  namespace CommonEventHandler
    namespace CommonEventHandler
    {
        //This class was adapted based on code from the following URL:
        // http://codeproject.com/csharp/ComonEventHandler.asp
        // "A Universal Event Handler Factory" by Ulrich Proeller


        using System;
        using System.Collections;
        using System.Reflection;
        using System.Reflection.Emit;
        using System.Threading;

        /// <summary>
        /// This delegate describes the signature of the event, which is emited by the generated helper classes.
        /// </summary>
        public delegate void CommonEventHandlerDlgt(Type EventType, object[] args);

        /// <summary>
        /// Summary description for EventHandlerFactory.
        /// </summary>
        public class EventHandlerFactory
        {

            private EventHandlerTypeEmitter emitter;
            private string helperName;

            /// <summary>
            /// Creates the EventHandlerFactory.
            /// </summary>
            /// <param Name="Name">Name of the Factory. Is used as naming component of the event handlers to create.</param>
            public EventHandlerFactory(string Name)
            {
                helperName = Name;
                emitter = new EventHandlerTypeEmitter(Name);
            }

            /// <summary>
            /// Creates an event handler for the specified event
            /// </summary>
            /// <param Name="Info">The event info class of the event</param>
            /// <returns>The created event handler help class.</returns>
            public object GetEventHandler(EventInfo Info)
            {
                Type eventHandlerType = emitter.GetEventHandlerType(Info);

                // Call constructor of event handler type to create event handler
                ConstructorInfo myCtor = eventHandlerType.GetConstructor(new Type[] { typeof(EventInfo) });
                object[] ctorArgs = new object[] { Info };
                object eventHandler = myCtor.Invoke(ctorArgs);

                return eventHandler;
            }




            #region class EventHandlerTypeEmitter


            public class EventHandlerTypeEmitter
            {
                private static Hashtable handlerTypes = new Hashtable();
                string assemblyName;
                AssemblyBuilder asmBuilder = null;
                ModuleBuilder helperModule = null;

                /// <summary>
                /// Constructor
                /// </summary>
                /// <param Name="Name">The Name, which the is given to assembly, module and class.</param>
                public EventHandlerTypeEmitter(string Name)
                {
                    assemblyName = Name + Guid.NewGuid().ToString();
                }

                /// <summary>
                /// Emits dynamically a event handler with a given signature, which fills all arguments of the event in an object array 
                /// and calls a common event.
                /// </summary>
                /// <returns></returns>
                public Type GetEventHandlerType(EventInfo Info)
                {
                    string handlerName = assemblyName + Info.Name;
                    Type tpEventHandler = (Type)handlerTypes[handlerName];
                    if (tpEventHandler == null)
                    {
                        // Create the type
                        tpEventHandler = EmitHelperClass(handlerName, Info);
                        handlerTypes.Add(handlerName, tpEventHandler);
                    }
                    return tpEventHandler;
                }




                private Type EmitHelperClass(string HandlerName, EventInfo Info)
                {
                    if (helperModule == null)
                    {
                        AssemblyName myAsmName = new AssemblyName();
                        myAsmName.Name = assemblyName + "Helper";
                        AppDomain domain = Thread.GetDomain();
                        //AppDomain domain = AppDomain.CreateDomain("testingHelper");
                        asmBuilder = domain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run);
                        helperModule = asmBuilder.DefineDynamicModule(assemblyName + "Module", true);
                    }

                    //////////////////////////////////////////////////////////////////////////
                    // Define Type
                    //////////////////////////////////////////////////////////////////////////
                    TypeBuilder helperTypeBld = helperModule.DefineType(HandlerName + "Helper", TypeAttributes.Public);

                    // Define fields
                    FieldBuilder typeField = helperTypeBld.DefineField("eventType", typeof(Type), FieldAttributes.Private);
                    FieldBuilder eventField = helperTypeBld.DefineField("CommonEvent", typeof(CommonEventHandlerDlgt), FieldAttributes.Private);
                    EventBuilder commonEvent = helperTypeBld.DefineEvent("CommonEvent", EventAttributes.None, typeof(CommonEventHandlerDlgt));

                    //////////////////////////////////////////////////////////////////////////
                    // Build Constructor
                    //////////////////////////////////////////////////////////////////////////
                    Type objType = Type.GetType("System.Object");
                    ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);

                    // Build constructor with arguments (Type)
                    Type[] ctorParams = new Type[] { typeof(EventInfo) };
                    ConstructorBuilder ctor = helperTypeBld.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, ctorParams);

                    // Call constructor of base class
                    ILGenerator ctorIL = ctor.GetILGenerator();
                    ctorIL.Emit(OpCodes.Ldarg_0);
                    ctorIL.Emit(OpCodes.Call, objCtor);

                    // store first argument to typeField
                    ctorIL.Emit(OpCodes.Ldarg_0);
                    ctorIL.Emit(OpCodes.Ldarg_1);
                    ctorIL.Emit(OpCodes.Stfld, typeField);

                    // return
                    ctorIL.Emit(OpCodes.Ret);
                    // Now constructor is finished!

                    //////////////////////////////////////////////////////////////////////////
                    // Build customized event handler
                    //////////////////////////////////////////////////////////////////////////
                    Type ehType = Info.EventHandlerType;
                    MethodInfo mi = ehType.GetMethod("Invoke");
                    ParameterInfo[] eventParams = mi.GetParameters();

                    // Build the type list of the parameter
                    int paramCount = eventParams.Length;
                    Type[] invokeParams = new Type[paramCount];
                    for (int i = 0; i < paramCount; i++)
                    {
                        invokeParams[i] = eventParams[i].ParameterType;
                    }

                    string methodName = "CustomEventHandler";
                    MethodAttributes attr = MethodAttributes.Public | MethodAttributes.HideBySig;
                    MethodBuilder invokeMthd = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);

                    // A ILGenerator can now be spawned, attached to the MethodBuilder.
                    ILGenerator ilGen = invokeMthd.GetILGenerator();

                    // Create Label before return
                    Label endOfMethod = ilGen.DefineLabel();

                    // Create a local variable of type ‘string’, and call it ‘args’ 
                    LocalBuilder localObj = ilGen.DeclareLocal(typeof(object[]));
                    localObj.SetLocalSymInfo("args"); // Provide Name for the debugger. 

                    // create object array of proper length
                    ilGen.Emit(OpCodes.Ldc_I4, paramCount);
                    ilGen.Emit(OpCodes.Newarr, typeof(object));
                    ilGen.Emit(OpCodes.Stloc_0);

                    // Now put all arguments in the object array
                    for (int i = 0; i < paramCount; i++)
                    {
                        byte i1b = Convert.ToByte(i + 1);
                        ilGen.Emit(OpCodes.Ldloc_0);
                        ilGen.Emit(OpCodes.Ldc_I4, i);
                        ilGen.Emit(OpCodes.Ldarg_S, i1b);

                        // Is argument value type?
                        if (invokeParams[i].IsValueType)
                        {
                            // Box the value type
                            ilGen.Emit(OpCodes.Box, invokeParams[i]);
                        }
                        // Put the argument in the object array
                        ilGen.Emit(OpCodes.Stelem_Ref);
                    }

                    // raise common event
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldfld, eventField);
                    ilGen.Emit(OpCodes.Brfalse_S, endOfMethod);
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldfld, eventField);
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldfld, typeField);
                    ilGen.Emit(OpCodes.Ldloc_0);
                    MethodInfo raiseEventMethod = typeof(CommonEventHandlerDlgt).GetMethod("Invoke", BindingFlags.Public | BindingFlags.Instance);
                    if (raiseEventMethod == null) throw new ApplicationException("CommonEventHandlerDlg:Invoke not found");
                    ilGen.Emit(OpCodes.Callvirt, raiseEventMethod);

                    // return 
                    ilGen.MarkLabel(endOfMethod);
                    ilGen.Emit(OpCodes.Ret);

                    //////////////////////////////////////////////////////////////////////////
                    // add_CommonEvent
                    //////////////////////////////////////////////////////////////////////////
                    methodName = "add_CommonEvent";
                    attr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                    invokeParams = new Type[1];
                    invokeParams[0] = typeof(CommonEventHandlerDlgt);
                    MethodBuilder addMethod = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);
                    ilGen = addMethod.GetILGenerator();

                    // define code
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldfld, eventField);
                    ilGen.Emit(OpCodes.Ldarg_1);

                    Type[] combineTypes = new Type[] { typeof(Delegate), typeof(Delegate) };
                    MethodInfo combineMtd = typeof(Delegate).GetMethod("Combine", combineTypes);
                    if (combineMtd == null) throw new ApplicationException("Delegate:Combine not found");
                    ilGen.Emit(OpCodes.Call, combineMtd);
                    ilGen.Emit(OpCodes.Castclass, typeof(CommonEventHandlerDlgt));
                    ilGen.Emit(OpCodes.Stfld, eventField);
                    ilGen.Emit(OpCodes.Ret);

                    // Set add method
                    commonEvent.SetAddOnMethod(addMethod);

                    //////////////////////////////////////////////////////////////////////////
                    // remove_CommonEvent
                    //////////////////////////////////////////////////////////////////////////
                    methodName = "remove_CommonEvent";
                    attr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;
                    invokeParams = new Type[1];
                    invokeParams[0] = typeof(CommonEventHandlerDlgt);
                    MethodBuilder removeMethod = helperTypeBld.DefineMethod(methodName, attr, typeof(void), invokeParams);
                    ilGen = removeMethod.GetILGenerator();

                    // define code
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldarg_0);
                    ilGen.Emit(OpCodes.Ldfld, eventField);
                    ilGen.Emit(OpCodes.Ldarg_1);

                    MethodInfo removeMtd = typeof(Delegate).GetMethod("Remove", combineTypes);
                    if (removeMtd == null) throw new ApplicationException("Delegate:Remove not found");
                    ilGen.Emit(OpCodes.Call, removeMtd);
                    ilGen.Emit(OpCodes.Castclass, typeof(CommonEventHandlerDlgt));
                    ilGen.Emit(OpCodes.Stfld, eventField);
                    ilGen.Emit(OpCodes.Ret);

                    // Set add method
                    commonEvent.SetAddOnMethod(addMethod);

                    //////////////////////////////////////////////////////////////////////////
                    // Now event handler is finished!
                    //////////////////////////////////////////////////////////////////////////
                    return helperTypeBld.CreateType();
                }
            }

            #endregion

        }

    }

    #endregion
}