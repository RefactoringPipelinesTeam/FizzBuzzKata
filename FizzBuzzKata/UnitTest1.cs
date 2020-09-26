using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using Refactoring.Pipelines;
using Refactoring.Pipelines.ApprovalTests;
using Refactoring.Pipelines.InputsAndOutputs;

namespace FizzBuzzKata
{
    [TestClass]
    [UseReporter(typeof(BeyondCompareReporter))]
    public class UnitTest1
    {
        //    [TestMethod]
        //    public void FizzBuzzTest()
        //    {
        //        var result = FizzBuzz.Convert(1);
        //        Assert.AreEqual(result, "1");

        //    }

        //------------------------------- Step 1, create this template
        [TestMethod]
        public void TestPipeline()
        {
            PipelineApprovals.Verify(CreatePipeline());

        }

        IGraphNode CreatePipeline()
        {
            return new InputPipe<object>("input");
        }

        [TestMethod]
        public void FizzBuzzPipeline()
        {
            var fizzBuzzPipeline = CreateFizzBuzzPipeline();
            PipelineApprovals.Verify(fizzBuzzPipeline);
        }

        [TestMethod]
        public void TestSystem()
        {
            Approvals.VerifyAll(Enumerable.Range(1, 20), i => $"{i} => {Convert(i)}");
        }

        string Convert(int i)
        {
            var fizzBuzzPipeline = CreateFizzBuzzPipeline();
            var inputsAndOutputs = fizzBuzzPipeline.GetInputs<int>().AndOutputs<string>();
            inputsAndOutputs.Send(i);
            return inputsAndOutputs.Output1.SingleResult;

        }
        private IGraphNode CreateFizzBuzzPipeline()
        {
            var inputPipe = new InputPipe<int>("number");
            var fizzPipe = inputPipe.ProcessFunction(FizzConverter);
            var buzzPipe = inputPipe.ProcessFunction(BuzzConverter);
            var joinedPipe = fizzPipe.JoinTo(buzzPipe).Process((a, b) => a + b);
            var joinedPipe2 = joinedPipe.JoinTo(inputPipe).ProcessFunction(DefaultPipe);
            joinedPipe2.Collect();
            return inputPipe;
        }

        private string FizzConverter(int arg)
        {
            return arg % 3 == 0 ? "Fizz" : "";
        }
        private string BuzzConverter(int arg)
        {
            return arg % 5 == 0 ? "Buzz" : "";
        }

        string DefaultPipe(Tuple<string, int> stringAndNumber)
        {
            return stringAndNumber.Item1 == "" ? stringAndNumber.Item2.ToString() : stringAndNumber.Item1;
        }
    }
}