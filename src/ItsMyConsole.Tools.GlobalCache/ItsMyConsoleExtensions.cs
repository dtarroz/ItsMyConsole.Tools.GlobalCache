namespace ItsMyConsole.Tools.GlobalCache
{
    /// <summary>
    /// Extension de ItsMyConsole pour inclure les outils de cache global
    /// </summary>
    public static class ItsMyConsoleExtensions
    {
        private static readonly GlobalCacheTools GlobalCacheTools = new GlobalCacheTools();

        /// <summary>
        /// L'accès au cache global
        /// </summary>
        /// <param name="commandTools">Les outils de commandes pour accéder au cache global</param>
        public static GlobalCacheTools GlobalCache(this CommandTools commandTools) {
            return GlobalCacheTools;
        }
    }
}
