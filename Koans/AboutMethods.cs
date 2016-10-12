using Xunit;
using DotNetCoreKoans.Engine;
using System.Reflection;
using System;

namespace DotNetCoreKoans.Koans
{
    public static class ExtensionMethods
    {

        //A CLASS DEFINES A TYPE
        //this Koan is referring to the class that's being extended, koan is referring to the instance of the koan class upon which the extension method is being called. Using an extension method makes a lot more sense when you're doing so on a class that can't otherwise be access (e.g., string) ... otherwise, why not just put the method on Koan class???
        public static string HelloWorld(this Koan koan)
        {
            return "Hello!";
        }

        public static string SayHello(this Koan koan, string name)
        {
            return String.Format("Hello, {0}!", name);
        }

        //WHAT THE HECK IS GOING ON HERE W/ PARAMS STRING[]names? IS THIS TELLING THE COMPILER TO BREAK UP EACH STRING IN THE names[] AND USE IT AS AN INDIVIDUAL PARAMETER????

        //OK, IT IS TURNING NAMES INTO AN ARRAY OF STRINGS AND THEN RETURNING THAT ARRAY. INTERESTING!!!

        public static string[] MethodWithVariableArguments(this Koan koan, params string[] names)
        {
            return names;
        }

//EXTENSION METHODS exist on a type, which are also classes. So if you can't get to the class itself, e.g., string, you put an exntension method on that class/type (without creating an instance). below, the this refers to the method in which the extension method is being called. there is a 'this' on that as well.
        public static string SayHi(this String str)
        {
            return "Hi, " + str;
        }
    }

    public class AboutMethods : Koan
    {
        //Extension Methods allow us to "add" methods to any class
        //without having to recompile. You only have to reference the
        //namespace the methods are in to use them. Here, since both the
        //ExtensionMethods class and the AboutMethods class are in the
        //DotNetKoans.CSharp namespace, AboutMethods can automatically
        //find them

        [Step(1)]
        public void ExtensionMethodsShowUpInTheCurrentClass()
        {
            //here this. refers to the class
            Assert.Equal("Hello!", this.HelloWorld());
        }

        [Step(2)]
        public void ExtensionMethodsWithParameters()
        {
            Assert.Equal("Hello, Cory!", this.SayHello("Cory"));
        }

        [Step(3)]
        public void ExtensionMethodsWithVariableParameters()
        {
            Assert.Equal(new string[] {"Cory", "Will", "Corey"}, this.MethodWithVariableArguments("Cory", "Will", "Corey"));
        }

        //Extension methods can extend any class my referencing 
        //the name of the class they are extending. For example, 
        //we can "extend" the string class like so:

        [Step(4)]
        public void ExtendingCoreClasses()
        {
            Assert.Equal("Hi, Cory", "Cory".SayHi());
        }

        //Of course, any of the parameter things you can do with 
        //extension methods you can also do with local methods

        private string[] LocalMethodWithVariableParameters(params string[] names)
        {
            return names;
        }

        [Step(5)]
        public void LocalMethodsWithVariableParams()
        {
            Assert.Equal(new string[] {"Cory", "Will", "Corey"}, this.LocalMethodWithVariableParameters("Cory", "Will", "Corey"));
        }

        //Note how we called the method by saying "this.LocalMethodWithVariableParameters"
        //That isn't necessary for local methods

        [Step(6)]
        public void LocalMethodsWithoutExplicitReceiver()
        {
            Assert.Equal(new string[] {"Cory","Will","Corey"}, LocalMethodWithVariableParameters("Cory", "Will", "Corey"));
        }

        //this.whatever() INSTANTIATES/GIVES AN INSTANCE

        //But it is required for Extension Methods, since it needs
        //an instance variable. So this wouldn't work, giving a
        //compile-time error:
        //Assert.Equal(FILL_ME_IN, MethodWithVariableArguments("Cory", "Will", "Corey"));

        class InnerSecret
        {
            public static string Key() { return "Key"; }
            public string Secret() { return "Secret"; }
            protected string SuperSecret() { return "This is secret"; }
            private string SooperSeekrit() { return "No one will find me!"; }
        }

        class StateSecret : InnerSecret
        {
            public string InformationLeak() { return SuperSecret(); }
        }

        //Static methods don't require an instance of the object
        //in order to be called. 
        [Step(7)]
        public void CallingStaticMethodsWithoutAnInstance()
        {
            Assert.Equal("Key", InnerSecret.Key());
        }

        //In fact, you can't call it on an instance variable
        //of the object. So this wouldn't compile:
        //InnerSecret secret = new InnerSecret();
        //Assert.Equal(FILL_ME_IN, secret.Key());

        
        [Step(8)]
        public void CallingPublicMethodsOnAnInstance()
        {
            InnerSecret secret = new InnerSecret();
            Assert.Equal("Secret", secret.Secret());
        }

        //Protected methods can only be called by a subclass
        //We're going to call the public method called
        //InformationLeak of the StateSecret class which returns
        //the value from the protected method SuperSecret
        [Step(9)]
        public void CallingProtectedMethodsOnAnInstance()
        {
            StateSecret secret = new StateSecret();
            Assert.Equal("This is secret", secret.InformationLeak());
        }

        //But, we can't call the private methods of InnerSecret
        //either through an instance, or through a subclass. It
        //just isn't available.

        //Ok, well, that isn't entirely true. Reflection can get
        //you just about anything, and though it's way out of scope
        //for this...
        [Step(10)]
        public void SubvertPrivateMethods()
        {
            InnerSecret secret = new InnerSecret();
            string superSecretMessage = secret.GetType()
                .GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(secret, null) as string;
            Assert.Equal("No one will find me!", superSecretMessage);
        }

        //Up till now we've had explicit return types. It's also
        //possible to create methods which dynamically shift
        //the type based on the input. These are referred to
        //as generics

        public static T GiveMeBack<T>(T p1)
        {
            return p1;
        }

        [Step(11)]
        public void CallingGenericMethods()
        {
            Assert.Equal(typeof(int), GiveMeBack<int>(1).GetType());

            Assert.Equal("Hi!", GiveMeBack<string>("Hi!"));
        }
    }
}