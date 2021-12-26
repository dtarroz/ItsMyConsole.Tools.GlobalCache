using System;
using System.Collections.Generic;

namespace ItsMyConsole.Tools.GlobalCache
{
    /// <summary>
    /// Outils pour le cache global
    /// </summary>
    public class GlobalCacheTools
    {
        private readonly Dictionary<string, object> _globalCache = new Dictionary<string, object>();

        internal GlobalCacheTools() { }

        /// <summary>
        /// Ajouter ou modifier une donnée dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        /// <param name="value">La valeur à ajouter ou mettre à jour dans le cache associée à la clé</param>
        public void Set(string key, object value) {
            ThrowIfKeyIsNotValid(key, nameof(key));
            _globalCache[key] = value;
        }

        private static void ThrowIfKeyIsNotValid(string key, string name) {
            switch (key) {
                case null: throw new ArgumentNullException(name);
                case "":   throw new ArgumentException("value not empty", name);
            }
        }

        /// <summary>
        /// Récupérer la valeur dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        public T Get<T>(string key) {
            ThrowIfKeyIsNotValid(key, nameof(key));
            return _globalCache.ContainsKey(key) ? (T)_globalCache[key] : default;
        }

        /// <summary>
        /// Récupérer la valeur dans le cache associée à la clé si elle existe.
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        /// <param name="value">La valeur en cache si la clé associée existe</param>
        /// <returns>true si la clé existe</returns>
        public bool TryGetValue<T>(string key, out T value) {
            ThrowIfKeyIsNotValid(key, nameof(key));
            bool isExists = _globalCache.ContainsKey(key);
            value = isExists ? (T)_globalCache[key] : default;
            return isExists;
        }

        /// <summary>
        /// Supprimer la valeur dans le cache associée à la clé
        /// </summary>
        /// <param name="key">La clé unique qui représente la donnée dans le cache</param>
        public void Remove(string key) {
            ThrowIfKeyIsNotValid(key, nameof(key));
            _globalCache.Remove(key);
        }
    }
}
