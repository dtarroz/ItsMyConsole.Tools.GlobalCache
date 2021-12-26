namespace ItsMyConsole.Tools.GlobalCache
{
    /// <summary>
    /// Outils pour le cache global
    /// </summary>
    public class GlobalCacheTools
    {
        internal GlobalCacheTools() { }

        /// <summary>
        /// Ajouter ou modifier une donnée dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        /// <param name="value">La valeur à ajouter ou mettre à jour dans le cache associée à la clé</param>
        public void Set(string key, object value) { }

        /// <summary>
        /// Récupérer la valeur dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        public T Get<T>(string key) {
            return default;
        }

        /// <summary>
        /// Récupérer la valeur dans le cache associée à la clé si elle existe.
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        /// <param name="value">La valeur en cache si la clé associée existe</param>
        /// <returns>true si la clé existe</returns>
        public bool TryGetValue<T>(string key, out T value) {
            value = default;
            return false;
        }

        /// <summary>
        /// Supprimer la valeur dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        public void Remove(string key) { }
    }
}
