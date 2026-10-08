using System.Collections.Generic;
using UnityEngine;

// Librerías de autenticación y servicios (como en el PDF de la clase)

using Unity.Services.Core;
using UnityEngine.UnityConsent;   // Para el consentimiento del usuario
using Unity.Services.Analytics;   // Para usar analytics

/// <summary>
/// Script único que guarda y envía TODOS los analytics del juego.
/// Se usa desde cualquier otro script con:  AnalyticsManager.Instance.NombreDelMetodo(...)
///
/// IMPORTANTE: cada evento y parámetro tiene que estar creado en el Event Manager
/// (dashboard de Unity Analytics) con el MISMO nombre y tipo de dato, o Unity lo ignora.
/// No llamar a estos métodos desde un Update() todos los frames.
/// </summary>
public class AnalyticsManager : MonoBehaviour
{
    // ─────────────── SINGLETON ───────────────
    public static AnalyticsManager Instance { get; private set; }

    [Header("Configuración")]
    [Tooltip("true = llama a Flush() después de cada evento para que suba casi al instante (útil para testear)")]
    [SerializeField] private bool enviarAlInstante = true;

    [Tooltip("true = además de enviar, imprime cada evento en la consola")]
    [SerializeField] private bool mostrarEnConsola = true;

    // ─────────────── ESTADO INTERNO ───────────────
    private bool inicializado = false;

    // Eventos que se llamaron antes de que terminen de cargar los servicios
    private readonly List<CustomEvent> eventosPendientes = new List<CustomEvent>();

    private float tiempoInicioNivel;
    private float tiempoInicioObjetivo;
    private int ultimoObjetivo = 0;

    // Cuántas veces habló el jugador con cada NPC
    private readonly Dictionary<string, int> charlasPorNpc = new Dictionary<string, int>();

    // Momento en que empezó la persecución de cada enemigo (por id)
    private readonly Dictionary<int, float> inicioPersecucion = new Dictionary<int, float>();

    private bool juegoCerrado = false;

    // ─────────────── INICIALIZACIÓN ───────────────
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // sobrevive a los cambios de escena
    }

    async void Start() // los métodos async son como corrutinas
    {
        try
        {
            // Esperamos a que se inicialicen los servicios de Unity
            // (si otro script ya los inicializó, no lo hacemos de nuevo)
            if (UnityServices.State != ServicesInitializationState.Initialized)
                await UnityServices.InitializeAsync();

            // El consentimiento NO se setea acá: lo maneja el cartel del menú principal
            // con EndUserConsent.SetConsentState(...). Si el jugador no acepta,
            // Unity no manda los eventos aunque se llamen estos métodos.

            inicializado = true;
            Debug.Log("[Analytics] Servicios inicializados.");

            // Mandamos los eventos que se llamaron mientras cargaba
            foreach (CustomEvent e in eventosPendientes)
                AnalyticsService.Instance.RecordEvent(e);
            eventosPendientes.Clear();
            if (enviarAlInstante) AnalyticsService.Instance.Flush();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("[Analytics] No se pudieron inicializar los servicios: " + e.Message);
        }
    }

    // ─────────────── MÉTODO GENERAL DE ENVÍO ───────────────
    private void Enviar(CustomEvent evento, string log)
    {
        if (mostrarEnConsola) print(log);

        if (!inicializado)
        {
            eventosPendientes.Add(evento); // se manda cuando termine de inicializar
            return;
        }

        AnalyticsService.Instance.RecordEvent(evento);
        if (enviarAlInstante) AnalyticsService.Instance.Flush();
    }

    // =====================================================================
    //                              NIVEL
    // =====================================================================

    /// level_start → En el Start() de la escena, al cargar el nivel.
    public void LevelStart(int nivel)
    {
        tiempoInicioNivel = Time.time;
        tiempoInicioObjetivo = Time.time;

        Enviar(new CustomEvent("level_start")
        {
            { "nivel", nivel }
        }, $"Evento level_start. Parámetro nivel: {nivel}");
    }

    /// level_complete → Al terminar la charla con Borges.
    public void LevelComplete()
    {
        float tiempoTotal = Time.time - tiempoInicioNivel;

        Enviar(new CustomEvent("level_complete")
        {
            { "tiempo_total", tiempoTotal }
        }, $"Evento level_complete. Parámetro tiempo_total: {tiempoTotal}");
    }

    /// wake_up_complete → Al terminar la secuencia completa al despertarse.
    public void WakeUpComplete(float tiempoSegundos)
    {
        Enviar(new CustomEvent("wake_up_complete")
        {
            { "tiempo_segundos", tiempoSegundos }
        }, $"Evento wake_up_complete. Parámetro tiempo_segundos: {tiempoSegundos}");
    }

    // =====================================================================
    //                            OBJETIVOS
    // =====================================================================

    /// objective_complete + objective_time → Cada vez que cambia el objetivo del banner superior.
    /// Manda los dos eventos juntos, como pide la tabla.
    public void ObjectiveComplete(int objetivoId)
    {
        ultimoObjetivo = objetivoId;

        Enviar(new CustomEvent("objective_complete")
        {
            { "objetivo_id", objetivoId }
        }, $"Evento objective_complete. Parámetro objetivo_id: {objetivoId}");

        float tiempoSegundos = Time.time - tiempoInicioObjetivo;
        tiempoInicioObjetivo = Time.time; // reinicia para el próximo objetivo

        Enviar(new CustomEvent("objective_time")
        {
            { "tiempo_segundos", tiempoSegundos }
        }, $"Evento objective_time. Parámetro tiempo_segundos: {tiempoSegundos}");
    }

    // =====================================================================
    //                         ESCENAS / INTERACCIÓN
    // =====================================================================

    /// scene_enter → Al entrar a un lugar: depto. de Donato, tienda, trabajo, bar.
    public void SceneEnter(string escenaNombre)
    {
        Enviar(new CustomEvent("scene_enter")
        {
            { "escena_nombre", escenaNombre }
        }, $"Evento scene_enter. Parámetro escena_nombre: {escenaNombre}");
    }

    /// interact → Al apretar la tecla de interacción sobre un objeto.
    public void Interact(string objetoNombre)
    {
        Enviar(new CustomEvent("interact")
        {
            { "objeto_nombre", objetoNombre }
        }, $"Evento interact. Parámetro objeto_nombre: {objetoNombre}");
    }

    /// idle_time → Cuando pasan más de X segundos sin moverse.
    /// Llamarlo UNA vez por cada período quieto (no todos los frames).
    public void IdleTime(float tiempoQuieto)
    {
        Enviar(new CustomEvent("idle_time")
        {
            { "tiempo_quieto", tiempoQuieto }
        }, $"Evento idle_time. Parámetro tiempo_quieto: {tiempoQuieto}");
    }

    // =====================================================================
    //                             DIÁLOGOS
    // =====================================================================

    /// dialogue_start + dialogue_start_day → Al interactuar con Donato o Borges.
    public void DialogueStart(string npcNombre, int dia)
    {
        Enviar(new CustomEvent("dialogue_start")
        {
            { "npc_nombre", npcNombre }
        }, $"Evento dialogue_start. Parámetro npc_nombre: {npcNombre}");

        Enviar(new CustomEvent("dialogue_start_day")
        {
            { "dia", dia }
        }, $"Evento dialogue_start_day. Parámetro dia: {dia}");
    }

    /// dialogue_skipped + npc_talk_count → Al cerrar el diálogo.
    /// saltado = true si el jugador lo salteó.
    public void DialogueEnd(string npcNombre, bool saltado)
    {
        Enviar(new CustomEvent("dialogue_skipped")
        {
            { "saltado", saltado }
        }, $"Evento dialogue_skipped. Parámetro saltado: {saltado}");

        // Suma una charla a ese NPC
        charlasPorNpc.TryGetValue(npcNombre, out int cantidad);
        cantidad++;
        charlasPorNpc[npcNombre] = cantidad;

        Enviar(new CustomEvent("npc_talk_count")
        {
            { "cantidad", cantidad }
        }, $"Evento npc_talk_count. Parámetro cantidad: {cantidad} (NPC: {npcNombre})");
    }

    // =====================================================================
    //                         TIENDA / DINERO
    // =====================================================================

    /// shop_open → Al activar el Canvas Tienda.
    public void ShopOpen(int dineroActual)
    {
        Enviar(new CustomEvent("shop_open")
        {
            { "dinero_actual", dineroActual }
        }, $"Evento shop_open. Parámetro dinero_actual: {dineroActual}");
    }

    /// shop_close → Al cerrar el Canvas Tienda. compro = true si compró algo.
    public void ShopClose(bool compro)
    {
        Enviar(new CustomEvent("shop_close")
        {
            { "compro", compro }
        }, $"Evento shop_close. Parámetro compro: {compro}");
    }

    /// item_purchase → Al confirmar la compra (por ej. la cámara).
    public void ItemPurchase(string itemNombre)
    {
        Enviar(new CustomEvent("item_purchase")
        {
            { "item_nombre", itemNombre }
        }, $"Evento item_purchase. Parámetro item_nombre: {itemNombre}");
    }

    /// not_enough_money → Cuando falla una compra por falta de dinero.
    public void NotEnoughMoney(int dineroFaltante)
    {
        Enviar(new CustomEvent("not_enough_money")
        {
            { "dinero_faltante", dineroFaltante }
        }, $"Evento not_enough_money. Parámetro dinero_faltante: {dineroFaltante}");
    }

    /// work_complete → Al terminar la tarea del trabajo.
    public void WorkComplete(int dineroGanado)
    {
        Enviar(new CustomEvent("work_complete")
        {
            { "dinero_ganado", dineroGanado }
        }, $"Evento work_complete. Parámetro dinero_ganado: {dineroGanado}");
    }

    /// camera_obtained → Al agregarse la cámara al inventario.
    public void CameraObtained()
    {
        float tiempoTotal = Time.time - tiempoInicioNivel;

        Enviar(new CustomEvent("camera_obtained")
        {
            { "tiempo_total", tiempoTotal }
        }, $"Evento camera_obtained. Parámetro tiempo_total: {tiempoTotal}");
    }

    // =====================================================================
    //                             ENEMIGOS
    // =====================================================================

    /// enemy_detect_player → Cuando el jugador entra en el rango de visión del enemigo.
    public void EnemyDetectPlayer(string enemigoTipo)
    {
        Enviar(new CustomEvent("enemy_detect_player")
        {
            { "enemigo_tipo", enemigoTipo }
        }, $"Evento enemy_detect_player. Parámetro enemigo_tipo: {enemigoTipo}");
    }

    /// enemy_chase_start → Cuando el enemigo pasa a estado de persecución.
    public void EnemyChaseStart(int enemigoId)
    {
        inicioPersecucion[enemigoId] = Time.time;

        Enviar(new CustomEvent("enemy_chase_start")
        {
            { "enemigo_id", enemigoId }
        }, $"Evento enemy_chase_start. Parámetro enemigo_id: {enemigoId}");
    }

    /// enemy_chase_escape + enemy_chase_time → Al terminar la persecución
    /// (escapo = true si el enemigo lo perdió de vista, false si lo atrapó).
    public void EnemyChaseEnd(int enemigoId, bool escapo)
    {
        Enviar(new CustomEvent("enemy_chase_escape")
        {
            { "escapo", escapo }
        }, $"Evento enemy_chase_escape. Parámetro escapo: {escapo}");

        float duracion = 0f;
        if (inicioPersecucion.TryGetValue(enemigoId, out float inicio))
        {
            duracion = Time.time - inicio;
            inicioPersecucion.Remove(enemigoId);
        }

        Enviar(new CustomEvent("enemy_chase_time")
        {
            { "duracion", duracion }
        }, $"Evento enemy_chase_time. Parámetro duracion: {duracion}");
    }

    /// player_hit → En OnTriggerEnter2D / OnCollisionEnter2D con el enemigo.
    public void PlayerHit(int damageRecibido)
    {
        Enviar(new CustomEvent("player_hit")
        {
            { "damage_recibido", damageRecibido }
        }, $"Evento player_hit. Parámetro damage_recibido: {damageRecibido}");
    }

    // =====================================================================
    //                          PAUSA / SALIR
    // =====================================================================

    /// game_pause → Al abrir el menú de pausa.
    public void GamePause()
    {
        float tiempoJugado = Time.time;

        Enviar(new CustomEvent("game_pause")
        {
            { "tiempo_jugado", tiempoJugado }
        }, $"Evento game_pause. Parámetro tiempo_jugado: {tiempoJugado}");
    }

    /// game_quit → Llamarlo al salir al menú. Al cerrar el juego se llama solo.
    public void GameQuit()
    {
        if (juegoCerrado) return; // evita mandarlo dos veces
        juegoCerrado = true;

        Enviar(new CustomEvent("game_quit")
        {
            { "ultimo_objetivo", ultimoObjetivo }
        }, $"Evento game_quit. Parámetro ultimo_objetivo: {ultimoObjetivo}");

        // Forzamos el envío para que no se pierda al cerrar
        if (inicializado) AnalyticsService.Instance.Flush();
    }

    void OnApplicationQuit()
    {
        GameQuit();
    }
}