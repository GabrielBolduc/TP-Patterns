// L'"Observateur" est l'objet qui surveille (notre GameOverManager)
// Il s'agit du même principe que ton fichier IObserver.cs
public interface IObserver
{
    // La méthode que le Sujet va appeler pour envoyer une mise à jour.
    // On passe le sujet lui-même pour que l'observateur puisse
    // demander les informations dont il a besoin (ex: Qui est mort ?).
    void Update(ISubject subject);
}