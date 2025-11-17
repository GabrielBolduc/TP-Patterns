# Explications claires des patterns utilisés



*J’ai également ajouté des commentaires dans mon code afin d’expliquer l’utilisation des patterns.*



## 1- Singleton

* C'est un patron qui garantit qu'une classe n'a qu'une seule et unique instance dans toute l'application.

* **Utilité dans le programme :** Yvan voulait des propriétés systèmes comme un ratio de force de frappe et le temps d’attente. Au lieu de passer ces réglages en paramètres à chaque objet, nous avons créé un cerveau central.
    * Il gère les règles du jeu (`DamageModifier`, `BaseHealth`)
    * Il gère l’état global (`IsSimulationRunning`).
    * Il fournit un générateur `Random` partagé.

* **Utilisation dans le code :**
    * La classe `classes/GameSettings.cs` est le singleton.
    * `Program.cs` : On l’utilise pour démarrer/arrêter la boucle.
    * `Player.cs` : L’utilise pour connaître la vie de base (`Instance.BaseHealth`) et le modificateur de dégâts (`Instance.DamageModifier`).
    * `GameOverManager.cs` : L’utilise pour arrêter le jeu (`Instance.IsSimulationRunning = false`).
    * `WeaponFactory.cs` : L’utilise pour le `Rng` afin de créer des armes aléatoires.

---

## 2- Observer

* C’est un patron qui sert à gérer les notifications automatiques entre objets, en particularité lorsqu’un changement d’état doit être communiqué à plusieurs abonnés.

* **Utilité dans le programme :** Yvan voulait qu’une classe `GameOverManager` s’occupe de la fin.

* **La mauvaise façon (sans pattern) :** Mettre un `if (health <= 0)` dans la classe `Player` et lui faire connaître le `GameOverManager`.

* **La bonne façon (Observer) :** Le `Player` ne sait même pas que le `GameOverManager` existe. Il se contente d’appeler le `Notify()`. Le `GameOverManager` s'est abonné au `Player` et reçoit `Update()`. Il vérifie alors `if (player.Health <= 0)` et agit en conséquence.

* **Utilisation dans le code :**
    * Les interfaces : `interfaces/ISubject.cs` et `interfaces/IObserver.cs`.
    * `Player.cs` : Il implémente `ISubject`, a une `List<IObserver>`, et appelle `Notify()` dans la méthode `TakeDamage()`.
    * L'Observateur : `classes/GameOverManager.cs`. Il implémente `IObserver` et a la méthode `Update()` qui contient la logique de fin de partie.
    * `Program.cs` : les lignes `player1.Attach(gameOverManager);` et `player2.Attach(gameOverManager);` sont le moment où l'observateur "s'abonne" aux sujets.

---

## 3- Decorator

* C'est un patron qui permet d'ajouter de nouvelles fonctionnalités à un objet dynamiquement, en l'enveloppant dans un autre objet.

* **Utilité dans le programme :** Yvan voulait des "armes de toutes sortes" et des "dégâts variables".

* **La mauvaise façon (sans pattern) :** Créer des classes pour chaque combinaison : `EpeeEnFeu.cs`, `EpeeAiguisee.cs`, `EpeeEnFeuAiguisee.cs`... C'est ce qu'on appelle une "explosion de classes".

* **La bonne façon (Decorator) :**
    * Les objets de base : `BasicSword`, `HeavyAxe`.
    * Les enveloppes : `FireEnchantment`, `SharpnessEnchantment`.
    * Ensuite on les assemble. Une `new FireEnchantment(new BasicSword())` est une épée (5 dégâts) enveloppée de feu (+4 dégâts) pour un total de 9 dégâts.
    
* **Utilisation dans le code :**
    * L'Interface : `interfaces/IWeapon.cs`. C'est le contrat commun.
    * Décorateurs Concrets : `classes/FireEnchantment.cs` et `classes/SharpnessEnchantment.cs`.
    * L'Assembleur : `classes/WeaponFactory.cs`. Sa méthode `CreateRandomWeapon` assemble pour créer une arme unique à chaque tour.