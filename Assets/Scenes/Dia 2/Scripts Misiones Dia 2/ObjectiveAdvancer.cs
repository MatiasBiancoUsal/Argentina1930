using UnityEngine;

/// <summary>
/// Componente reutilizable para eventos que NO son un trigger de colisión
/// ni un OnEnable, como "cuando termina el diálogo con Donato". Muchos
/// sistemas de diálogo tienen un UnityEvent tipo "On Dialogue End" en el
/// Inspector: enganchá el método Advance() de este componente ahí.
///
/// Pegalo en el mismo GameObject del NPC (Donato, Borges, etc.) o en
/// cualquier objeto que te resulte cómodo, y configurá Required/Next Step.
/// </summary>
public class ObjectiveAdvancer : MonoBehaviour
{
    public ObjectiveStep requiredStep;
    public ObjectiveStep nextStep;

    /// <summary>
    /// Enganchá este método (sin parámetros) en cualquier UnityEvent
    /// del Inspector, por ejemplo el "On Dialogue End" del diálogo de Donato.
    /// </summary>
    public void Advance()
    {
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.TryAdvance(requiredStep, nextStep);
    }
}
