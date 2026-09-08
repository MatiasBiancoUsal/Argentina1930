using UnityEngine;

/// <summary>
/// Dispara el primer objetivo de la secuencia (ej: "Andá al departamento
/// de Donato" apenas el jugador se despierta). Pegalo en el GameObject
/// del jugador, de la cama, o del controlador de la escena.
/// </summary>
public class ObjectiveStarter : MonoBehaviour
{
    public ObjectiveStep firstStep = ObjectiveStep.GoToDonato;

    [Tooltip("Si está activo, dispara el objetivo apenas arranca la escena")]
    public bool onStart = true;

    void Start()
    {
        if (onStart) TriggerStart();
    }

    /// <summary>
    /// Llamalo a mano si no querés que se dispare en Start (por ejemplo,
    /// desde un Animation Event al final de la animación de despertarse,
    /// o desde el final de una cutscene).
    /// </summary>
    public void TriggerStart()
    {
        ObjectiveManager.Instance.SetObjective(firstStep);
    }
}
