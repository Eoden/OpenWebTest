### Comment tester rapidement le swagger:
La BDD est en cache donc vide par default, toutes les méthodes getAll retourneront donc des valeurs vides.
1) S'authentifier avec le cadenas sur la droite
2) Créer un Contact via la méthode POST
3) Faire du GET / PUT / DELETE sur le contact créé
4) Répéter les memes étapes pour la partie "Skill"
5) Une fois des Contacts et des Skills en BDD, créer des LinkContactSkill en fonction des ID Contact et Skill précédemment créé.
6) Effectuer un GetAll sur les contact pour voir


### Les choses mises en place :

Afin de répondre au problématiques de Contact possédant plusieurs Skills, j'ai créé un objet LinkContactSkill, possédant les ID du Contact et du Skill. Ainsi il suffit de créer les Skill et les Contact, puis de lier ensemble ceux que l'on souhaite. Un contact peut donc avoir plusieurs Skill, et un Skill appartenir a plusieurs Contact.

Une architecture en 3 couches :
- REST : partie exposé a l'exterieur, elle porte les objets de type ContactV1 et ContactRequestV1, des règles simples peuvent y être appliqué. (Ex : des attributs mandatory, ou des Regex sur des attributs …)
- SERVICE : partie applicative, la ou les différentes règles metier complexes sont misent en place.
- DATA : partie dédié a l’échange avec la base de donnée. (Aucune complexité dans cette couche)

Chaque objet possède une structure différente en fonction de son besoin et en fonction de la couche architecturale qui l’utilise. 
Ex, un contact a :
-  ContactV1 qui est le type de retour de l’api en V1, soit l’objet complet exposé aux applications qui viendront potentiellement consommer l’API.
- CreateContactRequestV1 qui est un objet d’entrée de l’API, il est simplifié afin de ne pas donner aux applis externes qui viennent consommer l’API la main sur l’ID de l’objet et ses Skills.
- Contact (on l’appel souvent ContactEntity) qui est l’objet stocké en BDD, il n’est pas forcement identique a l’objet exposé dans la couche REST, pour illustrer l’exemple, j’ai choisi de ne pas stocker le « fullname » en BDD, mais de concaténer le champs fullname lors du mapping qui retourne vers la couche REST. 

Les Objets ContactV1 et CreateContactRequestV1, sont mis dans un projet a part, et devraient être idéalement mis dans un package Nuget versioné, afin que n’importe quelle Application de l’entreprise puisse facilement récupérer sa structure. L’objet Contact lui n’est pas exposé car des règles métiers peuvent l’affecter lors du passage des couches REST aux couches DATA.

Les Objets comme CreateContactRequest servent à propager plus facilement de la couche REST a la couche SERVICE

Utilisation d’un automapper pour éviter de passer les attributs 1 a 1.

Methodes du swagger documentés (la génération se fait automatiquement en fonction des commentaires mis dans les méthodes).

Une base de donnée UseInMemoryDatabase, donc vidé a chaque démarrage de l’application.

Ajout de Règles de validation sur les Classes portés par les contrôleurs (RegexMail, Mandatory values, range …)

Un système d’authentification Basic, il est possible de s’authentifier avec n’importe quel Login et Mdp  pour simplifier les tests, cependant il est obligatoire d’être authentifié pour appeler les différentes méthodes.
/!\ Ne pas utiliser l’authentification proposé en Alert par Chrome /!\  elle a tendance a sauvegarder les logs en session, et il est parfois impossible de se Logout. (Bien passer par le petit cadenas) 

Gestion de retours d’erreurs multiple (400 / 401 , avec message explicite en retour, voir PUT, ou message générique pour le reste)

Mise en Place de Tests unitaires de la partie REST, utilisation de mock avec la librairie « fakeItEasy », qui simplifie grandement l’utilisation des mocks. La seule partie réellement interessante des TU, est pour la partie « PUT » du contact, car il y a une vraie règle de gestion sur l’ID, le reste est mock de bout en bout, met permet de voir le principe de base. Je me suis dit qu’il ne servait a rien de rajouter des TU mocké de partout, un exemple sur 2 point d’entrés suffit. Pour simplifier j'ai mis la partie Mock, et Fixture directement dans le TestController. (bien entendu quand on a bcp de test il vaut mieux les séparer pour une meilleur lisibilité)

### Les choses possible a rajouter :

Associer à tous les objets en BDD des dates de dernières mise a jour (timestamp) et lors de la sauvegarde, verifier que l’objet qu’on manipule est bien le meme que celui présent en base (si plusieurs utilisateurs modifient le meme objet par ex), et si ce n’est pas le cas remonter une exception.

La nature de la base de donnée (UseInMemoryDatabase) ne permet pas de créer de dependences fortes (Foreign key), donc lors de la suppression d’un Contact par exemple, le delete on cascade ne fonctionne pas. Il peut donc y avoir des LinkContactSkill zombies dans la BDD. Mais au vu de la durée de vie de la BDD je pense qu’il n’est pas pertinent d’aller supprimer ces liens via une méthode. Je vous rassure, je sais parfaitement gérer les bases relationnelles.

Utilisation du « PATCH » pout modifier seulement une partie de l’objet via l’API

Rajouter plus de gestion types d’erreurs, et Utiliser un objet erreur spécifique afin de documenter plus précisément l’erreur en fonction d’une règle metier.

Ajouter des tests unitaires sur toute les couches, et couvrir 100% des méthodes metier.

Ajouter des tests d’intégration et des tests systèmes.

Mettre en place un système de check sur les Claims pour authoriser ou non la modification sur tel ou tel objet en base.
