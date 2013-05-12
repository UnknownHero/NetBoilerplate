namespace Infrastructure.Bootstrapper
{
    public interface IBootstrapper
    {
        /// <summary>
        /// Set to Infrastructure.Root.ApplicationContainer  IOC-container
        /// </summary>
        void Run();
    }
}