using UnityEngine;

/// <summary>
/// Componente reutilizable para UI que se activa con SetActive, como el
/// Canvas de la tienda. Pegalo en el MISMO GameObject del Canvas Tienda:
/// cada vez que ese Canvas se activa, intenta avanzar el objetivo.
///
/// Configurás Required Step / Next Step en el Inspector, igual que en
/// ObjectiveTrigger. Sirve para cualquier otra pantalla que se abra
/// más adelante (inventario, mapa, etc.) sin escribir código nuevo.
/// </summary>
public class ObjectiveOnEnable : MonoBehaviour
{
    public ObjectiveStep requiredStep;
    public ObjectiveStep nextStep;

    void OnEnable()
    {
        if (ObjectiveManager.Instance != null)
            ObjectiveManager.Instance.TryAdvance(requiredStep, nextStep);
    }
}
