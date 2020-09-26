using Refactoring.Pipelines;
using Refactoring.Pipelines.ApprovalTests;

static class FizzBuzz
{
    public static string Convert(int i)
    {
        // Set up Pipeline
        var inputpipe = new InputPipe<int>("number");
        var pipe = inputpipe.Process(_ => _.ToString());
        var methodCallCollector = pipe.Collect();
        inputpipe.Send(i);
        var variable = methodCallCollector.SingleResult;
        // ApprovalPipeline
        PipelineApprovals.Verify(inputpipe);
        // Send thru pipeline
        // Original code
        return i.ToString();
    }
}