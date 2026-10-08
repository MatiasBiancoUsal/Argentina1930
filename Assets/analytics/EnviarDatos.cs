using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnviarDatos : MonoBehaviour
{
    Scene escenaActual;

    public int vidas;
    public int municion;
    public int objetive_complete;
    public int dialogue_start_day;
    public int shop_open;
    public int work_complete;
    public int game_quit;
    public int not_enough_money;
    public int npc_talk_count;
    public int enemy_chase_start;
    public int player_hit;



    public void EnviarDato()
    {

        CustomEvent iniciarnivel = new CustomEvent("level_start")
        {
            {"dia", dialogue_start_day},
            {"dinero_actual", shop_open},
            {"dinero_ganado", work_complete},
            {"ultimo_objetivo", game_quit},
            {"dinero_faltante", not_enough_money},
            {"cantidad", npc_talk_count},
            {"enemigo_id", enemy_chase_start},
            {"damage_recibido", player_hit},

        };
    }

    public void Start()
    {
        escenaActual = SceneManager.GetActiveScene();
    }


}
