![Logo](docs/logo.png)

# ItsMyConsole.Tools.GlobalCache
Outil de cache global depuis toutes les interprétations de ligne de commande pour le Framework [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole)

## Sommaire
- [Pourquoi faire ?](#pourquoi-faire-)
- [Getting Started](#getting-started)
- [Comment se servir de l'outil ?](#comment-se-servir-de-loutil-)
- [Ajout ou modification d'une donnée dans le cache](#ajout-ou-modification-dune-donnée-dans-le-cache)
- [Récupération des données du cache](#récupération-des-données-du-cache)
- [Suppression des données du cache](#suppression-des-données-du-cache)

## Pourquoi faire ?
Vous allez pouvoir étendre le Framework pour application Console .Net [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) avec un outil pour mettre en cache des données accessibles depuis toutes les interprétations de ligne de commande.

L'outil ```ItsMyConsole.Tools.GlobalCache``` met à disposition :
 - L'ajout de données en cache associé à une clé unique
 - La lecture des données en cache à partir d'une clé
 - La suppression des données du cache associés à une clé

## Getting Started
1. Créer un projet **"Application Console .Net"** avec le nom *"MyExampleConsole"*
2. Ajouter [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) au projet depuis le gestionnaire de package NuGet
3. Ajouter ```ItsMyConsole.Tools.GlobalCache``` au projet depuis le gestionnaire de package NuGet
4. Dans le projet, modifier la méthode **"Main"** dans le fichier **"Program.cs"** par le code suivant :
```cs
using ItsMyConsole;
using ItsMyConsole.Tools.GlobalCache;
using System;
using System.Threading.Tasks;

namespace MyExampleConsole
{
    class Program
    {
        static async Task Main() 
        {
            ConsoleCommandLineInterpreter ccli = new ConsoleCommandLineInterpreter();

            // Console configuration
            ccli.Configure(options => 
            {
                options.Prompt = ">> ";
                options.LineBreakBetweenCommands = true;
                options.HeaderText = "##################\n#  Global Cache  #\n##################\n";
                options.TrimCommand = true;
            });

            // Update global cache
            // Example : set MyNewValue
            ccli.AddCommand("^set (.+)$", RegexOptions.IgnoreCase, tools => 
            {
                string value = tools.CommandMatch.Groups[1].Value;
                if (tools.GlobalCache().TryGetValue<string>("<KEY>", out string oldValue))
                    Console.WriteLine($"Old value: {oldValue}");  
                tools.GlobalCache().Set("<KEY>", value);
                Console.WriteLine("Global cache updated");
            });
            
            // Get the value in the global cache 
            // Example : get
            ccli.AddCommand("^get$", RegexOptions.IgnoreCase, tools => 
            {
                string value = tools.GlobalCache().Get<string>("<KEY>");
                Console.WriteLine($"Value: {value}"); 
            });

            await ccli.RunAsync();
        }
    }
}
```

Voici le résultat attendu lors de l'utilisation de la Console :

![MyExampleProject](docs/MyExampleProject.png) 

Dans cet exemple de code on a configuré avec ```Configure```, le prompt d’attente des commandes ```options.Prompt```, la présence d'un saut de ligne entre les saisies ```options.LineBreakBetweenCommands``` et l’en-tête affichée au lancement ```options.HeaderText```. 

Puis avec le premier ```AddCommand```, on a ajouté un pattern d’interprétation des lignes de commande ```^set (.+)$``` *(commence par **"set"** et suivi d'un texte)*  qui est insensible à la casse ```RegexOptions.IgnoreCase```.

Et avec le deuxième ```AddCommand```, on a ajouté un pattern d’interprétation des lignes de commande ```^get$``` *(seulement **"get"**)*  qui est insensible à la casse ```RegexOptions.IgnoreCase```.

Lors de l'exécution de la Console, si on saisit une commande qui commence par **"set"** avec la valeur à mettre en cache, il lancera l'implémentation de l'action associée (le premier ```AddCommand```). Il commence à lire le texte saisi (qui est la nouvelle valeur à mettre en cache) en utilisant ```tools.CommandMatch``` depuis les outils disponibles *(résultat du Match de l'expression régulière)*. Ensuite il récupére et vérifie si on a déjà une valeur en cache ```tools.GlobalCache().TryGetValue```, si c'est le cas, il affiche le résultat (*"Old value"*) puis il met à jour le cache avec la nouvelle valeur ```tools.GlobalCache().Set```.

Si on saisit la commande **"get"**, il lancera l'implémentation de l'action associée (le deuxième ```AddCommand```). Il lit le cache ```tools.GlobalCache().Get``` et affiche la valeur obtenue.

Dans cet exemple, il lit et met à jour le cache sur une seule et unique clé **"&lt;KEY&gt;"** qui est accessible sur plusieurs actions configurées.

Maintenant que l'on a configuré la Console et l'implémentation des actions, l'utilisation de ```RunAsync``` lance la mise en attente d'une saisie de commande par l'utilisateur.

## Comment se servir de l'outil ?

Vous pouvez accéder à l'outil Global Cache lorsque vous ajoutez une interprétation de commande avec ```AddCommand```.

```cs
ConsoleCommandLineInterpreter ccli = new ConsoleCommandLineInterpreter();

// Add command
ccli.AddCommand("<PATERN>", tools => 
{
    string value = tools.GlobalCache().Get<string>("<KEY>");
});
```

Vous devez ajouter ```using ItsMyConsole.Tools.GlobalCache;``` pour avoir accès a l'outil Global Cache depuis ```tools``` de ```AddCommand```.

## Ajout ou modification d'une donnée dans le cache

Vous pouvez ajouter ou modifier des données dans le cache en utilisant ```Set```.

| Propriété | Description |
| :-------- | :---------- |
| key | La clé unique qui représente la donnée dans le cache |
| value | La valeur à ajouter ou mettre à jour dans le cache associée à la clé |

```cs
ccli.AddCommand("<PATERN>", tools => 
{
    tools.GlobalCache().Set("<KEY>", "My Value in cache");
});
```

## Récupération des données du cache

Vous pouvez récupérer une donnée dans le cache associé à une clé en utilisant ```Get```.

| Propriété | Description |
| :-------- | :---------- |
| key | La clé unique qui représente la donnée dans le cache |

```cs
ccli.AddCommand("<PATERN>", tools => 
{
    string value = tools.GlobalCache().Get<string>("<KEY>");
});
```

Vous avez aussi la possibilité de tester la présence de la clé dans le cache avant de récupérer sa valeur en utilisant ```TryGetValue```.

| Propriété | Description |
| :-------- | :---------- |
| key | La clé unique qui représente la donnée dans le cache |
| *out* value | La valeur en cache si la clé associée existe |

Si la clé existe dans le cache, il retourne ```true``` avec son contenu dans ```value```.

```cs
ccli.AddCommand("<PATERN>", tools => 
{
    if (tools.GlobalCache().TryGetValue<string>("<KEY>", out string value))
    {
        // Insert your code here
    }
});
```

## Suppression des données du cache

Vous pouvez supprimer une donnée du cache associé à une clé en utilisant ```Remove```.

| Propriété | Description |
| :-------- | :---------- |
| key | La clé unique qui représente la donnée dans le cache |

```cs
ccli.AddCommand("<PATERN>", tools => 
{
    tools.GlobalCache().Remove("<KEY>");
});
```
