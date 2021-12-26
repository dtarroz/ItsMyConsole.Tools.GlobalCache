using System;
using Xunit;

namespace ItsMyConsole.Tools.GlobalCache.Tests
{
    public class GlobalCacheTools_Tests
    {
        [Fact]
        public void Set_ArgumentNullException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentNullException>(() => globalCacheTools.Set(null, ""));
        }

        [Fact]
        public void Set_ArgumentException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentException>(() => globalCacheTools.Set("", ""));
        }

        [Fact]
        public void Set_OneKey() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY", "VALUE");

            SetValue(ref globalCacheTools, "KEY", "NEW VALUE");
        }

        private static void InitValue<T>(ref GlobalCacheTools globalCacheTools, string key, T value) {
            CheckNoValue<T>(ref globalCacheTools, key);
            SetValue(ref globalCacheTools, key, value);
        }

        private static void CheckNoValue<T>(ref GlobalCacheTools globalCacheTools, string key) {
            T readGet = globalCacheTools.Get<T>(key);
            Assert.Equal(default(T), readGet);

            bool isExist = globalCacheTools.TryGetValue(key, out T readTryGet);
            Assert.False(isExist);
            Assert.Equal(default(T), readTryGet);
        }

        private static void SetValue<T>(ref GlobalCacheTools globalCacheTools, string key, T value) {
            globalCacheTools.Set(key, value);
            CheckValue(ref globalCacheTools, key, value);
        }

        private static void CheckValue<T>(ref GlobalCacheTools globalCacheTools, string key, T value) {
            T readGet = globalCacheTools.Get<T>(key);
            Assert.Equal(value, readGet);

            bool isExist = globalCacheTools.TryGetValue(key, out T readTryGet);
            Assert.True(isExist);
            Assert.Equal(value, readTryGet);
        }

        [Fact]
        public void Set_TwoKey() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY 1", "VALUE 1");
            InitValue(ref globalCacheTools, "KEY 2", 123);

            SetValue(ref globalCacheTools, "KEY 1", "NEW VALUE 1");
            SetValue(ref globalCacheTools, "KEY 2", 456);
        }

        [Fact]
        public void Set_Null() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue<object>(ref globalCacheTools, "KEY NULL", null);
        }

        [Fact]
        public void Set_ChangeType() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY TYPE", "STRING VALUE");

            SetValue(ref globalCacheTools, "KEY TYPE", 789);
        }

        [Fact]
        public void Set_Cast() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY CAST", "STRING VALUE");

            Assert.Throws<InvalidCastException>(() => globalCacheTools.Get<int>("KEY CAST"));
            Assert.Throws<InvalidCastException>(() => globalCacheTools.TryGetValue("KEY CAST", out int _));
        }

        [Fact]
        public void Get_ArgumentNullException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentNullException>(() => globalCacheTools.Get<object>(null));
        }

        [Fact]
        public void Get_ArgumentException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentException>(() => globalCacheTools.Get<object>(""));
        }

        [Fact]
        public void Remove_ArgumentNullException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentNullException>(() => globalCacheTools.Remove(null));
        }

        [Fact]
        public void Remove_ArgumentException() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            Assert.Throws<ArgumentException>(() => globalCacheTools.Remove(""));
        }

        [Fact]
        public void Remove_NotExists() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            globalCacheTools.Remove("NotExists");
        }

        [Fact]
        public void Remove_OneKey() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY REMOVE", "DELETE");

            globalCacheTools.Remove("KEY REMOVE");

            CheckNoValue<string>(ref globalCacheTools, "KEY REMOVE");
        }

        [Fact]
        public void Remove_TwoKey() {
            GlobalCacheTools globalCacheTools = new GlobalCacheTools();

            InitValue(ref globalCacheTools, "KEY TO REMOVE", "REMOVE");
            InitValue(ref globalCacheTools, "KEY TO KEEP", "KEEP");

            globalCacheTools.Remove("KEY TO REMOVE");

            CheckNoValue<string>(ref globalCacheTools, "KEY TO REMOVE");
            CheckValue(ref globalCacheTools, "KEY TO KEEP", "KEEP");
        }
    }
}
