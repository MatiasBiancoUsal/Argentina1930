using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Maneja el objetivo actual del jugador y lo muestra en un texto fijo
/// en la parte superior de la pantalla. Poné este script en un solo
/// GameObject de la escena (por ejemplo un objeto vacío llamado
/// "ObjectiveManager", hijo del Canvas del HUD).
///
/// Es un singleton: solo debe existir una instancia.
/// </summary>
public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    [Header("UI")]
    [Tooltip("Texto fijo arriba de la pantalla donde se muestra el objetivo actual")]
    public TMP_Text objectiveText;

    [Header("Configuración")]
    [Tooltip("Activalo si el HUD tiene que sobrevivir a cambios de escena")]
    public bool persistAcrossScenes = false;

    public ObjectiveStep CurrentStep { get; private set; } = ObjectiveStep.None;

    // Acá está el texto de cada paso. Si cambiás las frases, es el único lugar donde tocar.
    private readonly Dictionary<ObjectiveStep, string> messages = new()
    {
        { ObjectiveStep.GoToDonato,   "Andá al departamento de Donato" },
        { ObjectiveStep.TalkToDonato, "Hablá con Donato" },
        { ObjectiveStep.GoToStore,    "Andá a la tienda para averiguar sobre la cámara" },
        { ObjectiveStep.GoToWork,     "Andá a trabajar para comprar la cámara" },
        { ObjectiveStep.TalkToBorges, "Hablá con Borges" },
    };

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        if (persistAcrossScenes) DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Cambia el objetivo actual sin validar nada. Usalo para arrancar
    /// la secuencia (ej: al despertar) o para forzar un paso a mano.
    /// </summary>
    public void SetObjective(ObjectiveStep step)
    {
        CurrentStep = step;

        if (objectiveText == null)
        {
            Debug.LogWarning("ObjectiveManager: falta asignar 'objectiveText' en el Inspector.");
            return;
        }

        if (messages.TryGetValue(step, out var msg))
        {
            objectiveText.text = msg;
            objectiveText.gameObject.SetActive(true);
        }
        else
        {
            objectiveText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Avanza el objetivo SOLO si el paso actual es el esperado ("from").
    /// Esto es lo que evita que un trigger dispare el paso siguiente
    /// fuera de orden (ej: si el jugador reingresa a un lugar ya visitado).
    /// Devuelve true si avanzó, false si no hizo nada.
    /// </summary>
    public bool TryAdvance(ObjectiveStep from, ObjectiveStep to)
    {
        if (CurrentStep != from) return false;
        SetObjective(to);
        return true;
    }
}
