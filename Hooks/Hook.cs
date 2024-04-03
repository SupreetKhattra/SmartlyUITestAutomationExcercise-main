namespace specflowTesting1.Hooks
{
    [Binding]
    public class Hooks
    {
    BaseClass baseClass = new BaseClass();

    public string firstname = Environment.GetEnvironmentVariable("FIRSTNAME") ?? "Aaa111";
    private string lastname = Environment.GetEnvironmentVariable("LASTNAME") ?? "Aaa111";
    private string username = Environment.GetEnvironmentVariable("USERNAME") ?? "johndoe567_";
    private string password = Environment.GetEnvironmentVariable("PASSWORD") ?? "password111";

    private static bool isInitialized;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            if (!isInitialized)
            {
                isInitialized = true;
            }
        }

    [BeforeScenario]
    public void Setup()
    {
        baseClass.InitializeDrivers();

        //Console.WriteLine($"Variables used: {firstname}, {lastname}, {username}, {password}");

    }
    [AfterScenario]
     public void TearDown()
     {
          baseClass.Logout();

     }

     [AfterTestRun]
     public static void AfterTestRun()
     {
          //shutdown webdriver
          BaseClass.CloseDrivers();

     }
    }
}
