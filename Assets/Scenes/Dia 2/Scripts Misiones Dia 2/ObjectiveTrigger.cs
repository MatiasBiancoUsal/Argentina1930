using UnityEngine;

/// <summary>
/// Componente GENÉRICO Y REUTILIZABLE para lugares con Collider2D
/// (la puerta del depto de Donato, la entrada del bar, etc).
///
/// No escribís un script nuevo por cada lugar: pegás ESTE mismo script
/// en cada trigger y solo cambiás dos dropdowns en el Inspector
/// (Required Step / Next Step).
///
/// Requiere un Collider2D con "Is Trigger" tildado en el mismo objeto,
/// y que el jugador tenga el tag configurado en "Player Tag" (por defecto "Player").
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class ObjectiveTrigger : MonoBehaviour
{
    [Tooltip("El objetivo actual tiene que ser este para que el trigger dispare")]
    public ObjectiveStep requiredStep;

    [Tooltip("A qué objetivo avanza cuando el jugador entra")]
    public ObjectiveStep nextStep;

    [Tooltip("Tag que debe tener el objeto que entra al trigger")]
    public string playerTag = "Player";

    [Tooltip("Si está activo, el trigger se desactiva solo después de disparar una vez")]
    public bool disableAfterTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        bool advanced = ObjectiveManager.Instance.TryAdvance(requiredStep, nextStep);

        if (advanced && disableAfterTrigger)
            enabled = false;
    }
}
