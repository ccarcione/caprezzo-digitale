namespace CaprezzoDigitale.WebApi.ExtensionMethods
{
    public static class IWebHostEnvironmentExtension
    {
        public static bool IsDockerLocal(this IWebHostEnvironment env)
            => env.EnvironmentName.Equals("docker-local", StringComparison.InvariantCultureIgnoreCase);
    }
}
