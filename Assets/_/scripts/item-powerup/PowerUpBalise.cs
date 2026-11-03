using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// À placer sur le joueur (ex. PlayerArmature).
// La touche est gérée par le CES (Watcher) :
//   Conditions : Input (touche E)  +  Power Up (mode Disponible)
//   Action     : Invoke Events → Activer()
public class PowerUpBalise : MonoBehaviour
{
    [Header("Déblocage")]
    public bool debloque = false;          // false au départ : débloqué par Debloquer()

    [Space]
    public GameObject[] faisceaux;

    [Header("Réglages")]
    [Tooltip("Secondes d'affichage du faisceau. 0 ou -1 : le faisceau reste affiché jusqu'à la prochaine activation.")]
    public float dureeFaisceau = 3f;       // 0 ou -1 = toujours affiché
    public float cooldown = 10f;           // secondes avant de pouvoir réutiliser

    [Header("Interface (HUD)")]
    public GameObject icone;               // conteneur de l'icône dans le Canvas
    public Image remplissage;              // Image en mode « Filled »
    public Color couleurPret = Color.white;
    public Color couleurRecharge = Color.gray;

    [Header("Événements")]
    [Tooltip("Appelé quand le power-up devient utilisable : au déblocage et à la fin de chaque cooldown.")]
    public UnityEvent quandDisponible;
    [Tooltip("Appelé au moment où le power-up est utilisé (début du cooldown).")]
    public UnityEvent quandUtilise;

    [Header("Son (optionnel)")]
    public AudioSource sonActivation;

    float prochainUsage = 0f;
    float finFaisceau = 0f;
    GameObject faisceauActif;
    bool enRecharge = false;

    // Image (frame) où le power-up est redevenu prêt, lue par la condition CES « Power Up » (mode Devient Pret)
    [HideInInspector] public int frameDisponible = -10;

    void Start()
    {
        // Tous les faisceaux sont cachés au départ
        foreach (GameObject f in faisceaux)
        {
            if (f != null) f.SetActive(false);
        }

        // L'icône n'apparaît qu'une fois le power-up débloqué
        if (icone != null) icone.SetActive(debloque);

        // Déjà débloqué au départ (ex. niveau 3) : le power-up est disponible tout de suite
        if (debloque) SignalerDisponible();
    }

    void Update()
    {
        // Cacher le faisceau quand sa durée est écoulée (sauf si durée 0 ou -1 = toujours affiché)
        if (faisceauActif != null && dureeFaisceau > 0f && Time.time >= finFaisceau)
        {
            faisceauActif.SetActive(false);
            faisceauActif = null;
        }

        // Fin du cooldown : prévenir que le power-up est de nouveau disponible
        if (enRecharge && Time.time >= prochainUsage)
        {
            enRecharge = false;
            SignalerDisponible();
        }

        MettreAJourInterface();
    }

    // ───── Méthodes publiques à brancher dans le CES ─────

    // CONDITION du Watcher : vrai si débloqué ET cooldown terminé
    public bool EstDisponible()
    {
        return debloque && Time.time >= prochainUsage;
    }

    // ACTION du Watcher : allume le faisceau de l'objet le plus proche
    public void Activer()
    {
        if (!EstDisponible()) return;      // sécurité si la condition est oubliée

        GameObject plusProche = TrouverPlusProche();
        if (plusProche == null) return;    // plus aucun objet à trouver

        if (faisceauActif != null) faisceauActif.SetActive(false);

        faisceauActif = plusProche;
        faisceauActif.SetActive(true);

        finFaisceau = Time.time + dureeFaisceau;
        prochainUsage = Time.time + cooldown;

        if (sonActivation != null) sonActivation.Play();

        enRecharge = true;
        quandUtilise.Invoke();
    }

    // À appeler par le CES quand le joueur obtient le power-up
    public void Debloquer()
    {
        if (debloque) return;              // déjà débloqué : rien à faire
        debloque = true;
        if (icone != null) icone.SetActive(true);
        SignalerDisponible();
    }

    // ───── Logique interne ─────

    void SignalerDisponible()
    {
        frameDisponible = Time.frameCount;
        quandDisponible.Invoke();
    }

    GameObject TrouverPlusProche()
    {
        GameObject meilleur = null;
        float meilleureDistance = Mathf.Infinity;

        foreach (GameObject f in faisceaux)
        {
            if (f == null) continue;   // objet détruit (déjà ramassé)

            // Objet ramassé mais désactivé plutôt que détruit
            Transform parent = f.transform.parent;
            if (parent != null && !parent.gameObject.activeInHierarchy) continue;

            float distance = Vector3.Distance(transform.position, f.transform.position);
            if (distance < meilleureDistance)
            {
                meilleureDistance = distance;
                meilleur = f;
            }
        }
        return meilleur;
    }

    void MettreAJourInterface()
    {
        if (remplissage == null) return;

        float restant = prochainUsage - Time.time;

        if (restant > 0f)
        {
            // En recharge : grise et se remplit graduellement
            remplissage.fillAmount = 1f - (restant / cooldown);
            remplissage.color = couleurRecharge;
        }
        else
        {
            // Prêt
            remplissage.fillAmount = 1f;
            remplissage.color = couleurPret;
        }
    }
}


// ───── Texte d'aide dans l'Inspector (outil d'éditeur, rien à modifier) ─────

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(PowerUpBalise))]
public class PowerUpBaliseEditor : UnityEditor.Editor
{
    const string aide = "Glisser ici le GameObject du faisceau placé à l'intérieur de chaque item (l'enfant, pas l'item lui-même).";

    public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
    {
        UnityEngine.UIElements.VisualElement racine = new UnityEngine.UIElements.VisualElement();
        UnityEditor.UIElements.InspectorElement.FillDefaultInspector(racine, serializedObject, this);

        // Ajoute le texte d'aide juste sous la liste des faisceaux
        UnityEngine.UIElements.VisualElement champ = UnityEngine.UIElements.UQueryExtensions.Q(racine, "PropertyField:faisceaux");
        if (champ != null)
        {
            UnityEngine.UIElements.Label label = new UnityEngine.UIElements.Label(aide);
            label.style.whiteSpace = UnityEngine.UIElements.WhiteSpace.Normal;
            label.style.fontSize = 10;
            label.style.color = Color.gray;
            label.style.marginLeft = 3;
            label.style.marginBottom = 6;
            champ.parent.Insert(champ.parent.IndexOf(champ) + 1, label);
        }
        return racine;
    }
}
#endif
