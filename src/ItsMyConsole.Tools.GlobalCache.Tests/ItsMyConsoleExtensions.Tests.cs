using System;
using System.Reflection;
using Xunit;

namespace ItsMyConsole.Tools.GlobalCache.Tests;

public class ItsMyConsoleExtensions_Tests
{
    [Fact]
    public void GlobalCache_Persistent() {
        CommandTools commandTools = CreateNewCommandTools();
        commandTools.GlobalCache().Set("PERSISTENT", "VALUE");

        commandTools = CreateNewCommandTools();

        string value = commandTools.GlobalCache().Get<string>("PERSISTENT");
        Assert.Equal("VALUE", value);
    }

    private static CommandTools CreateNewCommandTools() {
        const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
        ConstructorInfo constructorInfo = typeof(CommandTools).GetConstructor(bindingFlags, Type.EmptyTypes);
        return (CommandTools)constructorInfo?.Invoke(null);
    }
}
