//Task1();

// Task2();

//Task3();

//Task4();

//await Task5();

//await Task6();

await Task7();

// Task执行并行任务示例1

void Task1()
{
// 创建任务数组
    var tasks = new Task[10];

    for (var i = 0; i < tasks.Length; i++)
    {
        var taskId = i + 1;

        // 使用Task.Run方法提交任务
        tasks[i] = Task.Run(() =>
        {
            Console.WriteLine("任务 {0} 运行在线程 {1} 中", taskId, Task.CurrentId);
            // 执行任务逻辑
        });
    }

// 等待所有任务完成
    Task.WaitAll(tasks);

    Console.WriteLine("所有任务运行完成。");
    Console.ReadKey();
}

// Task执行并行任务示例2
void Task2()
{
    static long Fib(int n)
    {
        if (n is 0 or 1)
        {
            return n;
        }
        else
        {
            return Fib(n - 1) + Fib(n - 2);
        }
    }

    const int n = 10; // 计算斐波那契数列的前n项

    var tasks = new Task<long>[n];

    for (var i = 0; i < n; i++)
    {
        var index = i; // 需要在闭包内使用循环变量时需要赋值给另外一个变量

        if (i < 2)
        {
            tasks[i] = Task.FromResult((long)i);
        }
        else
        {
            tasks[i] = Task.Run(() => Fib(index));
        }
    }

    // 等待所有任务完成
    Task.WaitAll(tasks);

    // 打印结果
    for (var i = 0; i < n; i++)
    {
        Console.Write("{0} ", tasks[i].Result);
    }

    Console.ReadKey();
}

// Task执行并行任务示例3
void Task3()
{
    long Factorial(int n)
    {
        if (n == 0) return 1;
        return n * Factorial(n - 1);
    }

    const int n = 5; // 计算阶乘的数

    var task = Task.Factory.StartNew(() => Factorial(n));

    Console.WriteLine("计算阶乘...");

    // 等待任务完成
    task.Wait();

    Console.WriteLine("{0}! = {1}", n, task.Result);
    Console.ReadKey();
}

// Task执行并行任务示例3
void Task4()
{
    const string filePath = "test.txt";

    var task = File.ReadAllTextAsync(filePath); // Task.FromResult(File.ReadAllText(filePath))

    Console.WriteLine("读取文件内容...");

    // 等待任务完成
    task.Wait();

    Console.WriteLine("文件内容: {0}", task.Result);
    Console.ReadKey();
}

async Task Task5()
{
    async Task<string> LongOperationAsync()
    {
        // 模拟耗时操作
        await Task.Delay(TimeSpan.FromSeconds(3));

        return "完成";
    }

    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}开始耗时操作...");

    // 等待异步方法完成
    var result = await LongOperationAsync();

    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}耗时操作完成: {result}");
    Console.ReadKey();
}

async Task Task6()
{
    async Task<string> LongOperationAsync(int id)
    {
        // 模拟耗时操作
        await Task.Delay(TimeSpan.FromSeconds(1 + id));

        return $"{DateTime.Now:ss.fff}完成 {id}";
    }

    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}开始耗时操作...");

    // 等待多个异步任务完成
    var task1 = LongOperationAsync(1);
    var task2 = LongOperationAsync(2);
    var task3 = LongOperationAsync(3);

    var results = await Task.WhenAll(task1, task2, task3);
    var resultStr = string.Join(",", results);

    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}耗时操作完成: {resultStr}");
    Console.ReadKey();
}

async Task Task7()
{
    async Task<string> MethodAAsync()
    {
        await Task.Delay(1000);
        return $"{DateTime.Now:ss.fff}>Hello";
    }

    async Task<string> MethodBAsync()
    {
        await Task.Delay(2000);
        return $"{DateTime.Now:ss.fff}>World";
    }

    async Task<string> CombineResultsAAsync()
    {
        var resultA = await MethodAAsync();
        var resultB = await MethodBAsync();
        return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {resultA} | {resultB}";
    }

    async Task<string> CombineResultsBAsync()
    {
        var resultA = await MethodAAsync().ConfigureAwait(false);
        var resultB = await MethodBAsync().ConfigureAwait(false);
        return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {resultA} | {resultB}";
    }

    Console.WriteLine(await CombineResultsAAsync());
    Console.WriteLine("===========华丽的分割符==============");
    Console.WriteLine(await CombineResultsBAsync());

    Console.ReadKey();
}