using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using DotNetCoreKoans.Engine;


namespace DotNetCoreKoans.Koans
{
    public class AboutArrays : Koan
    {
        [Step(1)]
        public void CreatingArrays()
        {
            var empty_array = new object[] { };
            //empty_array type is Object[] ... will it work with lower-cased object
            Assert.Equal(typeof(object[]), empty_array.GetType());

            //Note that you have to explicitly check for subclasses
            Assert.True(typeof(Array).IsAssignableFrom(empty_array.GetType()));

            Assert.Equal(0, empty_array.Length);
        }

        // [Step(2)]
        // public void ArrayLiterals()
        // {
        //     //You don't have to specify a type if the arguments can be inferred
        //     var array = new [] { 42 };
        //     Assert.Equal(typeof(int[]), array.GetType());
        //     Assert.Equal(new int[] { 42 }, array);

        //     //Are arrays 0-based or 1-based?
        //     Assert.Equal(42, array[0]);

        //     //This is important because...
        //     //TODO:Add back once this bug is fixed: https://github.com/dotnet/corefx/issues/9998
        //     //Assert.True(array.IsFixedSize);

        //     //...it means we can't do this: array[1] = 13;
        //     Assert.Throws(typeof(FillMeIn), delegate() { array[1] = 13; });

        //     //This is because the array is fixed at length 1. You could write a function
        //     //which created a new array bigger than the last, copied the elements over, and
        //     //returned the new array. Or you could do this:
        //     List<int> dynamicArray = new List<int>();
        //     dynamicArray.Add(42);
        //     Assert.Equal(array, dynamicArray.ToArray());

        //     dynamicArray.Add(13);
        //     Assert.Equal((new int[] { 42, FILL_ME_IN}), dynamicArray.ToArray());
        // }

        [Step(3)]
        public void AccessingArrayElements()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

            Assert.Equal("peanut", array[0]);
            Assert.Equal("jelly", array[3]);
            
            //This doesn't work: Assert.Equal(FILL_ME_IN, array[-1]);
        }

        [Step(4)]

        //METHODS FOR SLICING ARRAYS
        public void SlicingArrays()
        {
            var array = new[] { "peanut", "butter", "and", "jelly" };

			Assert.Equal(new string[] {"peanut", "butter"}, array.Take(2).ToArray());
			Assert.Equal(new string[] {"butter", "and"}, array.Skip(1).Take(2).ToArray());
        }

        [Step(5)]
        public void PushingAndPopping()
        {
            var array = new [] { 1, 2};
            var stack = new Stack(array);
            //last is pushed to the top of the stack, which is after 2
            //first one to go out is one just pushed in
            // Console.WriteLine(stack.Peek());
            stack.Push("last");
            //at this point, the array below looks like: "last"[0], 2[1], 1[2]
            // Console.WriteLine(stack.ToArray()[0]);
            // Console.WriteLine(stack.ToArray()[1]);
            // Console.WriteLine(stack.ToArray()[2]);
            //To Array creates a copy in reverse order, but we're only using this in the assert, not in the actual stack!!!
            Assert.Equal(new object []{"last", 2, 1}, stack.ToArray());
            var poppedValue = stack.Pop();
            // Console.WriteLine(poppedValue);
            Assert.Equal("last", poppedValue);
            Assert.Equal(new object [] {2, 1}, stack.ToArray());
        }

        [Step(6)]
        public void Shifting()
        {
            //Shift == Remove First Element
            //Unshift == Insert Element at Beginning
            //C# doesn't provide this natively. You have a couple
            //of options, but we'll use the LinkedList<T> to implement
            var array = new[] { "Hello", "World" };
            var list = new LinkedList<string>(array);

            list.AddFirst("Say");
            //expect this to be "Say", so the list itself 
            Console.WriteLine($"{0}", list.ToArray()[0]);
            Assert.Equal(new string []{"Say", "Hello", "World"}, list.ToArray());

            list.RemoveLast();
           
            Console.WriteLine(list.ToArray()[0]);
            
            Assert.Equal(new string[] {"Say", "Hello"}, list.ToArray());

            list.RemoveFirst();
           
            Console.WriteLine(list.ToArray()[0]);
            
            Assert.Equal(new string[] {"Hello"}, list.ToArray());
           
            Console.WriteLine(list.ToArray()[0]);

            list.AddAfter(list.Find("Hello"), "World");
            Assert.Equal(new string[] {"Hello", "World"}, list.ToArray());
        }
    }
}