# Documentación y Historial de Cambios - Proyecto Skeletons

> **Nota de Autoría:** Este archivo de documentación (.md) fue redactado por mí, **Antigravity** (tu asistente de IA de Google DeepMind). Sin embargo, es importante aclarar que **toda la base del proyecto, las interfaces y los cambios realizados antes de `GameplayUI` y `PauseUI` fueron creados y desarrollados enteramente por ti (Alejandro)**. Mi intervención comenzó exclusivamente a partir de `GameplayUI` y `PauseUI` para ayudarte a corregir la lógica, refactorizar el código y organizar la arquitectura, ya que el tiempo te comía y necesitabas ayuda rápida para acomodarlo.

---

## 🎮 Visión General del Proyecto (Funcionamiento del Juego)
"Skeletons" es un juego estratégico estilo *Tug-of-War* (tira y afloja, similar a Battle Cats o Clash Royale), combinado con mecánicas de *Clicker*. 

El jugador controla al bando de los **Esqueletos (Player)**, cuyo objetivo principal es destruir la Torre Enemiga (Torre 2) antes de que los **Humanos (Enemy)** logren destruir la Torre Aliada (Torre 1). 

### Mecánicas Principales:
1.  **Generación de Terreno:** El campo de batalla (el camino entre las torres) se construye dinámicamente utilizando `Advanced_Dungeon_Generator`.
2.  **Sistema de Tienda e Inventario:** El jugador puede navegar por la Tienda (`ShopUI`), comprar diferentes tipos de esqueletos o armas, y estos se guardarán de forma permanente en un archivo JSON gracias al `InventoryManager`.
3.  **Invocación tipo Clicker:** Durante la partida (`GameplayUI`), el jugador revisa los esqueletos en su inventario mediante un panel interactivo horizontal. Al elegir uno, puede invocarlo usando un botón de Spawn.
4.  **Progresión del Cooldown:** El botón de invocación tiene un tiempo de recarga (Cooldown). A medida que los esqueletos del jugador asesinen humanos enemigos, la velocidad de respuesta del botón de invocación mejorará permanentemente durante la partida, permitiendo invocar tropas cada vez más rápido.
5.  **Combate Autónomo:** Una vez invocadas, las unidades usan Inteligencia Artificial básica para caminar automáticamente hacia la base enemiga y detenerse a atacar si encuentran a un enemigo en el rango, usando las animaciones de `sanctum_pixel` y efectos de impacto de `JMO Assets`.

---

## 📝 Historial de Cambios Implementados

### 🎨 Interfaz de Usuario (UI) y Animaciones
*   **Animaciones Únicas**: Se implementaron transiciones únicas usando `DOTween` para la apertura (`Show`) y cierre (`Hide`) en `MenuUI`, `Shop_UI`, `SettingsUI`, `ConfirmUICharacter`, `ConfirmUI_Item`, `GameplayUI` y `PauseUI`.
*   **Confirmaciones de Tienda**: Se conectaron las UI de `ConfirmUICharacter.cs` y `ConfirmUI_Item.cs` para mostrar dinámicamente la imagen doble del ítem seleccionado y el cuadro de estadísticas.
*   **Corrección de Prefab (Item.prefab)**: Se arreglaron las anclas (Anchors) y el tamaño (SizeDelta = 200x200) del `Item.prefab`. Se apagó el "Word Wrap" de TextMeshPro para evitar el bug visual del texto vertical y se hizo reaparecer el ícono dentro de los LayoutGroups.
*   **Limpieza de Código**: Se eliminaron absolutamente todos los `Debug.Log` de prueba y los comentarios (`//`) de los scripts de UI creados para mantener la regla estricta de "Cero Código Espagueti".
*   **PauseUI**: Se conectó la lógica de pausa (`Time.timeScale`) y se integró un botón de "Ajustes" (Settings) adicional que se mostraba en el Canvas.

### 🏗️ Arquitectura Base de Gameplay (Torres y Unidades)
Se creó un sistema orientado a objetos limpio y escalable usando herencia:
*   `Entity.cs`: Clase abstracta padre. Controla la salud, la recepción de daño y eventos de muerte sin usar el `Update`.
*   `BaseTower.cs`: Hereda de `Entity`. Representa las torres del jugador y del enemigo, enviando alertas al GameManager cuando son destruidas.
*   `Unit.cs`: Hereda de `Entity`. Contiene la Inteligencia Artificial base (Buscar, Moverse y Atacar).
*   `Team.cs`: Enum simple para distinguir aliados (`Player`) de enemigos (`Enemy`).
*   `GameManager.cs`: Singleton encargado del flujo de la partida. Avisa cuando un jugador gana o pierde basado en qué torre cae.

### 🎒 Inventario y Mecánica Clicker
*   **InventoryManager.cs**: Refactorizado por completo. Ahora guarda y carga en formato `.json` los Skeletons/Items obtenidos de la tienda de forma segura.
*   **Integración Tienda-Inventario**: Al presionar "Comprar" en las ventanas de confirmación, los ítems se registran y guardan directamente en el `InventoryManager`.
*   **GameplayUI.cs**: 
    *   Conectado para actualizar automáticamente las barras de vida de las torres 1 y 2.
    *   Lee la lista del `InventoryManager` y la dibuja dinámicamente en un *Horizontal Scroll View*.
    *   Lógica de Clicker implementada: Al seleccionar un esqueleto, el botón "Spawn" se activa con un cooldown base de 3 segundos, el cual se reduce progresivamente cada vez que un humano es asesinado (`RegisterEnemyKilled`).
