using UnityEngine;
using ColliderEventSystem;

// Condition du CES pour le power-up PowerUpBalise.
// Apparaît dans « Add Condition » sous le nom « Power Up ».
//   Disponible   : vraie TANT QUE le power-up est débloqué et que le cooldown est terminé
//                  (à combiner avec Input pour la touche d'activation)
//   Devient Pret : vraie SEULEMENT AU MOMENT où le power-up redevient prêt
//                  (fin du cooldown, déblocage, ou début de scène si déjà débloqué)
public class PowerUpCondition : ConditionBase
{
    public enum Mode
    {
        Disponible,
        DevientPret,
    }

    [Tooltip("Le script PowerUpBalise à vérifier. Laisser vide si le Watcher est sur le joueur : il sera trouvé automatiquement.")]
    public PowerUpBalise powerUp;

    [Tooltip("Disponible : vrai tant que le power-up peut être utilisé (pour la touche). Devient Pret : vrai seulement au moment où le cooldown se termine (pour réafficher une icône).")]
    public Mode mode = Mode.Disponible;

    public override void OnAwake()
    {
        if (powerUp == null) powerUp = GetComponent<PowerUpBalise>();
    }

    public override bool Evaluate()
    {
        if (powerUp == null) return false;

        if (mode == Mode.Disponible)
        {
            return powerUp.EstDisponible();
        }

        // Devient Pret : tolérance d'une image, car selon l'ordre d'exécution des scripts
        // le Watcher peut vérifier juste avant ou juste après l'événement.
        return Time.frameCount - powerUp.frameDisponible <= 1;
    }

    public override bool TryGetValidationWarning(out string message)
    {
        if (powerUp == null && GetComponent<PowerUpBalise>() == null)
        {
            message = "Aucun PowerUpBalise trouvé sur cet objet : glisser le joueur dans le champ Power Up.";
            return true;
        }
        message = null;
        return false;
    }
}
