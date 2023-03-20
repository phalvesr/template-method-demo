namespace TemplateMethodDemo.TemplateMethod.Extensions;

public static class TaskExtensions
{
    public static async Task<Result> WhenAllHandlingException(params Task[] tasks)
    {
        var taskAggregate = Task.WhenAll(tasks);

        try
        {
            await taskAggregate;
            
            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure();
        }
    }
}