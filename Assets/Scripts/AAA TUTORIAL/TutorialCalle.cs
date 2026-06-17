using System.Collections;
using UnityEngine;

public class TutorialCalle : MonoBehaviour
{
    void Start()
    {
        if (TutorialManager.Instancia != null)
            StartCoroutine(SecuenciaTutorial());
    }

    IEnumerator SecuenciaTutorial()
    {
        TutorialManager.Instancia?.MostrarPaso(TutorialManager.PasoTutorial.Moverse);

        yield return new WaitForSeconds(5f);

        if (TutorialManager.Instancia != null)
            TutorialManager.Instancia.MostrarPaso(TutorialManager.PasoTutorial.IrAlBar);
    }
}