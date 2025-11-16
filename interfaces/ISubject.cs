// Le "Sujet" est l'objet qui est observé (notre Joueur)
// Il s'agit du même principe que ton fichier ISubject.cs
public interface ISubject
{
    // Ajoute un observateur à la liste
    void Attach(IObserver observer);
    
    // Retire un observateur de la liste
    void Detach(IObserver observer);
    
    // Notifie tous les observateurs abonnés
    void Notify();
}